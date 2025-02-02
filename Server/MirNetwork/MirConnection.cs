using System.Collections.Concurrent;
using System.Net.Sockets;
using Server.MirDatabase;
using Server.MirEnvir;
using Server.MirObjects;
using C = ClientPackets;
using S = ServerPackets;
using System.Text.RegularExpressions;
using Server.Utils;
using ClientPackets;

namespace Server.MirNetwork
{
    public enum GameStage { None, Login, Select, Game, Observer, Disconnected }

    public class MirConnection
    {
        protected static Envir Envir
        {
            get { return Envir.Main; }
        }

        protected static MessageQueue MessageQueue
        {
            get { return MessageQueue.Instance; }
        }

        public readonly int SessionID;
        public readonly string IPAddress;

        public GameStage Stage;

        private TcpClient _client;
        private ConcurrentQueue<Packet> _receiveList;
        private ConcurrentQueue<Packet> _sendList; 
        private Queue<Packet> _retryList;

        private bool _disconnecting;
        public bool Connected;
        public bool Disconnecting
        {
            get { return _disconnecting; }
            set
            {
                if (_disconnecting == value) return;
                _disconnecting = value;
                TimeOutTime = Envir.Time + 500;
            }
        }
        public readonly long TimeConnected;
        public long TimeDisconnected, TimeOutTime;

        byte[] _rawData = new byte[0];
        byte[] _rawBytes = new byte[8 * 1024];

        public AccountInfo Account;
        public PlayerObject Player;

        public List<MirConnection> Observers = new List<MirConnection>();
        public MirConnection Observing;

        public List<ItemInfo> SentItemInfo = new List<ItemInfo>();
        public List<QuestInfo> SentQuestInfo = new List<QuestInfo>();
        public List<RecipeInfo> SentRecipeInfo = new List<RecipeInfo>();
        public List<UserItem> SentChatItem = new List<UserItem>(); //TODO - Add Expiry time
        public List<MapInfo> SentMapInfo = new List<MapInfo>();
        public List<ulong> SentHeroInfo = new List<ulong>();
        public bool WorldMapSetupSent;
        public bool StorageSent;
        public bool HeroStorageSent;
        public Dictionary<long, DateTime> SentRankings = new Dictionary<long, DateTime>();

        private DateTime _dataCounterReset;
        private int _dataCounter;
        private FixedSizedQueue<Packet> _lastPackets;

        public MirConnection(int sessionID, TcpClient client)
        {
            SessionID = sessionID;
            IPAddress = client.Client.RemoteEndPoint.ToString().Split(':')[0];

            Envir.UpdateIPBlock(IPAddress, TimeSpan.FromSeconds(Settings.IPBlockSeconds));

            MessageQueue.Enqueue(IPAddress + ", Connected.");

            _client = client;
            _client.NoDelay = true;

            TimeConnected = Envir.Time;
            TimeOutTime = TimeConnected + Settings.TimeOut;

            _lastPackets = new FixedSizedQueue<Packet>(10);

            _receiveList = new ConcurrentQueue<Packet>();
            _sendList = new ConcurrentQueue<Packet>();
            _sendList.Enqueue(new S.Connected());
            _retryList = new Queue<Packet>();

            Connected = true;
            BeginReceive();
        }

        public void AddObserver(MirConnection c)
        {
            Observers.Add(c);

            if (c.Observing != null)
                c.Observing.Observers.Remove(c);
            c.Observing = this;

            c.Stage = GameStage.Observer;
        }

        private void BeginReceive()
        {
            if (!Connected) return;

            try
            {
                _client.Client.BeginReceive(_rawBytes, 0, _rawBytes.Length, SocketFlags.None, ReceiveData, _rawBytes);
            }
            catch
            {
                Disconnecting = true;
            }
        }

        private void ReceiveData(IAsyncResult result)
        {
            if (!Connected) return;

            int dataRead;

            try
            {
                dataRead = _client.Client.EndReceive(result);
            }
            catch
            {
                Disconnecting = true;
                return;
            }

            if (dataRead == 0)
            {
                Disconnecting = true;
                return;
            }

            if (_dataCounterReset < Envir.Now)
            {
                _dataCounterReset = Envir.Now.AddSeconds(5);
                _dataCounter = 0;
            }

            _dataCounter++;

            try
            {
                byte[] rawBytes = result.AsyncState as byte[];

                byte[] temp = _rawData;
                _rawData = new byte[dataRead + temp.Length];
                Buffer.BlockCopy(temp, 0, _rawData, 0, temp.Length);
                Buffer.BlockCopy(rawBytes, 0, _rawData, temp.Length, dataRead);

                Packet p;

                while ((p = Packet.ReceivePacket(_rawData, out _rawData)) != null)
                    _receiveList.Enqueue(p);
            }
            catch
            {
                Envir.UpdateIPBlock(IPAddress, TimeSpan.FromHours(24));

                MessageQueue.Enqueue($"{IPAddress} Disconnected, Invalid packet.");

                Disconnecting = true;
                return;
            }

            if (_dataCounter > Settings.MaxPacket)
            {
                Envir.UpdateIPBlock(IPAddress, TimeSpan.FromHours(24));

                List<string> packetList = new List<string>();

                while (_lastPackets.Count > 0)
                {
                    _lastPackets.TryDequeue(out Packet pkt);

                    Enum.TryParse<ClientPacketIds>((pkt?.Index ?? 0).ToString(), out ClientPacketIds cPacket);

                    packetList.Add(cPacket.ToString());
                }

                MessageQueue.Enqueue($"{IPAddress} Disconnected, Large amount of Packets. LastPackets: {String.Join(",", packetList.Distinct())}.");

                Disconnecting = true;
                return;
            }

            BeginReceive();
        }
        private void BeginSend(List<byte> data)
        {
            if (!Connected || data.Count == 0) return;

            //Interlocked.Add(ref Network.Sent, data.Count);

            try
            {
                _client.Client.BeginSend(data.ToArray(), 0, data.Count, SocketFlags.None, SendData, Disconnecting);
            }
            catch
            {
                Disconnecting = true;
            }
        }
        private void SendData(IAsyncResult result)
        {
            try
            {
                _client.Client.EndSend(result);
            }
            catch
            { }
        }
        
        public void Enqueue(Packet p)
        {
            if (p == null) return;
            if (_sendList != null && p != null)
                _sendList.Enqueue(p);

            if (!p.Observable) return;
            foreach (MirConnection c in Observers)
                c.Enqueue(p);
        }

        public void Process()
        {
            if (_client == null || !_client.Connected)
            {
                Disconnect(20);
                return;
            }

            while (!_receiveList.IsEmpty && !Disconnecting)
            {
                Packet p;
                if (!_receiveList.TryDequeue(out p)) continue;

                _lastPackets.Enqueue(p);

                TimeOutTime = Envir.Time + Settings.TimeOut;
                ProcessPacket(p);

                if (_receiveList == null)
                    return;
            }

            while (_retryList.Count > 0)
                _receiveList.Enqueue(_retryList.Dequeue());

            if (Envir.Time > TimeOutTime)
            {
                Disconnect(21);
                return;
            }

            if (_sendList == null || _sendList.Count <= 0) return;

            List<byte> data = new List<byte>();

            while (!_sendList.IsEmpty)
            {
                Packet p;
                if (!_sendList.TryDequeue(out p) || p == null) continue;
                data.AddRange(p.GetPacketBytes());
            }

            BeginSend(data);
        }
        private void ProcessPacket(Packet p)
        {
            if (p == null || Disconnecting) return;

            switch (p.Index)
            {
                case (short)ClientPacketIds.ClientVersion:
                    ClientVersion((ClientVersion) p);
                    break;
                case (short)ClientPacketIds.Disconnect:
                    Disconnect(22);
                    break;
                case (short)ClientPacketIds.KeepAlive: // Keep Alive
                    ClientKeepAlive((KeepAlive)p);
                    break;
                case (short)ClientPacketIds.NewAccount:
                    NewAccount((NewAccount) p);
                    break;
                case (short)ClientPacketIds.ChangePassword:
                    ChangePassword((ChangePassword) p);
                    break;
                case (short)ClientPacketIds.Login:
                    Login((Login) p);
                    break;
                case (short)ClientPacketIds.NewCharacter:
                    NewCharacter((NewCharacter) p);
                    break;
                case (short)ClientPacketIds.DeleteCharacter:
                    DeleteCharacter((DeleteCharacter) p);
                    break;
                case (short)ClientPacketIds.StartGame:
                    StartGame((StartGame) p);
                    break;
                case (short)ClientPacketIds.LogOut:
                    LogOut();
                    break;
                case (short)ClientPacketIds.Turn:
                    Turn((Turn) p);
                    break;
                case (short)ClientPacketIds.Walk:
                    Walk((Walk) p);
                    break;
                case (short)ClientPacketIds.Run:
                    Run((Run) p);
                    break;
                case (short)ClientPacketIds.Chat:
                    Chat((Chat) p);
                    break;
                case (short)ClientPacketIds.MoveItem:
                    MoveItem((MoveItem) p);
                    break;
                case (short)ClientPacketIds.StoreItem:
                    StoreItem((StoreItem) p);
                    break;
                case (short)ClientPacketIds.DepositRefineItem:
                    DepositRefineItem((DepositRefineItem)p);
                    break;
                case (short)ClientPacketIds.RetrieveRefineItem:
                    RetrieveRefineItem((RetrieveRefineItem)p);
                    break;
                case (short)ClientPacketIds.RefineCancel:
                    RefineCancel((RefineCancel)p);
                    break;
                case (short)ClientPacketIds.RefineItem:
                    RefineItem((RefineItem)p);
                    break;
                case (short)ClientPacketIds.CheckRefine:
                    CheckRefine((CheckRefine)p);
                    break;
                case (short)ClientPacketIds.ReplaceWedRing:
                    ReplaceWedRing((ReplaceWedRing)p);
                    break;
                case (short)ClientPacketIds.DepositTradeItem:
                    DepositTradeItem((DepositTradeItem)p);
                    break;
                case (short)ClientPacketIds.RetrieveTradeItem:
                    RetrieveTradeItem((RetrieveTradeItem)p);
                    break;
                case (short)ClientPacketIds.TakeBackItem:
                    TakeBackItem((TakeBackItem) p);
                    break;
                case (short)ClientPacketIds.MergeItem:
                    MergeItem((MergeItem) p);
                    break;
                case (short)ClientPacketIds.EquipItem:
                    EquipItem((EquipItem) p);
                    break;
                case (short)ClientPacketIds.RemoveItem:
                    RemoveItem((RemoveItem) p);
                    break;
                case (short)ClientPacketIds.RemoveSlotItem:
                    RemoveSlotItem((RemoveSlotItem)p);
                    break;
                case (short)ClientPacketIds.SplitItem:
                    SplitItem((SplitItem) p);
                    break;
                case (short)ClientPacketIds.UseItem:
                    UseItem((UseItem) p);
                    break;
                case (short)ClientPacketIds.DropItem:
                    DropItem((DropItem) p);
                    break;
                case (short)ClientPacketIds.TakeBackHeroItem:
                    TakeBackHeroItem((TakeBackHeroItem)p);
                    break;
                case (short)ClientPacketIds.TransferHeroItem:
                    TransferHeroItem((TransferHeroItem)p);
                    break;
                case (short)ClientPacketIds.DropGold:
                    DropGold((DropGold) p);
                    break;
                case (short)ClientPacketIds.PickUp:
                    PickUp();
                    break;
                case (short)ClientPacketIds.RequestMapInfo:
                    RequestMapInfo((RequestMapInfo)p);
                    break;
                case (short)ClientPacketIds.TeleportToNPC:
                    TeleportToNPC((TeleportToNPC)p);
                    break;
                case (short)ClientPacketIds.SearchMap:
                    SearchMap((SearchMap)p);
                    break;
                case (short)ClientPacketIds.Inspect:
                    Inspect((Inspect)p);
                    break;
                case (short)ClientPacketIds.Observe:
                    Observe((Observe)p);
                    break;
                case (short)ClientPacketIds.ChangeAMode:
                    ChangeAMode((ChangeAMode)p);
                    break;
                case (short)ClientPacketIds.ChangePMode:
                    ChangePMode((ChangePMode)p);
                    break;
                case (short)ClientPacketIds.ChangeTrade:
                    ChangeTrade((ChangeTrade)p);
                    break;
                case (short)ClientPacketIds.Attack:
                    Attack((Attack)p);
                    break;
                case (short)ClientPacketIds.RangeAttack:
                    RangeAttack((RangeAttack)p);
                    break;
                case (short)ClientPacketIds.Harvest:
                    Harvest((Harvest)p);
                    break;
                case (short)ClientPacketIds.CallNPC:
                    CallNPC((CallNPC)p);
                    break;
                case (short)ClientPacketIds.BuyItem:
                    BuyItem((BuyItem)p);
                    break;
                case (short)ClientPacketIds.CraftItem:
                    CraftItem((CraftItem)p);
                    break;
                case (short)ClientPacketIds.SellItem:
                    SellItem((SellItem)p);
                    break;
                case (short)ClientPacketIds.RepairItem:
                    RepairItem((RepairItem)p);
                    break;
                case (short)ClientPacketIds.BuyItemBack:
                    BuyItemBack((BuyItemBack)p);
                    break;
                case (short)ClientPacketIds.SRepairItem:
                    SRepairItem((SRepairItem)p);
                    break;
                case (short)ClientPacketIds.MagicKey:
                    MagicKey((MagicKey)p);
                    break;
                case (short)ClientPacketIds.Magic:
                    Magic((Magic)p);
                    break;
                case (short)ClientPacketIds.SwitchGroup:
                    SwitchGroup((SwitchGroup)p);
                    return;
                case (short)ClientPacketIds.AddMember:
                    AddMember((AddMember)p);
                    return;
                case (short)ClientPacketIds.DellMember:
                    DelMember((DelMember)p);
                    return;
                case (short)ClientPacketIds.GroupInvite:
                    GroupInvite((GroupInvite)p);
                    return;
                case (short)ClientPacketIds.NewHero:
                    NewHero((NewHero)p);
                    break;
                case (short)ClientPacketIds.SetAutoPotValue:
                    SetAutoPotValue((SetAutoPotValue)p);
                    break;
                case (short)ClientPacketIds.SetAutoPotItem:
                    SetAutoPotItem((SetAutoPotItem)p);
                    break;
                case (short)ClientPacketIds.SetHeroBehaviour:
                    SetHeroBehaviour((SetHeroBehaviour)p);
                    break;
                case (short)ClientPacketIds.ChangeHero:
                    ChangeHero((ChangeHero)p);
                    break;
                case (short)ClientPacketIds.TownRevive:
                    TownRevive();
                    return;
                case (short)ClientPacketIds.SpellToggle:
                    SpellToggle((SpellToggle)p);
                    return;
                case (short)ClientPacketIds.ConsignItem:
                    ConsignItem((ConsignItem)p);
                    return;
                case (short)ClientPacketIds.MarketSearch:
                    MarketSearch((MarketSearch)p);
                    return;
                case (short)ClientPacketIds.MarketRefresh:
                    MarketRefresh();
                    return;
                case (short)ClientPacketIds.MarketPage:
                    MarketPage((MarketPage) p);
                    return;
                case (short)ClientPacketIds.MarketBuy:
                    MarketBuy((MarketBuy)p);
                    return;
                case (short)ClientPacketIds.MarketGetBack:
                    MarketGetBack((MarketGetBack)p);
                    return;
                case (short)ClientPacketIds.MarketSellNow:
                    MarketSellNow((MarketSellNow)p);
                    return;
                case (short)ClientPacketIds.RequestUserName:
                    RequestUserName((RequestUserName)p);
                    return;
                case (short)ClientPacketIds.RequestChatItem:
                    RequestChatItem((RequestChatItem)p);
                    return;
                case (short)ClientPacketIds.EditGuildMember:
                    EditGuildMember((EditGuildMember)p);
                    return;
                case (short)ClientPacketIds.EditGuildNotice:
                    EditGuildNotice((EditGuildNotice)p);
                    return;
                case (short)ClientPacketIds.GuildInvite:
                    GuildInvite((GuildInvite)p);
                    return;
                case (short)ClientPacketIds.RequestGuildInfo:
                    RequestGuildInfo((RequestGuildInfo)p);
                    return;
                case (short)ClientPacketIds.GuildNameReturn:
                    GuildNameReturn((GuildNameReturn)p);
                    return;
                case (short)ClientPacketIds.GuildStorageGoldChange:
                    GuildStorageGoldChange((GuildStorageGoldChange)p);
                    return;
                case (short)ClientPacketIds.GuildStorageItemChange:
                    GuildStorageItemChange((GuildStorageItemChange)p);
                    return;
                case (short)ClientPacketIds.GuildWarReturn:
                    GuildWarReturn((GuildWarReturn)p);
                    return;
                case (short)ClientPacketIds.MarriageRequest:
                    MarriageRequest((MarriageRequest)p);
                    return;
                case (short)ClientPacketIds.MarriageReply:
                    MarriageReply((MarriageReply)p);
                    return;
                case (short)ClientPacketIds.ChangeMarriage:
                    ChangeMarriage((ChangeMarriage)p);
                    return;
                case (short)ClientPacketIds.DivorceRequest:
                    DivorceRequest((DivorceRequest)p);
                    return;
                case (short)ClientPacketIds.DivorceReply:
                    DivorceReply((DivorceReply)p);
                    return;
                case (short)ClientPacketIds.AddMentor:
                    AddMentor((AddMentor)p);
                    return;
                case (short)ClientPacketIds.MentorReply:
                    MentorReply((MentorReply)p);
                    return;
                case (short)ClientPacketIds.AllowMentor:
                    AllowMentor((AllowMentor)p);
                    return;
                case (short)ClientPacketIds.CancelMentor:
                    CancelMentor((CancelMentor)p);
                    return;
                case (short)ClientPacketIds.TradeRequest:
                    TradeRequest((TradeRequest)p);
                    return;
                case (short)ClientPacketIds.TradeGold:
                    TradeGold((TradeGold)p);
                    return;
                case (short)ClientPacketIds.TradeReply:
                    TradeReply((TradeReply)p);
                    return;
                case (short)ClientPacketIds.TradeConfirm:
                    TradeConfirm((TradeConfirm)p);
                    return;
                case (short)ClientPacketIds.TradeCancel:
                    TradeCancel((TradeCancel)p);
                    return;
                case (short)ClientPacketIds.EquipSlotItem:
                    EquipSlotItem((EquipSlotItem)p);
                    break;
                case (short)ClientPacketIds.FishingCast:
                    FishingCast((FishingCast)p);
                    break;
                case (short)ClientPacketIds.FishingChangeAutocast:
                    FishingChangeAutocast((FishingChangeAutocast)p);
                    break;
                case (short)ClientPacketIds.AcceptQuest:
                    AcceptQuest((AcceptQuest)p);
                    break;
                case (short)ClientPacketIds.FinishQuest:
                    FinishQuest((FinishQuest)p);
                    break;
                case (short)ClientPacketIds.AbandonQuest:
                    AbandonQuest((AbandonQuest)p);
                    break;
                case (short)ClientPacketIds.ShareQuest:
                    ShareQuest((ShareQuest)p);
                    break;
                case (short)ClientPacketIds.AcceptReincarnation:
                    AcceptReincarnation();
                    break;
                case (short)ClientPacketIds.CancelReincarnation:
                     CancelReincarnation();
                    break;
                case (short)ClientPacketIds.CombineItem:
                    CombineItem((CombineItem)p);
                    break;
                case (short)ClientPacketIds.AwakeningNeedMaterials:
                    AwakeningNeedMaterials((AwakeningNeedMaterials)p);
                    break;
                case (short)ClientPacketIds.AwakeningLockedItem:
                    Enqueue(new S.AwakeningLockedItem { UniqueID = ((AwakeningLockedItem)p).UniqueID, Locked = ((AwakeningLockedItem)p).Locked });
                    break;
                case (short)ClientPacketIds.Awakening:
                    Awakening((Awakening)p);
                    break;
                case (short)ClientPacketIds.DisassembleItem:
                    DisassembleItem((DisassembleItem)p);
                    break;
                case (short)ClientPacketIds.DowngradeAwakening:
                    DowngradeAwakening((DowngradeAwakening)p);
                    break;
                case (short)ClientPacketIds.ResetAddedItem:
                    ResetAddedItem((ResetAddedItem)p);
                    break;
                case (short)ClientPacketIds.SendMail:
                    SendMail((SendMail)p);
                    break;
                case (short)ClientPacketIds.ReadMail:
                    ReadMail((ReadMail)p);
                    break;
                case (short)ClientPacketIds.CollectParcel:
                    CollectParcel((CollectParcel)p);
                    break;
                case (short)ClientPacketIds.DeleteMail:
                    DeleteMail((DeleteMail)p);
                    break;
                case (short)ClientPacketIds.LockMail:
                    LockMail((LockMail)p);
                    break;
                case (short)ClientPacketIds.MailLockedItem:
                    Enqueue(new S.MailLockedItem { UniqueID = ((MailLockedItem)p).UniqueID, Locked = ((MailLockedItem)p).Locked });
                    break;
                case (short)ClientPacketIds.MailCost:
                    MailCost((MailCost)p);
                    break;
                case (short)ClientPacketIds.RequestIntelligentCreatureUpdates:
                    RequestIntelligentCreatureUpdates((RequestIntelligentCreatureUpdates)p);
                    break;
                case (short)ClientPacketIds.UpdateIntelligentCreature:
                    UpdateIntelligentCreature((UpdateIntelligentCreature)p);
                    break;
                case (short)ClientPacketIds.IntelligentCreaturePickup:
                    IntelligentCreaturePickup((IntelligentCreaturePickup)p);
                    break;
                case (short)ClientPacketIds.AddFriend:
                    AddFriend((AddFriend)p);
                    break;
                case (short)ClientPacketIds.RemoveFriend:
                    RemoveFriend((RemoveFriend)p);
                    break;
                case (short)ClientPacketIds.RefreshFriends:
                    {
                        if (Stage != GameStage.Game) return;
                        Player.GetFriends();
                        break;
                    }
                case (short)ClientPacketIds.AddMemo:
                    AddMemo((AddMemo)p);
                    break;
                case (short)ClientPacketIds.GuildBuffUpdate:
                    GuildBuffUpdate((GuildBuffUpdate)p);
                    break;
                case (short)ClientPacketIds.GameshopBuy:
                    GameshopBuy((GameshopBuy)p);
                    return;
                case (short)ClientPacketIds.NPCConfirmInput:
                    NPCConfirmInput((NPCConfirmInput)p);
                    break;
                case (short)ClientPacketIds.ReportIssue:
                    ReportIssue((ReportIssue)p);
                    break;
                case (short)ClientPacketIds.GetRanking:
                    GetRanking((GetRanking)p);
                    break;
                case (short)ClientPacketIds.Opendoor:
                    Opendoor((Opendoor)p);
                    break;
                case (short)ClientPacketIds.GetRentedItems:
                    GetRentedItems();
                    break;
                case (short)ClientPacketIds.ItemRentalRequest:
                    ItemRentalRequest();
                    break;
                case (short)ClientPacketIds.ItemRentalFee:
                    ItemRentalFee((ItemRentalFee)p);
                    break;
                case (short)ClientPacketIds.ItemRentalPeriod:
                    ItemRentalPeriod((ItemRentalPeriod)p);
                    break;
                case (short)ClientPacketIds.DepositRentalItem:
                    DepositRentalItem((DepositRentalItem)p);
                    break;
                case (short)ClientPacketIds.RetrieveRentalItem:
                    RetrieveRentalItem((RetrieveRentalItem)p);
                    break;
                case (short)ClientPacketIds.CancelItemRental:
                    CancelItemRental();
                    break;
                case (short)ClientPacketIds.ItemRentalLockFee:
                    ItemRentalLockFee();
                    break;
                case (short)ClientPacketIds.ItemRentalLockItem:
                    ItemRentalLockItem();
                    break;
                case (short)ClientPacketIds.ConfirmItemRental:
                    ConfirmItemRental();
                    break;
                default:
                    MessageQueue.Enqueue(string.Format("Invalid packet received. Index : {0}", p.Index));
                    break;
            }
        }

        public void SoftDisconnect(byte reason)
        {
            Stage = GameStage.Disconnected;
            TimeDisconnected = Envir.Time;
            
            lock (Envir.AccountLock)
            {
                if (Player != null)
                    Player.StopGame(reason);

                if (Account != null && Account.Connection == this)
                    Account.Connection = null;
            }

            Account = null;
        }
        public void Disconnect(byte reason)
        {
            if (!Connected) return;

            Connected = false;
            Stage = GameStage.Disconnected;
            TimeDisconnected = Envir.Time;

            lock (Envir.Connections)
                Envir.Connections.Remove(this);

            lock (Envir.AccountLock)
            {
                if (Player != null)
                    Player.StopGame(reason);

                if (Account != null && Account.Connection == this)
                    Account.Connection = null;
            }

            if (Observing != null)
                Observing.Observers.Remove(this);            

            Account = null;

            _sendList = null;
            _receiveList = null;
            _retryList = null;
            _rawData = null;

            if (_client != null) _client.Client.Dispose();
            _client = null;
        }
        public void SendDisconnect(byte reason)
        {
            if (!Connected)
            {
                Disconnecting = true;
                SoftDisconnect(reason);
                return;
            }
            
            Disconnecting = true;

            List<byte> data = new List<byte>();

            data.AddRange(new S.Disconnect { Reason = reason }.GetPacketBytes());

            BeginSend(data);
            SoftDisconnect(reason);
        }
        public void CleanObservers()
        {
            foreach (MirConnection c in Observers)
            {
                c.Stage = GameStage.Login;
                c.Enqueue(new S.ReturnToLogin());
            }
        }

        private void ClientVersion(ClientVersion p)
        {
            if (Stage != GameStage.None) return;

            if (Settings.CheckVersion)
            {
                bool match = false;

                foreach (var hash in Settings.VersionHashes)
                {
                    if (Functions.CompareBytes(hash, p.VersionHash))
                    {
                        match = true;
                        break;
                    }
                }

                if (!match)
                {
                    Disconnecting = true;

                    List<byte> data = new List<byte>();

                    data.AddRange(new S.ClientVersion { Result = 0 }.GetPacketBytes());

                    BeginSend(data);
                    SoftDisconnect(10);
                    MessageQueue.Enqueue(SessionID + ", Disconnnected - Wrong Client Version.");
                    return;
                }
            }

            MessageQueue.Enqueue(SessionID + ", " + IPAddress + ", Client version matched.");
            Enqueue(new S.ClientVersion { Result = 1 });

            Stage = GameStage.Login;
        }
        private void ClientKeepAlive(KeepAlive p)
        {
            Enqueue(new S.KeepAlive
            {
                Time = p.Time
            });
        }
        private void NewAccount(NewAccount p)
        {
            if (Stage != GameStage.Login) return;

            MessageQueue.Enqueue(SessionID + ", " + IPAddress + ", New account being created.");
            Envir.NewAccount(p, this);
        }
        private void ChangePassword(ChangePassword p)
        {
            if (Stage != GameStage.Login) return;

            MessageQueue.Enqueue(SessionID + ", " + IPAddress + ", Password being changed.");
            Envir.ChangePassword(p, this);
        }
        private void Login(Login p)
        {
            if (Stage != GameStage.Login) return;

            MessageQueue.Enqueue(SessionID + ", " + IPAddress + ", User logging in.");
            Envir.Login(p, this);
        }
        private void NewCharacter(NewCharacter p)
        {
            if (Stage != GameStage.Select) return;

            Envir.NewCharacter(p, this, Account.AdminAccount);
        }
        private void DeleteCharacter(DeleteCharacter p)
        {
            if (Stage != GameStage.Select) return;
            
            if (!Settings.AllowDeleteCharacter)
            {
                Enqueue(new S.DeleteCharacter { Result = 0 });
                return;
            }

            CharacterInfo temp = null;
            

            for (int i = 0; i < Account.Characters.Count; i++)
			{
			    if (Account.Characters[i].Index != p.CharacterIndex) continue;

			    temp = Account.Characters[i];
			    break;
			}

            if (temp == null)
            {
                Enqueue(new S.DeleteCharacter { Result = 1 });
                return;
            }

            temp.Deleted = true;
            temp.DeleteDate = Envir.Now;
            Envir.RemoveRank(temp);
            Enqueue(new S.DeleteCharacterSuccess { CharacterIndex = temp.Index });
        }
        private void StartGame(StartGame p)
        {
            if (Stage != GameStage.Select) return;

            if (!Settings.AllowStartGame && (Account == null || (Account != null && !Account.AdminAccount)))
            {
                Enqueue(new S.StartGame { Result = 0 });
                return;
            }

            if (Account == null)
            {
                Enqueue(new S.StartGame { Result = 1 });
                return;
            }


            CharacterInfo info = null;

            for (int i = 0; i < Account.Characters.Count; i++)
            {
                if (Account.Characters[i].Index != p.CharacterIndex) continue;

                info = Account.Characters[i];
                break;
            }
            if (info == null)
            {
                Enqueue(new S.StartGame { Result = 2 });
                return;
            }

            if (info.Banned)
            {
                if (info.ExpiryDate > Envir.Now)
                {
                    Enqueue(new S.StartGameBanned { Reason = info.BanReason, ExpiryDate = info.ExpiryDate });
                    return;
                }
                info.Banned = false;
            }
            info.BanReason = string.Empty;
            info.ExpiryDate = DateTime.MinValue;

            long delay = (long) (Envir.Now - info.LastLogoutDate).TotalMilliseconds;


            //if (delay < Settings.RelogDelay)
            //{
            //    Enqueue(new S.StartGameDelay { Milliseconds = Settings.RelogDelay - delay });
            //    return;
            //}

            Player = new PlayerObject(info, this);
            Player.StartGame();
        }

        public void LogOut()
        {
            if (Stage != GameStage.Game) return;

            if (Envir.Time < Player.LogTime)
            {
                Enqueue(new S.LogOutFailed());
                return;
            }

            Player.StopGame(23);

            Stage = GameStage.Select;
            Player = null;

            Enqueue(new S.LogOutSuccess { Characters = Account.GetSelectInfo() });
        }

        private void Turn(Turn p)
        {
            if (Stage != GameStage.Game) return;

            if (Player.ActionTime > Envir.Time)
                _retryList.Enqueue(p);
            else
                Player.Turn(p.Direction);
        }
        private void Walk(Walk p)
        {
            if (Stage != GameStage.Game) return;

            if (Player.ActionTime > Envir.Time)
                _retryList.Enqueue(p);
            else
                Player.Walk(p.Direction);
        }
        private void Run(Run p)
        {
            if (Stage != GameStage.Game) return;

            if (Player.ActionTime > Envir.Time)
                _retryList.Enqueue(p);
            else
                Player.Run(p.Direction);
        }
        
        private void Chat(Chat p)
        {
            if (p.Message.Length > Globals.MaxChatLength)
            {
                SendDisconnect(2);
                return;
            }

            if (Stage != GameStage.Game) return;

            Player.Chat(p.Message, p.LinkedItems);
        }

        private void MoveItem(MoveItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.MoveItem(p.Grid, p.From, p.To);
        }
        private void StoreItem(StoreItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.StoreItem(p.From, p.To);
        }

        private void DepositRefineItem(DepositRefineItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.DepositRefineItem(p.From, p.To);
        }

        private void RetrieveRefineItem(RetrieveRefineItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.RetrieveRefineItem(p.From, p.To);
        }

        private void RefineCancel(RefineCancel p)
        {
            if (Stage != GameStage.Game) return;

            Player.RefineCancel();
        }

        private void RefineItem(RefineItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.RefineItem(p.UniqueID);
        }

        private void CheckRefine(CheckRefine p)
        {
            if (Stage != GameStage.Game) return;

            Player.CheckRefine(p.UniqueID);
        }

        private void ReplaceWedRing(ReplaceWedRing p)
        {
            if (Stage != GameStage.Game) return;

            Player.ReplaceWeddingRing(p.UniqueID);
        }

        private void DepositTradeItem(DepositTradeItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.DepositTradeItem(p.From, p.To);
        }
        
        private void RetrieveTradeItem(RetrieveTradeItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.RetrieveTradeItem(p.From, p.To);
        }
        private void TakeBackItem(TakeBackItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.TakeBackItem(p.From, p.To);
        }
        private void MergeItem(MergeItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.MergeItem(p.GridFrom, p.GridTo, p.IDFrom, p.IDTo);
        }
        private void EquipItem(EquipItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.EquipItem(p.Grid, p.UniqueID, p.To);
        }
        private void RemoveItem(RemoveItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.RemoveItem(p.Grid, p.UniqueID, p.To);
        }
        private void RemoveSlotItem(RemoveSlotItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.RemoveSlotItem(p.Grid, p.UniqueID, p.To, p.GridTo, p.FromUniqueID);
        }
        private void SplitItem(SplitItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.SplitItem(p.Grid, p.UniqueID, p.Count);
        }
        private void UseItem(UseItem p)
        {
            if (Stage != GameStage.Game) return;

            switch (p.Grid)
            {
                case MirGridType.Inventory:
                    Player.UseItem(p.UniqueID);
                    break;
                case MirGridType.HeroInventory:
                    Player.HeroUseItem(p.UniqueID);
                    break;
            }            
        }
        private void DropItem(DropItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.DropItem(p.UniqueID, p.Count, p.HeroInventory);
        }

        private void TakeBackHeroItem(TakeBackHeroItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.TakeBackHeroItem(p.From, p.To);
        }

        private void TransferHeroItem(TransferHeroItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.TransferHeroItem(p.From, p.To);
        }
        private void DropGold(DropGold p)
        {
            if (Stage != GameStage.Game) return;

            Player.DropGold(p.Amount);
        }
        private void PickUp()
        {
            if (Stage != GameStage.Game) return;

            Player.PickUp();
        }

        private void RequestMapInfo(RequestMapInfo p)
        {
            if (Stage != GameStage.Game) return;

            Player.RequestMapInfo(p.MapIndex);
        }

        private void TeleportToNPC(TeleportToNPC p)
        {
            if (Stage != GameStage.Game) return;

            Player.TeleportToNPC(p.ObjectID);
        }

        private void SearchMap(SearchMap p)
        {
            if (Stage != GameStage.Game) return;

            Player.SearchMap(p.Text);
        }
        private void Inspect(Inspect p)
        {
            if (Stage != GameStage.Game && Stage != GameStage.Observer) return;

            if (p.Ranking)
            {
                Envir.Inspect(this, (int)p.ObjectID);
            }
            else if (p.Hero)
            {
                Envir.InspectHero(this, (int)p.ObjectID);
            }
            else
            {
                Envir.Inspect(this, p.ObjectID);
            } 
        }
        private void Observe(Observe p)
        {
            if (Stage != GameStage.Game && Stage != GameStage.Observer) return;

            Envir.Observe(this, p.Name);
        }
        private void ChangeAMode(ChangeAMode p)
        {
            if (Stage != GameStage.Game) return;

            Player.AMode = p.Mode;

            Enqueue(new S.ChangeAMode {Mode = Player.AMode});
        }
        private void ChangePMode(ChangePMode p)
        {
            if (Stage != GameStage.Game) return;

            Player.PMode = p.Mode;

            Enqueue(new S.ChangePMode { Mode = Player.PMode });
        }
        private void ChangeTrade(ChangeTrade p)
        {
            if (Stage != GameStage.Game) return;

            Player.AllowTrade = p.AllowTrade;
        }
        private void Attack(Attack p)
        {
            if (Stage != GameStage.Game) return;

            if (!Player.Dead && (Player.ActionTime > Envir.Time || Player.AttackTime > Envir.Time))
                _retryList.Enqueue(p);
            else
                Player.Attack(p.Direction, p.Spell);
        }
        private void RangeAttack(RangeAttack p)
        {
            if (Stage != GameStage.Game) return;

            if (!Player.Dead && (Player.ActionTime > Envir.Time || Player.AttackTime > Envir.Time))
                _retryList.Enqueue(p);
            else
                Player.RangeAttack(p.Direction, p.TargetLocation, p.TargetID);
        }
        private void Harvest(Harvest p)
        {
            if (Stage != GameStage.Game) return;

            if (!Player.Dead && Player.ActionTime > Envir.Time)
                _retryList.Enqueue(p);
            else
                Player.Harvest(p.Direction);
        }

        private void CallNPC(CallNPC p)
        {
            if (Stage != GameStage.Game) return;

            if (p.Key.Length > 30) //No NPC Key should be that long.
            {
                SendDisconnect(2);
                return;
            }

            if (p.ObjectID == Envir.DefaultNPC.LoadedObjectID && Player.NPCObjectID == Envir.DefaultNPC.LoadedObjectID)
            {
                Player.CallDefaultNPC(p.Key);
                return;
            }

            if (p.ObjectID == uint.MaxValue)
            {
                Player.CallDefaultNPC(DefaultNPCType.Client, null);
                return;
            }

            Player.CallNPC(p.ObjectID, p.Key);
        }

        private void BuyItem(BuyItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.BuyItem(p.ItemIndex, p.Count, p.Type);
        }
        private void CraftItem(CraftItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.CraftItem(p.UniqueID, p.Count, p.Slots);
        }
        private void SellItem(SellItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.SellItem(p.UniqueID, p.Count);
        }
        private void RepairItem(RepairItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.RepairItem(p.UniqueID);
        }
        private void BuyItemBack(BuyItemBack p)
        {
            if (Stage != GameStage.Game) return;

           // Player.BuyItemBack(p.UniqueID, p.Count);
        }
        private void SRepairItem(SRepairItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.RepairItem(p.UniqueID, true);
        }
        private void MagicKey(MagicKey p)
        {
            if (Stage != GameStage.Game) return;

            HumanObject actor = Player;
            if (p.Key > 16 || p.OldKey > 16)
            {
                if (!Player.HeroSpawned || Player.Hero.Dead) return;
                actor = Player.Hero;
            }

            for (int i = 0; i < actor.Info.Magics.Count; i++)
            {
                UserMagic magic = actor.Info.Magics[i];
                if (magic.Spell != p.Spell)
                {
                    if (magic.Key == p.Key)
                        magic.Key = 0;
                    continue;
                }

                magic.Key = p.Key;
            }
        }
        private void Magic(Magic p)
        {
            if (Stage != GameStage.Game) return;

            HumanObject actor = Player;
            if (Player.HeroSpawned && p.ObjectID == Player.Hero.ObjectID)
                actor = Player.Hero;

            if (actor.Dead) return;

            if (!actor.Dead && (actor.ActionTime > Envir.Time || actor.SpellTime > Envir.Time))
                _retryList.Enqueue(p);
            else
                actor.BeginMagic(p.Spell, p.Direction, p.TargetID, p.Location, p.SpellTargetLock);
        }

        private void SwitchGroup(SwitchGroup p)
        {
            if (Stage != GameStage.Game) return;

            Player.SwitchGroup(p.AllowGroup);
        }
        private void AddMember(AddMember p)
        {
            if (Stage != GameStage.Game) return;

            Player.AddMember(p.Name);
        }
        private void DelMember(DelMember p)
        {
            if (Stage != GameStage.Game) return;

            Player.DelMember(p.Name);
        }
        private void GroupInvite(GroupInvite p)
        {
            if (Stage != GameStage.Game) return;

            Player.GroupInvite(p.AcceptInvite);
        }

        private void NewHero(NewHero p)
        {
            if (Stage != GameStage.Game) return;

            Player.NewHero(p);
        }

        private void SetAutoPotValue(SetAutoPotValue p)
        {
            if (Stage != GameStage.Game) return;

            Player.SetAutoPotValue(p.Stat, p.Value);
        }

        private void SetAutoPotItem(SetAutoPotItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.SetAutoPotItem(p.Grid, p.ItemIndex);
        }

        private void SetHeroBehaviour(SetHeroBehaviour p)
        {
            if (Stage != GameStage.Game) return;

            Player.SetHeroBehaviour(p.Behaviour);
        }

        private void ChangeHero(ChangeHero p)
        {
            if (Stage != GameStage.Game) return;

            Player.ChangeHero(p.ListIndex);
        }

        private void TownRevive()
        {
            if (Stage != GameStage.Game) return;

            Player.TownRevive();
        }

        private void SpellToggle(SpellToggle p)
        {
            if (Stage != GameStage.Game) return;

            if (p.canUse > SpellToggleState.None)
            {
                Player.SpellToggle(p.Spell, p.canUse);
                return;
            }
            if (Player.HeroSpawned)
                Player.Hero.SpellToggle(p.Spell, p.canUse);            
        }
        private void ConsignItem(ConsignItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.ConsignItem(p.UniqueID, p.Price, p.Type);
        }
        private void GuildTerritoryPage(C.GuildTerritoryPage p)
        {
            if (Stage != GameStage.Game) return;

            Player.GetGuildTerritories(p.Page);
        }

        private void PurchaseGuildTerritory(C.PurchaseGuildTerritory p)
        {
            if (Stage != GameStage.Game) return;

            Player.PurchaseGuildTerritory(p.Owner);
        }
        private void MarketSearch(C.MarketSearch p)
        {
            if (Stage != GameStage.Game) return;

            Player.UserMatch = p.Usermode;
            Player.MinShapes = p.MinShape;
            Player.MaxShapes = p.MaxShape;
            Player.MarketPanelType = p.MarketType;

            Player.MarketSearch(p.Match, p.Type);
        }
        private void MarketRefresh()
        {
            if (Stage != GameStage.Game) return;

            Player.MarketSearch(string.Empty, Player.MatchType);
        }

        private void MarketPage(MarketPage p)
        {
            if (Stage != GameStage.Game) return;

            Player.MarketPage(p.Page);
        }
        private void MarketBuy(MarketBuy p)
        {
            if (Stage != GameStage.Game) return;

            Player.MarketBuy(p.AuctionID, p.BidPrice);
        }
        private void MarketSellNow(MarketSellNow p)
        {
            if (Stage != GameStage.Game) return;

            Player.MarketSellNow(p.AuctionID);
        }

        private void MarketGetBack(MarketGetBack p)
        {
            if (Stage != GameStage.Game) return;

            Player.MarketGetBack(p.Mode, p.AuctionID);
        }
        private void RequestUserName(RequestUserName p)
        {
            if (Stage != GameStage.Game) return;

            Player.RequestUserName(p.UserID);
        }
        private void RequestChatItem(RequestChatItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.RequestChatItem(p.ChatItemID);
        }
        private void EditGuildMember(EditGuildMember p)
        {
            if (Stage != GameStage.Game) return;
            Player.EditGuildMember(p.Name,p.RankName,p.RankIndex,p.ChangeType);
        }
        private void EditGuildNotice(EditGuildNotice p)
        {
            if (Stage != GameStage.Game) return;
            Player.EditGuildNotice(p.notice);
        }
        private void GuildInvite(GuildInvite p)
        {
            if (Stage != GameStage.Game) return;

            Player.GuildInvite(p.AcceptInvite);
        }
        private void RequestGuildInfo(RequestGuildInfo p)
        {
            if (Stage != GameStage.Game) return;
            Player.RequestGuildInfo(p.Type);
        }
        private void GuildNameReturn(GuildNameReturn p)
        {
            if (Stage != GameStage.Game) return;
            Player.GuildNameReturn(p.Name);
        }
        private void GuildStorageGoldChange(GuildStorageGoldChange p)
        {
            if (Stage != GameStage.Game) return;
            Player.GuildStorageGoldChange(p.Type, p.Amount);
        }
        private void GuildStorageItemChange(GuildStorageItemChange p)
        {
            if (Stage != GameStage.Game) return;
            Player.GuildStorageItemChange(p.Type, p.From, p.To);
        }
        private void GuildWarReturn(GuildWarReturn p)
        {
            if (Stage != GameStage.Game) return;
            Player.GuildWarReturn(p.Name);
        }


        private void MarriageRequest(MarriageRequest p)
        {
            if (Stage != GameStage.Game) return;

            Player.MarriageRequest();
        }

        private void MarriageReply(MarriageReply p)
        {
            if (Stage != GameStage.Game) return;

            Player.MarriageReply(p.AcceptInvite);
        }

        private void ChangeMarriage(ChangeMarriage p)
        {
            if (Stage != GameStage.Game) return;

            if (Player.Info.Married == 0)
            {
                Player.AllowMarriage = !Player.AllowMarriage;
                if (Player.AllowMarriage)
                    Player.ReceiveChat("You're now allowing marriage requests.", ChatType.Hint);
                else
                    Player.ReceiveChat("You're now blocking marriage requests.", ChatType.Hint);
            }
            else
            {
                Player.AllowLoverRecall = !Player.AllowLoverRecall;
                if (Player.AllowLoverRecall)
                    Player.ReceiveChat("You're now allowing recall from lover.", ChatType.Hint);
                else
                    Player.ReceiveChat("You're now blocking recall from lover.", ChatType.Hint);
            }
        }

        private void DivorceRequest(DivorceRequest p)
        {
            if (Stage != GameStage.Game) return;

            Player.DivorceRequest();
        }

        private void DivorceReply(DivorceReply p)
        {
            if (Stage != GameStage.Game) return;

            Player.DivorceReply(p.AcceptInvite);
        }

        private void AddMentor(AddMentor p)
        {
            if (Stage != GameStage.Game) return;

            Player.AddMentor(p.Name);
        }

        private void MentorReply(MentorReply p)
        {
            if (Stage != GameStage.Game) return;

            Player.MentorReply(p.AcceptInvite);
        }

        private void AllowMentor(AllowMentor p)
        {
            if (Stage != GameStage.Game) return;

                Player.AllowMentor = !Player.AllowMentor;
                if (Player.AllowMentor)
                    Player.ReceiveChat(GameLanguage.Instance.AllowingMentorRequests, ChatType.Hint);
                else
                    Player.ReceiveChat(GameLanguage.Instance.BlockingMentorRequests, ChatType.Hint);
        }

        private void CancelMentor(CancelMentor p)
        {
            if (Stage != GameStage.Game) return;

            Player.MentorBreak(true);
        }

        private void TradeRequest(TradeRequest p)
        {
            if (Stage != GameStage.Game) return;

            Player.TradeRequest();
        }
        private void TradeGold(TradeGold p)
        {
            if (Stage != GameStage.Game) return;

            Player.TradeGold(p.Amount);
        }
        private void TradeReply(TradeReply p)
        {
            if (Stage != GameStage.Game) return;

            Player.TradeReply(p.AcceptInvite);
        }
        private void TradeConfirm(TradeConfirm p)
        {
            if (Stage != GameStage.Game) return;

            Player.TradeConfirm(p.Locked);
        }
        private void TradeCancel(TradeCancel p)
        {
            if (Stage != GameStage.Game) return;

            Player.TradeCancel();
        }
        private void EquipSlotItem(EquipSlotItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.EquipSlotItem(p.Grid, p.UniqueID, p.To, p.GridTo, p.ToUniqueID);
        }

        private void FishingCast(FishingCast p)
        {
            if (Stage != GameStage.Game) return;

            Player.FishingCast(p.CastOut, true);
        }

        private void FishingChangeAutocast(FishingChangeAutocast p)
        {
            if (Stage != GameStage.Game) return;

            Player.FishingChangeAutocast(p.AutoCast);
        }

        private void AcceptQuest(AcceptQuest p)
        {
            if (Stage != GameStage.Game) return;

            Player.AcceptQuest(p.QuestIndex); //p.NPCIndex,
        }

        private void FinishQuest(FinishQuest p)
        {
            if (Stage != GameStage.Game) return;

            Player.FinishQuest(p.QuestIndex, p.SelectedItemIndex);
        }

        private void AbandonQuest(AbandonQuest p)
        {
            if (Stage != GameStage.Game) return;

            Player.AbandonQuest(p.QuestIndex);
        }

        private void ShareQuest(ShareQuest p)
        {
            if (Stage != GameStage.Game) return;

            Player.ShareQuest(p.QuestIndex);
        }

        private void AcceptReincarnation()
        {
            if (Stage != GameStage.Game) return;

            if (Player.ReincarnationHost != null && Player.ReincarnationHost.ReincarnationReady)
            {
                Player.Revive(Player.Stats[Stat.HP] / 2, true);
                Player.ReincarnationHost = null;
                return;
            }

            Player.ReceiveChat("Reincarnation failed", ChatType.System);
        }

        private void CancelReincarnation()
        {
            if (Stage != GameStage.Game) return;
            Player.ReincarnationExpireTime = Envir.Time;

        }

        private void CombineItem(CombineItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.CombineItem(p.Grid, p.IDFrom, p.IDTo);
        }

        private void Awakening(Awakening p)
        {
            if (Stage != GameStage.Game) return;

            Player.Awakening(p.UniqueID, p.Type);
        }

        private void AwakeningNeedMaterials(AwakeningNeedMaterials p)
        {
            if (Stage != GameStage.Game) return;

            Player.AwakeningNeedMaterials(p.UniqueID, p.Type);
        }

        private void DisassembleItem(DisassembleItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.DisassembleItem(p.UniqueID);
        }

        private void DowngradeAwakening(DowngradeAwakening p)
        {
            if (Stage != GameStage.Game) return;

            Player.DowngradeAwakening(p.UniqueID);
        }

        private void ResetAddedItem(ResetAddedItem p)
        {
            if (Stage != GameStage.Game) return;

            Player.ResetAddedItem(p.UniqueID);
        }

        public void SendMail(SendMail p)
        {
            if (Stage != GameStage.Game) return;

            if (p.Gold > 0 || p.ItemsIdx.Length > 0)
            {
                Player.SendMail(p.Name, p.Message, p.Gold, p.ItemsIdx, p.Stamped);
            }
            else
            {
                Player.SendMail(p.Name, p.Message);
            }
        }

        public void ReadMail(ReadMail p)
        {
            if (Stage != GameStage.Game) return;

            Player.ReadMail(p.MailID);
        }

        public void CollectParcel(CollectParcel p)
        {
            if (Stage != GameStage.Game) return;

            Player.CollectMail(p.MailID);
        }

        public void DeleteMail(DeleteMail p)
        {
            if (Stage != GameStage.Game) return;

            Player.DeleteMail(p.MailID);
        }

        public void LockMail(LockMail p)
        {
            if (Stage != GameStage.Game) return;

            Player.LockMail(p.MailID, p.Lock);
        }

        public void MailCost(MailCost p)
        {
            if (Stage != GameStage.Game) return;

            uint cost = Player.GetMailCost(p.ItemsIdx, p.Gold, p.Stamped);

            Enqueue(new S.MailCost { Cost = cost });
        }

        private void RequestIntelligentCreatureUpdates(RequestIntelligentCreatureUpdates p)
        {
            if (Stage != GameStage.Game) return;

            Player.SendIntelligentCreatureUpdates = p.Update;
        }

        private void UpdateIntelligentCreature(UpdateIntelligentCreature p)
        {
            if (Stage != GameStage.Game) return;

            ClientIntelligentCreature petUpdate = p.Creature;
            if (petUpdate == null) return;

            if (p.ReleaseMe)
            {
                Player.ReleaseIntelligentCreature(petUpdate.PetType);
                return;
            }
            else if (p.SummonMe)
            {
                Player.SummonIntelligentCreature(petUpdate.PetType);
                return;
            }
            else if (p.UnSummonMe)
            {
                Player.UnSummonIntelligentCreature(petUpdate.PetType);
                return;
            }
            else
            {
                //Update the creature info
                for (int i = 0; i < Player.Info.IntelligentCreatures.Count; i++)
                {
                    if (Player.Info.IntelligentCreatures[i].PetType == petUpdate.PetType)
                    {
                        var reg = new Regex(@"^[A-Za-z0-9]{" + Globals.MinCharacterNameLength + "," + Globals.MaxCharacterNameLength + "}$");

                        if (reg.IsMatch(petUpdate.CustomName))
                        {
                            Player.Info.IntelligentCreatures[i].CustomName = petUpdate.CustomName;
                        }

                        Player.Info.IntelligentCreatures[i].SlotIndex = petUpdate.SlotIndex;
                        Player.Info.IntelligentCreatures[i].Filter = petUpdate.Filter;
                        Player.Info.IntelligentCreatures[i].petMode = petUpdate.petMode;
                    }
                    else continue;
                }

                if (Player.CreatureSummoned)
                {
                    if (Player.SummonedCreatureType == petUpdate.PetType)
                        Player.UpdateSummonedCreature(petUpdate.PetType);
                }
            }
        }

        private void IntelligentCreaturePickup(IntelligentCreaturePickup p)
        {
            if (Stage != GameStage.Game) return;

            Player.IntelligentCreaturePickup(p.MouseMode, p.Location);
        }

        private void AddFriend(AddFriend p)
        {
            if (Stage != GameStage.Game) return;

            Player.AddFriend(p.Name, p.Blocked);
        }

        private void RemoveFriend(RemoveFriend p)
        {
            if (Stage != GameStage.Game) return;

            Player.RemoveFriend(p.CharacterIndex);
        }

        private void AddMemo(AddMemo p)
        {
            if (Stage != GameStage.Game) return;

            Player.AddMemo(p.CharacterIndex, p.Memo);
        }
        private void GuildBuffUpdate(GuildBuffUpdate p)
        {
            if (Stage != GameStage.Game) return;
            Player.GuildBuffUpdate(p.Action,p.Id);
        }
        private void GameshopBuy(GameshopBuy p)
        {
            if (Stage != GameStage.Game) return;
            Player.GameshopBuy(p.GIndex, p.Quantity, p.PType);
        }

        private void NPCConfirmInput(NPCConfirmInput p)
        {
            if (Stage != GameStage.Game) return;

            Player.NPCData["NPCInputStr"] = p.Value;

            if (p.NPCID == Envir.DefaultNPC.LoadedObjectID && Player.NPCObjectID == Envir.DefaultNPC.LoadedObjectID)
            {
                Player.CallDefaultNPC(p.PageName);
                return;
            }

            Player.CallNPC(Player.NPCObjectID, p.PageName);
        }

        public List<byte[]> Image = new List<byte[]>();
        
        private void ReportIssue(ReportIssue p)
        {
            if (Stage != GameStage.Game) return;

            return;

            // Image.Add(p.Image);

            // if (p.ImageChunk >= p.ImageSize)
            // {
            //     System.Drawing.Image image = Functions.ByteArrayToImage(Functions.CombineArray(Image));
            //     image.Save("Reported-" + Player.Name + "-" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg");
            //     Image.Clear();
            // }
        }
        private void GetRanking(GetRanking p)
        {
            if (Stage != GameStage.Game && Stage != GameStage.Observer) return;
            Envir.GetRanking(this, p.RankType, p.RankIndex, p.OnlineOnly);
        }

        private void Opendoor(Opendoor p)
        {
            if (Stage != GameStage.Game) return;
            Player.Opendoor(p.DoorIndex);
        }

        private void GetRentedItems()
        {
            if (Stage != GameStage.Game)
                return;

            Player.GetRentedItems();
        }

        private void ItemRentalRequest()
        {
            if (Stage != GameStage.Game)
                return;

            Player.ItemRentalRequest();
        }

        private void ItemRentalFee(ItemRentalFee p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SetItemRentalFee(p.Amount);
        }

        private void ItemRentalPeriod(ItemRentalPeriod p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.SetItemRentalPeriodLength(p.Days);
        }

        private void DepositRentalItem(DepositRentalItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.DepositRentalItem(p.From, p.To);
        }

        private void RetrieveRentalItem(RetrieveRentalItem p)
        {
            if (Stage != GameStage.Game)
                return;

            Player.RetrieveRentalItem(p.From, p.To);
        }

        private void CancelItemRental()
        {
            if (Stage != GameStage.Game)
                return;

            Player.CancelItemRental();
        }

        private void ItemRentalLockFee()
        {
            if (Stage != GameStage.Game)
                return;

            Player.ItemRentalLockFee();
        }

        private void ItemRentalLockItem()
        {
            if (Stage != GameStage.Game)
                return;

            Player.ItemRentalLockItem();
        }

        private void ConfirmItemRental()
        {
            if (Stage != GameStage.Game)
                return;

            Player.ConfirmItemRental();
        }

        public void CheckItemInfo(ItemInfo info, bool dontLoop = false)
        {
            if ((dontLoop == false) && (info.ClassBased | info.LevelBased)) //send all potential data so client can display it
            {
                for (int i = 0; i < Envir.ItemInfoList.Count; i++)
                {
                    if ((Envir.ItemInfoList[i] != info) && (Envir.ItemInfoList[i].Name.StartsWith(info.Name)))
                        CheckItemInfo(Envir.ItemInfoList[i], true);
                }
            }

            if (SentItemInfo.Contains(info)) return;
            Enqueue(new S.NewItemInfo { Info = info });
            SentItemInfo.Add(info);
        }
        public void CheckItem(UserItem item)
        {
            CheckItemInfo(item.Info);

            for (int i = 0; i < item.Slots.Length; i++)
            {
                if (item.Slots[i] == null) continue;

                CheckItemInfo(item.Slots[i].Info);
            }

            CheckHeroInfo(item);
        }
        private void CheckHeroInfo(UserItem item)
        {
            if (item.AddedStats[Stat.Hero] == 0) return;
            if (SentHeroInfo.Contains(item.UniqueID)) return;

            HeroInfo heroInfo = Envir.GetHeroInfo(item.AddedStats[Stat.Hero]);
            if (heroInfo == null) return;

            Enqueue(new S.NewHeroInfo { Info = heroInfo.ClientInformation });
            SentHeroInfo.Add(item.UniqueID);
        }
    }

    public class MirConnectionLog {
        public string IPAddress = "";
        public List<long> AccountsMade = new List<long>();
        public List<long> CharactersMade = new List<long>();
    }
}

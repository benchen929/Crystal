﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using ClientPackets;
using ServerPackets;

public class GameLanguage_CN : GameLanguage
{
    public GameLanguage_CN()
    {
                         //Client
                         PetMode_Both = "[下属: 攻击和移动]";
                         PetMode_MoveOnly = "[下属: 不攻击]";
                         PetMode_AttackOnly = "[下属: 不移动]";
                         PetMode_None = "[下属: 不攻击或移动]";
                         PetMode_FocusMasterTarget = "[下属: 锁定主人的目标]";

                         AttackMode_Peace = "[模式: 和平]";
                         AttackMode_Group = "[模式: 组队]";
                         AttackMode_Guild = "[模式: 行会]";
                         AttackMode_EnemyGuild = "[模式: 敌对行会]";
                         AttackMode_RedBrown = "[模式: 善恶]";
                         AttackMode_All = "[模式: 全体]";

                         LogOutTip = "你要退出传奇2吗?";
                         ExitTip = "你要关闭传奇2吗?";
                         DiedTip = "你已经死亡; 需要在城镇复活吗?";
                         DropTip = "你确定要丢弃 {0} 吗?";

                         Inventory = "背包 ({0})";
                         Character = "人物 ({0})";
                         Skills = "技能 ({0})";
                         Quests = "任务 ({0})";
                         Options = "选项 ({0})";
                         Menu = "菜单";
                         GameShop = "游戏商城 ({0})";
                         BigMap = "大地图 ({0})";
                         DuraPanel = "持久度面板";
                         Mail = "邮件";
                         Exit = "关闭 ({0})";
                         LogOut = "退出 ({0})";
                         Help = "帮助 ({0})";
                         Keybinds = "按键";
                         Ranking = "排行榜 ({0})";
                         Creatures = "下属 ({0})";
                         Mount = "坐骑 ({0})";
                         Fishing = "钓鱼 ({0})";
                         Friends = "好友 ({0})";
                         Mentor = "师徒 ({0})";
                         Relationship = "伴侣 ({0})";
                         Groups = "组队 ({0})";
                         Guild = "行会 ({0})";
                         Expire = "到期: {0}";
                         ExpireNever = "到期: 永不";
                         ExpirePaused = "到期: 暂停";
                         Never = "永不";
                         Trade = "交易 ({0})";
                         Size = "大小";
                         ChatSettings = "聊天设置";
                         Rotate = "旋转";
                         Close = "关闭 ({0})";
                         GameMaster = "GameMaster";

                         PatchErr = "无法获得更新信息";
                         LastOnline = "上次在线";

                         Gold = "金币";
                         Credit = "元宝";

                         YouGained = "你获得了 {0}.";

                         YouGained2 = "你获得了 {0:###;###;###} {1}";

                         ExperienceGained = "获得经验 {0}";

                         HeroInventory = "英雄背包 ({0})";
                         HeroCharacter = "英雄人物 ({0})";
                         HeroSkills = "英雄技能 ({0})";
                         HeroExperienceGained = "英雄获得经验 {0}";

                         ItemDescription = "物品描述";
                         RequiredLevel = "需要等级 : {0}";
                         RequiredDC = "需要攻击 : {0}";
                         RequiredMC = "需要魔法 : {0}";
                         RequiredSC = "需要道术 : {0}";
                         ClassRequired = "需要职业 : {0}";

                         Holy = "神圣: + {0} (+{1})";
                         Holy2 = "神圣: + {0}";
                         Accuracy = "准确: + {0} (+{1})";
                         Accuracy2 = "准确: + {0}";
                         Agility = "敏捷: + {0} (+{1})";
                         Agility2 = "敏捷: + {0}";
                         DC = "攻击 + {0}~{1} (+{2})";
                         DC2 = "攻击 + {0}~{1}";
                         MC = "魔法 + {0}~{1} (+{2})";
                         MC2 = "魔法 + {0}~{1}";
                         SC = "道术 + {0}~{1} (+{2})";
                         SC2 = "道术 + {0}~{1}";
                         Durability = "持久度";
                         Weight = "重:";
                         AC = "防御 + {0}~{1} (+{2})";
                         AC2 = "防御 + {0}~{1}";
                         MAC = "魔防 + {0}~{1} (+{2})";
                         MAC2 = "魔防 + {0}~{1}";
                         Luck = "幸运 + {0}";

                         DeleteCharacter = "你确定要删除人物 {0} 吗";
                         CharacterDeleted = "你的人物删除成功.";
                         CharacterCreated = "你的人物创建成功.";

                         Resolution = "分辨率";
                         Autostart = "自动启动";
                         Usrname = "账号";
                         Password = "密码";

                         ShuttingDown = "连接断开: 服务器已关闭.";
                         MaxCombine = "最大堆叠数量 : {0}{1}Shift + 左键拆分";
                         Count = " 数量 {0}";
                         ExtraSlots8 = "你确定用1;000;000金币购买8个额外的格子吗?" +
                         "下次你可以购买4个额外的格子达到最大40个格子的上限.";
                         ExtraSlots4 = "你确定用: {0:###;###} 金币购买4个额外的格子吗?";

                         Chat_All = "全部";
                         Chat_Short = "喊话";
                         Chat_Whisper = "私聊";
                         Chat_Lover = "伴侣";
                         Chat_Mentor = "师徒";
                         Chat_Group = "组队";
                         Chat_Guild = "行会";
                         ExpandedStorageLocked = "扩展的仓库空间被锁定";
                         ExtraStorage = "你愿意花费 1;000;000 金币租用10天额外的仓库空间吗?";
                         ExtendYourRentalPeriod = "你愿意花费 1;000;000 金币延长你的租期10天吗?";

                         CannotLeaveGame = "{0}秒内无法离开游戏";
                         SelectKey = "请选择按键: {0}";

                         WeaponSpiritFire = "你的武器因精神火球而炙热.";
                         SpiritsFireDisappeared = "精神火球消失了.";
                         WeddingRing = "婚戒";
                         ItemTextFormat = "{0}{1}{2} {3}";
                         DropAmount = "丢弃数量:";
                         LowMana = "没有足够的法力来释放.";
                         NoCreatures = "你还没有下属.";
                         NoMount = "你还没有坐骑.";
                         NoFishingRod = "你没有装备钓鱼竿.";
                         AttemptingConnect = "尝试连接服务器.{0}尝试:{1}";

                         CreatingCharactersDisabled = "当前禁止创建人物.";
                         InvalidCharacterName = "你的人物名字不符合要求.";
                         NoClass = "你选择的职业不存在.";
                         ToManyCharacters = "你无法创建多于 {0} 的人物.";
                         CharacterNameExists = "人物名字已经存在.";
                         WarriorsDes = "Warriors are a class of great strength and vitality. They are not easily killed in battle and have the advantage of being able to use" +
                                        " a variety of heavy weapons and Armour. Therefore; Warriors favor attacks that are based on melee physical damage. They are weak in ranged" +
                                        " attacks; however the variety of equipment that are developed specifically for Warriors complement their weakness in ranged combat.";
                         WizardDes = "Wizards are a class of low strength and stamina; but have the ability to use powerful spells. Their offensive spells are very effective; but" +
                                        " because it takes time to cast these spells; they're likely to leave themselves open for enemy's attacks. Therefore; the physically weak wizards" +
                                        " must aim to attack their enemies from a safe distance.";
                         TaoistDes = "Taoists are well disciplined in the study of Astronomy; Medicine; and others aside from Mu-Gong. Rather then directly engaging the enemies; their" +
                                        " specialty lies in assisting their allies with support. Taoists can summon powerful creatures and have a high resistance to magic; and is a class" +
                                        " with well balanced offensive and defensive abilities.";
                         AssassinDes = "Assassins are members of a secret organization and their history is relatively unknown. They're capable of hiding themselves and performing attacks" +
                                        " while being unseen by others; which naturally makes them excellent at making fast kills. It is necessary for them to avoid being in battles with" +
                                        " multiple enemies due to their weak vitality and strength.";
                         ArcherDes = "Archers are a class of great accuracy and strength; using their powerful skills with bows to deal extraordinary damage from range. Much like" +
                                        " wizards; they rely on their keen instincts to dodge oncoming attacks as they tend to leave themselves open to frontal attacks. However; their" +
                                        " physical prowess and deadly aim allows them to instil fear into anyone they hit.";
                         DateSent = "发送日期 : {0}";
                         Send = "发送";
                         Reply = "回复";
                         Read = "阅读";
                         Delete = "删除";
                         BlockList = "黑名单";
                         EnterMailToName = "请输入要发送邮件的玩家名字.";
                         AddFriend = "添加";
                         RemoveFriend = "移除";
                         FriendMemo = "备注";
                         FriendMail = "邮件";
                         FriendWhisper = "私聊";
                         FriendEnterAddName = "请输入要添加的玩家名字.";
                         FriendEnterBlockName = "请输入要屏蔽的玩家名字.";
                         AddMentor = "添加师徒";
                         RemoveMentorMentee = "移除师徒";
                         MentorRequests = "允许/禁止师徒请求";
                         MentorEnterName = "请输入你师傅的名字.";
                         RestedBuff = "充分休息{0}增加经验百分比 {1}%{2}";

                         ItemTypeWeapon = "武器";
                         ItemTypeArmour = "盔甲";
                         ItemTypeHelmet = "头盔";
                         ItemTypeNecklace = "项链";
                         ItemTypeBracelet = "手镯";
                         ItemTypeRing = "戒指";
                         ItemTypeAmulet = "护身符";
                         ItemTypeBelt = "腰带";
                         ItemTypeBoots = "鞋子";
                         ItemTypeStone = "石头";
                         ItemTypeTorch = "火把";
                         ItemTypePotion = "药剂";
                         ItemTypeOre = "矿石";
                         ItemTypeMeat = "肉";
                         ItemTypeCraftingMaterial = "手工材料";
                         ItemTypeScroll = "卷轴";
                         ItemTypeGem = "珠宝";
                         ItemTypeMount = "坐骑";
                         ItemTypeBook = "书籍";
                         ItemTypeScript = "Script";
                         ItemTypeReins = "Reins";
                         ItemTypeBells = "Bells";
                         ItemTypeSaddle = "马鞍";
                         ItemTypeRibbon = "丝带";
                         ItemTypeMask = "面具";
                         ItemTypeFood = "食物";
                         ItemTypeHook = "钩子";
                         ItemTypeFloat = "Float";
                         ItemTypeBait = "Bait";
                         ItemTypeFinder = "Finder";
                         ItemTypeReel = "Reel";
                         ItemTypeFish = "Fish";
                         ItemTypeQuest = "任务";
                         ItemTypeAwakening = "觉醒";
                         ItemTypePets = "宠物";
                         ItemTypeTransform = "变形";
                         ItemTypeDeco = "Deco";
                         ItemTypeMonsterSpawn = "SpawnEgg";
                         ItemTypeSealedHero = "封印的英雄";

                         ItemGradeCommon = "普通";
                         ItemGradeRare = "稀有";
                         ItemGradeLegendary = "传说";
                         ItemGradeMythical = "神话";
                         ItemGradeHeroic = "英雄";
                         NoAccountID = "该账号不存在.";
                         IncorrectPasswordAccountID = "账号或者密码不正确.";
                         GroupSwitch = "允许/禁止组队请求";
                         GroupAdd = "添加";
                         GroupRemove = "移除";
                         GroupAddEnterName = "请输入要添加的玩家名字.";
                         GroupRemoveEnterName = "请输入要移除的玩家名字.";
                         TooHeavyToHold = "太重了无法拿起.";
                         SwitchMarriage = "允许/禁止结婚";
                         RequestMarriage = "请求结婚";
                         RequestDivorce = "请求离婚";
                         MailLover = "伴侣邮件";
                         WhisperLover = "伴侣私聊";
        EnterDelName = "请输入人物名字";
        IncorrectEnter = "错误的输入";
        DeletingCharactersDisabled = "当前禁止删除人物";
        StartGameDelay = "你需要等待 {0} 秒才可以登录此人物";
        StartingGameDisabled = "当前禁止开始游戏";
        SuitableWeaponSkill = "你必须装备合适的武器来施放这个技能";
        CastDelay = "施放技能 {0} 需要等待 {1} 秒";
        ThrustingOn = "开启刺杀剑术";
        ThrustingOff = "关闭刺杀剑术";
        HalfMoonOn = "开启半月弯刀";
        HalfMoonOff = "关闭半月弯刀";
        CrossHalfMoonOn = "开启圆月弯刀";
        CrossHalfMoonOff = "关闭圆月弯刀";
        NothingFound = "什么都没有发现";
        ShareQuest = "{0} 想要共享一个任务给你，你接受吗";
        ObservingLoggedOff = "你观察的玩家已经下线";
        DuraZero1 = "{0} 不再为你效力";
        DuraZero2 = "{0} 的持久度降到了0";
        EnterRequiredInformation = "请输入需要的信息";
        LeftGroup = "你离开了队伍";
        GroupDel = "-{0} 离开了队伍";
        GroupInvite = "你要与 {0} 组队吗";
        GroupAddMember = "-{0} 加入了队伍";
        MarketFail0 = "在死亡的时候无法使用市场";
        MarketFail2 = "该物品已经售出";
        MarketFail3 = "该物品已过期，无法购买";
        MarketFail4 = "你没有足够的重量或空间来购买此物品";
        MarketFail5 = "你无法购买你自己的物品";
        MarketFail6 = "你离商人太远了";
        MarketFail7 = "你的金币超出上限";
        MarketFail8 = "该物品不满足拍卖的最低要求";
        MarketFail9 = "该物品的拍卖已结束";
        JoinGuild = "你要加入行会 {0} 吗";
        GuildNameRequest1 = "请输入行会名字，长度必须是3~20个字符";
        GuildRequestWar = "请输入要发动行会战争的行会名字";
        GuildMemberChange1 = "{0} 上线了.";
        GuildMemberChange2 = "{0} 加入了行会.";
        GuildMemberChange3 = "{0} 被移出了行会.";
        GuildMemberChange4 = "{0} 离开了行会.";
        GuildStorageGoldChange1 = "{0} 捐赠了 {1} 金币到行会基金.";
        GuildStorageGoldChange2 = "{0} 取出了 {1} 金币从行会基金.";
        NewHero1 = "当前禁止创建新的英雄";
        NewHero2 = "你的英雄名字不符合要求";
        NewHero3 = "你无法创建更多英雄";
        NewHero4 = "相同的英雄名字已经存在";
        NewHero5 = "没有足够的背包空间";
        NewHero6 = "英雄创建成功";
        MarriageRequest = "{0} 请求与你结婚";
        DivorceRequest = "{0} 请求与你离婚";
        MentorRequest = "{0} (等级 {1}) 请求你传授他 {2} 之道.";
        TradeRequest = "玩家 {0} 请求与你交易";
        TradeCancel = "交易取消.\r\n必须与对方面对面才可以交易";
        Awakening1 = "你没有提供足够的材料";
        Awakening2 = "觉醒已达到最高等级";
        Awakening3 = "该物品无法觉醒";
        ParcelCollected1 = "没有包裹可接收";
        ParcelCollected2 = "所有包裹接收完成";
        RequestReincarnation = "你希望被救活吗?";
        NewIntelligentCreature = "请给你的下属命名";
        NameInfoLabel1 = " 数量 {0}/{1}";
        NameInfoLabel2 = " 纯度 {0}";
        NameInfoLabel3 = " 品质 {0}";
        NameInfoLabel4 = " 忠诚度 {0} / {1}";
        NameInfoLabel5 = " 营养 {0}";
        NameInfoLabel6 = " 持久度 {0}";
        AttackInfoLabel1 = "增加 +{0} 持久度";
        AttackInfoLabel2 = "Seals for {0}";
        AttackInfoLabel3 = "增加 +{0} 攻击";
        AttackInfoLabel4 = "增加 +{0} 魔法";
        AttackInfoLabel5 = "增加 +{0} 道术";
        AttackInfoLabel6 = "背包重量 + {0}% ";
        AttackInfoLabel7 = "经验 + {0}% ";
        AttackInfoLabel8 = "掉率 + {0}% ";
        AttackInfoLabel9 = "诅咒 + {0}";
        AttackInfoLabel10 = "增加 +{0} 准确";
        AttackInfoLabel11 = "攻击速度: ";
        AttackInfoLabel12 = "增加 +{0} 攻击速度";
        AttackInfoLabel13 = "冰冻几率: + {0}";
        AttackInfoLabel14 = "增加 +{0} 冰冻几率";
        AttackInfoLabel15 = "中毒几率: + {0}";
        AttackInfoLabel16 = "增加 +{0} 中毒几率";
        AttackInfoLabel17 = "暴击几率: + {0}";
        AttackInfoLabel18 = "韧性: + {0}";
        AttackInfoLabel19 = "暴击伤害: + {0}";
        AttackInfoLabel20 = "反弹几率: {0}";
        AttackInfoLabel21 = "吸血比例: {0}%";
        AttackInfoLabel22 = "经验倍率: ";
        AttackInfoLabel23 = "掉率: ";
        AttackInfoLabel24 = "金币倍率: ";
        DefenceInfoLabel1 = "增加 +{0} 防御";
        DefenceInfoLabel2 = "咬钩几率 + ";
        DefenceInfoLabel3 = "发现几率 + ";
        DefenceInfoLabel4 = "成功几率 + ";
        DefenceInfoLabel5 = "增加 +{0} 魔防";
        DefenceInfoLabel6 = "自动收杆几率 + {0}%";
        DefenceInfoLabel7 = "最大生命值 + {0}";
        DefenceInfoLabel8 = "最大法力值 + {0}";
        DefenceInfoLabel9 = "最大生命值 + {0}%";
        DefenceInfoLabel10 = "最大法力值 + {0}%";
        DefenceInfoLabel11 = "最大防御 + {0}%";
        DefenceInfoLabel12 = "最大魔防 + {0}%";
        DefenceInfoLabel13 = "生命恢复 + {0}";
        DefenceInfoLabel14 = "法力恢复 + {0}";
        DefenceInfoLabel15 = "中毒恢复 + {0}";
        DefenceInfoLabel16 = "增加 +{0} 敏捷";
        DefenceInfoLabel17 = "强度 + {0}";
        DefenceInfoLabel18 = "毒物躲避 + {0}";
        DefenceInfoLabel19 = "增加 +{0} 毒物躲避";
        DefenceInfoLabel20 = "魔法躲避 + {0}";
        DefenceInfoLabel21 = "增加 +{0} 魔法躲避";
        DefenceInfoLabel22 = "最大攻击 + {0}%";
        DefenceInfoLabel23 = "最大魔法 + {0}%";
        DefenceInfoLabel24 = "最大道术 + {0}%";
        DefenceInfoLabel25 = "所有伤害减少 + {0}%";
        WeightInfoLabel1 = "腕力 + {0}";
        WeightInfoLabel2 = "穿戴重量 + {0}";
        WeightInfoLabel3 = "背包重量 + {0}";
        WeightInfoLabel4 = "免助跑";
        WeightInfoLabel5 = "时间 : {0}";
        AwakeInfoLabel1 = "{0} 觉醒({1})";
        AwakeInfoLabel2 = "最大 {0} + {1}";
        AwakeInfoLabel3 = "等级 {0} : ";
        AwakeInfoLabel4 = "最大";
        SocketInfoLabel1 = "插槽 : {0}";
        SocketInfoLabel2 = "空";
        SocketInfoLabel3 = "Ctrl + 右键打开插槽";
        NeedInfoLabel1 = "需求防御 : {0}";
        NeedInfoLabel2 = "需求魔防 : {0}";
        NeedInfoLabel3 = "最高等级 : {0}";
        NeedInfoLabel4 = "需求基础防御 : {0}";
        NeedInfoLabel5 = "需求基础魔防 : {0}";
        NeedInfoLabel6 = "需求基础攻击 : {0}";
        NeedInfoLabel7 = "需求基础魔法 : {0}";
        NeedInfoLabel8 = "需求基础道术 : {0}";
        NeedInfoLabel9 = "未知的需求类型";
        NeedInfoLabel10 = "售价 : {0} 金币";
        BindInfoLabel1 = "死亡不掉落";
        BindInfoLabel2 = "无法丢弃";
        BindInfoLabel3 = "无法升级";
        BindInfoLabel4 = "无法出售";
        BindInfoLabel5 = "无法交易";
        BindInfoLabel6 = "无法存仓库";
        BindInfoLabel7 = "无法修理";
        BindInfoLabel8 = "无法特修";
        BindInfoLabel9 = "死亡消失";
        BindInfoLabel10 = "掉落消失";
        BindInfoLabel11 = "无法作为婚戒";
        BindInfoLabel12 = "英雄无法使用";
        BindInfoLabel13 = "装备绑定";
        BindInfoLabel14 = "已绑定至: ";
        BindInfoLabel15 = "被诅咒";
        BindInfoLabel16 = "无法被用于任何物品";
        BindInfoLabel17 = "可以被用于: ";
        BindInfoLabel18 = "到期剩余 {0}";
        BindInfoLabel19 = "已到期";
        BindInfoLabel20 = "封印剩余 {0}";
        BindInfoLabel21 = "物品出租者: ";
        BindInfoLabel22 = "租约剩余: {0}";
        BindInfoLabel23 = "租约到期";
        BindInfoLabel24 = "租约锁定剩余: {0}";
        BindInfoLabel25 = "租约锁定已到期";
        OverlapInfoLabel1 = "按住 CTRL 和 左键 部分修理\n武器和首饰";
        OverlapInfoLabel2 = "按住 CTRL 和 左键 部分修理\n盔甲和防具";
        OverlapInfoLabel3 = "按住 CTRL 和 左键 与其他物品合并\n有几率摧毁合并物品";
        OverlapInfoLabel4 = "按住 CTRL 和 左键 与其他物品合并\n不会摧毁合并物品";
        OverlapInfoLabel5 = "按住 CTRL 和 左键 全部修理\nn武器和首饰";
        OverlapInfoLabel6 = "按住 CTRL 和 左键 全部修理\nn盔甲和防具";
        OverlapInfoLabel7 = "按住 CTRL 和 左键 封印物品";
        StoryInfoLabel1 = "增加 {0} 元宝到你的账号";
        GMMadeLabel1 = "由GM制造";
        Old = "[旧]";
        New = "[新]";
        Unknown = "未知的";
        CancelItemRental = "物品出租取消\r\n物品出租需要与其他玩家面对面站立";
        AutoRun1 = "[自动跑步: 开]";
        AutoRun2 = "[自动跑步: 关]";
        CannotDrop = "你无法丢弃 {0}";
        TargetFar = "目标太远";
        NeedBow = "你必须装备一把弓来施放这个技能";
        SearchNPC = "查找NPC";
        TeleportToNPC1 = "花费 {0} 金币传送到这个NPC?";
        NoPath = "没有找到合适的路径";
        DoubleSlashOn = "开启双刀";
        DoubleSlashOff = "关闭双刀";
        BuffTypeHiding = "对很多怪物隐形\n";
        BuffTypeMoonLight = "在一定距离内对玩家\n和很多怪物隐形\n";
        BuffTypeEnergyShield = "被攻击时 {0}% 几率恢复 {1} 生命值\n";
        BuffTypeDarkBody = "对很多怪物隐形并且可以移动\n";
        BuffTypeVampireShot = "赋予你吸血鬼的能力\n这种能力在你施放某些技能\n的时候会释放出来\n";
        BuffTypePoisonShot = "赋予你使用毒药的能力\n这种能力在你施放某些技能\n的时候会释放出来\n";
        BuffTypeConcentration = "增加元素提取的几率\n";
        BuffTypeMagicBooster = "增加魔法: {0}-{1}.\n增加法力消耗 {2}%.\n";
        BuffTypeTransform = "改变你的外观\n";
        BuffTypeMentee = "2倍速度增加技能点\n";
        BuffTypeBlindness = "减少视野范围\n";
        BuffTypeNewbie = "为你的公会成员提供激励\n";
        Decreases = "减少";
        Increases = "增加";
        Caster = "\n施放者: {0}";
        ActiveBuffs = "生效的 Buffs\n";
        Seconds = "秒";
        PoisonTypeGreen = "受到 {0} 伤害每 {1} {2}.\n";
        PoisonTypeRed = "减少防御比率 10% 每 {0} {1}.\n";
        PoisonTypeSlow = "减少移动速度\n";
        PoisonTypeFrozen = "无法施法，移动\n和攻击\n";
        PoisonTypeStun = "增加受到的伤害 20% 每 {0} {1}.\n";
        PoisonTypeParalysis = "无法移动和攻击\n";
        PoisonTypeDelayedExplosion = "一定时间后爆炸\n";
        PoisonTypeBleeding = "受到 {0} 伤害每 {1} {2}.\n";
        PoisonTypeLRParalysis = "无法移动和攻击\n受到攻击取消\n";
        PoisonTypeBlindness = "导致暂时致盲\n";
        PoisonTypeDazed = "无法攻击\n";
        ActivePoisons = "生效的毒药\n";
        RemoveFriendConfirm = "你确定要移除 '{0}' 吗?";
        NotOnline = "玩家不在线";
        ShowAll = "显示所有";
        BuyWithGold = "用金币购买";
        BuyWithCredits = "用元宝购买";
        AddMember1 = "你的队伍人数已达上限";
        AddMember2 = "你不是队长";
        ShowOffline = "显示离线";
        StatusHeaders = "行会名\n\n等级\n\n成员";
        RecruitMember = "招募成员";
        EditRank = "编辑头衔";
        SelectRank = "选择头衔";
        KickMember = "移除成员";
        StoreItem = "存储物品";
        RetrieveItem = "取出物品";
        AlterAlliance = "变更同盟";
        ChangeNotice = "变更公告";
        ActivateBuff = "激活Buff";
        RequestBuffGuild1 = "没有足够的点数";
        RequestBuffGuild2 = "行会等级不够";
        RequestBuffGuild3 = "你没有足够的权限";
        RequestBuffGuild4 = "已经有该Buff";
        RequestBuffGuild5 = "没有足够的行会基金";
        Available = "可用";
        RefreshInterface1 = "没有足够的等级";
        CountingDown = "倒计时";
        Obtained = "已获得";
        Active = "已生效";
        Inactive = "未生效";
        HintLabel1 = "最小行会等级: ";
        HintLabel2 = "需要点数: ";
        HintLabel3 = "激活费用: {0} 金币";
        HintLabel4 = "剩余时间: {0} 分钟";
        HintLabel5 = "Buff持续时间: {0} 分钟";
        MissingRank = "无头衔";
        AddNew = "新建";
        OnNewRank = "你确定要把 {0} 的头衔改为 {1} 吗?";
        DeleteMember = "你确定要把 {0} 移出行会吗?";
        Online = "在线";
        Today = "今天";
        Yesterday = "昨天";
        DaysAgo = "几天前";
        CreateRank = "你确定要创建一个新的头衔吗?";
        Rank = "头衔";
        Deposit = "存款";
        StorageRemoveGold = "要取出的金币:";
        HelpPage1 = "快捷键信息";
        HelpPage2 = "聊天快捷键";
        HelpPage3 = "移动";
        HelpPage4 = "攻击";
        HelpPage5 = "收集物品";
        HelpPage6 = "生命值";
        HelpPage7 = "技能";
        HelpPage8 = "法力值";
        HelpPage9 = "聊天";
        HelpPage10 = "队伍";
        HelpPage11 = "购买";
        HelpPage12 = "出售";
        HelpPage13 = "修理";
        HelpPage14 = "交易";
        HelpPage15 = "查看";
        HelpPage16 = "统计";
        HelpPage17 = "任务";
        HelpPage18 = "坐骑";
        HelpPage19 = "钓鱼";
        HelpPage20 = "珠宝";
        HelpPage21 = "英雄";
        HelpPage22 = "行会Buffs";
        ShortcutPage1_1 = "关闭游戏";
        ShortcutPage1_2 = "退出游戏";
        ShortcutPage1_3 = "技能按钮";
        ShortcutPage1_4 = "背包窗口 (打开 / 关闭)";
        ShortcutPage1_5 = "状态窗口 (打开 / 关闭)";
        ShortcutPage1_6 = "技能窗口 (打开 / 关闭)";
        ShortcutPage1_7 = "组队窗口 (打开 / 关闭)";
        ShortcutPage1_8 = "交易窗口 (打开 / 关闭)";
        ShortcutPage1_9 = "好友窗口 (打开 / 关闭)";
        ShortcutPage1_10 = "小地图窗口 (打开 / 关闭)";
        ShortcutPage1_11 = "行会窗口 (打开 / 关闭)";
        ShortcutPage1_12 = "商城窗口 (打开 / 关闭)";
        ShortcutPage1_13 = "结婚窗口 (打开 / 关闭)";
        ShortcutPage1_14 = "腰带窗口 (打开 / 关闭)";
        ShortcutPage1_15 = "选项窗口 (打开 / 关闭)";
        ShortcutPage1_16 = "帮助窗口 (打开 / 关闭)";
        ShortcutPage1_17 = "上马 / 下马";
        ShortcutPage1_18 = "锁定技能到非指针目标";
        ShortcutPage2_1 = "切换下属攻击模式";
        ShortcutPage2_2 = "切换玩家攻击模式";
        ShortcutPage2_3 = "和平模式 - 只能攻击怪物";
        ShortcutPage2_4 = "组队模式 - 可以攻击队友以外的目标";
        ShortcutPage2_5 = "行会模式 - 可以攻击行会成员以外的目标";
        ShortcutPage2_6 = "善恶模式 - 只能攻击红名玩家和怪物";
        ShortcutPage2_7 = "全体模式 - 可以攻击所有目标";
        ShortcutPage2_8 = "显示大地图";
        ShortcutPage2_9 = "显示技能栏";
        ShortcutPage2_10 = "自动跑步 开启 / 关闭";
        ShortcutPage2_11 = "显示 / 隐藏 界面";
        ShortcutPage2_12 = "高亮 / 拾取 物品";
        ShortcutPage2_13 = "Ctrl + 右键";
        ShortcutPage2_14 = "查看其他玩家";
        ShortcutPage2_15 = "截屏";
        ShortcutPage2_16 = "打开 / 关闭 钓鱼窗口";
        ShortcutPage2_17 = "师徒窗口 (打开 / 关闭)";
        ShortcutPage2_18 = "下属拾取 (多目标)";
        ShortcutPage2_19 = "下属拾取 (单目标)";
        ShortcutPage3_1 = "/(玩家)";
        ShortcutPage3_2 = "与其他玩家私聊";
        ShortcutPage3_3 = "!(文字)";
        ShortcutPage3_4 = "对附近的其他玩家喊话";
        ShortcutPage3_5 = "!~(文字)";
        ShortcutPage3_6 = "与行会成员聊天";
        ShortcutInfoPage1 = "快捷键";
        ShortcutInfoPage2 = "信息";
        EnterValue = "输入一个数值";
        HeroBehaviour = "英雄行为: ";
        ActiveHero = "你希望将 {0} 作为活动的英雄吗?";
        CreatureName1 = "请为你的下属输入新的名字";
        NameLength = "下属名字长度必须在 {0} 和 {1} 个字符之间";
        CreatureName2 = "请输入下属的名字来验证";
        VerificationFailed = "验证失败!!";
        Auto = "自动/";
        SemiAuto = "半自动";
        PickupItems = "可以拾取物品 ({0}{1}).";
        ProduceBlackStones = "可以制造黑石";
        ProducePearls = "可以制造珍珠, 用于购买下属物品";
        FoodBuff = "食物Buff: {0}";
        Others = "其他";
        RentalFee = "租借费用:";
        RentDuration = "你打算将 {0} 租借给 {1} 多长时间? (1 到 30 天).";
        RentalPeriod = "租借时长: {0} 天";
        KeyboardSettings = "按键设置";
        KeyboardSettingsReset = "按键设置已经被重置";
        AssignRuleStrict = "分配规则: 严格";
        AssignRuleRelaxed = "分配规则: 宽松";
        TYPE = "类型";
        SENDER = "发件人";
        MESSAGE = "内容";
        MailDelete = "这个邮件包含物品或金币, 你确定要删除吗?";
        SendAmount = "发送数量:";
        SkillMode1 = "[技能模式: ~]";
        SkillMode2 = "[技能模式: Ctrl]";
        SkillHint = "{0}\nMP: {1}\n冷却时间: {2}\n按键: {3}";
        MiniMapKey = "小地图 ({0})";
        InviteToGroup = "组队邀请";
        FriendButton = "添加到好友列表";
        Trade2 = "交易";
        ObserveButton = "观察";
        HPMode1 = "[HP/MP 模式 1]";
        HPMode2 = "[HP/MP 模式 2]";
        MovementStyle1 = "[新移动方式]";
        MovementStyle2 = "[旧移动方式]";
        Keyboard = "按键 ({0})";
        Fencing = "基本剑术\n\n被动技能\n\n随着练习等级增加准确\n\n当前技能等级 {0}\n下一级 {1}";
        Slaying = "攻杀剑术\n\n被动技能\n\n随着练习等级增加准确和攻击\n\n当前技能等级 {0}\n下一级 {1}";
        Thrusting = "刺杀剑术\n\n开关技能\n\n随着练习等级增加攻击和距离\n\n当前技能等级 {0}\n下一级 {1}";
        Rage = "愤怒\n\nBuff技能\n法力消耗: {2}\n\n在一定时间内增加你的攻击\n攻击和持续时间随着练习等级增加\n\n当前技能等级 {0}\n下一级 {1}";
        ProtectionField = "保护区\n\nBuff技能\n法力消耗: {2}\n\n在一定时间内增加你的防御\n防御和持续时间随着练习等级增加\n\n当前技能等级 {0}\n下一级 {1}";
        HalfMoon = "半月弯刀\n\n开关技能\n法力消耗: {2} 每次攻击\n\n挥动武器对周围半圆形范围内的敌人造成伤害\n\n当前技能等级 {0}\n下一级 {1}";
        FlamingSword = "烈火剑法\n\n主动技能\n法力消耗: {2}\n\n召唤火焰精灵，使你的下一次攻击对目标造成毁灭性打击\n\n当前技能等级 {0}\n下一级 {1}";
        ShoulderDash = "野蛮冲撞\n\n主动技能\n法力消耗: {2}\n\n战士可以用肩膀攻击目标，将其向后推\n如果目标碰到任何障碍物，就会造成伤害\n\n当前技能等级 {0}\n下一级 {1}";
        CrossHalfMoon = "圆月弯刀\n\n开关技能\n法力消耗: {2} 每次攻击\n\n战士使用两股强大的半月波\n对站在他周围的所有目标造成伤害\n\n当前技能等级 {0}\n下一级 {1}";
        TwinDrakeBlade = "双龙斩\n\n主动技能\n法力消耗: {2}\n\n多倍攻击的艺术\n有较低的几率使目标昏迷\n昏迷的怪物受到额外50%的伤害\n\n当前技能等级 {0}\n下一级 {1}";
        Entrapment = "擒龙手\n\n主动技能\n法力消耗: {2}\n\n麻痹怪物并把它拉向施放者\n\n当前技能等级 {0}\n下一级 {1}";
        LionRoar = "狮子吼\n\n主动技能\n法力消耗: {2}\n\n麻痹施放者周围的怪物，持续时间随着技能等级增加\n\n当前技能等级 {0}\n下一级 {1}";
        CounterAttack = "反击\n\nBuff技能\n法力消耗: {2}\n\n短时间内增加防御和魔防\n有几率防御一次攻击并反击\n\n当前技能等级 {0}\n下一级 {1}";
        ImmortalSkin = "不朽皮肤\n\nBuff技能\n法力消耗: {2}\n\n增加你的防御\n\n当前技能等级 {0}\n下一级 {1}";
        Fury = "狂怒\n\nBuff技能\n法力消耗: {2}\n\n在一段时间内增加你的准确\n\n当前技能等级 {0}\n下一级 {1}";
        SlashingBurst = "日闪\n\n主动技能\n法力消耗: {2}\n\n跳过一格的距离攻击目标或怪物\n\n当前技能等级 {0}\n下一级 {1}";
        BladeAvalanche = "破攻闪\n\n主动技能\n法力消耗: {2}\n\n发出三个方向的剑气攻击敌人\n\n当前技能等级 {0}\n下一级 {1}";
        FireBall = "火球术\n\n立即施放\n法力消耗: {2}\n\n将火元素聚集成一个火球攻击敌人\n\n当前技能等级 {0}\n下一级 {1}";
        ThunderBolt = "雷电术\n\n立即施放\n法力消耗: {2}\n\n用闪电击中敌人，造成高伤害\n\n当前技能等级 {0}\n下一级 {1}";
        GreatFireBall = "大火球\n\n立即施放\n法力消耗: {2}\n\n比火球术对敌人造成更大的伤害\n\n当前技能等级 {0}\n下一级 {1}";
        Repulsion = "抗拒火环\n\n立即施放\n法力消耗: {2}\n\n用火的力量推动周围的敌人\n\n当前技能等级 {0}\n下一级 {1}";
        HellFire = "地狱火\n\n立即施放\n法力消耗: {2}\n\n发射出一条火焰攻击前方敌人\n\n当前技能等级 {0}\n下一级 {1}";
        Lightning = "疾光电影\n\n立即施放\n法力消耗: {2}\n\n发射出一条雷电攻击前方敌人\n\n当前技能等级 {0}\n下一级 {1}";
        ElectricShock = "诱惑之光\n\n立即施放\n法力消耗: {2}\n\n发出强烈的冲击波击中怪物\n\n当前技能等级 {0}\n下一级 {1}";
        Teleport = "瞬息移动\n\n立即施放\n法力消耗: {2}\n\n传送到一个随机的位置\n\n当前技能等级 {0}\n下一级 {1}";
        FireWall = "火墙\n\n立即施放\n法力消耗: {2}\n\n在指定的位置制造一个火墙\n攻击经过该区域的敌人\n\n当前技能等级 {0}\n下一级 {1}";
        FireBang = "爆裂火焰\n\n立即施放\n法力消耗: {2}\n\n点燃指定区域\n烧毁该区域内的所有敌人\n\n当前技能等级 {0}\n下一级 {1}";
        ThunderStorm = "地狱雷光\n\n立即施放\n法力消耗: {2}\n\n在施法者周围制造雷电风暴\n伤害范围内所有不死系的敌人\n\n当前技能等级 {0}\n下一级 {1}";
        MagicShield = "魔法盾\n\n立即施放\n法力消耗: {2}\n\n在施法者周围制造一个吸收伤害的保护领域\n\n当前技能等级 {0}\n下一级 {1}";
        TurnUndead = "圣言术\n\n立即施放\n法力消耗: {2}\n\n有几率杀死符合等级要求的不死系怪物\n\n当前技能等级 {0}\n下一级 {1}";
        IceStorm = "冰咆哮\n\n立即施放\n法力消耗: {2}\n\n在指定区域制造一场冰风暴\n攻击区域内的敌人\n\n当前技能等级 {0}\n下一级 {1}";

                         //Server
                         Welcome = "欢迎来到 {0} 服务器.";
                         OnlinePlayers = "当前在线人数: {0}";
                         WeaponLuck = "你的武器幸运值增加了.";
                         WeaponCurse = "你的武器受到了诅咒.";
                         WeaponNoEffect = "没有效果.";
                         InventoryIncreased = "背包空间增加了.";
                         FaceToTrade = "你必须面对交易者.";
                         NoTownTeleport = "你无法在这里使用回城卷";
                         CanNotRandom = "你无法在这里使用随机卷";
                         CanNotDungeon = "你无法在这里使用地牢逃脱卷";
                         CannotResurrection = "在活着的时候无法使用复活卷";
                         CanNotDrop = "当前地图无法丢弃物品";
                         NewMail = "有新邮件到达.";
                         CouldNotFindPlayer = "无法找到玩家 {0}";
                         BeenPoisoned = "你中毒了";
                         AllowingMentorRequests = "你现在允许师徒请求了.";
                         BlockingMentorRequests = "你现在禁止师徒请求了.";

                         //common
                         LowLevel = "你的等级不够.";
                         LowGold = "金币不够.";
                         LevelUp = "恭喜你升级了！你的生命和法力已经恢复.";
                         LowDC = "你的攻击不够.";
                         LowMC = "你的魔法不够.";
                         LowSC = "你的道术不够.";
                         GameName = "Legend of Mir 2";
                         ExpandedStorageExpiresOn = "扩展仓库空间到期于";

                         NotFemale = "你不是女性.";
                         NotMale = "你不是男性.";
                         NotInGuild = "你没有加入行会.";
                         NoMentorship = "你当前不存在师徒关系.";
                         NoBagSpace = "背包空间不足.";
    }
}

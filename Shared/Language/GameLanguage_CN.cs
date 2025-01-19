using System.Reflection;

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

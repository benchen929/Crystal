public class GameLanguage
{
    public static GameLanguage Instance;

    //Client
    public string PetMode_Both = "[Pet: Attack and Move]",
                         PetMode_MoveOnly = "[Pet: Do Not Attack]",
                         PetMode_AttackOnly = "[Pet: Do Not Move]",
                         PetMode_None = "[Pet: Do Not Attack or Move]",
                         PetMode_FocusMasterTarget = "[Pet: Focus Master Target]",

                         AttackMode_Peace = "[Mode: Peaceful]",
                         AttackMode_Group = "[Mode: Group]",
                         AttackMode_Guild = "[Mode: Guild]",
                         AttackMode_EnemyGuild = "[Mode: Enemy Guild]",
                         AttackMode_RedBrown = "[Mode: Red/Brown]",
                         AttackMode_All = "[Mode: Attack All]",

                         LogOutTip = "Do you want to log out of Legend of Mir2?",
                         ExitTip = "Do you want to quit Legend of Mir2?",
                         DiedTip = "You have died, Do you want to revive in town?",
                         DropTip = "Are you sure you want to drop {0}?",

                         Inventory = "Inventory ({0})",
                         Character = "Character ({0})",
                         Skills = "Skills ({0})",
                         Quests = "Quests ({0})",
                         Options = "Options ({0})",
                         Menu = "Menu",
                         GameShop = "Game Shop ({0})",
                         BigMap = "BigMap ({0})",
                         DuraPanel = "Dura Panel",
                         Mail = "Mail",
                         Exit = "Exit ({0})",
                         LogOut = "Log Out ({0})",
                         Help = "Help ({0})",
                         Keybinds = "Keybinds",
                         Ranking = "Ranking ({0})",
                         Creatures = "Creatures ({0})",
                         Mount = "Mount ({0})",
                         Fishing = "Fishing ({0})",
                         Friends = "Friends ({0})",
                         Mentor = "Mentor ({0})",
                         Relationship = "Relationship ({0})",
                         Groups = "Groups ({0})",
                         Guild = "Guild ({0})",
                         Expire = "Expire: {0}",
                         ExpireNever = "Expire: Never",
                         ExpirePaused = "Expire: Paused",
                         Never = "Never",
                         Trade = "Trade ({0})",
                         Size = "Size",
                         ChatSettings = "Chat Settings",
                         Rotate = "Rotate",
                         Close = "Close ({0})",
                         GameMaster = "GameMaster",

                         PatchErr = "Could not get Patch Information",
                         LastOnline = "Last Online",

                         Gold = "Gold",
                         Credit = "Credit",

                         YouGained = "You gained {0}.",

                         YouGained2 = "You gained {0:###,###,###} {1}",

                         ExperienceGained = "Experience Gained {0}",

                         HeroInventory = "Hero Inventory ({0})",
                         HeroCharacter = "Hero Character ({0})",
                         HeroSkills = "Hero Skills ({0})",
                         HeroExperienceGained = "Hero Experience Gained {0}",

                         ItemDescription = "Item Description",
                         RequiredLevel = "Required Level : {0}",
                         RequiredDC = "Required DC : {0}",
                         RequiredMC = "Required MC : {0}",
                         RequiredSC = "Required SC : {0}",
                         ClassRequired = "Class Required : {0}",

                         Holy = "Holy: + {0} (+{1})",
                         Holy2 = "Holy: + {0}",
                         Accuracy = "Accuracy: + {0} (+{1})",
                         Accuracy2 = "Accuracy: + {0}",
                         Agility = "Agility: + {0} (+{1})",
                         Agility2 = "Agility: + {0}",
                         DC = "DC + {0}~{1} (+{2})",
                         DC2 = "DC + {0}~{1}",
                         MC = "MC + {0}~{1} (+{2})",
                         MC2 = "MC + {0}~{1}",
                         SC = "SC + {0}~{1} (+{2})",
                         SC2 = "SC + {0}~{1}",
                         Durability = "Durability",
                         Weight = "W:",
                         AC = "AC + {0}~{1} (+{2})",
                         AC2 = "AC + {0}~{1}",
                         MAC = "MAC + {0}~{1} (+{2})",
                         MAC2 = "MAC + {0}~{1}",
                         Luck = "Luck + {0}",

                         DeleteCharacter = "Are you sure you want to Delete the character {0}",
                         CharacterDeleted = "Your character was deleted successfully.",
                         CharacterCreated = "Your character was created successfully.",

                         Resolution = "Resolution",
                         Autostart = "Auto start",
                         Usrname = "Username",
                         Password = "Password",

                         ShuttingDown = "Disconnected: Server is shutting down.",
                         MaxCombine = "Max Combine Count : {0}{1}Shift + Left click to split the stack",
                         Count = " Count {0}",
                         ExtraSlots8 = "Are you sure you would like to buy 8 extra slots for 1,000,000 gold?" +
                         "Next purchase you can unlock 4 extra slots up to a maximum of 40 slots.",
                         ExtraSlots4 = "Are you sure you would like to unlock 4 extra slots? for gold: {0:###,###}",

                         Chat_All = "All",
                         Chat_Short = "Shout",
                         Chat_Whisper = "Whisper",
                         Chat_Lover = "Lover",
                         Chat_Mentor = "Mentor",
                         Chat_Group = "Group",
                         Chat_Guild = "Guild",
                         ExpandedStorageLocked = "Expanded Storage Locked",
                         ExtraStorage = "Would you like to rent extra storage for 10 days at a cost of 1,000,000 gold?",
                         ExtendYourRentalPeriod = "Would you like to extend your rental period for 10 days at a cost of 1,000,000 gold?",

                         CannotLeaveGame = "Cannot leave game for {0} seconds",
                         SelectKey = "Select the Key for: {0}",

                         WeaponSpiritFire = "Your weapon is glowed by spirit of fire.",
                         SpiritsFireDisappeared = "The spirits of fire disappeared.",
                         WeddingRing = "WeddingRing",
                         ItemTextFormat = "{0}{1}{2} {3}",
                         DropAmount = "Drop Amount:",
                         LowMana = "Not Enough Mana to cast.",
                         NoCreatures = "You do not own any creatures.",
                         NoMount = "You do not own a mount.",
                         NoFishingRod = "You are not holding a fishing rod.",
                         AttemptingConnect = "Attempting to connect to the server.{0}Attempt:{1}",

                         CreatingCharactersDisabled = "Creating new characters is currently disabled.",
                         InvalidCharacterName = "Your Character Name is not acceptable.",
                         NoClass = "The class you selected does not exist. Contact a GM for assistance.",
                         ToManyCharacters = "You cannot make anymore then {0} Characters.",
                         CharacterNameExists = "A Character with this name already exists.",
                         WarriorsDes = "Warriors are a class of great strength and vitality. They are not easily killed in battle and have the advantage of being able to use" +
                                        " a variety of heavy weapons and Armour. Therefore, Warriors favor attacks that are based on melee physical damage. They are weak in ranged" +
                                        " attacks, however the variety of equipment that are developed specifically for Warriors complement their weakness in ranged combat.",
                         WizardDes = "Wizards are a class of low strength and stamina, but have the ability to use powerful spells. Their offensive spells are very effective, but" +
                                        " because it takes time to cast these spells, they're likely to leave themselves open for enemy's attacks. Therefore, the physically weak wizards" +
                                        " must aim to attack their enemies from a safe distance.",
                         TaoistDes = "Taoists are well disciplined in the study of Astronomy, Medicine, and others aside from Mu-Gong. Rather then directly engaging the enemies, their" +
                                        " specialty lies in assisting their allies with support. Taoists can summon powerful creatures and have a high resistance to magic, and is a class" +
                                        " with well balanced offensive and defensive abilities.",
                         AssassinDes = "Assassins are members of a secret organization and their history is relatively unknown. They're capable of hiding themselves and performing attacks" +
                                        " while being unseen by others, which naturally makes them excellent at making fast kills. It is necessary for them to avoid being in battles with" +
                                        " multiple enemies due to their weak vitality and strength.",
                         ArcherDes = "Archers are a class of great accuracy and strength, using their powerful skills with bows to deal extraordinary damage from range. Much like" +
                                        " wizards, they rely on their keen instincts to dodge oncoming attacks as they tend to leave themselves open to frontal attacks. However, their" +
                                        " physical prowess and deadly aim allows them to instil fear into anyone they hit.",
                         DateSent = "Date Sent : {0}",
                         Send = "Send",
                         Reply = "Reply",
                         Read = "Read",
                         Delete = "Delete",
                         BlockList = "Block List",
                         EnterMailToName = "Please enter the name of the person you would like to mail.",
                         AddFriend = "Add",
                         RemoveFriend = "Remove",
                         FriendMemo = "Memo",
                         FriendMail = "Mail",
                         FriendWhisper = "Whisper",
                         FriendEnterAddName = "Please enter the name of the person you would like to Add.",
                         FriendEnterBlockName = "Please enter the name of the person you would like to Block.",
                         AddMentor = "Add Mentor",
                         RemoveMentorMentee = "Remove Mentor/Mentee",
                         MentorRequests = "Allow/Disallow Mentor Requests",
                         MentorEnterName = "Please enter the name of the person you would like to be your Mentor.",
                         RestedBuff = "Rested{0}Increases Exp Rate by {1}%{2}",

                         ItemTypeWeapon = "Weapon",
                         ItemTypeArmour = "Armour",
                         ItemTypeHelmet = "Helmet",
                         ItemTypeNecklace = "Necklace",
                         ItemTypeBracelet = "Bracelet",
                         ItemTypeRing = "Ring",
                         ItemTypeAmulet = "Amulet",
                         ItemTypeBelt = "Belt",
                         ItemTypeBoots = "Boots",
                         ItemTypeStone = "Stone",
                         ItemTypeTorch = "Torch",
                         ItemTypePotion = "Potion",
                         ItemTypeOre = "Ore",
                         ItemTypeMeat = "Meat",
                         ItemTypeCraftingMaterial = "CraftingMaterial",
                         ItemTypeScroll = "Scroll",
                         ItemTypeGem = "Gem",
                         ItemTypeMount = "Mount",
                         ItemTypeBook = "Book",
                         ItemTypeScript = "Script",
                         ItemTypeReins = "Reins",
                         ItemTypeBells = "Bells",
                         ItemTypeSaddle = "Saddle",
                         ItemTypeRibbon = "Ribbon",
                         ItemTypeMask = "Mask",
                         ItemTypeFood = "Food",
                         ItemTypeHook = "Hook",
                         ItemTypeFloat = "Float",
                         ItemTypeBait = "Bait",
                         ItemTypeFinder = "Finder",
                         ItemTypeReel = "Reel",
                         ItemTypeFish = "Fish",
                         ItemTypeQuest = "Quest",
                         ItemTypeAwakening = "Awakening",
                         ItemTypePets = "Pets",
                         ItemTypeTransform = "Transform",
                         ItemTypeDeco = "Deco",
                         ItemTypeMonsterSpawn = "SpawnEgg",
                         ItemTypeSealedHero = "SealedHero",

                         ItemGradeCommon = "Common",
                         ItemGradeRare = "Rare",
                         ItemGradeLegendary = "Legendary",
                         ItemGradeMythical = "Mythical",
                         ItemGradeHeroic = "Heroic",
                         NoAccountID = "The AccountID does not exist.",
                         IncorrectPasswordAccountID = "Incorrect Password and AccountID combination.",
                         GroupSwitch = "Allow/Disallow Group Requests",
                         GroupAdd = "Add",
                         GroupRemove = "Remove",
                         GroupAddEnterName = "Please enter the name of the person you wish to add.",
                         GroupRemoveEnterName = "Please enter the name of the person you wish to remove.",
                         TooHeavyToHold = "It is too heavy to Hold.",
                         SwitchMarriage = "Allow/Block Marriage",
                         RequestMarriage = "Request Marriage",
                         RequestDivorce = "Request Divorce",
                         MailLover = "Mail Lover",
                         WhisperLover = "Whisper Lover";

    //Server
    public string Welcome = "Welcome to the {0} Server.",
                         OnlinePlayers = "Online Players: {0}",
                         WeaponLuck = "Luck dwells within your weapon.",
                         WeaponCurse = "Curse dwells within your weapon.",
                         WeaponNoEffect = "No effect.",
                         InventoryIncreased = "Inventory size increased.",
                         FaceToTrade = "You must face someone to trade.",
                         NoTownTeleport = "You cannot use Town Teleports here",
                         CanNotRandom = "You cannot use Random Teleports here",
                         CanNotDungeon = "You cannot use Dungeon Escapes here",
                         CannotResurrection = "You cannot use Resurrection Scrolls whilst alive",
                         CanNotDrop = "You cannot drop items on this map",
                         NewMail = "New mail has arrived.",
                         CouldNotFindPlayer = "Could not find player {0}",
                         BeenPoisoned = "You have been poisoned",
                         AllowingMentorRequests = "You're now allowing mentor requests.",
                         BlockingMentorRequests = "You're now blocking mentor requests.";

    //common
    public string LowLevel = "You are not a high enough level.",
                         LowGold = "Not enough gold.",
                         LevelUp = "Congratulations! You have leveled up. Your HP and MP have been restored.",
                         LowDC = "You do not have enough DC.",
                         LowMC = "You do not have enough MC.",
                         LowSC = "You do not have enough SC.",
                         GameName = "Legend of Mir 2",
                         ExpandedStorageExpiresOn = "Expanded Storage Expires On",

                         NotFemale = "You are not Female.",
                         NotMale = "You are not Male.",
                         NotInGuild = "You are not in a guild.",
                         NoMentorship = "You don't currently have a Mentorship to cancel.",
                         NoBagSpace = "You do not have enough space.";


    public static void LoadClientLanguage(string language)
    {
        if (language == "English")
        {
            Instance = new GameLanguage_EN();
        }
        else
        {
            Instance = new GameLanguage_CN();
        }
    }


    public static void LoadServerLanguage(string language)
    {
        if (language == "English")
        {
            Instance = new GameLanguage_EN();
        }
        else
        {
            Instance = new GameLanguage_CN();
        }
    }
}

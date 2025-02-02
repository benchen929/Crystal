using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;

namespace Client.MirScenes.Dialogs
{
    public sealed class HelpDialog : MirImageControl
    {
        public List<HelpPage> Pages = new List<HelpPage>();

        public MirButton CloseButton, NextButton, PreviousButton;
        public MirLabel PageLabel;
        public HelpPage CurrentPage;

        public int CurrentPageNumber = 0;

        public HelpDialog()
        {
            Index = 920;
            Library = Libraries.Prguse;
            Movable = true;
            Sort = true;

            Location = Center;

            MirImageControl TitleLabel = new MirImageControl
            {
                Index = 57,
                Library = Libraries.Title,
                Location = new Point(18, 9),
                Parent = this
            };

            PreviousButton = new MirButton
            {
                Index = 240,
                HoverIndex = 241,
                PressedIndex = 242,
                Library = Libraries.Prguse2,
                Parent = this,
                Size = new Size(16, 16),
                Location = new Point(210, 485),
                Sound = SoundList.ButtonA,
            };
            PreviousButton.Click += (o, e) =>
            {
                CurrentPageNumber--;

                if (CurrentPageNumber < 0) CurrentPageNumber = Pages.Count - 1;

                DisplayPage(CurrentPageNumber);
            };

            NextButton = new MirButton
            {
                Index = 243,
                HoverIndex = 244,
                PressedIndex = 245,
                Library = Libraries.Prguse2,
                Parent = this,
                Size = new Size(16, 16),
                Location = new Point(310, 485),
                Sound = SoundList.ButtonA,
            };
            NextButton.Click += (o, e) =>
            {
                CurrentPageNumber++;

                if (CurrentPageNumber > Pages.Count - 1) CurrentPageNumber = 0;

                DisplayPage(CurrentPageNumber);
            };

            PageLabel = new MirLabel
            {
                Text = "",
                Font = new Font(Settings.FontName, 9F),
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                NotControl = true,
                Location = new Point(230, 480),
                Size = new Size(80, 20)
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(509, 3),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();

            LoadImagePages();

            DisplayPage();
        }

        private void LoadImagePages()
        {
            Point location = new Point(12, 35);

            Dictionary<string, string> keybinds = new Dictionary<string, string>();

            List<HelpPage> imagePages = new List<HelpPage> { 
                new HelpPage(GameLanguage.Instance.HelpPage1, -1, new ShortcutPage1 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage(GameLanguage.Instance.HelpPage1, -1, new ShortcutPage2 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage(GameLanguage.Instance.HelpPage2, -1, new ShortcutPage3 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage(GameLanguage.Instance.HelpPage3, 0, null) { Parent = this, Location = location, Visible = false }, 
                new HelpPage(GameLanguage.Instance.HelpPage4, 1, null) { Parent = this, Location = location, Visible = false }, 
                new HelpPage(GameLanguage.Instance.HelpPage5, 2, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage6, 3, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage7, 4, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage7, 5, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage8, 6, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage9, 7, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage10, 8, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.Durability, 9, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage11, 10, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage12, 11, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage13, 12, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage14, 13, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage15, 14, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage16, 15, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage16, 16, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage16, 17, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage16, 18, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage16, 19, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage16, 20, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage17, 21, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage17, 22, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage17, 23, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage17, 24, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage18, 25, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage18, 26, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage19, 27, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage20, 28, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage21, 29, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage21, 30, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage21, 31, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage21, 32, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage21, 33, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage22, 34, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage22, 35, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.HelpPage22, 36, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.ItemTypeAwakening, 37, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.ItemTypeAwakening, 38, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.ItemTypeAwakening, 39, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.ItemTypeAwakening, 40, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(GameLanguage.Instance.ItemTypeAwakening, 41, null) { Parent = this, Location = location, Visible = false },
            };

            Pages.AddRange(imagePages);
        }


        public void DisplayPage(string pageName)
        {
            if (Pages.Count < 1) return;

            for (int i = 0; i < Pages.Count; i++)
            {
                if (Pages[i].Title.ToLower() != pageName.ToLower()) continue;

                DisplayPage(i);
                break;
            }
        }

        public void DisplayPage(int id = 0)
        {
            if (Pages.Count < 1) return;

            if (id > Pages.Count - 1) id = Pages.Count - 1;
            if (id < 0) id = 0;

            if (CurrentPage != null)
            {
                CurrentPage.Visible = false;
                if (CurrentPage.Page != null) CurrentPage.Page.Visible = false;
            }

            CurrentPage = Pages[id];

            if (CurrentPage == null) return;

            CurrentPage.Visible = true;
            if (CurrentPage.Page != null) CurrentPage.Page.Visible = true;
            CurrentPageNumber = id;

            CurrentPage.PageTitleLabel.Text = id + 1 + ". " + CurrentPage.Title;

            PageLabel.Text = string.Format("{0} / {1}", id + 1, Pages.Count);

            Show();
        }


        public void Toggle()
        {
            if (!Visible)
                Show();
            else
                Hide();
        }
    }

    public class ShortcutPage1 : ShortcutInfoPage
    {
        public ShortcutPage1()
        {
            Shortcuts = new List<ShortcutInfo>
            {
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Exit), GameLanguage.Instance.ShortcutPage1_1),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Logout), GameLanguage.Instance.ShortcutPage1_2),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill1) + "-" + CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill8), GameLanguage.Instance.ShortcutPage1_3),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Inventory), GameLanguage.Instance.ShortcutPage1_4),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Equipment), GameLanguage.Instance.ShortcutPage1_5),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Skills), GameLanguage.Instance.ShortcutPage1_6),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Group), GameLanguage.Instance.ShortcutPage1_7),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Trade), GameLanguage.Instance.ShortcutPage1_8),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Friends), GameLanguage.Instance.ShortcutPage1_9),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Minimap), GameLanguage.Instance.ShortcutPage1_10),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Guilds), GameLanguage.Instance.ShortcutPage1_11),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.GameShop), GameLanguage.Instance.ShortcutPage1_12),
                //Shortcuts.Add(new ShortcutInfo("K", "Rental window (open / close)"));
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Relationship), GameLanguage.Instance.ShortcutPage1_13),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Belt), GameLanguage.Instance.ShortcutPage1_14),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Options), GameLanguage.Instance.ShortcutPage1_15),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Help), GameLanguage.Instance.ShortcutPage1_16),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Mount), GameLanguage.Instance.ShortcutPage1_17),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.TargetSpellLockOn), GameLanguage.Instance.ShortcutPage1_18)
            };

            LoadKeyBinds();
        }
    }
    public class ShortcutPage2 : ShortcutInfoPage
    {
        public ShortcutPage2()
        {
            Shortcuts = new List<ShortcutInfo>
            {
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.ChangePetmode), GameLanguage.Instance.ShortcutPage2_1),
                //Shortcuts.Add(new ShortcutInfo("Ctrl + F", "Change the font in the chat box"));
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.ChangeAttackmode), GameLanguage.Instance.ShortcutPage2_2),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodePeace), GameLanguage.Instance.ShortcutPage2_3),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeGroup), GameLanguage.Instance.ShortcutPage2_4),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeGuild), GameLanguage.Instance.ShortcutPage2_5),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeRedbrown), GameLanguage.Instance.ShortcutPage2_6),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeAll), GameLanguage.Instance.ShortcutPage2_7),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Bigmap), GameLanguage.Instance.ShortcutPage2_8),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Skillbar), GameLanguage.Instance.ShortcutPage2_9),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Autorun), GameLanguage.Instance.ShortcutPage2_10),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Cameramode), GameLanguage.Instance.ShortcutPage2_11),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Pickup), GameLanguage.Instance.ShortcutPage2_12),
                new ShortcutInfo(GameLanguage.Instance.ShortcutPage2_13, GameLanguage.Instance.ShortcutPage2_14),
                //Shortcuts.Add(new ShortcutInfo("F12", "Chat macros"));
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Screenshot), GameLanguage.Instance.ShortcutPage2_15),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Fishing), GameLanguage.Instance.ShortcutPage2_16),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Mentor), GameLanguage.Instance.ShortcutPage2_17),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.CreaturePickup), GameLanguage.Instance.ShortcutPage2_18),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.CreatureAutoPickup), GameLanguage.Instance.ShortcutPage2_19)
            };

            LoadKeyBinds();
        }
    }
    public class ShortcutPage3 : ShortcutInfoPage
    {
        public ShortcutPage3()
        {
            Shortcuts = new List<ShortcutInfo>
            {
                //Shortcuts.Add(new ShortcutInfo("` / Ctrl", "Change the skill bar"));
                new ShortcutInfo(GameLanguage.Instance.ShortcutPage3_1, GameLanguage.Instance.ShortcutPage3_2),
                new ShortcutInfo(GameLanguage.Instance.ShortcutPage3_3, GameLanguage.Instance.ShortcutPage3_4),
                new ShortcutInfo(GameLanguage.Instance.ShortcutPage3_5, GameLanguage.Instance.ShortcutPage3_6)
            };

            LoadKeyBinds();
        }
    }

    public class ShortcutInfo
    {
        public string Shortcut { get; set; }
        public string Information { get; set; }

        public ShortcutInfo(string shortcut, string info)
        {
            Shortcut = shortcut.Replace("\n", " + ");
            Information = info;
        }
    }

    public class ShortcutInfoPage : MirControl
    {
        protected List<ShortcutInfo> Shortcuts = new List<ShortcutInfo>();

        public ShortcutInfoPage()
        {
            Visible = false;

            MirLabel shortcutTitleLabel = new MirLabel
            {
                Text = GameLanguage.Instance.ShortcutInfoPage1,
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                ForeColour = Color.White,
                Font = new Font(Settings.FontName, 10F),
                Parent = this,
                AutoSize = true,
                Location = new Point(13, 75),
                Size = new Size(100, 30)
            };

            MirLabel infoTitleLabel = new MirLabel
            {
                Text = GameLanguage.Instance.ShortcutInfoPage2,
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                ForeColour = Color.White,
                Font = new Font(Settings.FontName, 10F),
                Parent = this,
                AutoSize = true,
                Location = new Point(114, 75),
                Size = new Size(405, 30)
            };
        }

        public void LoadKeyBinds()
        {
            if (Shortcuts == null) return;

            for (int i = 0; i < Shortcuts.Count; i++)
            {
                MirLabel shortcutLabel = new MirLabel
                {
                    Text = Shortcuts[i].Shortcut,
                    ForeColour = Color.Yellow,
                    DrawFormat = TextFormatFlags.VerticalCenter,
                    Font = new Font(Settings.FontName, 9F),
                    Parent = this,
                    AutoSize = true,
                    Location = new Point(18, 107 + (20 * i)),
                    Size = new Size(95, 23),
                };

                MirLabel informationLabel = new MirLabel
                {
                    Text = Shortcuts[i].Information,
                    DrawFormat = TextFormatFlags.VerticalCenter,
                    ForeColour = Color.White,
                    Font = new Font(Settings.FontName, 9F),
                    Parent = this,
                    AutoSize = true,
                    Location = new Point(119, 107 + (20 * i)),
                    Size = new Size(400, 23),
                };
            }  
        }
    }

    public class HelpPage : MirControl
    {
        public string Title;
        public int ImageID;
        public MirControl Page;

        public MirLabel PageTitleLabel;

        public HelpPage(string title, int imageID, MirControl page)
        {
            Title = title;
            ImageID = imageID;
            Page = page;

            NotControl = true;
            Size = new System.Drawing.Size(508, 396 + 40);

            BeforeDraw += HelpPage_BeforeDraw;

            PageTitleLabel = new MirLabel
            {
                Text = Title,
                Font = new Font(Settings.FontName, 10F, FontStyle.Bold),
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Size = new System.Drawing.Size(242, 30),
                Location = new Point(135, 4)
            };
        }

        void HelpPage_BeforeDraw(object sender, EventArgs e)
        {
            if (ImageID < 0) return;

            Libraries.Help.Draw(ImageID, new Point(DisplayLocation.X, DisplayLocation.Y + 40), Color.White, false);
        }
    }
}

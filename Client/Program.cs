using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Client.Resolution;
using Launcher;

namespace Client
{
    internal static class Program
    {
        public static CMain Form;

        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    if (arg.ToLower() == "-tc") Settings.UseTestConfig = true;
                }
            }

            #if DEBUG
                Settings.UseTestConfig = true;
            #endif

            try
            {
                if (RuntimePolicyHelper.LegacyV2RuntimeEnabledSuccessfully == true) { }

                Packet.IsServer = false;
                Settings.Load();
                ConfigLauncher.Load();
                Settings.FullScreen = ConfigLauncher.Instance.FullScreen;
                Settings.TopMost = ConfigLauncher.Instance.WindowOnTop;
                Settings.FPSCap = ConfigLauncher.Instance.FPSCap;
                Settings.Resolution = ConfigLauncher.Instance.Resolution;
                Settings.UseConfig = true;
                Settings.IPAddress = ConfigLauncher.Instance.ServerIP;
                Settings.Port = ConfigLauncher.Instance.ServerPort;
                Settings.AccountID = ConfigLauncher.Instance.UserName;
                Settings.Password = ConfigLauncher.Instance.PassWord;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                CheckResolutionSetting();

                Application.Run(Form = new CMain());

                Settings.Save();
            }
            catch (Exception ex)
            {
                CMain.SaveError(ex.ToString());
            }
        }

        public static class RuntimePolicyHelper
        {
            public static bool LegacyV2RuntimeEnabledSuccessfully { get; private set; }

            static RuntimePolicyHelper()
            {
                //ICLRRuntimeInfo clrRuntimeInfo =
                //    (ICLRRuntimeInfo)RuntimeEnvironment.GetRuntimeInterfaceAsObject(
                //        Guid.Empty,
                //        typeof(ICLRRuntimeInfo).GUID);

                //try
                //{
                //    clrRuntimeInfo.BindAsLegacyV2Runtime();
                //    LegacyV2RuntimeEnabledSuccessfully = true;
                //}
                //catch (COMException)
                //{
                //    // This occurs with an HRESULT meaning 
                //    // "A different runtime was already bound to the legacy CLR version 2 activation policy."
                //    LegacyV2RuntimeEnabledSuccessfully = false;
                //}
            }

            [ComImport]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            [Guid("BD39D1D2-BA2F-486A-89B0-B4B0CB466891")]
            private interface ICLRRuntimeInfo
            {
                void xGetVersionString();
                void xGetRuntimeDirectory();
                void xIsLoaded();
                void xIsLoadable();
                void xLoadErrorString();
                void xLoadLibrary();
                void xGetProcAddress();
                void xGetInterface();
                void xSetDefaultStartupFlags();
                void xGetDefaultStartupFlags();

                [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
                void BindAsLegacyV2Runtime();
            }
        }

        public static void CheckResolutionSetting()
        {
            var parsedOK = DisplayResolutions.GetDisplayResolutions();
            if (!parsedOK)
            {
                MessageBox.Show("Could not get display resolutions", "Get Display Resolution Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            if (!DisplayResolutions.IsSupported(Settings.Resolution))
            {
                MessageBox.Show($"Client does not support {Settings.Resolution}. Setting Resolution to 1024x768.",
                                "Invalid Client Resolution",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                Settings.Resolution = (int)eSupportedResolution.w1024h768;
                Settings.Save();
            }
        }
    }
}
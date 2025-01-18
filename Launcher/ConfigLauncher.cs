using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Launcher
{
    internal class ConfigLauncher
    {
        public static ConfigLauncher Instance { get; set; } = new ConfigLauncher();

        public static void Load()
        {
            if (File.Exists("ConfigLauncher.ini"))
            {
                string ts = File.ReadAllText("ConfigLauncher.ini", Encoding.UTF8);
                Instance = JsonConvert.DeserializeObject<ConfigLauncher>(ts);
            }
        }

        public static void Save()
        {
            string output = JsonConvert.SerializeObject(Instance, Formatting.Indented);
            File.WriteAllText("ConfigLauncher.ini", output, Encoding.UTF8);
        }

        public string ClientFileName { get; set; } = "Client.exe";

        public string Language { get; set; } = "English";

        public int ServerSel { get; set; } = 0;

        public int ResolutionSel { get; set; } = 0;
        public int Resolution { get; set; } = 1024;

        public bool FullScreen { get; set; } = false;

        public bool FPSCap { get; set; } = true;

        public bool WindowOnTop { get; set; } = false;

        public string UserName { get; set; } = "";

        public string PassWord { get; set; } = "";

        public string ServerIP { get; set; } = "127.0.0.1";

        public int ServerPort { get; set; } = 7000;

        public string FtpIP { get; set; } = "43.156.113.191";

        public string FtpUser { get; set; } = "ftpr";

        public string FtpPW { get; set; } = "";

        public string VersionFileName { get; set; } = "ClientVersion.json";
    }
}

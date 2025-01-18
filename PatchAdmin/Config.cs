using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PatchAdmin
{
    internal class Config
    {
        public static Config Instance { get; set; } = new Config();

        public static void Load()
        {
            if (File.Exists("Config.ini"))
            {
                string ts = File.ReadAllText("Config.ini", Encoding.UTF8);
                Instance = JsonConvert.DeserializeObject<Config>(ts);
            }
        }

        public static void Save()
        {
            string output = JsonConvert.SerializeObject(Instance, Formatting.Indented);
            File.WriteAllText("Config.ini", output, Encoding.UTF8);
        }

        public string ClientPath { get; set; } = "";

        public string FtpIP { get; set; } = "43.156.113.191";

        public string FtpUser { get; set; } = "ftpr";

        public string FtpPW { get; set; } = "";

        public string VersionFileName { get; set; } = "ClientVersion.json";
    }
}

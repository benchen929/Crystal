using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PatchAdmin
{
    public class ClientVersion
    {
        public static ClientVersion Load(string fn)
        {
            if (File.Exists(fn))
            {
                string ts = File.ReadAllText(fn, Encoding.UTF8);
                return JsonConvert.DeserializeObject<ClientVersion>(ts);
            }
            else
            {
                return null;
            }
        }

        public void Save(string fn)
        {
            string output = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(fn, output, Encoding.UTF8);
        }

        public List<VerFileInfo> VerFileInfoList { get; set; } = new List<VerFileInfo>();
    }

    public class VerFileInfo
    {
        public string FileName { get; set; } = "";
        public byte[] CheckSum { get; set; }

        public VerFileInfo()
        { }

        public VerFileInfo(string fileName, string clientPath)
        {
            FileName = fileName.Replace(clientPath, "");
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream stream = File.OpenRead(fileName))
                    CheckSum = md5.ComputeHash(stream);
            }
        }

        public bool IsMatch(VerFileInfo vfi, long offSet = 0)
        {
            if (vfi.CheckSum == null || CheckSum == null || vfi.CheckSum.Length + offSet > CheckSum.Length || offSet < 0) return false;

            for (int i = 0; i < vfi.CheckSum.Length; i++)
                if (CheckSum[offSet + i] != vfi.CheckSum[i])
                    return false;

            return true;
        }
    }
}

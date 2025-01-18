using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using FluentFTP;
using PatchAdmin;

namespace Launcher
{
    public partial class FormMain : Form
    {
        private List<string> ServerList = new List<string>();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                string[] tArray = File.ReadAllLines("ServerList.txt", Encoding.UTF8);
                foreach (var item in tArray)
                {
                    ServerList.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (ConfigLauncher.Instance.Language == "English")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            comboBox2.SelectedIndex = ConfigLauncher.Instance.ResolutionSel;
            comboBox3.SelectedIndex = ConfigLauncher.Instance.ServerSel;
            checkBox1.Checked = ConfigLauncher.Instance.FullScreen;
            checkBox2.Checked = ConfigLauncher.Instance.FPSCap;
            checkBox3.Checked = ConfigLauncher.Instance.WindowOnTop;
            textBox1.Text = ConfigLauncher.Instance.UserName;
            textBox2.Text = ConfigLauncher.Instance.PassWord;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                ConfigLauncher.Instance.Language = "English";
                ConfigLauncher.Save();

                checkBox1.Text = "FullScreen";
                checkBox2.Text = "FPS Cap";
                checkBox3.Text = "WindowOnTop";

                button1.Text = "Update";
                button2.Text = "Launch";

                comboBox3.Items.Clear();
                foreach (var item in ServerList)
                {
                    comboBox3.Items.Add(item.Split('|')[1]);
                }
                if (comboBox3.Items.Count > 0)
                {
                    comboBox3.SelectedIndex = ConfigLauncher.Instance.ServerSel;
                }
            }
            else
            {
                ConfigLauncher.Instance.Language = "中文";
                ConfigLauncher.Save();

                checkBox1.Text = "全屏";
                checkBox2.Text = "限制FPS";
                checkBox3.Text = "窗口置顶";

                button1.Text = "更新";
                button2.Text = "启动";

                comboBox3.Items.Clear();
                foreach (var item in ServerList)
                {
                    comboBox3.Items.Add(item.Split('|')[2]);
                }
                if (comboBox3.Items.Count > 0)
                {
                    comboBox3.SelectedIndex = ConfigLauncher.Instance.ServerSel;
                }
            }

            if (File.Exists("GameDocumentationList.txt"))
            {
                string[] lines = File.ReadAllLines("GameDocumentationList.txt");
                List<string> linesEN = new List<string>();
                List<string> linesCN = new List<string>();
                foreach (var line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.StartsWith("EN|"))
                        {
                            linesEN.Add(line.Replace("EN|", ""));
                        }
                        if (line.StartsWith("CN|"))
                        {
                            linesCN.Add(line.Replace("CN|", ""));
                        }
                    }
                }
                listBox1.Items.Clear();
                if (comboBox1.SelectedIndex == 0)
                {
                    foreach (var item in linesEN)
                    {
                        listBox1.Items.Add(item);
                    }
                }
                else
                {
                    foreach (var item in linesCN)
                    {
                        listBox1.Items.Add(item);
                    }
                }
                if (listBox1.Items.Count > 0)
                {
                    listBox1.SelectedIndex = 0;
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigLauncher.Instance.ServerSel = comboBox3.SelectedIndex;
            ConfigLauncher.Instance.ServerIP = ServerList[comboBox3.SelectedIndex].Split('|')[0].Split(':')[0];
            ConfigLauncher.Instance.ServerPort = Convert.ToInt32(ServerList[comboBox3.SelectedIndex].Split('|')[0].Split(':')[1]);
            ConfigLauncher.Save();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigLauncher.Instance.ResolutionSel = comboBox2.SelectedIndex;
            ConfigLauncher.Instance.Resolution = Convert.ToInt32(comboBox2.Text.Split("x")[0]);
            ConfigLauncher.Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ConfigLauncher.Instance.FullScreen = checkBox1.Checked;
            ConfigLauncher.Save();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ConfigLauncher.Instance.FPSCap = checkBox2.Checked;
            ConfigLauncher.Save();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            ConfigLauncher.Instance.WindowOnTop = checkBox3.Checked;
            ConfigLauncher.Save();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ConfigLauncher.Instance.UserName = textBox1.Text;
            ConfigLauncher.Save();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ConfigLauncher.Instance.PassWord = textBox2.Text;
            ConfigLauncher.Save();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string fn = System.AppDomain.CurrentDomain.BaseDirectory + @"GameDoc\" + listBox1.SelectedItem.ToString() + ".rtf";
                if (File.Exists(fn))
                {
                    richTextBox1.LoadFile(fn);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(ConfigLauncher.Instance.ClientFileName);
                //Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(DoWork);
            thread.IsBackground = true;
            thread.Start();
        }

        private void DoWork()
        {
            this.BeginInvoke(new Action(() =>
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }));

            try
            {
                ClientVersion clientVersionRemote;
                ClientVersion clientVersionLocal;
                string clientVersionRemoteName = "ClientVersionRemote.json";

                using (var ftp = new FtpClient(ConfigLauncher.Instance.FtpIP, ConfigLauncher.Instance.FtpUser, ConfigLauncher.Instance.FtpPW))
                {
                    ftp.Connect();
                    //ShowMessage("ftp服务器连接成功");

                    // The last parameter forces FluentFTP to use LIST -a 
                    // for getting a list of objects in the parent directory.
                    if (!ftp.FileExists("/" + ConfigLauncher.Instance.VersionFileName))
                    {
                        //远程客户端版本文件不存在
                        if (ConfigLauncher.Instance.Language == "English")
                        {
                            MessageBox.Show("No Remote ClientVersion File, Update Failed");
                        }
                        else
                        {
                            MessageBox.Show("服务器端客户端版本文件不存在，更新失败");
                        }
                        this.BeginInvoke(new Action(() =>
                        {
                            button1.Enabled = true;
                            button2.Enabled = true;
                        }));
                        return;
                    }
                    else
                    {
                        // download a file and ensure the local directory is created
                        ftp.DownloadFile(clientVersionRemoteName, "/" + ConfigLauncher.Instance.VersionFileName, FtpLocalExists.Overwrite);
                        clientVersionRemote = ClientVersion.Load(clientVersionRemoteName);
                        File.Delete(clientVersionRemoteName);
                        //ShowMessage("服务器端客户端版本文件下载完成");
                    }

                    clientVersionLocal = new ClientVersion();
                    string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.*", SearchOption.AllDirectories);
                    this.BeginInvoke(new Action(() => 
                    {
                        progressBar1.Maximum = files.Length;
                    }));
                    int x = 0;
                    foreach (var file in files)
                    {
                        var vfi = new VerFileInfo(file, AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 1));
                        clientVersionLocal.VerFileInfoList.Add(vfi);
                        //ShowMessage($"客户端文件【{vfi.FileName}】添加完成");
                        this.BeginInvoke(new Action(() =>
                        {
                            x++;
                            label1.Text = $"{x}/{files.Length}";
                            label2.Text = vfi.FileName;
                            if (x <= progressBar1.Maximum)
                            {
                                progressBar1.Value = x;
                            }
                        }));
                    }
                    //ShowMessage("所有的客户端文件添加完成");

                    List<VerFileInfo> tList = new List<VerFileInfo>();
                    foreach (var vfi in clientVersionRemote.VerFileInfoList)
                    {
                        var vfi2 = clientVersionLocal.VerFileInfoList.Where(x => x.FileName == vfi.FileName && x.IsMatch(vfi)).FirstOrDefault();
                        if (vfi2 == null)
                        {
                            tList.Add(vfi);
                        }
                    }

                    this.BeginInvoke(new Action(() =>
                    {
                        progressBar1.Maximum = tList.Count;
                    }));
                    x = 0;
                    foreach (var vfi in tList)
                    {
                        // download a file and ensure the local directory is created, verify the file after download
                        ftp.DownloadFile(AppDomain.CurrentDomain.BaseDirectory + vfi.FileName, vfi.FileName.Replace(@"\", @"/"), FtpLocalExists.Overwrite);
                        this.BeginInvoke(new Action(() =>
                        {
                            x++;
                            label1.Text = $"{x}/{tList.Count}";
                            label2.Text = vfi.FileName;
                            if (x <= progressBar1.Maximum)
                            {
                                progressBar1.Value = x;
                            }
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.BeginInvoke(new Action(() =>
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }));
        }
    }
}

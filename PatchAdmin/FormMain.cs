using FluentFTP;

namespace PatchAdmin
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            textBox1.Text = Config.Instance.ClientPath;
            textBox3.Text = Config.Instance.FtpIP;
            textBox4.Text = Config.Instance.FtpUser;
            textBox5.Text = Config.Instance.FtpPW;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                Config.Instance.ClientPath = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Config.Instance.ClientPath))
            {
                MessageBox.Show("��ѡ��ͻ���·��");
                return;
            }

            textBox2.Clear();

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
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }));

            try
            {
                ClientVersion clientVersionRemote;
                ClientVersion clientVersionLocal;
                string clientVersionRemoteName = "ClientVersionRemote.json";

                using (var ftp = new FtpClient(Config.Instance.FtpIP, Config.Instance.FtpUser, Config.Instance.FtpPW))
                {
                    ftp.Connect();
                    ShowMessage("ftp���������ӳɹ�");

                    // The last parameter forces FluentFTP to use LIST -a 
                    // for getting a list of objects in the parent directory.
                    if (!ftp.FileExists("/" + Config.Instance.VersionFileName))
                    {
                        //Զ�̿ͻ��˰汾�ļ�������
                        clientVersionRemote = new ClientVersion();
                        ShowMessage("�������˿ͻ��˰汾�ļ�������");
                    }
                    else
                    {
                        // download a file and ensure the local directory is created
                        ftp.DownloadFile(clientVersionRemoteName, "/" + Config.Instance.VersionFileName, FtpLocalExists.Overwrite);
                        clientVersionRemote = ClientVersion.Load(clientVersionRemoteName);
                        ShowMessage("�������˿ͻ��˰汾�ļ��������");
                    }
                }

                clientVersionLocal = new ClientVersion();
                string[] files = Directory.GetFiles(Config.Instance.ClientPath, "*.*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    var vfi = new VerFileInfo(file, Config.Instance.ClientPath);
                    clientVersionLocal.VerFileInfoList.Add(vfi);
                    ShowMessage($"�ͻ����ļ���{vfi.FileName}��������");
                }
                ShowMessage("���еĿͻ����ļ�������");

                string tempPath = AppDomain.CurrentDomain.BaseDirectory + "Patch";
                if (Directory.Exists(tempPath)) Directory.Delete(tempPath, true);
                if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);

                int x = 0;
                foreach (var vfi in clientVersionLocal.VerFileInfoList)
                {
                    var vfi2 = clientVersionRemote.VerFileInfoList.Where(x => x.FileName == vfi.FileName && x.IsMatch(vfi)).FirstOrDefault();
                    if (vfi2 == null)
                    {
                        FileInfo fileInfo = new FileInfo(tempPath + vfi.FileName);
                        if (!Directory.Exists(fileInfo.DirectoryName)) Directory.CreateDirectory(fileInfo.DirectoryName);
                        File.Copy(Config.Instance.ClientPath + vfi.FileName, tempPath + vfi.FileName, true);
                        ShowMessage($"�������ļ���{vfi.FileName}���������");
                        x++;
                    }
                }

                if (x > 0)
                {
                    clientVersionLocal.Save(tempPath + "\\" + Config.Instance.VersionFileName);
                    ShowMessage("�ͻ��˰汾�ļ��������");
                }

                File.Delete(clientVersionRemoteName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.BeginInvoke(new Action(() =>
            {
                button1.Enabled = true;
                button2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }));
        }

        private void ShowMessage(string msg)
        {
            this.BeginInvoke(new Action(() =>
            {
                textBox2.AppendText(msg + System.Environment.NewLine);
                textBox2.ScrollToCaret();
            }));
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Config.Instance.FtpIP = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Config.Instance.FtpUser = textBox4.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            Config.Instance.FtpPW = textBox5.Text;
        }
    }
}

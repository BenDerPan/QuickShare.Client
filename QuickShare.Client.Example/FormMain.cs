using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickShare.Client.Example
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择上传文件";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbFile.Text = dialog.FileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            lbOutput.Items.Clear();
            QsConfig config = new QsConfig();
            config.ServerAddr = tbApiUrl.Text.Trim();
            config.AdminId = tbUser.Text.Trim() ;
            config.AdminPwd = tbPwd.Text.Trim();
            QsClient client = new QsClient(config);
            Log("登录", "账号登录...");
            if (client.Login())
            {
                Log("登录", "账号登录...OK");
                Log("上传", $"开始上传文件：{tbFile.Text.Trim()} ...");
                if (client.UploadFile(tbFile.Text.Trim(), out var url))
                {
                    Log("上传成功", $"上传文件成功：{tbFile.Text.Trim()} ，文件远程访问地址：{url}");
                }
                else
                {
                    Log("上传失败", $"上传文件失败：{tbFile.Text.Trim()} ...Error");
                }
            }
            else
            {
                Log("登录", "账号登录...Error");
                Log("错误", "登录失败，上传停止");
            }
            
            

        }

        void Log(string title,string message)
        {
            var msg = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] - [{title}] - {message}";
            lbOutput.Items.Add(msg);
            lbOutput.SelectedIndex = lbOutput.Items.Count - 1;
        }
    }
}

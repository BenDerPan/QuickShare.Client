namespace QuickShare.Client.Example
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gpConfig = new System.Windows.Forms.GroupBox();
            this.btnChoose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbApiUrl = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.lbOutput = new System.Windows.Forms.ListBox();
            this.gpConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpConfig
            // 
            this.gpConfig.Controls.Add(this.btnChoose);
            this.gpConfig.Controls.Add(this.label4);
            this.gpConfig.Controls.Add(this.tbPwd);
            this.gpConfig.Controls.Add(this.label3);
            this.gpConfig.Controls.Add(this.tbUser);
            this.gpConfig.Controls.Add(this.label2);
            this.gpConfig.Controls.Add(this.tbFile);
            this.gpConfig.Controls.Add(this.label1);
            this.gpConfig.Controls.Add(this.tbApiUrl);
            this.gpConfig.Controls.Add(this.btnUpload);
            this.gpConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpConfig.Location = new System.Drawing.Point(0, 0);
            this.gpConfig.Name = "gpConfig";
            this.gpConfig.Size = new System.Drawing.Size(800, 154);
            this.gpConfig.TabIndex = 1;
            this.gpConfig.TabStop = false;
            this.gpConfig.Text = "配置";
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(469, 62);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(75, 23);
            this.btnChoose.TabIndex = 9;
            this.btnChoose.Text = "选择...";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(220, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Password:";
            // 
            // tbPwd
            // 
            this.tbPwd.Location = new System.Drawing.Point(285, 108);
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.Size = new System.Drawing.Size(118, 21);
            this.tbPwd.TabIndex = 7;
            this.tbPwd.Text = "admin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "User:";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(77, 108);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(90, 21);
            this.tbUser.TabIndex = 5;
            this.tbUser.Text = "admin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "选择文件:";
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(77, 64);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(386, 21);
            this.tbFile.TabIndex = 3;
            this.tbFile.Text = "C:\\Users\\user\\Downloads\\dotnet-sdk-2.1.4-win-x64.exe";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "API:";
            // 
            // tbApiUrl
            // 
            this.tbApiUrl.Location = new System.Drawing.Point(77, 19);
            this.tbApiUrl.Name = "tbApiUrl";
            this.tbApiUrl.Size = new System.Drawing.Size(386, 21);
            this.tbApiUrl.TabIndex = 1;
            this.tbApiUrl.Text = "http://192.168.102.164:40200";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(695, 17);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lbOutput
            // 
            this.lbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOutput.FormattingEnabled = true;
            this.lbOutput.ItemHeight = 12;
            this.lbOutput.Location = new System.Drawing.Point(0, 154);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(800, 296);
            this.lbOutput.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbOutput);
            this.Controls.Add(this.gpConfig);
            this.Name = "FormMain";
            this.Text = "QuickShare文件上传客户端";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.gpConfig.ResumeLayout(false);
            this.gpConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpConfig;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbApiUrl;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ListBox lbOutput;
    }
}


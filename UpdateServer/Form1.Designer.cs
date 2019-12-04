namespace UpdateServer
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnTestMN = new System.Windows.Forms.Button();
            this.btnRequestClientUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstMN = new System.Windows.Forms.ListBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblOnlineCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbIP = new System.Windows.Forms.ComboBox();
            this.btnSelectRootDir = new System.Windows.Forms.Button();
            this.txtRootDir = new System.Windows.Forms.TextBox();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lbladdress = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(785, 541);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnClearLog);
            this.tabPage1.Controls.Add(this.btnTestMN);
            this.tabPage1.Controls.Add(this.btnRequestClientUpdate);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lstMN);
            this.tabPage1.Controls.Add(this.lblVersion);
            this.tabPage1.Controls.Add(this.txtVersion);
            this.tabPage1.Controls.Add(this.lblOnlineCount);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.listBoxLog);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cmbIP);
            this.tabPage1.Controls.Add(this.btnSelectRootDir);
            this.tabPage1.Controls.Add(this.txtRootDir);
            this.tabPage1.Controls.Add(this.listBoxFiles);
            this.tabPage1.Controls.Add(this.lblPort);
            this.tabPage1.Controls.Add(this.txtPort);
            this.tabPage1.Controls.Add(this.lbladdress);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(777, 515);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(533, 168);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 38;
            this.btnClearLog.Text = "清除";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnTestMN
            // 
            this.btnTestMN.Location = new System.Drawing.Point(443, 168);
            this.btnTestMN.Name = "btnTestMN";
            this.btnTestMN.Size = new System.Drawing.Size(75, 23);
            this.btnTestMN.TabIndex = 37;
            this.btnTestMN.Text = "测试MN";
            this.btnTestMN.UseVisualStyleBackColor = true;
            this.btnTestMN.Click += new System.EventHandler(this.btnTestMN_Click);
            // 
            // btnRequestClientUpdate
            // 
            this.btnRequestClientUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRequestClientUpdate.Location = new System.Drawing.Point(618, 126);
            this.btnRequestClientUpdate.Name = "btnRequestClientUpdate";
            this.btnRequestClientUpdate.Size = new System.Drawing.Size(151, 23);
            this.btnRequestClientUpdate.TabIndex = 36;
            this.btnRequestClientUpdate.Text = "要求在线客户更新程序";
            this.btnRequestClientUpdate.UseVisualStyleBackColor = true;
            this.btnRequestClientUpdate.Click += new System.EventHandler(this.btnRequestClientUpdate_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(616, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "数采议MN列表";
            // 
            // lstMN
            // 
            this.lstMN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMN.FormattingEnabled = true;
            this.lstMN.ItemHeight = 12;
            this.lstMN.Location = new System.Drawing.Point(618, 175);
            this.lstMN.Name = "lstMN";
            this.lstMN.Size = new System.Drawing.Size(151, 328);
            this.lstMN.TabIndex = 34;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(616, 61);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(29, 12);
            this.lblVersion.TabIndex = 33;
            this.lblVersion.Text = "版本";
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Location = new System.Drawing.Point(651, 58);
            this.txtVersion.MaxLength = 10;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(118, 21);
            this.txtVersion.TabIndex = 32;
            // 
            // lblOnlineCount
            // 
            this.lblOnlineCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOnlineCount.AutoSize = true;
            this.lblOnlineCount.Location = new System.Drawing.Point(616, 111);
            this.lblOnlineCount.Name = "lblOnlineCount";
            this.lblOnlineCount.Size = new System.Drawing.Size(89, 12);
            this.lblOnlineCount.TabIndex = 31;
            this.lblOnlineCount.Text = "在线客户数量:0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "服务日志";
            // 
            // listBoxLog
            // 
            this.listBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(9, 192);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(599, 316);
            this.listBoxLog.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "更新程序存放的起始路径";
            // 
            // cmbIP
            // 
            this.cmbIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIP.FormattingEnabled = true;
            this.cmbIP.Location = new System.Drawing.Point(651, 4);
            this.cmbIP.Name = "cmbIP";
            this.cmbIP.Size = new System.Drawing.Size(118, 20);
            this.cmbIP.TabIndex = 27;
            // 
            // btnSelectRootDir
            // 
            this.btnSelectRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectRootDir.Location = new System.Drawing.Point(572, 4);
            this.btnSelectRootDir.Name = "btnSelectRootDir";
            this.btnSelectRootDir.Size = new System.Drawing.Size(37, 21);
            this.btnSelectRootDir.TabIndex = 26;
            this.btnSelectRootDir.Text = "浏览";
            this.btnSelectRootDir.UseVisualStyleBackColor = true;
            this.btnSelectRootDir.Click += new System.EventHandler(this.btnSelectRootDir_Click);
            // 
            // txtRootDir
            // 
            this.txtRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRootDir.Location = new System.Drawing.Point(152, 4);
            this.txtRootDir.Name = "txtRootDir";
            this.txtRootDir.Size = new System.Drawing.Size(414, 21);
            this.txtRootDir.TabIndex = 25;
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.ItemHeight = 12;
            this.listBoxFiles.Location = new System.Drawing.Point(9, 30);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(599, 136);
            this.listBoxFiles.TabIndex = 22;
            // 
            // lblPort
            // 
            this.lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(616, 34);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 12);
            this.lblPort.TabIndex = 24;
            this.lblPort.Text = "端口";
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.Location = new System.Drawing.Point(651, 31);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(118, 21);
            this.txtPort.TabIndex = 20;
            this.txtPort.Text = "8383";
            // 
            // lbladdress
            // 
            this.lbladdress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbladdress.AutoSize = true;
            this.lbladdress.Location = new System.Drawing.Point(616, 7);
            this.lbladdress.Name = "lbladdress";
            this.lbladdress.Size = new System.Drawing.Size(29, 12);
            this.lbladdress.TabIndex = 23;
            this.lbladdress.Text = "地址";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(618, 85);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(151, 23);
            this.btnStart.TabIndex = 21;
            this.btnStart.Text = "启动服务";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "C:\\Users\\Public\\Desktop";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 553);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Tag = "";
            this.Text = "UpdateServer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Click += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnTestMN;
        private System.Windows.Forms.Button btnRequestClientUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstMN;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblOnlineCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbIP;
        private System.Windows.Forms.Button btnSelectRootDir;
        private System.Windows.Forms.TextBox txtRootDir;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lbladdress;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnClearLog;

    }
}


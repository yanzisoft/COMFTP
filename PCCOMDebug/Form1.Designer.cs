namespace PCCOMDebug
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
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMN = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSelFile = new System.Windows.Forms.Button();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTestCOMTCP = new System.Windows.Forms.Button();
            this.cmPorts = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cmbIP = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lbladdress = new System.Windows.Forms.Label();
            this.txtBaudRate = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            this.serialPort1.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.serialPort1_PinChanged);
            this.serialPort1.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort1_ErrorReceived);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(716, 388);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtBaudRate);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtVersion);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtMN);
            this.tabPage1.Controls.Add(this.btnUpdate);
            this.tabPage1.Controls.Add(this.btnSelFile);
            this.tabPage1.Controls.Add(this.btnSendFile);
            this.tabPage1.Controls.Add(this.txtFileName);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnTestCOMTCP);
            this.tabPage1.Controls.Add(this.cmPorts);
            this.tabPage1.Controls.Add(this.btnSend);
            this.tabPage1.Controls.Add(this.txtSend);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtData);
            this.tabPage1.Controls.Add(this.btnOpen);
            this.tabPage1.Controls.Add(this.lblPort);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(708, 362);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "COM";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(315, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 42;
            this.label6.Text = "速率";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(455, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "版本";
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Location = new System.Drawing.Point(490, 7);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(74, 21);
            this.txtVersion.TabIndex = 39;
            this.txtVersion.Text = "2014090400";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(570, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "MN";
            // 
            // txtMN
            // 
            this.txtMN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMN.Location = new System.Drawing.Point(593, 6);
            this.txtMN.Name = "txtMN";
            this.txtMN.Size = new System.Drawing.Size(109, 21);
            this.txtMN.TabIndex = 37;
            this.txtMN.Text = "123";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(601, 272);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(101, 23);
            this.btnUpdate.TabIndex = 36;
            this.btnUpdate.Text = "检查更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSelFile
            // 
            this.btnSelFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelFile.Location = new System.Drawing.Point(600, 306);
            this.btnSelFile.Name = "btnSelFile";
            this.btnSelFile.Size = new System.Drawing.Size(102, 23);
            this.btnSelFile.TabIndex = 35;
            this.btnSelFile.Text = "选择文件(&E)";
            this.btnSelFile.Click += new System.EventHandler(this.btnSelFile_Click);
            // 
            // btnSendFile
            // 
            this.btnSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendFile.Location = new System.Drawing.Point(600, 335);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(102, 23);
            this.btnSendFile.TabIndex = 34;
            this.btnSendFile.Text = "文件发送(&F)";
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(42, 259);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(552, 99);
            this.txtFileName.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(8, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "发送：";
            // 
            // btnTestCOMTCP
            // 
            this.btnTestCOMTCP.Location = new System.Drawing.Point(235, 6);
            this.btnTestCOMTCP.Name = "btnTestCOMTCP";
            this.btnTestCOMTCP.Size = new System.Drawing.Size(75, 23);
            this.btnTestCOMTCP.TabIndex = 31;
            this.btnTestCOMTCP.Text = "测试协议";
            this.btnTestCOMTCP.UseVisualStyleBackColor = true;
            this.btnTestCOMTCP.Click += new System.EventHandler(this.btnTestCOMTCP_Click);
            // 
            // cmPorts
            // 
            this.cmPorts.Location = new System.Drawing.Point(42, 8);
            this.cmPorts.Name = "cmPorts";
            this.cmPorts.Size = new System.Drawing.Size(100, 20);
            this.cmPorts.TabIndex = 30;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(600, 230);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(102, 23);
            this.btnSend.TabIndex = 29;
            this.btnSend.Text = "发送(&S)";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.Location = new System.Drawing.Point(42, 232);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(552, 21);
            this.txtSend.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(8, 234);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 27;
            this.label1.Text = "发送：";
            // 
            // txtData
            // 
            this.txtData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtData.Location = new System.Drawing.Point(10, 34);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtData.Size = new System.Drawing.Size(692, 192);
            this.txtData.TabIndex = 26;
            this.txtData.TextChanged += new System.EventHandler(this.txtData_TextChanged);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(157, 6);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(72, 23);
            this.btnOpen.TabIndex = 24;
            this.btnOpen.Text = "打开(&O)";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lblPort
            // 
            this.lblPort.Location = new System.Drawing.Point(6, 11);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(61, 20);
            this.lblPort.TabIndex = 25;
            this.lblPort.Text = "端口：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.btnConnect);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.cmbIP);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtPort);
            this.tabPage2.Controls.Add(this.lbladdress);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(708, 362);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TCP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(323, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 44;
            this.label7.Text = "版本";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(358, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(74, 21);
            this.textBox1.TabIndex = 43;
            this.textBox1.Text = "2014090400";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(438, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 42;
            this.label8.Text = "MN";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(461, 7);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(109, 21);
            this.textBox2.TabIndex = 41;
            this.textBox2.Text = "123";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(630, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(72, 20);
            this.btnConnect.TabIndex = 20;
            this.btnConnect.Text = "连接(&C)";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(8, 33);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(694, 328);
            this.listBox1.TabIndex = 19;
            // 
            // cmbIP
            // 
            this.cmbIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIP.FormattingEnabled = true;
            this.cmbIP.Items.AddRange(new object[] {
            "218.56.152.40"});
            this.cmbIP.Location = new System.Drawing.Point(41, 6);
            this.cmbIP.Name = "cmbIP";
            this.cmbIP.Size = new System.Drawing.Size(118, 20);
            this.cmbIP.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "端口";
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.Location = new System.Drawing.Point(200, 6);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(118, 21);
            this.txtPort.TabIndex = 10;
            this.txtPort.Text = "8383";
            // 
            // lbladdress
            // 
            this.lbladdress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbladdress.AutoSize = true;
            this.lbladdress.Location = new System.Drawing.Point(6, 9);
            this.lbladdress.Name = "lbladdress";
            this.lbladdress.Size = new System.Drawing.Size(29, 12);
            this.lbladdress.TabIndex = 11;
            this.lbladdress.Text = "地址";
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.txtBaudRate.Location = new System.Drawing.Point(349, 7);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.Size = new System.Drawing.Size(100, 20);
            this.txtBaudRate.TabIndex = 45;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 390);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "PCCOMDebug";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMN;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSelFile;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTestCOMTCP;
        private System.Windows.Forms.ComboBox cmPorts;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cmbIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lbladdress;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox txtBaudRate;
    }
}


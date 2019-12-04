namespace CECOMDebug
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtBaudRate = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmPorts = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMN = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbIP = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lbladdress = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM3";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            this.serialPort1.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.serialPort1_PinChanged);
            this.serialPort1.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort1_ErrorReceived);
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
            this.tabControl1.Size = new System.Drawing.Size(634, 452);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtBaudRate);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.cmPorts);
            this.tabPage1.Controls.Add(this.btnSend);
            this.tabPage1.Controls.Add(this.txtSend);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtData);
            this.tabPage1.Controls.Add(this.btnOpen);
            this.tabPage1.Controls.Add(this.lblPort);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(626, 423);
            this.tabPage1.Text = "COM";
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.txtBaudRate.Items.Add("115200");
            this.txtBaudRate.Items.Add("9600");
            this.txtBaudRate.Location = new System.Drawing.Point(270, 3);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.Size = new System.Drawing.Size(100, 23);
            this.txtBaudRate.TabIndex = 44;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(231, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 15);
            this.label6.Text = "速率";
            // 
            // cmPorts
            // 
            this.cmPorts.Location = new System.Drawing.Point(46, 3);
            this.cmPorts.Name = "cmPorts";
            this.cmPorts.Size = new System.Drawing.Size(100, 23);
            this.cmPorts.TabIndex = 16;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(546, 398);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(77, 23);
            this.btnSend.TabIndex = 20;
            this.btnSend.Text = "发送(&S)";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.Location = new System.Drawing.Point(44, 398);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(500, 23);
            this.txtSend.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(1, 401);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.Text = "发送：";
            // 
            // txtData
            // 
            this.txtData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtData.Location = new System.Drawing.Point(-1, 28);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(628, 364);
            this.txtData.TabIndex = 18;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(153, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(72, 23);
            this.btnOpen.TabIndex = 17;
            this.btnOpen.Text = "打开";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lblPort
            // 
            this.lblPort.Location = new System.Drawing.Point(1, 4);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(52, 20);
            this.lblPort.Text = "端口：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtVersion);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtMN);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.btnConnect);
            this.tabPage2.Controls.Add(this.cmbIP);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtPort);
            this.tabPage2.Controls.Add(this.lbladdress);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(626, 423);
            this.tabPage2.Text = "TCP";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(219, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 15);
            this.label7.Text = "版本";
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Location = new System.Drawing.Point(260, 3);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(76, 23);
            this.txtVersion.TabIndex = 47;
            this.txtVersion.Text = "2014090400";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(342, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 14);
            this.label8.Text = "MN";
            // 
            // txtMN
            // 
            this.txtMN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMN.Location = new System.Drawing.Point(371, 3);
            this.txtMN.Name = "txtMN";
            this.txtMN.Size = new System.Drawing.Size(167, 23);
            this.txtMN.TabIndex = 46;
            this.txtMN.Text = "123";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.Location = new System.Drawing.Point(4, 30);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(619, 386);
            this.listBox1.TabIndex = 18;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(551, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(72, 20);
            this.btnConnect.TabIndex = 17;
            this.btnConnect.Text = "连接(&C)";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbIP
            // 
            this.cmbIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbIP.Items.Add("218.56.152.40");
            this.cmbIP.Location = new System.Drawing.Point(46, 3);
            this.cmbIP.Name = "cmbIP";
            this.cmbIP.Size = new System.Drawing.Size(118, 23);
            this.cmbIP.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(170, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.Text = "端口";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(215, 3);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(50, 23);
            this.txtPort.TabIndex = 12;
            this.txtPort.Text = "8383";
            // 
            // lbladdress
            // 
            this.lbladdress.Location = new System.Drawing.Point(4, 6);
            this.lbladdress.Name = "lbladdress";
            this.lbladdress.Size = new System.Drawing.Size(36, 20);
            this.lbladdress.Text = "地址";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "CECOMDebugV4";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cmPorts;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cmbIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lbladdress;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox txtBaudRate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMN;
    }
}


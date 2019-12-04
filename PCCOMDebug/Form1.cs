using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using yanzisoft;
using System.Net;
using System.Reflection;

namespace PCCOMDebug
{
    public partial class Form1 : Form
    {
        private FrameManager serialPortFrameMgr = null;
        //private FileTransferProtocol ftp = new FileTransferProtocol();
        //TcpClient tcpclient = null;
        public Form1()
        {
            InitializeComponent();
        }

        private CSerialPortClient mSerialPortClient = null;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (serialPort1.IsOpen) serialPort1.Close();
            //    serialPort1.BaudRate = int.Parse(txtBaudRate.Text);
            //    serialPort1.PortName = cmPorts.SelectedItem.ToString();
            //    serialPort1.Open();
            //    txtData.Text +=string.Format("打开端口:{0}\r\n" ,cmPorts.SelectedItem.ToString());
            //}
            //catch (Exception er)
            //{
            //    txtData.Text += string.Format("[异常]:{0}", er.Message);
            //}

            if (mSerialPortClient == null)
            {
                mSerialPortClient = new CSerialPortClient(cmPorts.Text, int.Parse(txtBaudRate.Text),"123",0);
            }
            else
            {
                mSerialPortClient.Close();
                mSerialPortClient.SerialPort.PortName = this.cmPorts.Text;
                mSerialPortClient.SerialPort.BaudRate = int.Parse(txtBaudRate.Text);

            }
            mSerialPortClient.Open();
        }

        private void serialPort1_PinChanged(object sender, System.IO.Ports.SerialPinChangedEventArgs e)
        {
            //this.SetText(e.EventType.ToString());
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //
                //serialPort1.WriteLine(txtSend.Text);

                //byte[] rtn=System.Text.Encoding.Default.GetBytes(txtSend.Text);
                //serialPort1.Write(rtn, 0, rtn.Length);
                byte[] errData = new byte[] { 0x23, 0x7f, 0x08, 0xab, 0x7f, 0x7e, 0x00, 0x7f, 0xff, 0x7f };
                //serialPort1.Write(errData, 0, errData.Length);

                //FileTransferProtocol x = new FileTransferProtocol();
                byte[] b = FileTransferProtocol.GetFrameFileInfo(0xFF, @"t.txt");
                
                serialPort1.Write(b, 0, b.Length);
                byte[] c = FileTransferProtocol.GetFrameFileData(255 + 1 + 2 + 4, System.Text.Encoding.Default.GetBytes("你好，CE文件!"));
                
                serialPort1.Write(c, 0, c.Length);

                byte[] d = FileTransferProtocol.GetFrameFileSendFinish();

                serialPort1.Write(d, 0, d.Length);

                

            }
            catch (Exception er)
            {
                txtData.Text += string.Format("[异常]:{0}\r\n",er.Message);
            }
        }

        public void SetButtonState(Button theButton, string text)
        {
            try
            {
                this.Invoke(new EventHandler(delegate(object obj, EventArgs args)
                {
                    this.btnConnect.Text = text;
                    this.btnConnect.Update();
                }));
            }
            catch (Exception er)
            {

            }
        }

        public void AddItemToList(ListBox listbox, object item)
        {
            if (!this.IsDisposed)
            {
                try
                {
                    this.Invoke(new EventHandler(delegate(Object own, EventArgs args)
                    {
                        listbox.Items.Add(item);
                        listbox.Update();
                    }));
                }
                catch (Exception er)
                {

                }
            }
        }

        FrameManager fFrameMgr = null;
        TcpClient tcpclient = null;
        private NetworkStream stream = null;
        Thread receiveThread = null;
        FileStream fStream = null;
        Thread mThreadConnectToServer=null;
        private void ConnectOrDisconnect()
        {
            var ip=cmbIP.Text;
            var port=int.Parse(txtPort.Text);
            if(mThreadConnectToServer==null || mThreadConnectToServer.ThreadState==System.Threading.ThreadState.Stopped){

                mThreadConnectToServer = new Thread(new ThreadStart(delegate()
                {
                    try
                    {

                        if (stream != null) stream.Close();

                        tcpclient = new TcpClient(ip, port);
                        //SetButtonState(btnConnect, "断开(&S)");
                        tcpclient.ReceiveBufferSize = 0xFFFF + 1;
                        tcpclient.SendBufferSize = 0xFFFF + 1;
                        stream = tcpclient.GetStream();
                        int revFilePartCount = 0;

                        try
                        {
                            stream.Write(System.Text.Encoding.Default.GetBytes("OK"), 0, 2);
                            SetButtonState(btnConnect, "断开(&S)");
                        }
                        catch (Exception ex)
                        {
                            if (stream != null) stream.Close();
                            if (tcpclient != null) tcpclient.Close();
                            mThreadConnectToServer.Abort();
                            return;
                        }

                        //创建帧分析对象
                        fFrameMgr = new FrameManager((ushort)(serialPort1.ReadBufferSize * 3), stream);
                        //实现帧处理函数
                        fFrameMgr.OnReceiveFrame += new FrameManager.OnReceiveFrameHandler(
                            delegate(FileTransferProtocol.CommandType frameType, int parameter, byte[] commandData)
                            {

                                //NetworkStream nStream = (NetworkStream)_stream;
                                
                                switch (frameType)
                                {
                                    case FileTransferProtocol.CommandType.FileName:
                                        string fileName = System.Text.Encoding.Default.GetString(commandData, 0, commandData.Length);

                                        if (fileName != null)
                                        {
                                            if (fStream != null)
                                            {
                                                fStream.Close();
                                                fStream = null;
                                            }
                                            revFilePartCount = 0;
                                            //string CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                                            string CurrentPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                                            string startDir = CurrentPath + @"\update\";
                                            if (!Directory.Exists(startDir)) Directory.CreateDirectory(startDir);
                                            
                                            var dir = Path.GetDirectoryName(startDir + fileName);
                                            Debug.WriteLine("目录:"+dir);
                                            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                                            if (File.Exists(startDir + fileName)) File.Delete(startDir + fileName);
                                            fStream = File.Open(startDir + fileName, FileMode.CreateNew);

                                            AddItemToList(listBox1, DateTime.Now.ToString() + " 准备接收文件：" + fileName + " 文件总长度：" + parameter);
                                        }
                                        break;

                                    case FileTransferProtocol.CommandType.FileData:
                                        if (fStream != null && commandData != null && commandData.Length > 0)
                                        {
                                            revFilePartCount++;
                                            fStream.Write(commandData, 0, commandData.Length);
                                            AddItemToList(listBox1, string.Format("收到 第{0}块 数据长度:{1} 服务器端分块号:{2} ", revFilePartCount,commandData.Length,parameter));
                                        }
                                        break;

                                    case FileTransferProtocol.CommandType.FileSendFinish:

                                        if (fStream != null)
                                        {
                                            
                                            fStream.Close();
                                            fStream = null;
                                            AddItemToList(listBox1, DateTime.Now.ToString() + " 收到文件结束信号");
                                        }
                                        break;
                                    case FileTransferProtocol.CommandType.WhatIsYourMNandVersion:
                                        byte[] frame = FileTransferProtocol.GetFrameMNVersionLimited(1, "123", -1);
                                        stream.Write(frame, 0, frame.Length);
                                        break;
                                    case FileTransferProtocol.CommandType.Message:
                                        string message = System.Text.Encoding.Default.GetString(commandData, 0, commandData.Length);

                                        AddItemToList(listBox1, message);

                                        break;
                                }

                                return true;


                            });


                        //------------------------------------------------------------------------------------
                        //方式0数采仪被动更新（只有在更新服务器询问[指定列表的]数采仪时，则数据采仪才更新）
                        //------------------------------------------------------------------------------------
                        ////把 网络 或 串口 的 数据 传给 帧分析对象的Handler函数做分析
                        if (receiveThread == null||receiveThread.ThreadState==System.Threading.ThreadState.Stopped)
                        {
                            receiveThread = new Thread(new ThreadStart(delegate()
                            {

                                
                                int MaxLength = FileTransferProtocol.MaxFrameLength - FileTransferProtocol.FrameStructLength;//64*1024;0xFFFF+1;64K;
                                Byte[] data = new Byte[MaxLength];
                                try
                                {
                                    bool running = true;
                                    while (running)
                                    {
                                        Debug.WriteLine(string.Format("{0}", Thread.CurrentThread.Name));


                                        Int32 bytes = 0;
                                        try
                                        {
                                            bytes = stream.Read(data, 0, data.Length);
                                        }
                                        catch (System.IO.IOException er)
                                        {
                                            DisConnect();
                                        }
                                        if (bytes > 0)
                                        {
                                            //这是处理收到帧
                                            running = fFrameMgr.Handler(bytes, data);
                                        }
                                        else
                                        {
                                            //远端客户主动断开socket
                                            running = false;
                                        }

                                    }
                                }
                                catch (ThreadAbortException er)
                                {
                                    AddItemToList(listBox1, string.Format(DateTime.Now.ToString() + " 连接{0}:{1}未成功或已取消", ip, port));
                                    receiveThread = null;
                                }
                                catch (Exception err)
                                {
                                    AddItemToList(listBox1, DateTime.Now.ToString() + " " + err.Message);
                                }
                            }));
                            receiveThread.Start();
                        }
                        else
                        {
                            if (stream != null) stream.Close();
                            receiveThread.Abort();
                            receiveThread = null;
                        }
                        //-----------------------------------------方式0--------------------------------------

                    }
                    catch (ThreadAbortException er)
                    {
                        AddItemToList(listBox1, string.Format(DateTime.Now.ToString() + " 连接{0}:{1}未成功或已取消", ip, port));
                        mThreadConnectToServer = null;
                    }
                    catch (SocketException er)
                    {
                        AddItemToList(listBox1, DateTime.Now.ToString() + " " + er.Message);
                        DisConnect();
                    }
                }));
                mThreadConnectToServer.Start();
                
            }else{

                DisConnect();
                
            }
            

        }

        private void DisConnect()
        {
            SetButtonState(btnConnect, "连接(&C)");
            if (stream != null) stream.Close();
            stream = null;
            if (tcpclient != null) tcpclient.Close();
            tcpclient = null;

            if (mThreadConnectToServer != null) mThreadConnectToServer.Abort();
            mThreadConnectToServer = null;

            if (receiveThread != null) receiveThread.Abort();
            receiveThread = null;
            
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int count = serialPort1.BytesToRead;
            //MessageBox.Show("count:" + count);
            //Debug.WriteLine("count:" + count);

            byte[] tmp = new byte[count];
            serialPort1.Read(tmp, 0, count);

            //for (int i = 0; i < count; i++)
            //{
            //    SetText(string.Format("{0:X2} ", tmp[i]));
            //}
            serialPortFrameMgr.Handler(count, tmp);
        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            this.Invoke(new EventHandler(delegate(object own,EventArgs args){
                this.txtData.Text += text+"\r\n";
            }));
        }

        private void serialPort1_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            this.SetText("[异常]x" + e.EventType.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnumCOMS();
            InitializeIPList();

            ConnectOrDisconnect();
            SetupFrameHandler();
            txtBaudRate.SelectedIndex = txtBaudRate.Items.Count - 1;
        }

        private void InitializeIPList()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry ipHE = Dns.Resolve(hostname);
            foreach (IPAddress item in ipHE.AddressList)
            {
                cmbIP.Items.Add(item.ToString());
            }

            if (cmbIP.Items.Count > 0) cmbIP.SelectedIndex = 0;
        }

        //FileStream fStream = null;
        private void SetupFrameHandler()
        {
            
            serialPortFrameMgr = new FrameManager((ushort)(serialPort1.ReadBufferSize * 2), serialPort1);

            serialPortFrameMgr.OnReceiveFrame += new FrameManager.OnReceiveFrameHandler(
                delegate(FileTransferProtocol.CommandType frameType, int parameter, byte[] commandData)
                {

                    switch (frameType)
                    {
                        case FileTransferProtocol.CommandType.FileName:
                            string fileName = System.Text.Encoding.Default.GetString(commandData);

                            if (fileName != null)
                            {
                                string CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                                string startDir =CurrentPath+ @"update\";
                                if (File.Exists(fileName)) File.Delete(fileName);
                                fStream = File.Open(fileName, FileMode.CreateNew);

                                SetText(DateTime.Now.ToString() + " 准备接收文件：" + fileName + " 文件总长度：" + parameter);
                            }
                            break;

                        case FileTransferProtocol.CommandType.FileData:
                            if (fStream != null && commandData != null && commandData.Length > 0)
                            {
                                fStream.Write(commandData, 0, commandData.Length);
                                //SetText("收到文件数据长度" + commandData.Length);
                            }
                            break;

                        case FileTransferProtocol.CommandType.FileSendFinish:

                            if (fStream != null)
                            {
                                fStream.Close();
                                fStream = null;
                                SetText(DateTime.Now.ToString() + " 收到文件结束信号");
                            }
                            break;
                        case FileTransferProtocol.CommandType.WhatIsYourMNandVersion:
                            byte[] frame = FileTransferProtocol.GetFrameMNVersionLimited(1, "123",serialPort1.BaudRate);
                            serialPort1.Write(frame, 0, frame.Length);
                            break;
                        case FileTransferProtocol.CommandType.Message:
                            string message = System.Text.Encoding.Default.GetString(commandData);
                            this.Invoke(new EventHandler(delegate(object s, EventArgs ar)
                            {
                                this.txtData.Text += message + "\r\n";
                            }));
                            break;
                    }

                    return true;


                });
        }

        

        private void EnumCOMS()
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            this.cmPorts.Items.Clear();
            foreach (string port in ports)
            {
                this.cmPorts.Items.Add(port);
            }

            if (cmPorts.Items.Count > 0)
            {
                cmPorts.SelectedIndex = 0;
            }
        }

        private void btnTestCOMTCP_Click(object sender, EventArgs e)
        {

            CRC.CRC8 crc = new CRC.CRC8();

            int dataStartIndex = FileTransferProtocol.CommandDataIndex;
            //测试：准备发送新的文件
            string filename = @"c:\abc.txt";
            byte[] FileInfo = FileTransferProtocol.GetFrameFileInfo(255 + 256, filename);

            //帧头
            
            Trace.Assert(FileInfo[0] == FileTransferProtocol.FrameHead);
            //crc
            //Trace.Assert(FileInfo[1] == crc);
            //命令字
            Trace.Assert(FileInfo[1] == 0x00);
            //文件长度
            Trace.Assert(FileInfo[2] == 0xFF);
            Trace.Assert(FileInfo[3] == 0x01);
            Trace.Assert(FileInfo[4] == 0);
            Trace.Assert(FileInfo[5] == 0);

            byte[] bs = System.Text.Encoding.Default.GetBytes(filename);
            Trace.Assert(FileInfo[6] == bs[0]);
            Trace.Assert(FileInfo[7] == bs[1]);

            //帧偏移
            Trace.Assert((FileInfo[FileInfo.Length - 4] + FileInfo[FileInfo.Length - 3] * 256) == (FileInfo.Length - 1));
            
            Trace.Assert(FileInfo[FileInfo.Length - 2] == 0x7F );
            Trace.Assert(FileInfo[FileInfo.Length - 1] == 0xFF);

            byte[] data = new byte[] { 0x01, 0x02, 0xFF, 0xEF, 0xCC, 0xDD, 0x00, 0xAA };

            byte[] FileData = FileTransferProtocol.GetFrameFileData(0x01AABBFF, data);

            //帧头
            Trace.Assert(FileData[0] == FileTransferProtocol.FrameHead);
            //帧偏移
            Trace.Assert((FileData[FileData.Length - 3] * 256 + FileData[FileData.Length - 4]) == (FileData.Length - 1));
            //命令字
            Trace.Assert(FileData[1] == 0x1);

            //文件偏移
            Trace.Assert(FileData[2] == 0xFF);
            Trace.Assert(FileData[3] == 0xBB);
            Trace.Assert(FileData[4] == 0xAA);
            Trace.Assert(FileData[5] == 0x01);

            //文件数据
            Trace.Assert(FileData[6] == 0x01);
            Trace.Assert(FileData[7] == 0x02);
            Trace.Assert(FileData[8] == 0xFF);
            Trace.Assert(FileData[9] == 0xEF);

            Trace.Assert(FileData[10] == 0xCC);
            Trace.Assert(FileData[11] == 0xDD);

            //crc
            //byte[] x = new byte[] { 0x02, 0xFF, 0xBB, 0xAA, 0x01, 0x01, 0x02, 0xFF, 0xEF, 0xCC, 0xDD, 0x00, 0xAA, FileData[FileData.Length - 3], FileData[FileData.Length - 2] };

            //Trace.Assert(FileData[1] == crc.Crc(x, 0, x.Length));
            Trace.Assert(FileData[FileData.Length - 1] == 0x7F);

            //文件发送完毕
            byte[] f = FileTransferProtocol.GetFrameFileSendFinish();

            //帧头
            Trace.Assert(f[0] == FileTransferProtocol.FrameHead);
            //帧偏移
            Trace.Assert((f[f.Length - 3] * 256 + f[f.Length - 4]) == f.Length - 1);
            //命令字
            Trace.Assert(f[1] == 0x02);
            //crc
            //Trace.Assert(f[1] == crc.Crc(f, dataStartIndex, f.Length - 3));
            Trace.Assert(f[f.Length - 1] == 0x7F);


            //表示可以新的文件
            byte[] a = FileTransferProtocol.GetFrameWhatIsYourMNandVersion();
            TestCmdFrame(a, 0xFD, crc);

            //表示已经接收数据
            //byte[] Ac = FileTransferProtocol.GetFrameMNAndVersion(1,"123");
            //TestCmdFrame(Ac, 0xFE, crc);

            //表示已经保存文件完毕
            byte[] s = FileTransferProtocol.GetFrameCloseSocket();

            TestCmdFrame(s, 0xFF, crc);



        }

        private void TestCmdFrame(byte[] a, int cmd, CRC.CRC8 crc)
        {
            int dataStartIndex = FileTransferProtocol.CommandDataIndex;
            //帧头
            Trace.Assert(a[0] == FileTransferProtocol.FrameHead);
            //帧偏移
            Trace.Assert((a[a.Length - 3] * 256 + a[a.Length - 4]) == a.Length - 1);
            //命令字
            Trace.Assert(a[1] == cmd);
            //crc
            //Trace.Assert(a[1] == crc.Crc(a, dataStartIndex, a.Length - 3));
            Trace.Assert(a[a.Length - 1] == 0x7F);
        }

        private void btnSelFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            foreach (string i in openFileDialog1.FileNames)
            {
                txtFileName.Text += i + ";\r\n";
            }
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            //byte[] bytes = System.Text.Encoding.Default.GetBytes(txtSend.Text+"\r");
            

            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = cmPorts.SelectedItem.ToString();
                    serialPort1.Open();

                }
                new Thread(new ThreadStart(delegate()
                {

                    try
                    {
                        string filepath = @"d:\EnumCOMS.exe";

                        FileStream fs = File.Open(filepath, FileMode.Open);

                        //FileTransferProtocol ftp = new FileTransferProtocol();


                        byte[] fi = FileTransferProtocol.GetFrameFileInfo((int)fs.Length, System.IO.Path.GetFileName(filepath));
                        serialPort1.DiscardOutBuffer();
                        serialPort1.Write(fi, 0, fi.Length);

                        int bufferSize = serialPort1.WriteBufferSize / 2;
                        Byte[] bytes = new Byte[bufferSize];
                        //byte[] bytes = new byte[] { 0x30, 0x31, 0x32, 0x39 ,0x00 };


                        int fullTime = (int)(fs.Length / (long)bufferSize);
                        int mod = (int)(fs.Length % (long)bufferSize);
                        Byte[] modBytes = new Byte[mod];


                        for (int i = 0; i < fullTime; i++)
                        {
                            fs.Read(bytes, 0, bufferSize);
                            byte[] data = FileTransferProtocol.GetFrameFileData(i * bufferSize, bytes);
                            serialPort1.Write(data, 0, data.Length);
                        }
                        if (mod != 0)
                        {
                            fs.Read(modBytes, 0, mod);
                            byte[] data = FileTransferProtocol.GetFrameFileData(fullTime * bufferSize, modBytes);
                            serialPort1.Write(data, 0, data.Length);
                        }


                        fs.Close();

                        byte[] ff = FileTransferProtocol.GetFrameFileSendFinish();
                        serialPort1.Write(ff, 0, ff.Length);
                    }
                    catch (Exception er)
                    {
                        Trace.TraceError(er.Message);
                    }

                    MessageBox.Show("OK");

                })).Start();
            }
            catch (Exception er)
            {
                this.txtData.Text += er.Message;
            }

        }

        private void btnTestFrameHandler_Click(object sender, EventArgs e)
        {

        }



        private void btnTestGetFrameLength_Click(object sender, EventArgs e)
        {


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = FileTransferProtocol.GetFrameMNVersionLimited(int.Parse(txtSend.Text), txtMN.Text,-1);
                serialPort1.Write(data, 0, data.Length);
            }
            catch (Exception er)
            {

            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectOrDisconnect();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisConnect();
        }

        private void txtData_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
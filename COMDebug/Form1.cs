using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using yanzisoft;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Reflection;

namespace CECOMDebug
{
    public partial class Form1 : Form
    {
        delegate void SetTextCallback(string text);
        private FrameManager serialPortFrameMgr = null;
        public Form1()
        {
            InitializeComponent();

        }

        private string GetLineString(string txt)
        {
            return txt += "\r\n";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen) serialPort1.Close();
                serialPort1.ReadBufferSize = FileTransferProtocol.MaxFrameLength;
                serialPort1.WriteBufferSize = FileTransferProtocol.MaxFrameLength;
                serialPort1.BaudRate = int.Parse(txtBaudRate.Text);
                serialPort1.PortName = cmPorts.SelectedItem.ToString();
                serialPort1.Open();
                txtData.Text += GetLineString("打开端口:" + cmPorts.SelectedItem.ToString());

            }
            catch (Exception er)
            {
                txtData.Text += GetLineString("[异常]" + ":" + er.Message);
            }
        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            this.Invoke(new EventHandler(delegate(object own, EventArgs args)
            {
                this.txtData.Text += text + "\r\n";
            }));
        }

        FrameManager fFrameMgr = null;
        //FileTransferProtocol ftp = new FileTransferProtocol();
        TcpClient tcpclient = null;
        private NetworkStream stream = null;
        Thread receiveThread = null;
        FileStream fStream = null;
        Thread mThreadConnectToServer = null;
        private void ConnectOrDisconnect()
        {
            var ip = cmbIP.Text;
            var port = int.Parse(txtPort.Text);
            if (mThreadConnectToServer == null )
            {

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
                            delegate(/*object _stream,*/ FileTransferProtocol.CommandType frameType, int parameter, byte[] commandData)
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
                                            string CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                                            string startDir = CurrentPath + @"\update\";
                                            if (!Directory.Exists(startDir)) Directory.CreateDirectory(startDir);

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
                                            AddItemToList(listBox1, string.Format("收到 第{0}块 数据长度:{1} 服务器端分块号:{2} ", revFilePartCount, commandData.Length, parameter));
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
                                        byte[] frame = FileTransferProtocol.GetFrameWhatIsYourMNandVersion();
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
                        if (receiveThread == null )
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

            }
            else
            {

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
            //MessageBox.Show("count:"+count);

            byte[] tmp = new byte[count];
            serialPort1.Read(tmp, 0, count);

            serialPortFrameMgr.Handler(count, tmp);
        }

        private int FindValueIndex(byte[] data, int startIndex, byte findValue)
        {
            int rtn = -1;
            for (int i = startIndex; i < data.Length; i++)
            {
                if (data[i] == findValue)
                {
                    rtn = i;
                    break;
                }
            }
            return rtn;
        }

        private void serialPort1_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {

        }

        private void serialPort1_PinChanged(object sender, System.IO.Ports.SerialPinChangedEventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //serialPort1.WriteLine(txtSend.Text);
            //FileTransferProtocol x = new FileTransferProtocol();

            //byte[] b = x.GetFrameFileInfo(@"c:\text.txt".Length, @"c:\text.txt");
            //serialPort1.Write(b, 0, b.Length);
            //byte[] c = x.GetFrameFileData(255 + 1 + 2 + 4, new byte[] { 0x01, 0x02, 0x03, 0xEE, 0xFF });
            //serialPort1.Write(c, 0, c.Length);
            //byte[] d = x.GetFrameFileSendFinish();
            //serialPort1.Write(d, 0, d.Length);

            byte[] data = FileTransferProtocol.GetFrameMessage(txtSend.Text);
            serialPort1.Write(data, 0, data.Length);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnumCOMS();
            InitializeIPList();

            ConnectOrDisconnect();
            SetupFrameHandler();
        }

        //FileStream fStream = null;
        private void SetupFrameHandler()
        {
            
            serialPortFrameMgr = new FrameManager((ushort)(serialPort1.ReadBufferSize * 3), serialPort1);

            serialPortFrameMgr.OnReceiveFrame += new FrameManager.OnReceiveFrameHandler(
                delegate(/*object own,*/ FileTransferProtocol.CommandType frameType, int parameter, byte[] commandData)
                {
                    //SerialPort serialPort = (SerialPort)own;

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
                                string CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                                string startDir = CurrentPath + @"\update\";
                                if (!Directory.Exists(startDir)) Directory.CreateDirectory(startDir);
                                if (File.Exists(startDir + fileName)) File.Delete(startDir + fileName);
                                fStream = File.Open(startDir + fileName, FileMode.CreateNew);

                                SetText(DateTime.Now.ToString() + " 准备接收文件：" + fileName + " 文件总长度：" + parameter);
                            }
                            break;

                        case FileTransferProtocol.CommandType.FileData:
                            if (fStream != null && commandData != null && commandData.Length > 0)
                            {
                                fStream.Write(commandData, 0, commandData.Length);
                                SetText("收到文件数据长度" + commandData.Length);
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
                            //byte[] frame = FileTransferProtocol.GetFrameMNAndVersion(1, "456");
                            byte[] frame = FileTransferProtocol.GetFrameWhatIsYourMNandVersion();
                            serialPort1.Write(frame, 0, frame.Length);
                            break;
                        case FileTransferProtocol.CommandType.Message:
                            string message = System.Text.Encoding.Default.GetString(commandData, 0, commandData.Length);
                            this.Invoke(new EventHandler(delegate(object s, EventArgs ar)
                            {
                                this.txtData.Text += message + "\r\n";
                            }));
                            break;
                    }

                    return true;


                });
        }

        private void InitializeIPList()
        {
            string hostname = Dns.GetHostName();
            //IPHostEntry ipHE = Dns.Resolve(hostname);
            IPHostEntry ipHE = Dns.GetHostEntry(hostname);
            foreach (IPAddress item in ipHE.AddressList)
            {
                cmbIP.Items.Add(item.ToString());
            }

            if (cmbIP.Items.Count > 0) cmbIP.SelectedIndex = 0;
        }

        private void EnumCOMS()
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            this.cmPorts.Items.Clear();
            foreach (string port in ports)
            {
                this.cmPorts.Items.Insert(0, port);
            }

            cmPorts.SelectedIndex = 0;
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
                        
                        listbox.SelectedIndex = listbox.Items.Count - 1;
                        listbox.Update();
                    }));
                }
                catch (Exception er)
                {

                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            ConnectOrDisconnect();
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

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            DisConnect();
        }
    }
}
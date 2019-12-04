using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using yanzisoft;

namespace UpdateServer
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private string mIniFileName =Application.StartupPath +  @"\UpdateServer.ini";
        private List<string> fileNameList = new List<string>();
        private List<NetworkStream> streamList = new List<NetworkStream>();
        private List<Thread> threadList = new List<Thread>();
        private string[] section = new string[] { "base" };
        //private FileTransferProtocol ftp = new FileTransferProtocol();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeIPList();
            InitializeVersionAndMN();
            //Form1.CheckForIllegalCrossThreadCalls = false;
            LoadFiles(InitializeRootDir());
        }



        private void InitializeVersionAndMN()
        {
            
            StringBuilder bufferVersion = new StringBuilder(15);
            GetPrivateProfileString(section[0], "version", DateTime.Now.ToString("yyyyMMdd")+"00", bufferVersion, 15, mIniFileName);

            txtVersion.Text = bufferVersion.ToString();
            //最多200个MN
            StringBuilder MNList = new StringBuilder(4000);
            GetPrivateProfileString(section[0], "MN", "", MNList, 4000, mIniFileName);

            new Thread(delegate(object obj)
            {
                try
                {

                    Clear(lstMN);
                    string[] MNS=MNList.ToString().Split(',');
                    foreach (string m in MNS)
                    {
                        AddItemToList(lstMN,m);
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message, er.Source);
                }
            }).Start();
        }

        private string InitializeRootDir()
        {
            StringBuilder buffer = new StringBuilder(255);
            int rtn = GetPrivateProfileString(section[0], "rootdir", @"C:\Users\Public\Desktop", buffer, 255, mIniFileName);
            if (rtn > 0)
            {
                folderBrowserDialog1.SelectedPath = buffer.ToString();
                txtRootDir.Text = folderBrowserDialog1.SelectedPath;
            }

            return buffer.ToString();
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

        private void btnSelectRootDir_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                txtRootDir.Text = folderBrowserDialog1.SelectedPath;

                if (!txtRootDir.Text.EndsWith("\\")) txtRootDir.Text += "\\";
                WritePrivateProfileString(section[0], "rootdir", txtRootDir.Text, mIniFileName);
                string path = txtRootDir.Text;

                LoadFiles(path);
            }
        }

        private void LoadFiles(string path)
        {
            
            new Thread(delegate(object obj)
            {
                try
                {
                    Clear(listBoxFiles);
                    fileNameList.Clear();
                    Application.DoEvents();                 
                    string[] fs = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
                    foreach (string f in fs)
                    {
                        AddItemToList(listBoxFiles,f.Substring(txtRootDir.Text.Length));
                        fileNameList.Add(f.Substring(txtRootDir.Text.Length));
                        Application.DoEvents();
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message, er.Source);
                }
            }).Start();
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

        public void Clear(ListBox listbox)
        {
            this.Invoke(new EventHandler(delegate(Object own, EventArgs args)
            {
                listbox.Items.Clear();
                listbox.Update();
            }));
        }

        private Thread mUpdateServiceThread = null;
        TcpListener server = null;
        private void btnStart_Click(object sender, EventArgs e)
        {

            //开启服务
            if (mUpdateServiceThread == null)
            {
                mUpdateServiceThread = new Thread(delegate(object obj)
                {

                    try
                    {
                        //EventLog eventLog = new EventLog(Application.ProductName , ".", "DebugSource");

                        //eventLog.Source = "DebugSource";

                        object[] ipPortRootdirFilenamelist = (object[])obj;
                        // Set the TcpListener on port .
                        IPAddress localAddr = IPAddress.Parse(ipPortRootdirFilenamelist[0].ToString());
                        Int32 port = int.Parse(ipPortRootdirFilenamelist[1].ToString());
                        //string rootdir = (string)ipPortRootdirFilenamelist[2];
                        //ListBox.ObjectCollection Items = (ListBox.ObjectCollection)ipPortRootdirFilenamelist[3];

                        // TcpListener server = new TcpListener(port);
                        server = new TcpListener(localAddr, port);

                        // Start listening for client requests.
                        server.Start();

                        this.Invoke(new EventHandler(delegate(Object own, EventArgs args)
                        {
                            this.btnStart.Text = "停止服务";
                            this.btnStart.Update();
                        }));

                        // Enter the listening loop.
                        int count = 0;
                        //eventLog.WriteEntry(DateTime.Now.ToString() + " 更服务端口已开启。");
                        AddItemToList(listBoxLog, DateTime.Now.ToString() + " 更服务端口已开启。");

                        while (true)
                        {
                            //Console.Write("Waiting for a connection... ");

                            // Perform a blocking call to accept requests.
                            // You could also user server.AcceptSocket() here.
                            TcpClient client = server.AcceptTcpClient();
                            count++;
                            this.Invoke(new EventHandler(delegate(Object own, EventArgs args)
                            {
                                this.lblOnlineCount.Text = string.Format("在线客户数量:{0}", count);
                                this.lblOnlineCount.Update();
                            }));

                            //AddItemToList(listBoxLog,"在线客户端数量：" + count);
                            Thread NewClientThread = new Thread(delegate(object clientobj)
                            {

                                TcpClient tcpclient = null;
                                try
                                {
                                    object[] clientRootdirFilenamelist = (object[])clientobj;
                                    tcpclient = (TcpClient)clientRootdirFilenamelist[0];
                                    string rootdir = (string)clientRootdirFilenamelist[1];
                                    List<string> filenamelist = (List<string>)clientRootdirFilenamelist[2];

                                    NetworkStream stream = tcpclient.GetStream();
                                    lock (streamList)
                                    {
                                        streamList.Add(stream);
                                    }
                                    int MaxLength = FileTransferProtocol.MaxFrameLength - FileTransferProtocol.FrameStructLength;//64*1024;0xFFFF+1;64K;
                                    Byte[] data = new Byte[MaxLength];

                                    FrameManager fm = new FrameManager(MaxLength, tcpclient);
                                    //收到客户端发来的帧数据
                                    fm.OnReceiveFrame += new FrameManager.OnReceiveFrameHandler(delegate(FileTransferProtocol.CommandType frameType, int parameter, byte[] commandData)
                                    {
                                        //TcpClient tcpclient = (TcpClient)own;
                                        //NetworkStream stream = tcpclient.GetStream();

                                        switch (frameType)
                                        {
                                            //收到远端客户端关闭帧信号
                                            case FileTransferProtocol.CommandType.CloseSocket:
                                                if (tcpclient != null)
                                                {
                                                    tcpclient.Close();
                                                    Debug.WriteLine("CurrentThread:{0} 准备退出", Thread.CurrentThread.Name);

                                                }
                                                //是否继续帧分析处理
                                                return false;
                                            //如果是对方应答服务端发送的FileTransferProtocol.CommandType.WhatIsYourMNandVersion的指令
                                            case FileTransferProtocol.CommandType.MNAndVersion:
                                                //如果在MN例表中 且 version<服务端version

                                                string MN = System.Text.Encoding.Default.GetString(commandData,0,FileTransferProtocol.MNLength);
                                                int limited = BitConverter.ToInt32(commandData, FileTransferProtocol.MNLength);
                                                if (parameter < int.Parse(txtVersion.Text))
                                                {

                                                    if (lstMN.Items.Contains(MN))
                                                    {
                                                        SendFileToNet(stream, rootdir, filenamelist, MN, limited);
                                                    }
                                                    else
                                                    {
                                                        byte[] frame = FileTransferProtocol.GetFrameMessage(string.Format("此数采仪{0}未在可更新列表中。", MN));
                                                        stream.Write(frame, 0, frame.Length);
                                                    }

                                                }
                                                else
                                                {
                                                    byte[] frame = FileTransferProtocol.GetFrameMessage(string.Format("此数采仪{0}程序已经是最新版了。", MN));
                                                    stream.Write(frame, 0, frame.Length);
                                                }
                                                break;


                                        }
                                        //是否继续帧分析处理
                                        Debug.WriteLine("CurrentThread:{0} 继续运行", Thread.CurrentThread.Name);
                                        return true;
                                    });

                                    bool running = true;
                                    while (running)
                                    {
                                        Debug.WriteLine(string.Format("{0}", Thread.CurrentThread.ThreadState));

                                        Int32 bytes = stream.Read(data, 0, data.Length);
                                        if (bytes > 0)
                                        {
                                            running = fm.Handler(bytes, data);
                                        }
                                        else
                                        {
                                            //远端客户主动断开socket
                                            running = false;
                                        }

                                    }
                                }
                                catch (Exception er)
                                {
                                    //eventLog.WriteEntry(DateTime.Now.ToString() + "异常:" + er.Source + " " + er.Message);
                                    AddItemToList(listBoxLog, DateTime.Now.ToString() + "异常:" + er.Source + " " + er.Message);
                                }
                                finally
                                {

                                    tcpclient.Close();
                                }

                                lock ((object)count)
                                {
                                    count--;
                                    //AddItemToList(listBoxLog,"在线客户端数量：" + count);
                                    try
                                    {
                                        this.Invoke(new EventHandler(delegate(Object own, EventArgs args)
                                        {
                                            this.lblOnlineCount.Text = string.Format("在线客户数量:{0}", count);
                                            this.lblOnlineCount.Update();
                                        }));
                                    }
                                    catch (Exception er)
                                    {

                                    }
                                }
                            });
                            NewClientThread.Name = "读线程" + NewClientThread.ManagedThreadId;
                            threadList.Add(NewClientThread);
                            NewClientThread.Start(new object[] { client, ipPortRootdirFilenamelist[2]/*rootdir*/, ipPortRootdirFilenamelist[3]/*listBox1.Items*/ });
                            //Console.WriteLine("Connected!");

                        }

                    }
                    catch (SocketException er)
                    {
                        //Console.WriteLine("网络异常: {0}", e);
                        //MessageBox.Show(er.Message, "网络异常");
                        
                        //如果是server.Stop()停止服务引起的则忽略错误的显示
                        if (er.SocketErrorCode == SocketError.Interrupted)
                        {

                        }
                        else
                        {
                            AddItemToList(listBoxLog, DateTime.Now.ToString() + " " + er.Source + " " + er.Message);
                        }
                    }
                    finally
                    {
                        // Stop listening for new clients.
                        server.Stop();
                    }

                });
                mUpdateServiceThread.Name = "更新程序监听主线程";
                mUpdateServiceThread.Start(new object[] { cmbIP.Text, txtPort.Text,txtRootDir.Text, fileNameList });
            }
            else//关闭服务
            {
                server.Stop();
                AbortUpdateServiceThread();
                 
            }
            

        }

        /// <summary>
        /// 使用网络向外部发文件数据
        /// </summary>
        /// <param name="stream">网络数据流对象</param>
        /// <param name="rootdir">上传起始目录</param>
        /// <param name="filenamelist">文件列表（含相对路径）</param>
        /// <param name="MN">数采仪MN</param>
        /// <param name="limited">最大允许传输率</param>
        private void SendFileToNet(NetworkStream  stream, string rootdir, List<string> filenamelist,string MN,int limited)
        {
            FileStream fs = null;

            try
            {
                foreach (string filename in filenamelist)
                {

                    AddItemToList(listBoxLog, string.Format("{0} 准备传送{1}文件给数采仪:{2}", DateTime.Now.ToString(), rootdir + filename, MN));
                    fs = File.Open(rootdir + filename.ToString(), FileMode.Open,FileAccess.Read,FileShare.Read);

                    AddItemToList(listBoxLog, string.Format("数采仪:{1} 文件长度：{0}", fs.Length, MN));
                    byte[] fi = FileTransferProtocol.GetFrameFileInfo((int)fs.Length, filename.ToString());//System.IO.Path.GetFileName()
                    //AddItemToList(listBoxLog,string.Format("帧数据{0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2} {0:X2}", fi[0], fi[1], fi[2], fi[3], fi[4], fi[5], fi[6], fi[7], fi[8], fi[9], fi[10], fi[11], fi[12], fi[13], fi[14], fi[15]));
                    stream.Write(fi, 0, fi.Length);

                    int bufferSize = FileTransferProtocol.MaxFrameLength - FileTransferProtocol.FrameStructLength;
                    if (limited > 0)
                    {
                        bufferSize = limited / 10;//除以10表示由bit/s转为Byte/s
                    }
                    Byte[] bytes = new Byte[bufferSize];
                    //byte[] bytes = new byte[] { 0x30, 0x31, 0x32, 0x39 ,0x00 };


                    int fullTime = (int)(fs.Length / (long)bufferSize);
                    int mod = (int)(fs.Length % (long)bufferSize);
                    Byte[] modBytes = new Byte[mod];
                    AddItemToList(listBoxLog, string.Format("数采仪:{3} 分块：{0}*{1},尾块：{2}", bufferSize, fullTime, mod, MN));

                    for (int i = 0; i < fullTime; i++)
                    {
                        int rBytes = fs.Read(bytes, 0, bufferSize);
                        AddItemToList(listBoxLog, string.Format("正在传送第{0}块给数采仪:{1}", i+1, MN));
                        //byte[] data = FileTransferProtocol.GetFrameFileData(i * bufferSize, bytes);
                        byte[] data = FileTransferProtocol.GetFrameFileData(i+1, bytes);
                        stream.Write(data, 0, data.Length);
                    }
                    if (mod != 0)
                    {
                        int rBytes = fs.Read(modBytes, 0, mod);
                        AddItemToList(listBoxLog, string.Format("正在传送尾块给数采仪:{0}", MN));
                        //byte[] data = FileTransferProtocol.GetFrameFileData(fullTime * bufferSize, modBytes);
                        byte[] data = FileTransferProtocol.GetFrameFileData(-1, modBytes);
                        stream.Write(data, 0, data.Length);
                    }

                    byte[] ff = FileTransferProtocol.GetFrameFileSendFinish();

                    stream.Write(ff, 0, ff.Length);
                    fs.Close();
                    fs = null;
                    AddItemToList(listBoxLog, string.Format("{0} {1} 给数采仪:{2}文件传送完毕。", DateTime.Now.ToString(), filename, MN));
                }
            }
            catch (Exception er)
            {
                AddItemToList(listBoxLog, string.Format("数采仪:{0} {1} {2}", MN, er.Source, er.Message));
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void AbortAllTcpclientThread()
        {
            //EventLog eventLog = new EventLog(Application.ProductName, ".", "DebugSource");

            //eventLog.Source = "DebugSource";
            foreach (NetworkStream t in streamList)
            {
                t.Close();
            }

            foreach (Thread t in threadList)
            {
                t.Abort();
            }
        }


        private void AbortUpdateServiceThread()
        {
            try
            {
                AddItemToList(listBoxLog, DateTime.Now.ToString() + " 更服务端口已关闭。");
                this.Invoke(new EventHandler(delegate(Object own, EventArgs args)
                {
                    this.btnStart.Text = "启动服务";
                    this.btnStart.Update();
                }));

                if (mUpdateServiceThread != null)
                {
                    mUpdateServiceThread.Abort();
                    mUpdateServiceThread = null;
                    try
                    {
                    TcpClient tmp = new TcpClient(cmbIP.Text, int.Parse(txtPort.Text));
                    }
                    catch
                    {

                    }
                    
                }
                
            }
            catch (Exception er)
            {
                AddItemToList(listBoxLog, string.Format("异常:{0} {1}", er.Source, er.Message));
            }
        }

        private void btnRequestClientUpdate_Click(object sender, EventArgs e)
        {
            byte[] frame= FileTransferProtocol.GetFrameWhatIsYourMNandVersion();
            foreach (NetworkStream stream in streamList)
            {
                try
                {
                    stream.Write(frame, 0, frame.Length);
                }
                catch
                {

                }
            }
        }

        private void btnTestMN_Click(object sender, EventArgs e)
        {
            byte[] frame = FileTransferProtocol.GetFrameMNVersionLimited(255, "1234",-1);

            TcpClient tcpclient = new TcpClient(cmbIP.Text,int.Parse(txtPort.Text));

            Stream s = tcpclient.GetStream();
            s.Write(frame, 0, frame.Length);
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AbortAllTcpclientThread();
            AbortUpdateServiceThread();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            listBoxLog.Items.Clear();
        }

    }
}
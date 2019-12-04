using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Reflection;
namespace yanzisoft
{
    public class CSerialPortClient : IClient
    {
        #region IClient 成员

        public event ClientHandler.FileTransferStateMessageHandler OnFileTransferStateMessage;

        public event ClientHandler.OnReceiveFrameHandler OnReceiveFrame;

        #endregion
        private SerialPort mSerialPort = null;
        private FrameManager serialPortFrameMgr = null;
        private FileStream fStream = null;
        private string mThisDeviceMN=null;
        private int mVersion = 0;
        public CSerialPortClient(string portName, int baudRate, string thisDeviceMN, int version)
        {
            mSerialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
            mVersion = version;
            mThisDeviceMN = thisDeviceMN;
            Initialization();
        }

        public CSerialPortClient(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, string thisDeviceMN, int version)
        {
            mSerialPort = new SerialPort(portName, baudRate,parity,dataBits,stopBits);
            mVersion = version;
            mThisDeviceMN = thisDeviceMN;
            Initialization();
        }

        public void Open()
        {
            if (mSerialPort.IsOpen) mSerialPort.Close();
            mSerialPort.Open();
        }

        public void Close()
        {
            mSerialPort.Close();
        }

        public void Initialization()
        {
            mSerialPort.DataReceived += new SerialDataReceivedEventHandler(mSerialPort_DataReceived);
            serialPortFrameMgr = new FrameManager(mSerialPort.ReadBufferSize * 3, mSerialPort);

            serialPortFrameMgr.OnReceiveFrame += new FrameManager.OnReceiveFrameHandler(serialPortFrameMgr_OnReceiveFrame);
        }

        bool serialPortFrameMgr_OnReceiveFrame(FileTransferProtocol.CommandType frameType, int parameter, byte[] commandData)
        {
            switch (frameType)
            {
                case FileTransferProtocol.CommandType.FileName:
                    string fileName = System.Text.Encoding.Default.GetString(commandData);

                    if (fileName != null)
                    {
                        string CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                        string startDir = CurrentPath + @"update\";
                        if (File.Exists(fileName)) File.Delete(fileName);
                        fStream = File.Open(fileName, FileMode.CreateNew);

                        //SetText(DateTime.Now.ToString() + " 准备接收文件：" + fileName + " 文件总长度：" + parameter);
                        if (OnFileTransferStateMessage != null) OnFileTransferStateMessage(DateTime.Now.ToString() + " 准备接收文件：" + fileName + " 文件总长度：" + parameter);
                    }
                    break;

                case FileTransferProtocol.CommandType.FileData:
                    if (fStream != null && commandData != null && commandData.Length > 0)
                    {
                        fStream.Write(commandData, 0, commandData.Length);
                        //SetText("收到文件数据长度" + commandData.Length);
                        if (OnFileTransferStateMessage != null) OnFileTransferStateMessage("收到文件数据长度" + commandData.Length);
                    }
                    break;

                case FileTransferProtocol.CommandType.FileSendFinish:

                    if (fStream != null)
                    {
                        fStream.Close();
                        fStream = null;
                        if (OnFileTransferStateMessage != null) OnFileTransferStateMessage(DateTime.Now.ToString() + " 收到文件结束信号");
                        //SetText(DateTime.Now.ToString() + " 收到文件结束信号");
                    }
                    break;
                case FileTransferProtocol.CommandType.WhatIsYourMNandVersion:
                    byte[] frame = FileTransferProtocol.GetFrameMNVersionLimited(mVersion,mThisDeviceMN, mSerialPort.BaudRate);
                    mSerialPort.Write(frame, 0, frame.Length);
                    if (OnFileTransferStateMessage != null) OnFileTransferStateMessage(string.Format("服务端索要MN及版本，本机以应答:MN={0} 版本:{1}","123",1));
                    break;
                case FileTransferProtocol.CommandType.Message:
                    string message = System.Text.Encoding.Default.GetString(commandData);
                    if (OnFileTransferStateMessage != null) OnFileTransferStateMessage(message);
                    //this.Invoke(new EventHandler(delegate(object s, EventArgs ar)
                    //{
                    //    this.txtData.Text += message + "\r\n";
                    //}));
                    break;
                default:
                    if (OnFileTransferStateMessage != null) OnReceiveFrame(frameType, parameter, commandData);
                    break;
            }

            return true;
        }



        void mSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int count = mSerialPort.BytesToRead;

            byte[] tmp = new byte[count];
            mSerialPort.Read(tmp, 0, count);

            //for (int i = 0; i < count; i++)
            //{
            //    SetText(string.Format("{0:X2} ", tmp[i]));
            //}
            serialPortFrameMgr.Handler(count, tmp);         
        }

        public SerialPort SerialPort
        {
            get
            {
                return mSerialPort;
            }
        }


    }
}

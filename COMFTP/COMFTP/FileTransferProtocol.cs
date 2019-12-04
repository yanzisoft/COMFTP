/// <summary>
/// 串口文件传送协议 v4
/// 作者：晏增翔
/// 建立：2014/8/21
/// </summary>

using System;
using System.Diagnostics;
using System.ComponentModel;

namespace yanzisoft
{
    public static class FileTransferProtocol
    {
        public enum CommandType
        {
            [Description("向客户端发送文件名和文件长度")]
            FileName=0x00,
            [Description("向客户端发送文件数据")]
            FileData,
            [Description("向客户端发送文件结束信号")]
            FileSendFinish,
            [Description("字符串信息")]
            Message=0x10,
            [Description("发送DOS命令")]
            SendDOSCommand = 0xD0,
            [Description("请求客户端提供MN和版本号")]
            WhatIsYourMNandVersion=0xFD,
            [Description("应答服务端提供当前客户的MN和版本号")]
            MNAndVersion,
            [Description("请求服务端关闭套接字")]
            CloseSocket
        }


        public static ushort CommandTypeIndex = 1;  //命令字
        public static ushort ParameterIndex = 2;	//扩展参数 如(文件长度起始下标,文件数据偏移)
        //内部数据起始位置
        public static ushort DataStartIndex = CommandTypeIndex;

        //有parameter参数的数据起始下标
        public static ushort CommandDataIndex = 6;
        //从帧尾到Offset的偏移
        public static ushort FromFrametailToOffset = 4;

        public static byte FrameHead = 0xFF;
        public static byte[] FrameTail = new byte[] { 0x55,0xAA,0x7F };

        //如是帧长frameLength=1024; 则：[0000 0100 0000 0000] 即 0x0400;
        //new byte [] { (frameLength & 0xFF),(frameLength>>8) } 也就是 {0x00,0x04}=0x0400


        //帧长度最大2^16个字节64K字节
        public static int MaxFrameLength = 0xFFFF + 1;
        //帧长度最小＝帧头1字节+命令字1字节+帧Offset 2字节+帧尾3字节	
        public static int MinFrameLength = 7;
        //帧Offset 2字节
        public static int OffsetBytes = 2;

        //帧结构长度＝帧头1字节+命令字1字节+parameter参数4字节+帧Offset 2字节+帧尾3字节	
        public static int FrameStructLength = 11;
        //  0	    1	    2	    3	    4	    5	    6	    7			
        //  7EH	    01H	    XXH     XXH     XXH     XXH 	… …	XXH XXH	    55H	 AAH 7FH
        //  帧头	命令字	文件尺寸（见注1）4字节	        文件名  偏移	    帧尾(3字节)
        /// <summary>
        /// 数采仪设备MN字符串长度
        /// </summary>
        public static byte MNLength = 14;
        /// <summary>
        /// 最大允许传送输率的字节长度
        /// </summary>
        public static byte LimitedLength = 4;

        public static Byte[] GetFrameCommon(CommandType type, int parameter, byte[] data)
        {
            byte[] parts = new byte[] { FrameHead, (Byte)type, (byte)(parameter & 0x000000FF), (byte)((parameter & 0x0000FF00) >> 8), (byte)((parameter & 0x00FF0000) >> 16), (byte)(parameter >> 24) };

            byte[] rtn = new byte[parts.Length + data.Length + OffsetBytes + FileTransferProtocol.FrameTail.Length];

            if (rtn.Length > MaxFrameLength)
            {
                throw new Exception("已经超过最大帧长度：" + MaxFrameLength);
            }

            rtn[rtn.Length - 5] = (byte)((rtn.Length - 1) & 0x00FF);
            //parts[2] = (byte)(((rtn.Length - 1) & 0xFF00) >> 8);
            rtn[rtn.Length - 4] = (byte)((rtn.Length - 1) >> 8);


            Copy(parts, 0, rtn, 0, parts.Length);
            Copy(data, 0, rtn, parts.Length, data.Length);


            //CRC.CRC8 crc8 = new CRC.CRC8();
            //crc8.Crc(rtn, DataStartIndex, rtn.Length - 3);
            //byte crc = (byte)(crc8.Value & 0xFF);
            //rtn[1] = crc;

            rtn[rtn.Length - 3] = FrameTail[0];
            rtn[rtn.Length - 2] = FrameTail[1];
            rtn[rtn.Length - 1] = FrameTail[2];

            return rtn;
        }

        public static Byte[] GetFrameCommon(CommandType type)
        {
            byte[] rtn = new byte[] { FrameHead, (Byte)type, 0x00, 0x00, FrameTail[0], FrameTail[1], FrameTail[2] };

            rtn[rtn.Length - 5] = (byte)((rtn.Length - 1) & 0x00FF);
            rtn[rtn.Length - 4] = (byte)((rtn.Length - 1) >> 8);
            Debug.Assert(BitConverter.ToUInt16(rtn, ParameterIndex) == rtn.Length - 1);
            return rtn;

        }

        /// <summary>
        /// 构造包含文件长度与文件名信息的数据帧
        /// </summary>
        /// <param name="fileLength">文件长度</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含数据帧字节数组</returns>
        public static byte[] GetFrameFileInfo(int fileLength, string fileName)
        {

            byte[] bytes = System.Text.Encoding.Default.GetBytes(fileName);

            return GetFrameCommon(CommandType.FileName, fileLength, bytes);
        }

        /// <summary>
        /// 构造包含文件数据包的数据帧
        /// </summary>
        /// <param name="fileOffset">文件数据偏移位置</param>
        /// <param name="data">文件数据的字节数组</param>
        /// <returns>包含数据帧字节数组</returns>
        public static byte[] GetFrameFileData(int fileOffset, byte[] data)
        {
            return GetFrameCommon(CommandType.FileData, fileOffset, data);
        }

        /// <summary>
        /// 构造包含文件已被发送完成的消息数据帧
        /// </summary>
        /// <returns>包含数据帧字节数组</returns>
        public static byte[] GetFrameFileSendFinish()
        {

            return GetFrameCommon(CommandType.FileSendFinish);
        }

        /// <summary>
        /// 构造包含询问客户端方的MN及版本号及 最大允许传输率的帧数据 
        /// </summary>
        /// <returns></returns>
        public static byte[] GetFrameWhatIsYourMNandVersion()
        {
            return GetFrameCommon(CommandType.WhatIsYourMNandVersion);
        }

        /// <summary>
        ///  构造应答服务端方包含本机的MN及版本号及 服务端对该连接允许最大传输率的帧数据 
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="mn">数采仪标识</param>
        /// <param name="Limited">服务端对该数采仪最大允许传输率(如果小于则表示不限制)</param>
        /// <returns></returns>
        public static byte[] GetFrameMNVersionLimited(int version, string mn, int Limited)
        {
            byte[] datas = new byte[MNLength+LimitedLength];
            byte[] limiteds = BitConverter.GetBytes(Limited);
            datas[datas.Length - 4] = limiteds[0];
            datas[datas.Length - 3] = limiteds[1];
            datas[datas.Length - 2] = limiteds[2];
            datas[datas.Length - 1] = limiteds[3];
            byte[] mns=System.Text.Encoding.Default.GetBytes(mn);
            Copy(mns, 0, datas, 0, datas.Length);
            return GetFrameCommon(CommandType.MNAndVersion,version,datas);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static byte[] GetFrameCloseSocket()
        {
            return GetFrameCommon(CommandType.CloseSocket);
        }


        public static byte[] GetFrameMessage(string message)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(message);
            return GetFrameCommon(CommandType.Message,0, bytes);
        }
        // The unsafe keyword allows pointers to be used in the following method.

        public static unsafe void Copy(byte[] source, int sourceOffset, byte[] target,
            int targetOffset, int count)
        {
            // If either array is not instantiated, you cannot complete the copy.
            if ((source == null) || (target == null))
            {
                throw new System.ArgumentException();
            }

            // If either offset, or the number of bytes to copy, is negative, you
            // cannot complete the copy.
            if ((sourceOffset < 0) || (targetOffset < 0) || (count < 0))
            {
                throw new System.ArgumentException();
            }

            // If the number of bytes from the offset to the end of the array is 
            // less than the number of bytes you want to copy, you cannot complete
            // the copy. 
            if ((source.Length - sourceOffset < count) ||
                (target.Length - targetOffset < count))
            {
                throw new System.ArgumentException();
            }

            // The following fixed statement pins the location of the source and
            // target objects in memory so that they will not be moved by garbage
            // collection.
            fixed (byte* pSource = source, pTarget = target)
            {
                // Set the starting points in source and target for the copying.
                byte* ps = pSource + sourceOffset;
                byte* pt = pTarget + targetOffset;

                // Copy the specified number of bytes from source to target.
                for (int i = 0; i < count; i++)
                {
                    *pt = *ps;
                    pt++;
                    ps++;
                }
            }
        }
    }
}
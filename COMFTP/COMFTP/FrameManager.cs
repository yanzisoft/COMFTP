using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace yanzisoft
{
    public class FrameManager
    {

        /// <summary>
        /// 帧接收处理委托对象
        /// </summary>
        /// <param name="frameType">帧类型</param>
        /// <param name="parameter">对帧结构的参数部分</param>
        /// <param name="commandData">数据</param>
        /// <param name="extendData">附加数据</param>
        public delegate bool OnReceiveFrameHandler(FileTransferProtocol.CommandType frameType, int parameter, byte[] commandData);
        public event OnReceiveFrameHandler OnReceiveFrame;

        private byte[] frameBuffer = null;
        private int validBytes = 0;//有效字节数
        private byte frameHead = FileTransferProtocol.FrameHead;
        private int findFrameTailIndex = -1;
        private int mframeBufferSize = 0;

        /// <summary>
        /// 帧管理对象
        /// </summary>
        /// <param name="frameBufferSize">帧缓冲区大小</param>
        public FrameManager(int frameBufferSize)
        {

            if (frameBufferSize >= FileTransferProtocol.MaxFrameLength * 3)
            {
                mframeBufferSize = frameBufferSize;
            }
            else
            {
                mframeBufferSize = FileTransferProtocol.MaxFrameLength * 3;
            }
            frameBuffer = new byte[mframeBufferSize];
            
        }

        /// <summary>
        /// 帧管理对象
        /// </summary>
        /// <param name="frameBufferSize">帧缓冲区大小</param>
        /// /// <param name="extendData">附加数据</param>
        public FrameManager(int frameBufferSize, object sender)
        {
            if (frameBufferSize >= FileTransferProtocol.MaxFrameLength * 3)
            {
                mframeBufferSize = frameBufferSize;
            }
            else
            {
                mframeBufferSize = FileTransferProtocol.MaxFrameLength * 3;
            }
            frameBuffer = new byte[mframeBufferSize];
        }

        /// <summary>
        /// 查找可能的帧尾
        /// </summary>
        /// <param name="data">数据缓冲区</param>
        /// <param name="startIndex">开始偏移</param>
        /// <param name="findValue">需要查找的值</param>
        /// <returns></returns>
        private int FindTailIndex(byte[] data, int startIndex)
        {
            int rtn = -1;
            if (startIndex < 0) startIndex = 0;
            for (int i = startIndex; i < data.Length; i++)
            {
                if (data[i] == FileTransferProtocol.FrameTail[0])
                {
                    rtn = i;
                    break;
                }
            }
            return rtn;
        }

        /// <summary>
        /// 查找可能的帧尾
        /// </summary>
        /// <param name="data">数据缓冲区</param>
        /// <param name="startIndex">开始偏移</param>
        /// <param name="findValue">需要查找的值</param>
        /// <param name="MaxIndex">最大偏移</param>
        /// <returns></returns>
        private int FindTailIndex(byte[] data, int startIndex, int MaxIndex)
        {
            int rtn = -1;
            if (startIndex < 0) startIndex = 0;

            if (data.Length < MaxIndex)
            {
                MaxIndex = data.Length;
            }
            for (int i = startIndex; i < MaxIndex; i++)
            {
                if (data[i] == FileTransferProtocol.FrameTail[0])
                {
                    rtn = i;
                    break;
                }
            }
            return rtn;
        }

        /// <summary>
        /// 查找可能的帧尾
        /// </summary>
        /// <param name="data">数据缓冲区</param>
        /// <param name="startIndex">开始偏移</param>
        /// <param name="findValue">需要查找的值</param>
        /// <param name="MaxIndex">最大偏移</param>
        /// <returns></returns>
        private int FindTailIndexEx(byte[] data, int startIndex, int MaxIndex)
        {
            Debug.WriteLine(string.Format("startIndex={0},MaxIndex={1}", startIndex, MaxIndex));
            int rtn = -1;
            if (startIndex < 0) startIndex = 0;

            if (data.Length < MaxIndex)
            {
                MaxIndex = data.Length;
            }
            for (int i = startIndex; i < MaxIndex - 2; i++)
            {
                if (data[i] == FileTransferProtocol.FrameTail[0] && data[i + 1] == FileTransferProtocol.FrameTail[1] && data[i + 2] == FileTransferProtocol.FrameTail[2])
                {
                    rtn = i + (FileTransferProtocol.FrameTail.Length - 1);
                    break;
                }
            }
            return rtn;
        }

        public bool Handler(int readCount, byte[] readBuffer)
        {
            FileTransferProtocol.Copy(readBuffer, 0, frameBuffer, validBytes, readCount);
            validBytes += readCount;
            Debug.WriteLine("最大可能的帧冲区长度:" + validBytes);
            //如果找到的可能的帧尾
            while ((findFrameTailIndex = FindTailIndexEx(frameBuffer, findFrameTailIndex + 1, validBytes)) > -1 && validBytes >= FileTransferProtocol.MinFrameLength)
            {
                //如果足够长(有效数据长度不小于最小帧帧长度 找到的帧尾的下标需要不小于最小帧的帧尾下标)
                if (findFrameTailIndex >= FileTransferProtocol.MinFrameLength - 1 && findFrameTailIndex < validBytes)
                {
                    //帧头下标=帧尾下标 - 偏移
                    int headIndex = findFrameTailIndex - BitConverter.ToUInt16(frameBuffer, findFrameTailIndex - FileTransferProtocol.FromFrametailToOffset);
                    Debug.WriteLine(string.Format("@headIndex={0}", headIndex));
                    //如果对应下标项是帧头
                    if ((headIndex >= 0 && headIndex < findFrameTailIndex && headIndex < validBytes) && frameBuffer[headIndex] == frameHead)//只有headIndex大于等于0且小于findFrameTailIndex才有效
                    {
                        Debug.WriteLine(string.Format("#headIndex={0}", headIndex));
                        if(headIndex!=0){
                            Debug.WriteLine(string.Format("headIndex={0} frameBuffer=[{1:X2},{2:X2},{3:X2},{4:X2} ] findFrameTailIndex={5}", headIndex, frameBuffer[0], frameBuffer[1], frameBuffer[2], frameBuffer[3], findFrameTailIndex));
                        }
                        //前移帧数据
                        if (headIndex != 0)
                        {
                            FileTransferProtocol.Copy(frameBuffer, headIndex, frameBuffer, 0, validBytes - headIndex);
                            validBytes = validBytes - headIndex;
                            findFrameTailIndex = findFrameTailIndex - headIndex;
                        }
                        //处理数据
                        byte commandType = frameBuffer[FileTransferProtocol.CommandTypeIndex];
                        int frameLength = BitConverter.ToUInt16(frameBuffer, findFrameTailIndex - FileTransferProtocol.FromFrametailToOffset) + 1;
                        findFrameTailIndex = -1;
                        //Debug.WriteLine(string.Format("frameLength={0},validBytes={1},findFrameTailIndex={2},headIndex={3},cmdType={4}", frameLength, validBytes, findFrameTailIndex, headIndex, cmdType));

                        byte[] da = null;
                        if (frameLength > FileTransferProtocol.FrameStructLength)
                        {
                            da = new byte[frameLength - FileTransferProtocol.FrameStructLength];
                            FileTransferProtocol.Copy(frameBuffer, FileTransferProtocol.CommandDataIndex, da, 0, da.Length);
                        }

                        if (OnReceiveFrame != null)
                        {
                            //是否继续帧分析处理
                            if (false == OnReceiveFrame((FileTransferProtocol.CommandType)commandType, BitConverter.ToInt32(frameBuffer, FileTransferProtocol.ParameterIndex), da))
                                return false;
                        }
                        //清除已处理的帧
                        Array.Clear(frameBuffer, 0, frameLength);
                        //前移未处理的数据
                        int unhandleLength = validBytes - frameLength;
                        if (unhandleLength > 0)
                        {
                            FileTransferProtocol.Copy(frameBuffer, frameLength, frameBuffer, 0, unhandleLength);
                            validBytes = unhandleLength;
                            findFrameTailIndex = -1;
                        }
                        else
                        {
                            validBytes = 0;
                            findFrameTailIndex = -1;
                        }

                    }
                    else//对应下标项不是帧头
                    {
                        //继续查找下一个帧尾
                    }
                }
                else//不足则
                {
                    //继续把当前临时缓冲区readBuffer复制到帧缓冲区frameBuffer之中
                }
            }

            //是否继续帧分析处理
            //在外部调用本函数可根据该返回做退出无限循环操作
            findFrameTailIndex = validBytes - FileTransferProtocol.FrameTail.Length;
            return true;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace yanzisoft
{
    public class FrameManager
    {

        /// <summary>
        /// ֡���մ���ί�ж���
        /// </summary>
        /// <param name="frameType">֡����</param>
        /// <param name="parameter">��֡�ṹ�Ĳ�������</param>
        /// <param name="commandData">����</param>
        /// <param name="extendData">��������</param>
        public delegate bool OnReceiveFrameHandler(FileTransferProtocol.CommandType frameType, int parameter, byte[] commandData);
        public event OnReceiveFrameHandler OnReceiveFrame;

        private byte[] frameBuffer = null;
        private int validBytes = 0;//��Ч�ֽ���
        private byte frameHead = FileTransferProtocol.FrameHead;
        private int findFrameTailIndex = -1;
        private int mframeBufferSize = 0;

        /// <summary>
        /// ֡�������
        /// </summary>
        /// <param name="frameBufferSize">֡��������С</param>
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
        /// ֡�������
        /// </summary>
        /// <param name="frameBufferSize">֡��������С</param>
        /// /// <param name="extendData">��������</param>
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
        /// ���ҿ��ܵ�֡β
        /// </summary>
        /// <param name="data">���ݻ�����</param>
        /// <param name="startIndex">��ʼƫ��</param>
        /// <param name="findValue">��Ҫ���ҵ�ֵ</param>
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
        /// ���ҿ��ܵ�֡β
        /// </summary>
        /// <param name="data">���ݻ�����</param>
        /// <param name="startIndex">��ʼƫ��</param>
        /// <param name="findValue">��Ҫ���ҵ�ֵ</param>
        /// <param name="MaxIndex">���ƫ��</param>
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
        /// ���ҿ��ܵ�֡β
        /// </summary>
        /// <param name="data">���ݻ�����</param>
        /// <param name="startIndex">��ʼƫ��</param>
        /// <param name="findValue">��Ҫ���ҵ�ֵ</param>
        /// <param name="MaxIndex">���ƫ��</param>
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
            Debug.WriteLine("�����ܵ�֡��������:" + validBytes);
            //����ҵ��Ŀ��ܵ�֡β
            while ((findFrameTailIndex = FindTailIndexEx(frameBuffer, findFrameTailIndex + 1, validBytes)) > -1 && validBytes >= FileTransferProtocol.MinFrameLength)
            {
                //����㹻��(��Ч���ݳ��Ȳ�С����С֡֡���� �ҵ���֡β���±���Ҫ��С����С֡��֡β�±�)
                if (findFrameTailIndex >= FileTransferProtocol.MinFrameLength - 1 && findFrameTailIndex < validBytes)
                {
                    //֡ͷ�±�=֡β�±� - ƫ��
                    int headIndex = findFrameTailIndex - BitConverter.ToUInt16(frameBuffer, findFrameTailIndex - FileTransferProtocol.FromFrametailToOffset);
                    Debug.WriteLine(string.Format("@headIndex={0}", headIndex));
                    //�����Ӧ�±�����֡ͷ
                    if ((headIndex >= 0 && headIndex < findFrameTailIndex && headIndex < validBytes) && frameBuffer[headIndex] == frameHead)//ֻ��headIndex���ڵ���0��С��findFrameTailIndex����Ч
                    {
                        Debug.WriteLine(string.Format("#headIndex={0}", headIndex));
                        if(headIndex!=0){
                            Debug.WriteLine(string.Format("headIndex={0} frameBuffer=[{1:X2},{2:X2},{3:X2},{4:X2} ] findFrameTailIndex={5}", headIndex, frameBuffer[0], frameBuffer[1], frameBuffer[2], frameBuffer[3], findFrameTailIndex));
                        }
                        //ǰ��֡����
                        if (headIndex != 0)
                        {
                            FileTransferProtocol.Copy(frameBuffer, headIndex, frameBuffer, 0, validBytes - headIndex);
                            validBytes = validBytes - headIndex;
                            findFrameTailIndex = findFrameTailIndex - headIndex;
                        }
                        //��������
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
                            //�Ƿ����֡��������
                            if (false == OnReceiveFrame((FileTransferProtocol.CommandType)commandType, BitConverter.ToInt32(frameBuffer, FileTransferProtocol.ParameterIndex), da))
                                return false;
                        }
                        //����Ѵ����֡
                        Array.Clear(frameBuffer, 0, frameLength);
                        //ǰ��δ���������
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
                    else//��Ӧ�±����֡ͷ
                    {
                        //����������һ��֡β
                    }
                }
                else//������
                {
                    //�����ѵ�ǰ��ʱ������readBuffer���Ƶ�֡������frameBuffer֮��
                }
            }

            //�Ƿ����֡��������
            //���ⲿ���ñ������ɸ��ݸ÷������˳�����ѭ������
            findFrameTailIndex = validBytes - FileTransferProtocol.FrameTail.Length;
            return true;

        }
    }
}

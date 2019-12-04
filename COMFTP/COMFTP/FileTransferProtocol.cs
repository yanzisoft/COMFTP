/// <summary>
/// �����ļ�����Э�� v4
/// ���ߣ�������
/// ������2014/8/21
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
            [Description("��ͻ��˷����ļ������ļ�����")]
            FileName=0x00,
            [Description("��ͻ��˷����ļ�����")]
            FileData,
            [Description("��ͻ��˷����ļ������ź�")]
            FileSendFinish,
            [Description("�ַ�����Ϣ")]
            Message=0x10,
            [Description("����DOS����")]
            SendDOSCommand = 0xD0,
            [Description("����ͻ����ṩMN�Ͱ汾��")]
            WhatIsYourMNandVersion=0xFD,
            [Description("Ӧ�������ṩ��ǰ�ͻ���MN�Ͱ汾��")]
            MNAndVersion,
            [Description("�������˹ر��׽���")]
            CloseSocket
        }


        public static ushort CommandTypeIndex = 1;  //������
        public static ushort ParameterIndex = 2;	//��չ���� ��(�ļ�������ʼ�±�,�ļ�����ƫ��)
        //�ڲ�������ʼλ��
        public static ushort DataStartIndex = CommandTypeIndex;

        //��parameter������������ʼ�±�
        public static ushort CommandDataIndex = 6;
        //��֡β��Offset��ƫ��
        public static ushort FromFrametailToOffset = 4;

        public static byte FrameHead = 0xFF;
        public static byte[] FrameTail = new byte[] { 0x55,0xAA,0x7F };

        //����֡��frameLength=1024; ��[0000 0100 0000 0000] �� 0x0400;
        //new byte [] { (frameLength & 0xFF),(frameLength>>8) } Ҳ���� {0x00,0x04}=0x0400


        //֡�������2^16���ֽ�64K�ֽ�
        public static int MaxFrameLength = 0xFFFF + 1;
        //֡������С��֡ͷ1�ֽ�+������1�ֽ�+֡Offset 2�ֽ�+֡β3�ֽ�	
        public static int MinFrameLength = 7;
        //֡Offset 2�ֽ�
        public static int OffsetBytes = 2;

        //֡�ṹ���ȣ�֡ͷ1�ֽ�+������1�ֽ�+parameter����4�ֽ�+֡Offset 2�ֽ�+֡β3�ֽ�	
        public static int FrameStructLength = 11;
        //  0	    1	    2	    3	    4	    5	    6	    7			
        //  7EH	    01H	    XXH     XXH     XXH     XXH 	�� ��	XXH XXH	    55H	 AAH 7FH
        //  ֡ͷ	������	�ļ��ߴ磨��ע1��4�ֽ�	        �ļ���  ƫ��	    ֡β(3�ֽ�)
        /// <summary>
        /// �������豸MN�ַ�������
        /// </summary>
        public static byte MNLength = 14;
        /// <summary>
        /// ������������ʵ��ֽڳ���
        /// </summary>
        public static byte LimitedLength = 4;

        public static Byte[] GetFrameCommon(CommandType type, int parameter, byte[] data)
        {
            byte[] parts = new byte[] { FrameHead, (Byte)type, (byte)(parameter & 0x000000FF), (byte)((parameter & 0x0000FF00) >> 8), (byte)((parameter & 0x00FF0000) >> 16), (byte)(parameter >> 24) };

            byte[] rtn = new byte[parts.Length + data.Length + OffsetBytes + FileTransferProtocol.FrameTail.Length];

            if (rtn.Length > MaxFrameLength)
            {
                throw new Exception("�Ѿ��������֡���ȣ�" + MaxFrameLength);
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
        /// ��������ļ��������ļ�����Ϣ������֡
        /// </summary>
        /// <param name="fileLength">�ļ�����</param>
        /// <param name="fileName">�ļ���</param>
        /// <returns>��������֡�ֽ�����</returns>
        public static byte[] GetFrameFileInfo(int fileLength, string fileName)
        {

            byte[] bytes = System.Text.Encoding.Default.GetBytes(fileName);

            return GetFrameCommon(CommandType.FileName, fileLength, bytes);
        }

        /// <summary>
        /// ��������ļ����ݰ�������֡
        /// </summary>
        /// <param name="fileOffset">�ļ�����ƫ��λ��</param>
        /// <param name="data">�ļ����ݵ��ֽ�����</param>
        /// <returns>��������֡�ֽ�����</returns>
        public static byte[] GetFrameFileData(int fileOffset, byte[] data)
        {
            return GetFrameCommon(CommandType.FileData, fileOffset, data);
        }

        /// <summary>
        /// ��������ļ��ѱ�������ɵ���Ϣ����֡
        /// </summary>
        /// <returns>��������֡�ֽ�����</returns>
        public static byte[] GetFrameFileSendFinish()
        {

            return GetFrameCommon(CommandType.FileSendFinish);
        }

        /// <summary>
        /// �������ѯ�ʿͻ��˷���MN���汾�ż� ����������ʵ�֡���� 
        /// </summary>
        /// <returns></returns>
        public static byte[] GetFrameWhatIsYourMNandVersion()
        {
            return GetFrameCommon(CommandType.WhatIsYourMNandVersion);
        }

        /// <summary>
        ///  ����Ӧ�����˷�����������MN���汾�ż� ����˶Ը���������������ʵ�֡���� 
        /// </summary>
        /// <param name="version">�汾</param>
        /// <param name="mn">�����Ǳ�ʶ</param>
        /// <param name="Limited">����˶Ը������������������(���С�����ʾ������)</param>
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
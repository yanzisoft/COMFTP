using System;
using System.Collections.Generic;
using System.Text;

namespace yanzisoft
{
    public class CNetClient:IClient
    {
        public bool Connect()
        {
            return false;
        }

        public bool Disconnect()
        {
            return false;
        }



        #region IClient 成员

        public event ClientHandler.FileTransferStateMessageHandler OnFileTransferStateMessage;

        public event ClientHandler.OnReceiveFrameHandler OnReceiveFrame;

        #endregion
    }
}

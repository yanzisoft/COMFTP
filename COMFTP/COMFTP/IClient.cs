using System;
using System.Collections.Generic;
using System.Text;

namespace yanzisoft
{
    public static  class ClientHandler
    {
            public delegate void OnReceiveFrameHandler(FileTransferProtocol.CommandType frameType, int parameter, byte[] commandData);
            public delegate void FileTransferStateMessageHandler(string message);
    }
    public interface IClient
    {
        event ClientHandler.FileTransferStateMessageHandler OnFileTransferStateMessage;
        event ClientHandler.OnReceiveFrameHandler OnReceiveFrame;
    }
}

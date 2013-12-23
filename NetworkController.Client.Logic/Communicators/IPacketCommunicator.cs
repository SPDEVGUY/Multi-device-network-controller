using System;
using System.Collections.Generic;

namespace NetworkController.Client.Logic.Communicators
{
    public interface IPacketCommunicator
    {
        event CommunicatorBase.PacketReceivedEvent PacketReceived;
        bool IsListening { get; }
        bool CanSend { get; }
        long PacketsReceived { get; }
        int ListenPort { get; set; }
        void StopListening();
        void StartListening();
        void Dispose();
        void Send(string data);
        List<Exception> PopExceptions();
    }
}

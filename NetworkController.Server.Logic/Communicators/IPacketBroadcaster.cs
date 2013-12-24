using System;
using System.Collections.Generic;
using System.Net;

namespace NetworkController.Server.Logic.Communicators
{
    public interface IPacketBroadcaster : IDisposable
    {
        bool IsBroadcasting { get; }
        long PacketsSent { get; }
        long PacketVolumeSent { get; }
        int BroadcastPort { get; set; }
        string LastBroadcastData { get; }
        List<IPEndPoint> Clients { get; }

        void Stop();
        void Start();
        void Broadcast(string data);
        List<Exception> PopExceptions();
    }
}

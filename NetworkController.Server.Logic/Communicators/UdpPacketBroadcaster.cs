using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace NetworkController.Server.Logic.Communicators
{
    public class UdpPacketBroadcaster : PacketBroadcasterBase
    {
        public override List<IPEndPoint> Clients
        {
            get { 
                return new List<IPEndPoint>(); //Can't tell with udp.
            }
        }

        private UdpClient _udp;
        private IPEndPoint _target;
        private IPEndPoint _sendFrom;


        public UdpPacketBroadcaster()
        {
        }
        public UdpPacketBroadcaster(int port)
        {
            _target = new IPEndPoint(IPAddress.Broadcast, port);
            _sendFrom = new IPEndPoint(IPAddress.Any, 9051);
        }
        public UdpPacketBroadcaster(IPAddress address, int port)
        {
            _target = new IPEndPoint(address, port);
            _sendFrom = new IPEndPoint(IPAddress.Any, 9051);
        }

        protected override void InternalDispose()
        {
            if (_udp != null) _udp.Close();
            _udp = null;
        }

        protected override void InternalSetup()
        {
            _target = new IPEndPoint(IPAddress.Broadcast, BroadcastPort);
            _sendFrom = new IPEndPoint(IPAddress.Any, BroadcastPort+1);

            if (_udp != null) _udp.Close();
            _udp = new UdpClient(_sendFrom);
            _udp.EnableBroadcast = true;
            _udp.DontFragment = true;
        }

        protected override void InternalBroadcast(byte[] data)
        {
            _udp.SendAsync(data, data.Length, _target);
        }
    }
}

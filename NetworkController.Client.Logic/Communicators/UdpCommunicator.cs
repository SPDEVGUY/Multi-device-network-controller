using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using NetworkController.Client.Logic.DataTypes.Contracts;
using NetworkController.Client.Logic.DataTypes.Interfaces;

namespace NetworkController.Client.Logic.Communicators
{
    public class UdpCommunicator : CommunicatorBase
    {
        private UdpClient _udp;
        

        public UdpCommunicator()
        {
            ListenPoint = new IPEndPoint(IPAddress.Any, 9050);
        }
        public UdpCommunicator(int port)
        {
            ListenPoint = new IPEndPoint(IPAddress.Any, port);
        }
        public UdpCommunicator(IPAddress address, int port)
        {
            ListenPoint = new IPEndPoint(address, port);
        }

        protected override void InternalClose()
        {
            if(_udp != null) _udp.Close();
        }

        protected override void InternalSetup()
        {
            //
        }

        protected override void InternalOpen()
        {
            if (_udp != null) _udp.Close();
            _udp = new UdpClient(ListenPoint);
            _udp.EnableBroadcast = true;
            //_udp.ExclusiveAddressUse = false;
            _udp.DontFragment = true;
        }

        protected override byte[] InternalReceive(ref IPEndPoint lastSender)
        {
            return _udp.Receive(ref LastSender);
        }

        protected override void InternalSend(IPEndPoint target, byte[] data)
        {
            _udp.Send(data, data.Length, target);
        }

        public override bool CanSend
        {
            get { return IsListening; }
        }
    }
}

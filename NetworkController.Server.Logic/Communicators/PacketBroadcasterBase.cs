using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace NetworkController.Server.Logic.Communicators
{
    public abstract class PacketBroadcasterBase : IPacketBroadcaster, IDisposable
    {
        public bool IsBroadcasting
        {
            get { return _isBroadcasting; }
        }

        public long PacketsSent
        {
            get { lock (_broadcastQueue) return _packetsSent; }
        }

        public long PacketVolumeSent
        {
            get { lock (_broadcastQueue) return _packetVolumeSent; }
        }

        public string LastBroadcastData
        {
            get { lock (_broadcastQueue) return _lastBroadcastData; }
        }

        public int BroadcastPort
        {
            get { lock (this) return _broadcastPort; }
            set
            {
                lock (this)
                {
                    _broadcastPort = value;
                    ReconfigureBroadcaster();
                }
            }
        }

        public abstract List<IPEndPoint> Clients { get; }

        private List<Exception> _exceptions = new List<Exception>();
        private Queue<string> _broadcastQueue = new Queue<string>();
        private bool _isBroadcasting = false;
        private long _packetsSent = 0;
        private long _packetVolumeSent = 0;
        private string _lastBroadcastData = "";
        private int _broadcastPort = 9050;
        private bool _isReadyToSend = false;
        private Thread _broadcastThread;


        protected abstract void InternalDispose();
        protected abstract void InternalSetup();
        protected abstract void InternalBroadcast(byte[] data);

        public void Dispose()
        {
            if (_isBroadcasting)
            {
                Stop();
                InternalDispose();
            }
        }

        protected void ReconfigureBroadcaster()
        {
            if (_isBroadcasting)
            {
                Stop();
                Start();
            }
        }


        public void Stop()
        {
            _isBroadcasting = false;
            Thread.Sleep(10);
        }

        public void Start()
        {
            if (IsBroadcasting) return;
            _isBroadcasting = true;

            SetupBroadcaster();

            _broadcastThread = new Thread(BroadcastingThreadMethod) { Name = "Broadcasting Thread", IsBackground = true };
            _packetsSent = 0;
            _packetVolumeSent = 0;
            _broadcastThread.Start();
        }

        public void Broadcast(string data)
        {
            lock (_broadcastQueue) _broadcastQueue.Enqueue(data);
        }

        private void SetupBroadcaster()
        {
            try
            {
                InternalSetup();
                _isReadyToSend = true;
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public List<Exception> PopExceptions()
        {
            lock (this)
            {
                var result = _exceptions.ToList();
                _exceptions.Clear();
                return result;
            }
        }

        private void BroadcastingThreadMethod()
        {
            try
            {
                while (_isBroadcasting)
                {
                    if (_isReadyToSend)
                    {
                        lock (_broadcastQueue)
                            while (_broadcastQueue.Count > 0)
                            {

                                try
                                {
                                    var data = _broadcastQueue.Dequeue();
                                    var bytes = Encoding.ASCII.GetBytes(data);
                                    InternalBroadcast(bytes);
                                    _packetsSent++;
                                    _packetVolumeSent += bytes.Length;
                                    _lastBroadcastData = data;

                                }
                                catch (Exception ex)
                                {
                                    LogException(ex);
                                }
                            }
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
        

        private void LogException(Exception ex)
        {
            lock (this) _exceptions.Add(ex);
        }
    }
}

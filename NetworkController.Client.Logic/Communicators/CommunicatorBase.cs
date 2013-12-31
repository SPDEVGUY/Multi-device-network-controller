using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace NetworkController.Client.Logic.Communicators
{
    public abstract class CommunicatorBase : IDisposable, IPacketCommunicator
    {
        public delegate void PacketReceivedEvent(IPacketCommunicator sender, string data);
        
        public event PacketReceivedEvent PacketReceived;


        public bool IsListening { get { lock (this) return _isListening; } }
        public long PacketsReceived { get { lock (this) return _packetsReceived; } }
        public int ListenPort
        {
            get { return ListenPoint.Port; }
            set
            {
                lock (this)
                {
                    _isReadyToListen = false;
                    ListenPoint = new IPEndPoint(IPAddress.Any, value);
                }
                SetupListener();
            }
        }

        public void Dispose()
        {
            if (_isListening) StopListening();
        }

        public void StopListening()
        {
            _isListening = false;
            _listenThread.Abort(); // kill udp listener if there's nothing to listen to it will just hold the thread.
            Thread.Sleep(10);
            InternalClose();
        }

        public void StartListening()
        {
            if (_isListening) return;

            try
            {
                _listenThread = new Thread(ListeningThreadMethod) {Name = "UDP Listening Thread"};
                _processingThread = new Thread(ProcessingThreadMethod) {Name = "UDP Processing Thread"};
                _packetsReceived = 0;
                _isListening = true;
                InternalOpen();
                _listenThread.Start();
                _processingThread.Start();
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
        
        protected abstract void InternalClose();
        protected abstract void InternalSetup();
        protected abstract void InternalOpen();
        protected abstract byte[] InternalReceive(ref IPEndPoint lastSender);
        protected abstract void InternalSend(IPEndPoint target, byte[] data);
        protected IPEndPoint ListenPoint;
        protected IPEndPoint LastSender;

        private Queue<string> _rawReceiveQueue = new Queue<string>();
        private Queue<string> _rawSendQueue = new Queue<string>();
        private List<Exception> _exceptions = new List<Exception>();
        private bool _isListening = false;
        private long _packetsReceived = 0;
        private bool _isReadyToListen = false;
        private bool _dataReceived = false;
        private bool _dataToSend = false;
        private string _dataReceivedLock = "x";
        private Thread _listenThread;
        private Thread _processingThread;


        private void ProcessingThreadMethod()
        {
            while (_isListening)
            {
                try
                {
                    bool hasData;
                    lock (_dataReceivedLock) hasData = _dataReceived;
                    if (hasData)
                    {
                        string[] data;
                        lock (_rawReceiveQueue)
                        {
                            data = _rawReceiveQueue.ToArray();
                            _rawReceiveQueue.Clear();
                        }

                        try
                        {
                            foreach (var d in data) OnPacketReceived(d);
                        }
                        catch (Exception ex)
                        {
                            LogException(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }

                try
                {
                    bool sendData;
                    lock (_rawSendQueue) sendData = _dataToSend;
                    if (sendData && CanSend)
                    {
                        lock (_rawSendQueue)
                        {
                            while (_rawSendQueue.Count > 0)
                            {
                                var data = _rawSendQueue.Dequeue();
                                var dataBytes = Encoding.ASCII.GetBytes(data);
                                InternalSend(LastSender, dataBytes);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }

                Thread.Sleep(1);
            }
        }
        
        private void OnPacketReceived(string d)
        {
            if (PacketReceived != null) PacketReceived(this, d);
        }
        
        private void ListeningThreadMethod()
        {
            try
            {
                SetupListener();
                _isListening = true;
                while (_isListening)
                {
                    if (!_isReadyToListen) Thread.Sleep(100);

                    try
                    {
                        var received = InternalReceive(ref LastSender);
                        var strReceived = Encoding.ASCII.GetString(received);
                        lock (_rawReceiveQueue) _rawReceiveQueue.Enqueue(strReceived);
                        lock (_dataReceivedLock) _dataReceived = true;
                        _packetsReceived++;
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void SetupListener()
        {
            try
            {
                lock (this)
                {
                    InternalSetup();
                    _isReadyToListen = true;
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


        public abstract bool CanSend { get; }

        public void Send(string data)
        {
            lock (_rawSendQueue)
            {
                _dataToSend = true; 
                _rawSendQueue.Enqueue(data);
            }
        }
    }
}

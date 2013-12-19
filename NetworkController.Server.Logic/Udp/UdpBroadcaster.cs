using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NetworkController.Logic.Controller;
using System.Net.Sockets;
using System.Net;

namespace NetworkController.Server.Logic.Udp
{
    public class UdpBroadcaster : IDisposable
    {
        public bool IsBroadcasting { get { lock (this) return _isBroadcasting; } }
        public long PacketsSent { get { lock (this) return _packetsSent; } }
        public long PacketVolumeSent { get { lock (this) return _packetVolumeSent; } }
        public int UpdateTick { get { lock (this) return _updateTick; } set { lock (this) _updateTick = value; } }
        public double VelocityRetentionFactor 
        {
            get
            {
                lock (this)
                {
                    if (Observer != null && !Observer.IsDisposed) return Observer.VelocityRetentionFactor;
                }
                return -1;
            } set
            {
                lock (this)
                {
                    if (Observer != null && !Observer.IsDisposed) Observer.VelocityRetentionFactor = value;
                }
            }
        }
        public string LoadedPlugins
        {
            get
            {
                lock (this)
                {
                    if (Observer != null && !Observer.IsDisposed) return Observer.GetProviderList();
                    return "";
                }
            }
        }
        public string LastBroadcastData { get; set; }

        private List<Exception> _exceptions = new List<Exception>();
        private InputDeltaObserver Observer;
        private bool _isBroadcasting = false;
        private int _updateTick = 100;
        private long _packetsSent = 0;
        private long _packetVolumeSent = 0;
        private UdpClient _udp;
        private IPEndPoint _target;
        private IPEndPoint _sendFrom;
        private bool _isReadyToSend = false;
        private Thread _broadcastThread;

        public IPEndPoint Target
        {
            get { return _target; }
            set
            {
                lock (this)
                {
                    _isReadyToSend = false;
                    _target = value;
                }
                SetupBroadcaster(); 
            }
        }

        public UdpBroadcaster()
        {
            _target = new IPEndPoint(IPAddress.Broadcast, 9050);
            _sendFrom = new IPEndPoint(IPAddress.Any, 9051);
        }
        public UdpBroadcaster(int port)
        {
            _target = new IPEndPoint(IPAddress.Broadcast, port);
            _sendFrom = new IPEndPoint(IPAddress.Any, 9051);
        }
        public UdpBroadcaster(IPAddress address, int port)
        {
            _target = new IPEndPoint(address, port);
            _sendFrom = new IPEndPoint(IPAddress.Any, 9051);
        }
        
        public void Dispose()
        {
            if (_isBroadcasting) StopBroadcasting();
        }

        public void StopBroadcasting()
        {
            _isBroadcasting = false;
        }

        public void StartBroadcasting()
        {
            if (IsBroadcasting) return;

            _broadcastThread = new Thread(BroadcastingThreadMethod) { Name = "UDP Broadcasting Thread" };
            _packetsSent = 0;
            _packetVolumeSent = 0;
            _broadcastThread.Start();
        }

        public List<Exception> PopExceptions() { lock (this)
        {
            var result = _exceptions.ToList();
            _exceptions.Clear();
            return result;
        }}

        private void BroadcastingThreadMethod()
        {
            try
            {
                using (Observer = new InputDeltaObserver())
                {
                    Observer.StartCapturing();
                    LogExceptions(Observer.ProcessingExceptions);


                    SetupBroadcaster();
                    _isBroadcasting = true;
                    while (_isBroadcasting)
                    {
                        Observer.UpdateTick();
                        LogExceptions(Observer.ProcessingExceptions);

                        if (Observer.DirtiedDeltas.Count > 0)
                        {
                            var dirtied = Observer.SerializeDirtied();
                            BroadcastString(dirtied);
                        }

                        Thread.Sleep(_updateTick);
                    }

                    Observer.StopCapturing();
                    Observer.Dispose();
                    LogExceptions(Observer.ProcessingExceptions);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void SetupBroadcaster()
        {
            try
            {
                lock (this)
                {
                    if (_udp != null) _udp.Close();
                    _udp = new UdpClient(_sendFrom);
                    _udp.EnableBroadcast = true;
                    _isReadyToSend = true;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void BroadcastString(string data)
        {
            try
            {
                lock (this)
                {
                    if (!_isReadyToSend) return;
                    var bytes = Encoding.ASCII.GetBytes(data);
                    _udp.SendAsync(bytes, bytes.Length,_target);
                    _packetsSent++;
                    _packetVolumeSent += bytes.Length;
                    LastBroadcastData = data;
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
        private void LogExceptions(List<Exception> ex)
        {
            lock (this) _exceptions.AddRange(ex);
        }

    }
}

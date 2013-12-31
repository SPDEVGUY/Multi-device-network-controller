using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetworkController.Logic.Controller;
using NetworkController.Logic.Plugin.Interfaces;
using NetworkController.Server.Logic.Communicators;

namespace NetworkController.Server.Logic
{
    public class Broadcaster : IDisposable
    {
        private IPacketBroadcaster _communicator;
        private BroadcasterTypeEnum _communicationType;
        private int _broadcastPort = 9050;
        private List<Exception> _exceptions = new List<Exception>();
        private InputDeltaObserver _observer;
        private int _updateTick = 100;
        private bool _isRunning = false;
        private Thread _updateThread;

        public event EventHandler DriversReady;
        public bool IsBroadcasting { get { return _communicator != null && _communicator.IsBroadcasting; } }
        public long PacketsSent { get { return _communicator == null ? 0 : _communicator.PacketsSent; } }
        public long PacketVolumeSent { get { return _communicator == null ? 0 : _communicator.PacketVolumeSent; } }
        public string LastBroadcastData { get { return _communicator == null ? "" : _communicator.LastBroadcastData; } }

        public int BroadcastPort
        {
            get { return _broadcastPort; }
            set { _broadcastPort = value; }
        }
        public BroadcasterTypeEnum BroadcastType
        {
            get { return _communicationType; }
            set { _communicationType = value; }
        }

        public int UpdateTick { get { lock (this) return _updateTick; } set { lock (this) _updateTick = Math.Max(value,1); } }
        public double VelocityRetentionFactor 
        {
            get
            {
                lock (this)
                {
                    if (_observer != null && !_observer.IsDisposed) return _observer.VelocityRetentionFactor;
                }
                return -1;
            } set
            {
                lock (this)
                {
                    if (_observer != null && !_observer.IsDisposed) _observer.VelocityRetentionFactor = value;
                }
            }
        }
        public string LoadedPlugins
        {
            get
            {
                lock (this)
                {
                    if (_observer != null && !_observer.IsDisposed) return _observer.GetProviderList();
                    return "";
                }
            }
        }

        protected IPacketBroadcaster StartCommunicator()
        {
            if (_communicator != null) throw new NotSupportedException("Can't start while already started.");

            switch (_communicationType)
            {
                case BroadcasterTypeEnum.UDP:
                    _communicator = new UdpPacketBroadcaster();
                    break;
                case BroadcasterTypeEnum.TCP:
                    //TODO: _communicator = new TcpBroadcaster();
                    throw new NotSupportedException("TCP Not yet supported.");
                    break;
                default:
                    throw new NotSupportedException("Developer laziness not yet supported.");
            }

            _communicator.BroadcastPort = _broadcastPort;
            _communicator.Start();
            
            return _communicator;
        }

        
        public void Dispose()
        {
            if (_isRunning)
            {
                Stop();
            }
        }

        public void Stop()
        {
            _isRunning = false;
            Thread.Sleep(10);
        }
        public void Start()
        {
            if (_isRunning) return;
            _isRunning = true;
            _updateThread = new Thread(BroadcastingThreadMethod) { Name = "Broadcaster Update Thread", IsBackground = true};
            _updateThread.Start();
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
                using (_observer = new InputDeltaObserver())
                {
                    using (StartCommunicator())
                    {
                        _observer.StartCapturing();
                        LogExceptions(_observer.ProcessingExceptions);
                        _communicator.Start();

                        OnDriversReady();

                        while (_isRunning)
                        {
                            lock (this)
                            {
                                _observer.UpdateTick();
                                LogExceptions(_observer.ProcessingExceptions);

                                if (_observer.DirtiedDeltas.Count > 0)
                                {
                                    //Filter dirtied deltas
                                    //TODO

                                    //Serialize and send
                                    var dirtied = _observer.SerializeDirtied();
                                    _communicator.Broadcast(dirtied);
                                }
                            }

                            Thread.Sleep(_updateTick);
                        }

                        _communicator.Stop();
                        _communicator.Dispose();
                    }
                    _observer.StopCapturing();
                    _observer.Dispose();
                    LogExceptions(_observer.ProcessingExceptions);
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

        public List<IDriverAbstracter> GetDrivers()
        {
            if (_observer == null) return null;
            return _observer.Drivers;
        }

        protected void OnDriversReady()
        {
            if (DriversReady != null) DriversReady(this,null);
        }
    }
}

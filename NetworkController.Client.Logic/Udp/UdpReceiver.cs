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

namespace NetworkController.Client.Logic.Controllers
{
    public class UdpReceiver
    {
        private Dictionary<string, IDeltaButtonState> _buttonStates = new Dictionary<string, IDeltaButtonState>();
        private Dictionary<string, IDeltaGestureState> _gestureStates = new Dictionary<string, IDeltaGestureState>();
        private Dictionary<string, IDeltaSliderState> _sliderStates = new Dictionary<string, IDeltaSliderState>();
        private Dictionary<string, IDeltaDeviceState> _deviceStates = new Dictionary<string, IDeltaDeviceState>();

        private Queue<string> _rawReceiveQueue = new Queue<string>();

        public bool IsListening { get { lock (this) return _isListening; } }
        public long PacketsReceived { get { lock (this) return _packetsReceived; } }

        private List<Exception> _exceptions = new List<Exception>();
        private bool _isListening = false;
        private long _packetsReceived = 0;
        private UdpClient _udp;
        private IPEndPoint _listenOn;
        private IPEndPoint _lastSender;
        private Thread _listenThread;
        private Thread _processingThread;
        private bool _isReadyToListen = false;
        private bool _dataReceived = false;
        private string _dataReceivedLock = "x";

        public int ListenPort
        {
            get { return _listenOn.Port; }
            set
            {
                lock (this)
                {
                    _isReadyToListen = false;
                    _listenOn = new IPEndPoint(IPAddress.Any,value);
                }
                SetupListener(); 
            }
        }

        public UdpReceiver()
        {
            _listenOn = new IPEndPoint(IPAddress.Any, 9050);
        }
        public UdpReceiver(int port)
        {
            _listenOn = new IPEndPoint(IPAddress.Any, port);
        }
        public UdpReceiver(IPAddress address, int port)
        {
            _listenOn = new IPEndPoint(address, port);
        }
        
        public void Dispose()
        {
            if (_isListening) StopListening();
        }

        public void StopListening()
        {
            _isListening = false;
            _listenThread.Abort(); // kill udp listener if there's nothing to listen to it will just hold the thread.
            _udp.Close();
         }

        public void StartListening()
        {
            if (_isListening) return;

            _listenThread = new Thread(ListeningThreadMethod) { Name = "UDP Listening Thread" };
            _processingThread = new Thread(ProcessingThreadMethod) { Name = "UDP Processing Thread" };
            _packetsReceived = 0;
            _isListening = true;
            _listenThread.Start();
            _processingThread.Start();
        }

        public List<Exception> PopExceptions() { lock (this)
        {
            var result = _exceptions.ToList();
            _exceptions.Clear();
            return result;
        }}

        public List<IDeltaButtonState> GetAllButtonStates()
        {
            lock (_buttonStates) return _buttonStates.Values.ToList();
        }

        public List<IDeltaSliderState> GetAllSliderStates()
        {
            lock (_sliderStates) return _sliderStates.Values.ToList();
        }

        public List<IDeltaGestureState> GetAllGestureStates()
        {
            lock (_gestureStates) return _gestureStates.Values.ToList();
        }

        public IDeltaButtonState GetButtonState(string name)
        {
            lock (_buttonStates) return _buttonStates.ContainsKey(name) ? _buttonStates[name] : null;
        }

        public IDeltaGestureState GetGestureState(string name)
        {
            lock (_gestureStates) return _gestureStates.ContainsKey(name) ? _gestureStates[name] : null;
        }

        public IDeltaSliderState GetSliderState(string name)
        {
            lock (_sliderStates) return _sliderStates.ContainsKey(name) ? _sliderStates[name] : null;
        }

        private void ProcessingThreadMethod()
        {
            while (_isListening)
            {
                var hasData = false;
                lock (_dataReceivedLock) hasData = _dataReceived;
                if (hasData)
                {
                    string[] data;
                    lock (_rawReceiveQueue)
                    {
                        data = _rawReceiveQueue.ToArray();
                        _rawReceiveQueue.Clear();
                    }

                    foreach (var d in data)
                    {
                        //TODO: STUFF HERE
                        var js = new JavaScriptSerializer();
                        var items = js.Deserialize<List<AnyDeltaState>>(d);
                        if (items != null) StoreDeltaStates(items);
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        private void StoreDeltaStates(List<AnyDeltaState> items)
        {
            foreach (var i in items)
            {
                switch (i.DeltaType)
                {
                    case 0:
                        lock(_sliderStates) _sliderStates[i.ProviderName + "." + i.InputName] = i;
                        break;
                        
                    case 1:
                        lock (_buttonStates) _buttonStates[i.ProviderName + "." + i.InputName] = i;
                        break;
                        
                    case 2:
                        lock (_gestureStates) _gestureStates[i.ProviderName + "." + i.InputName] = i;
                        break;

                    default:
                        lock (_deviceStates) _deviceStates[i.ProviderName + "." + i.InputName] = i;
                        break;
                }
            }
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
                        var received = _udp.Receive(ref _lastSender);
                        var strReceived = Encoding.ASCII.GetString(received);
                        lock (_rawReceiveQueue) _rawReceiveQueue.Enqueue(strReceived);
                        lock (_dataReceivedLock) _dataReceived = true;
                        _packetsReceived++;
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
        }

        private void SetupListener()
        {
            try
            {
                lock (this)
                {
                    if (_udp != null) _udp.Close();
                    _udp = new UdpClient(_listenOn);
                    _udp.EnableBroadcast = true;
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
        private void LogExceptions(List<Exception> ex)
        {
            lock (this) _exceptions.AddRange(ex);
        }

    }
}

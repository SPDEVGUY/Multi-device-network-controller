using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using NetworkController.Client.Logic.Communicators;
using NetworkController.Client.Logic.DataTypes.Contracts;
using NetworkController.Client.Logic.DataTypes.Interfaces;

namespace NetworkController.Client.Logic
{
    public class Receiver
    {
        private Dictionary<string, IDeltaButtonState> _buttonStates = new Dictionary<string, IDeltaButtonState>();
        private Dictionary<string, IDeltaGestureState> _gestureStates = new Dictionary<string, IDeltaGestureState>();
        private Dictionary<string, IDeltaSliderState> _sliderStates = new Dictionary<string, IDeltaSliderState>();
        private Dictionary<string, IDeltaDeviceState> _deviceStates = new Dictionary<string, IDeltaDeviceState>();

        private ReceiverTypeEnum _communicationType = ReceiverTypeEnum.UDP;
        private IPacketCommunicator _communicator;
        private int _port = 9050;
        private MappingRequest _mappings;
        
        public ReceiverTypeEnum ReceiverType
        {
            get { return _communicationType; }
            set
            {
                if (value != _communicationType)
                {
                    _communicationType = value;
                    ReconfigureCommunication();
                }
            }
        }
        public int Port
        {
            get { return _port; }
            set
            {
                if (_port != value)
                {
                    _port = value;
                    _communicator.ListenPort = _port;
                }
            }
        }
        public long PacketsReceived { get { return _communicator == null ? 0 : _communicator.PacketsReceived; } }
        
        public MappingRequest Mappings
        {
            get { return _mappings; }
            set
            {
                if (value != _mappings)
                {
                    _mappings = value;
                    SendMappingRequest();
                }
            }
        }

        private void SendMappingRequest()
        {
            if (_communicator.CanSend)
            {
                var js = new JavaScriptSerializer();
                var data = js.Serialize(_mappings);
                _communicator.Send(data);
            }
        }

        public Receiver()
        {
            ReconfigureCommunication();
        }
        
        public void Start()
        {
            if (_communicator == null) ReconfigureCommunication();
            if (_communicator == null) return;

            _communicator.StartListening();
            SendMappingRequest();
        }

        public void Stop()
        {
            if (_communicator != null)
            {
                _communicator.StopListening();
                _communicator.Dispose();
            }
        }


        protected void ReconfigureCommunication()
        {
            var wasListening = false;
            if (_communicator != null)
            {
                if (_communicator.IsListening)
                {
                    wasListening = true;
                    Stop();
                }
                _communicator = null;
            }

            switch (_communicationType)
            {
                case ReceiverTypeEnum.UDP:
                    _communicator = new UdpCommunicator(_port);
                    break;
                case ReceiverTypeEnum.TCP:
                    throw new NotSupportedException("TCP not yet supported.");
                default:
                    throw new NotSupportedException("Developer laziness not supported.");
            }

            _communicator.PacketReceived += ProcessPacket;
            
            if(wasListening) Start();
        }

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

        public bool? IsPressed(string buttonName)
        {
            var state = GetButtonState(buttonName);
            if (state == null) return null;
            return state.IsPressed;
        }
        public int? SliderValue(string sliderName)
        {
            var state = GetSliderState(sliderName);
            if (state == null) return null;
            return state.Value;
        }
        public bool? GestureDetected(string gestureName)
        {
            var state = GetGestureState(gestureName);
            if (state == null) return null;
            return state.DetectedThisTick;
        }
        public int? GestureIntensity(string gestureName)
        {
            var state = GetGestureState(gestureName);
            if (state == null) return null;
            return state.Intensity;
        }

        private void StoreDeltaStates(List<AnyDeltaState> items)
        {
            foreach (var i in items)
            {
                switch (i.DeltaType)
                {
                    case 0:
                        lock (_sliderStates) _sliderStates[i.ProviderName + "." + i.InputName] = i;
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


        private void ProcessPacket(IPacketCommunicator comm, string data)
        {
            var js = new JavaScriptSerializer();
            var items = js.Deserialize<List<AnyDeltaState>>(data);
            if (items != null) StoreDeltaStates(items);
        }

    }
}

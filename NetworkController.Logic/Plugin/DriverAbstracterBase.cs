using NetworkController.Client.Logic.DataTypes.Interfaces;
using NetworkController.Logic.Plugin;
using NetworkController.Logic.Plugin.Attributes;
using NetworkController.Logic.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkController.Plugin.Mouse
{
    //Put this on your inheriting classes: [DriverAbstracter]
    public abstract class DriverAbstracterBase : IDriverAbstracter
    {

        public string ProviderName { get; set; }
        public int ThreadSleep = 100;

        public Queue<ISliderState> SliderQueue = new Queue<ISliderState>();
        public Queue<IButtonState> ButtonQueue = new Queue<IButtonState>();
        public Queue<IDeviceState> DeviceStateQueue = new Queue<IDeviceState>();
        public Queue<IGestureState> GestureQueue = new Queue<IGestureState>();
        protected bool CaptureThreadRunning;
        protected Thread CaptureThread;

        protected DriverAbstracterBase(string providerName)
        {
            ProviderName = providerName;
            CaptureThread = new Thread(CaptureThreadMethod) { Name = providerName + " Capture Thread"};
        }

        public List<ISliderState> PopSliderQueue()
        {
            lock (SliderQueue)
            {
                var items = SliderQueue.ToList();
                SliderQueue.Clear();
                return items;
            }
        }

        public List<IGestureState> PopGestureQueue()
        {
            lock (GestureQueue)
            {
                var items = GestureQueue.ToList();
                GestureQueue.Clear();
                return items;
            }
        }

        public List<IButtonState> PopButtonQueue()
        {
            lock (ButtonQueue)
            {
                var items = ButtonQueue.ToList();
                ButtonQueue.Clear();
                return items;
            }
        }

        public List<IDeviceState> PopDeviceStateQueue()
        {
            lock (DeviceStateQueue)
            {
                var items = DeviceStateQueue.ToList();
                DeviceStateQueue.Clear();
                return items;
            }
        }

        public void StartCapturing()
        {
            CaptureThread.Start();
        }

        public void StopCapturing()
        {
            CaptureThreadRunning = false;
            while (CaptureThread.ThreadState != ThreadState.Stopped)
            {
                Thread.Sleep(20);
            }
            SliderQueue.Clear();
            ButtonQueue.Clear();
            DeviceStateQueue.Clear();
            GestureQueue.Clear();
        }

        private void CaptureThreadMethod()
        {
            CaptureThreadRunning = true;
            InitializeDriver();
            while (CaptureThreadRunning)
            {
                CaptureCurrentState();
                Thread.Sleep(ThreadSleep);
            }
            DisposeDriver();
        }

        protected abstract void CaptureCurrentState();
        protected abstract void InitializeDriver();
        protected abstract void DisposeDriver();

        protected Dictionary<string, ISliderState> LastSliderValues = new Dictionary<string, ISliderState>();
        protected Dictionary<string, IButtonState> LastButtonStates = new Dictionary<string, IButtonState>();
        protected Dictionary<string, IDeviceState> LastDeviceStates = new Dictionary<string, IDeviceState>();


        protected void AddSliderValue(string inputName, int value, int minValue, int maxValue)
        {
            if (LastSliderValues.ContainsKey(inputName))
            {
                if (LastSliderValues[inputName] .Value == value) return; //Ignore same values to reduce load.
            }
            var state = new SliderState
                            {
                                InputName = inputName,
                                ProviderName = ProviderName,
                                Value = value,
                                MaxValue = maxValue,
                                MinValue = minValue,
                            };

            LastSliderValues[inputName] = state;
            lock(SliderQueue) SliderQueue.Enqueue(state);
        }
        protected void AddSliderValue(string inputName, int value)
        {
            AddSliderValue(inputName,value,Math.Min(value,0),Math.Max(value,0));
        }
        protected void AddButtonValue(string inputName, bool isPressed)
        {
            if (LastButtonStates.ContainsKey(inputName))
            {
                if (LastButtonStates[inputName].IsPressed == isPressed) return; //Ignore same values to reduce load.
            }
            var state = new ButtonState
            {
                InputName = inputName,
                ProviderName = ProviderName,
                IsPressed = isPressed,
            };

            LastButtonStates[inputName] = state;
            lock (ButtonQueue) ButtonQueue.Enqueue(state);
        }
        protected void AddDeviceStateValue(string inputName, bool isEnabled)
        {
            if (LastDeviceStates.ContainsKey(inputName))
            {
                if (LastDeviceStates[inputName].IsEnabled == isEnabled) return; //Ignore same values to reduce load.
            }
            var state = new DeviceState
            {
                InputName = inputName,
                ProviderName = ProviderName,
                IsEnabled = isEnabled,
            };

            LastDeviceStates[inputName] = state;
            lock (DeviceStateQueue) DeviceStateQueue.Enqueue(state);
        }

        protected void AddGestureValue(string inputName, int intensity)
        {
            var state = new GestureState
            {
                InputName = inputName,
                ProviderName = ProviderName,
                Intensity = intensity
            };

            lock (GestureQueue) GestureQueue.Enqueue(state);
        } 

        public void Dispose()
        {
            if (CaptureThreadRunning) StopCapturing();
        }




    }
}

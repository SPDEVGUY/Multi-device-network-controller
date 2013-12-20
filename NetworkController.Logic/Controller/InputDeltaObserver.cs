using System.Collections.Generic;
using System.Text;
using NetworkController.Client.Logic.DataTypes.Interfaces;
using NetworkController.Logic.Plugin;
using NetworkController.Logic.Plugin.Attributes;
using NetworkController.Logic.Plugin.Interfaces;
using System;
using System.Web.Script.Serialization;

namespace NetworkController.Logic.Controller
{
    public class InputDeltaObserver : IDisposable
    {
        public bool IsDisposed = false;

        public List<IDriverAbstracter> Drivers = new List<IDriverAbstracter>();
        public List<IStateRemapper> Remappers = new List<IStateRemapper>();
        public List<IGestureProcessor> GestureProcessors = new List<IGestureProcessor>();

        //The following are used by remappers, and gesture processors
        //populated by querying all samples from Drivers.
        public List<IDeviceState> DeviceStateQueue = new List<IDeviceState>();
        public List<IButtonState> ButtonQueue = new List<IButtonState>();
        public List<ISliderState> SliderQueue = new List<ISliderState>();
        public List<IGestureState> GestureQueue = new List<IGestureState>();
        public List<Exception> ProcessingExceptions = new List<Exception>();

        //The following are used to hold last final observed states
        //Can be used by gesture processor to detect movements.
        //Basically these are stateful representations of states, versus the above items which have no state
        public Dictionary<string, Dictionary<string, IServerDeltaDeviceState>> DeltaDeviceStates = new Dictionary<string, Dictionary<string, IServerDeltaDeviceState>>();
        public Dictionary<string, Dictionary<string, IServerDeltaButtonState>> DeltaButtonStates = new Dictionary<string, Dictionary<string, IServerDeltaButtonState>>();
        public Dictionary<string, Dictionary<string, IServerDeltaSliderState>> DeltaSliderStates = new Dictionary<string, Dictionary<string, IServerDeltaSliderState>>();
        public Dictionary<string, Dictionary<string, IServerDeltaGestureState>> DeltaGestureStates = new Dictionary<string, Dictionary<string, IServerDeltaGestureState>>();

        public List<IServerDeltaState> DirtiedDeltas = new List<IServerDeltaState>();
        public List<IServerDeltaState> AllDeltas = new List<IServerDeltaState>();
        public List<IServerMeasuredDeltaState> AllMeasuredDeltas = new List<IServerMeasuredDeltaState>();

        public double VelocityRetentionFactor = 0.97;

        public InputDeltaObserver()
        {
            Drivers = PluginController.Instance.GetProviders<IDriverAbstracter, DriverAbstracterAttribute>() ?? new List<IDriverAbstracter>();
            Remappers = PluginController.Instance.GetProviders<IStateRemapper, StateRemapperAttribute>() ?? new List<IStateRemapper>();
            GestureProcessors = PluginController.Instance.GetProviders<IGestureProcessor, GestureProcessorAttribute>() ?? new List<IGestureProcessor>();
            foreach (var p in GestureProcessors) p.Observer = this;

            ProcessingExceptions.AddRange(PluginController.Instance.LoadExceptions);
        }
        public void StartCapturing()
        {
            if (IsDisposed) return;
            foreach (var d in Drivers)
            {
                try { d.StartCapturing(); }
                catch (Exception ex)
                { LogProcessingException(d.GetType(), "StartCapturing", ex); }
            }
        }
        public void StopCapturing()
        {
            foreach (var d in Drivers)
            {
                try { d.StopCapturing(); }
                catch (Exception ex)
                { LogProcessingException(d.GetType(), "StopCapturing", ex); }
            }
        }
        public void UpdateTick()
        {
            ClearCurrentQueues();
            ReduceVelocityAndCoolOffGestures();
            CollectFromDrivers();
            PerformPreGestureRemapping();
            ProcessGestures();
            PerformPostGestureRemapping();
            AccumulateDeltas();
            FinalizeDeltas();
        }

        private void ReduceVelocityAndCoolOffGestures()
        {
            foreach (var d in AllMeasuredDeltas)
            {
                if (d.Velocity != 0 || d.Acceleration != 0)
                {
                    if (d.DeltaType == 1)
                    {
                        if (d.Velocity < 0.01 && d.Velocity > -0.01) d.Velocity = 0;
                        else
                        {
                            d.Velocity = Math.Round(VelocityRetentionFactor * d.Velocity, 4);
                        }
                    }

                    if (!DirtiedDeltas.Contains(d)) DirtiedDeltas.Add(d);
                }

                if (d is IDeltaGestureState)
                {
                    var g = (IDeltaGestureState) d;
                    if (g.DetectedThisTick || g.DetectedLastTick)
                    {
                        g.DetectedLastTick = g.DetectedThisTick;
                        g.DetectedThisTick = false;
                        DirtiedDeltas.Add(d);
                    }
                }

            }
        }

        public string SerializeDirtied()
        {
            var json = new JavaScriptSerializer();
            return json.Serialize(DirtiedDeltas);
        }
        public string SerializeAll()
        {
            var json = new JavaScriptSerializer();
            return json.Serialize(AllDeltas);
        }

        public string GetProviderList()
        {
            var result = new StringBuilder();

            result.AppendLine("[Drivers]");
            foreach (var i in Drivers)
            {
                result.AppendFormat("{0} - {1}", i.ProviderName, i.GetType());
                result.AppendLine();
            }


            result.AppendLine();
            result.AppendLine("[Remappers]");
            foreach (var i in Remappers)
            {
                result.AppendFormat("{0}", i.GetType());
                result.AppendLine();
            }

            result.AppendLine();
            result.AppendLine("[GestureProcessors]");
            foreach (var i in GestureProcessors)
            {
                result.AppendFormat("{0}", i.GetType());
                result.AppendLine();
            }

            return result.ToString();
        }

        private void FinalizeDeltas()
        {
            foreach (var i in DirtiedDeltas)
            {
                i.FinalizeDelta();
            }
        }

        private void AccumulateDeltas()
        {
            foreach (var i in DeviceStateQueue)
            {
                var d = GetDeviceStateDelta(i);
                d.ApplyNewState(i);
                if (!DirtiedDeltas.Contains(d)) DirtiedDeltas.Add(d);
            }
            foreach (var i in ButtonQueue)
            {
                var d = GetButtonDelta(i);
                d.ApplyNewState(i);
                if (!DirtiedDeltas.Contains(d)) DirtiedDeltas.Add(d);
            }
            foreach (var i in SliderQueue)
            {
                var d = GetSliderDelta(i);
                d.ApplyNewState(i);
                if(!DirtiedDeltas.Contains(d))DirtiedDeltas.Add(d);
            }
            foreach (var i in GestureQueue)
            {
                var d = GetGestureDelta(i);
                d.ApplyNewState(i);
                if (!DirtiedDeltas.Contains(d)) DirtiedDeltas.Add(d);
            }
        }

        private IServerDeltaDeviceState GetDeviceStateDelta(IDeviceState state)
        {
            if(!DeltaDeviceStates.ContainsKey(state.ProviderName))
                DeltaDeviceStates[state.ProviderName] = new Dictionary<string, IServerDeltaDeviceState>();

            if (!DeltaDeviceStates[state.ProviderName].ContainsKey(state.InputName))
            {
                var deltaState = new DeltaDeviceState
                                     {
                                         InputName = state.InputName,
                                         ProviderName = state.ProviderName,
                                         IsEnabled = false,
                                         TimeDelta = 1
                                     };

                DeltaDeviceStates[state.ProviderName][state.InputName] = deltaState;
                AllDeltas.Add(deltaState);
                return deltaState;
            }

            return DeltaDeviceStates[state.ProviderName][state.InputName];
        }
        private IServerDeltaButtonState GetButtonDelta(IButtonState state)
        {
            if(!DeltaButtonStates.ContainsKey(state.ProviderName))
                DeltaButtonStates[state.ProviderName] = new Dictionary<string, IServerDeltaButtonState>();

            if (!DeltaButtonStates[state.ProviderName].ContainsKey(state.InputName))
            {
                var deltaState = new DeltaButtonState
                                     {
                                         InputName = state.InputName,
                                         ProviderName = state.ProviderName,
                                         IsPressed = false,
                                         Acceleration = 0,
                                         Velocity = 0,
                                         LastPressCount = 0,
                                         PressCount = 0,
                                         TimeDelta = 1
                                     };
                
                DeltaButtonStates[state.ProviderName][state.InputName] = deltaState;
                AllDeltas.Add(deltaState);
                AllMeasuredDeltas.Add(deltaState);
                return deltaState;
            }

            return DeltaButtonStates[state.ProviderName][state.InputName];
        }
        private IServerDeltaSliderState GetSliderDelta(ISliderState state)
        {
            if(!DeltaSliderStates.ContainsKey(state.ProviderName))
                DeltaSliderStates[state.ProviderName] = new Dictionary<string, IServerDeltaSliderState>();

            if (!DeltaSliderStates[state.ProviderName].ContainsKey(state.InputName))
            {
                var deltaState = new DeltaSliderState
                                     {
                                         InputName = state.InputName,
                                         ProviderName = state.ProviderName,
                                         Acceleration = 0,
                                         Velocity = 0,
                                         TimeDelta = 1,
                                         MinValue = state.MinValue,
                                         MaxValue = state.MaxValue,
                                         Value = 0,
                                         LastValue = 0
                                     };
                
                DeltaSliderStates[state.ProviderName][state.InputName] = deltaState;
                AllDeltas.Add(deltaState);
                AllMeasuredDeltas.Add(deltaState);
                return deltaState;
            }

            return DeltaSliderStates[state.ProviderName][state.InputName];
        }
        private IServerDeltaGestureState GetGestureDelta(IGestureState state)
        {
            if(!DeltaGestureStates.ContainsKey(state.ProviderName))
                DeltaGestureStates[state.ProviderName] = new Dictionary<string, IServerDeltaGestureState>();

            if (!DeltaGestureStates[state.ProviderName].ContainsKey(state.InputName))
            {
                var deltaState = new DeltaGestureState
                                     {
                                         InputName = state.InputName,
                                         ProviderName = state.ProviderName,
                                         Acceleration = 0,
                                         Velocity = 0,
                                         TimeDelta = 1,
                                         DetectedLastTick = false,
                                         DetectedThisTick = false,
                                         Intensity = 0,
                                         LastIntensity = 0
                                     };
                
                DeltaGestureStates[state.ProviderName][state.InputName] = deltaState;
                AllDeltas.Add(deltaState);
                AllMeasuredDeltas.Add(deltaState);
                return deltaState;
            }

            return DeltaGestureStates[state.ProviderName][state.InputName];
        }
        
        private void ProcessGestures()
        {
            foreach (var g in GestureProcessors)
            {
                try
                {
                    g.PerformGestureDetection();
                }
                catch (Exception ex)
                { LogProcessingException(g.GetType(), "PerformGestureDetection", ex); }
            }
        }

        private void PerformPostGestureRemapping()
        {
            foreach (var r in Remappers)
            {
                try
                { r.PerformPostGestureRemapping(this); }
                catch (Exception ex)
                { LogProcessingException(r.GetType(), "PerformPostGestureRemapping", ex); }
            }
        }

        private void PerformPreGestureRemapping()
        {
            foreach (var r in Remappers)
            {
                try
                { r.PerformPreGestureRemapping(this); }
                catch (Exception ex)
                { LogProcessingException(r.GetType(), "PerformPreGestureRemapping", ex); }
            }
        }

        private void CollectFromDrivers()
        {
            foreach (var d in Drivers)
            {
                try
                { DeviceStateQueue.AddRange(d.PopDeviceStateQueue()); }
                catch (Exception ex)
                { LogProcessingException(d.GetType(), "PopDeviceStateQueue", ex); }

                try
                { ButtonQueue.AddRange(d.PopButtonQueue()); }
                catch (Exception ex)
                { LogProcessingException(d.GetType(), "PopButtonQueue", ex); }

                try
                { SliderQueue.AddRange(d.PopSliderQueue()); }
                catch (Exception ex)
                { LogProcessingException(d.GetType(), "PopSliderQueue", ex); }

                try
                { GestureQueue.AddRange(d.PopGestureQueue()); }
                catch (Exception ex)
                { LogProcessingException(d.GetType(), "PopGestureQueue", ex); }
            }
        }

        private void LogProcessingException(Type source, string method, Exception ex)
        {
            ProcessingExceptions.Add(new Exception(
                string.Format("Processing exception from {0}::{1}. {2}", source,method,ex.Message), ex));
        }

        private void ClearCurrentQueues()
        {
            DeviceStateQueue.Clear();
            ButtonQueue.Clear();
            SliderQueue.Clear();
            GestureQueue.Clear();
            ProcessingExceptions.Clear();
            DirtiedDeltas.Clear();
        }


        public void Dispose()
        {
            if (!IsDisposed)
            {
                foreach (var d in Drivers)
                {
                    try
                    { d.Dispose(); }
                    catch (Exception ex)
                    { LogProcessingException(d.GetType(), "Dispose", ex); }
                }
                IsDisposed = true;
            }
        }
    }
}

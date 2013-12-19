using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NetworkController.Client.Logic.Interfaces;
using NetworkController.Logic.Plugin.Interfaces;

namespace NetworkController.Logic.Plugin
{
    /* DeltaSliderState
     * -----------------
     * Represents a slider over time
     */


    [DataContract]
    public class DeltaSliderState : DeltaStateBase, IServerDeltaSliderState
    {
        public DeltaSliderState()
        {
            MaxValue = 255;
            MinValue = 0;
            DeltaType = 0;
        }

        private int _latestValue = 0;
        private long _latestTimeStamp = 0;
        public int Value { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int LastValue { get; set; }


        public void ApplyNewState(ISliderState newState)
        {
            if (newState.MaxValue > MaxValue) MaxValue = newState.MaxValue;
            if (newState.MinValue > MinValue) MinValue = newState.MinValue;
            if (newState.TimeStampTicks >= _latestTimeStamp)
            {
                _latestValue = newState.Value;
                _latestTimeStamp = newState.TimeStampTicks;
            }
        }

        protected override void ComputeStateDeltas()
        {
            var lastVelocity = Value - LastValue;
            LastValue = Value;
            Value = _latestValue;
            var thisVelocity = Value - LastValue;
            Acceleration = thisVelocity - lastVelocity;
            Velocity = thisVelocity;
        }
    }
}

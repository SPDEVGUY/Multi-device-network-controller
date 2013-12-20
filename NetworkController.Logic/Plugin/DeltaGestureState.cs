using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NetworkController.Client.Logic.DataTypes.Contracts;
using NetworkController.Client.Logic.DataTypes.Interfaces;
using NetworkController.Logic.Plugin.Interfaces;

namespace NetworkController.Logic.Plugin
{

    /* DeltaGestureState
     * -----------------
     * Delta gesture state allows tracking of gestures over state changes.
     */

    [DataContract]
    public class DeltaGestureState : MeasuredDeltaStateBase, IServerDeltaGestureState
    {
        public int Intensity { get; set; }
        public int LastIntensity { get; set; }
        public bool DetectedLastTick { get; set; }
        public bool DetectedThisTick { get; set; }
        private int _accumulatedIntensity = 0;

        public DeltaGestureState()
        {
            DeltaType = DeltaTypes.Gesture;
        }

        public void ApplyNewState(IGestureState newState)
        {
            DetectedThisTick = true; //If we are applying, then it was detected this time.
            _accumulatedIntensity += newState.Intensity;
        }

        protected override void ComputeStateDeltas()
        {
            var lastVelocity = Intensity - LastIntensity;
            LastIntensity = Intensity;
            Intensity = _accumulatedIntensity;
            var thisVelocity = Intensity - LastIntensity;
            Acceleration = thisVelocity - lastVelocity;
            Velocity = thisVelocity;
            _accumulatedIntensity = 0;
        }
    }
}

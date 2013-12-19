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

    /* DeltaGestureState
     * -----------------
     * Delta gesture state allows tracking of gestures over state changes.
     */

    [DataContract]
    public class DeltaGestureState : DeltaStateBase, IServerDeltaGestureState
    {
        public int Intensity { get; set; }
        public int LastIntensity { get; set; }
        public bool DetectedLastTick { get; set; }
        public bool DetectedThisTick { get; set; }
        private int _accumulatedIntensity = 0;

        public DeltaGestureState()
        {
            DeltaType = 2;
        }

        public void ApplyNewState(IGestureState newState)
        {
            DetectedThisTick = true; //If we are applying, then it was detected this time.
            _accumulatedIntensity += newState.Intensity;
        }

        protected override void ComputeStateDeltas()
        {
            LastIntensity = Intensity;
            Intensity = _accumulatedIntensity;
            Acceleration = Intensity - LastIntensity;
            Velocity = Velocity + Acceleration;
            _accumulatedIntensity = 0;
        }
    }
}

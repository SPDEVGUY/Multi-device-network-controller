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

    /* DeltaButtonState
     * -----------------
     * Represents the delta result for buttons
     */

    [DataContract]
    public class DeltaButtonState : DeltaStateBase, IServerDeltaButtonState
    {
        public bool IsPressed { get; set; }
        private int _accumulatedPressCount = 0;
        public int PressCount { get; set; }
        public int LastPressCount { get; set; }

        public DeltaButtonState()
        {
            DeltaType =1;
        }

        public void ApplyNewState(IButtonState newState)
        {
            var wasPressed = IsPressed;
            IsPressed = newState.IsPressed;
            if (!wasPressed && IsPressed)
            {
                _accumulatedPressCount++;
            }
        }

        protected override void ComputeStateDeltas()
        {
            LastPressCount = PressCount;
            PressCount = _accumulatedPressCount;
            _accumulatedPressCount = 0;

            Acceleration = Math.Max(PressCount - LastPressCount,0);
            Velocity = Velocity + Acceleration;
        }
    }
}

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

    /* DeltaButtonState
     * -----------------
     * Represents the delta result for buttons
     */

    [DataContract]
    public class DeltaButtonState : MeasuredDeltaStateBase, IServerDeltaButtonState
    {
        public bool IsPressed { get; set; }
        private int _accumulatedPressCount = 0;
        public int PressCount { get; set; }
        public int LastPressCount { get; set; }

        public DeltaButtonState()
        {
            DeltaType =DeltaTypes.Button;
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

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

    /* DeltaDeviceState
     * -----------------
     * Represents the delta result for device states
     */

    [DataContract]
    public class DeltaDeviceState : DeltaStateBase, IServerDeltaDeviceState
    {
        public bool IsEnabled { get; set; }
        public bool WasEnabled { get; set; }

        public DeltaDeviceState()
        {
            DeltaType =DeltaTypes.DeviceState;
        }

        public void ApplyNewState(IDeviceState newState)
        {
            WasEnabled = IsEnabled;
            IsEnabled = newState.IsEnabled;
        }

        protected override void ComputeStateDeltas()
        { }
    }
}

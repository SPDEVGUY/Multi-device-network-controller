using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NetworkController.Client.Logic.DataTypes.Interfaces;
using NetworkController.Logic.Plugin.Interfaces;

namespace NetworkController.Logic.Plugin
{

    /* DeviceState
     * -----------------
     * Represents the enabled state of a delta
     * The input is either enabed or disabled.
     */

    [DataContract]
    public class DeviceState : StateBase, IDeviceState
    {
        public bool IsEnabled { get; set; }
    }
}

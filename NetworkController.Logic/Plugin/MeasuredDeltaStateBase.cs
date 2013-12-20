using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NetworkController.Logic.Plugin.Interfaces;

namespace NetworkController.Logic.Plugin
{
    [DataContract]
    public abstract class MeasuredDeltaStateBase : DeltaStateBase, IServerDeltaState
    {
        public double Velocity { get; set; }
        public double Acceleration { get; set; }
 
    }
}

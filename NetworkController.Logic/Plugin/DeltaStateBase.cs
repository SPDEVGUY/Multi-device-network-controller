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
    public abstract class DeltaStateBase : StateBase, IServerDeltaState
    {
        public byte DeltaType { get; set; }
        public long TimeDelta { get; set; }


        public void FinalizeDelta()
        {
            var lastTicks = TimeStampTicks;
            TimeStampTicks = DateTime.Now.Ticks;
            TimeDelta = Math.Max(TimeStampTicks - lastTicks, 1); //Don't allow a delta of <= 0
            
            ComputeStateDeltas();
        }

        /// <summary>
        /// Acceleration, velocity should be calculated here using TimeDelta
        /// </summary>
        protected abstract void ComputeStateDeltas();
    }
}

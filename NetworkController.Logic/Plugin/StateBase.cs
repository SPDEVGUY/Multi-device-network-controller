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
    [DataContract]
    public class StateBase  :IState
    {
        public string InputName { get; set; }
        public string ProviderName { get; set; }
        public long TimeStampTicks { get; set; }

        public StateBase()
        {
            TimeStampTicks = DateTime.Now.Ticks;
            ProviderName = "Generic";
        }
    }
}

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

    /* ButtonState
     * -----------------
     * Represents any value item that can be turned on / off by depressing it
     * The input is either full on or full off.     * 
     */

    [DataContract]
    public class ButtonState : StateBase, IButtonState
    {
        public bool IsPressed { get; set; }
    }
}

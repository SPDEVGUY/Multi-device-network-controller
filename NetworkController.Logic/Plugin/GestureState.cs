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

    /* GestureState
     * -----------------
     * Represents a detected gesture provided directly from any API.
     * There are additional classes responsible for building gestures based off received input states.
     */

    [DataContract]
    public class GestureState : StateBase, IGestureState
    {

        public int Intensity { get; set; }

        public GestureState() { Intensity = 1; }

    }
}

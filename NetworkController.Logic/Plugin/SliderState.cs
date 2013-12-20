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
    /* SliderState
     * -----------------
     * Represents any value item that can be any point across a range.
     * Typically: Trigger buttons on controllers, Input X/Y coordinates.
     * 
     */


    [DataContract]
    public class SliderState : StateBase, ISliderState
    {
        public SliderState()
        {
            MaxValue = 255;
            MinValue = 0;
        }

        public int Value { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
    }
}

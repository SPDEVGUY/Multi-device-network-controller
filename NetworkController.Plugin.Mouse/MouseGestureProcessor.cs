using NetworkController.Logic.Controller;
using NetworkController.Logic.Plugin;
using NetworkController.Logic.Plugin.Attributes;
using NetworkController.Logic.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkController.Plugin.Mouse
{
    [GestureProcessor]
    public class MouseGestureProcessor : GestureProcessorBase
    {
        public MouseGestureProcessor() : base(Constants.ProviderName)
        { }

        public override void PerformGestureDetection()
        {
            //Find some slider deltas in the recently changed deltas
            var xPos = Observer.DirtiedDeltas.Find(
                    x =>
                    x.InputName == Constants.SliderXInputName &&
                    x.ProviderName == Constants.ProviderName
                );
            var yPos = Observer.DirtiedDeltas.Find(
                    x =>
                    x.InputName == Constants.SliderYInputName &&
                    x.ProviderName == Constants.ProviderName
                );

            //Deltas can be null if not found, so handle it.
            var xDiff = xPos == null ? 0 : xPos.Velocity;
            var yDiff = yPos == null ? 0 : yPos.Velocity;

            //Easier to understand logic if you make it a series of booleans...
            var activeLeft = xDiff < -50;
            var activeRight = xDiff > 50;
            var activeUp = yDiff < -50;
            var activeDown = yDiff > 50;
            var activeOnX = activeLeft || activeRight;
            var activeOnY = activeUp || activeDown;
            var xIntensity = (int)Math.Abs(xDiff) - 50;
            var yIntensity = (int)Math.Abs(yDiff) - 50;

            //Now recognize gestures off of booleans
            if (activeRight && !activeOnY) AddGestureState(Constants.GestureSwipeRight, xIntensity);
            if (activeLeft && !activeOnY) AddGestureState(Constants.GestureSwipeLeft, xIntensity);

            if (activeDown && !activeOnX) AddGestureState(Constants.GestureSwipeDown, yIntensity);
            if (activeUp && !activeOnX) AddGestureState(Constants.GestureSwipeUp, yIntensity);
        }
    }
}

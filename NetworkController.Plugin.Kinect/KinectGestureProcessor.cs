using Microsoft.Kinect;
using NetworkController.Client.Logic.DataTypes.Interfaces;
using NetworkController.Logic.Plugin;
using NetworkController.Logic.Plugin.Attributes;
using System;

namespace NetworkController.Plugin.Kinect
{
    [GestureProcessor]
    public class KinectGestureProcessor : GestureProcessorBase
    {
        public KinectGestureProcessor()
            : base(Constants.ProviderName)
        { }

        public const int HeadGestureVelocity = 75;
        public const int HandGestureVelocity = 50;
        public const int GestureDepth = 125;

        public override void PerformGestureDetection()
        {
            for (var ix = 0; ix < 8;ix++ )
            {
                var name = Constants.BodyName + ix;

                CheckHandGestures(name, JointType.HandRight);
                CheckHandGestures(name, JointType.HandLeft);

                CheckHeadGestures(name);
            }


            //TODO: Add gestrue recognition for hands, swiping and pressing.
            //Maybe add dodging on head tracking?

            //Find some slider deltas in the recently changed deltas
            //var xPos = Observer.DirtiedDeltas.Find(
            //        x =>
            //        x.InputName == Constants.SliderXInputName &&
            //        x.ProviderName == Constants.ProviderName
            //    ) as IMeasuredDeltaState;
            //var yPos = Observer.DirtiedDeltas.Find(
            //        x =>
            //        x.InputName == Constants.SliderYInputName &&
            //        x.ProviderName == Constants.ProviderName
            //    ) as IMeasuredDeltaState;

            ////Deltas can be null if not found, so handle it.
            //var xDiff = xPos == null ? 0 : xPos.Velocity;
            //var yDiff = yPos == null ? 0 : yPos.Velocity;

            ////Easier to understand logic if you make it a series of booleans...
            //var activeLeft = xDiff < -50;
            //var activeRight = xDiff > 50;
            //var activeUp = yDiff < -50;
            //var activeDown = yDiff > 50;
            //var activeOnX = activeLeft || activeRight;
            //var activeOnY = activeUp || activeDown;
            //var xIntensity = (int)Math.Abs(xDiff) - 50;
            //var yIntensity = (int)Math.Abs(yDiff) - 50;

            ////Now recognize gestures off of booleans
            //if (activeRight && !activeOnY) AddGestureState(Constants.GestureSwipeRight, xIntensity);
            //if (activeLeft && !activeOnY) AddGestureState(Constants.GestureSwipeLeft, xIntensity);

            //if (activeDown && !activeOnX) AddGestureState(Constants.GestureSwipeDown, yIntensity);
            //if (activeUp && !activeOnX) AddGestureState(Constants.GestureSwipeUp, yIntensity);
        }

        public void CheckHandGestures(string bodyName, JointType handJoint)
        {
            var handName = bodyName + "." + handJoint;
            var handZ = Observer.GetDirtiedDeltaByName<IDeltaSliderState>(Constants.ProviderName, handName + ".Z");
            if (handZ == null) return;

            var isPulling = handZ.Velocity < -HandGestureVelocity;
            if (isPulling) AddGestureState(handName + "." + Constants.Gestures.PullBack, (int)(handZ.Velocity + HandGestureVelocity));


            var gesturingActive = handZ.Value > GestureDepth;
            var gesturingActivating = handZ.LastValue <= GestureDepth && gesturingActive;
            if (gesturingActivating) AddGestureState(handName + "." + Constants.Gestures.PunchForward, (int)handZ.Velocity);
            if (gesturingActive)
            {
                AddGestureState(handName + "." + Constants.Gestures.Gesturing, 1);
                var handX = Observer.GetDirtiedDeltaByName<IDeltaSliderState>(Constants.ProviderName, handName + ".X");
                var handY = Observer.GetDirtiedDeltaByName<IDeltaSliderState>(Constants.ProviderName, handName + ".Y");

                var isSwipingLeft = handX.Velocity < -HandGestureVelocity;
                var isSwipingRight = handX.Velocity > HandGestureVelocity;
                var isSwipingUp = handY.Velocity > HandGestureVelocity;
                var isSwipingDown = handY.Velocity < -HandGestureVelocity;

                if (isSwipingLeft) AddGestureState(handName + "." + Constants.Gestures.SwipeLeft, (int)(handX.Velocity - HandGestureVelocity));
                if (isSwipingRight) AddGestureState(handName + "." + Constants.Gestures.SwipeRight, (int)(handX.Velocity + HandGestureVelocity));
                if (isSwipingUp) AddGestureState(handName + "." + Constants.Gestures.SwipeUp, (int)(handY.Velocity - HandGestureVelocity));
                if (isSwipingDown) AddGestureState(handName + "." + Constants.Gestures.SwipeDown, (int)(handY.Velocity + HandGestureVelocity));
            }
        }

        public void CheckHeadGestures(string bodyName)
        {
            var headName = bodyName + "." + JointType.Head;

            var headX = Observer.GetDirtiedDeltaByName<IDeltaSliderState>(Constants.ProviderName, headName + ".X");
            var headY = Observer.GetDirtiedDeltaByName<IDeltaSliderState>(Constants.ProviderName, headName + ".Y");
            var headZ = Observer.GetDirtiedDeltaByName<IDeltaSliderState>(Constants.ProviderName, headName + ".Z");

            var isDodgingLeft = headX != null && headX.Velocity < -HeadGestureVelocity;
            var isDodgingRight = headX != null && headX.Velocity > HeadGestureVelocity;
            var isDodgingForward = headZ != null && headZ.Velocity > HeadGestureVelocity;
            var isDodgingBack = headZ != null && headZ.Velocity < -HeadGestureVelocity;
            var isJumping = headY != null && headY.Velocity > HeadGestureVelocity;
            var isDucking = headY != null && headY.Velocity < -HeadGestureVelocity;

            if (isDodgingLeft)
                AddGestureState(headName + "." + Constants.Gestures.DodgeLeft, (int)(headX.Velocity + HeadGestureVelocity));
            if (isDodgingRight)
                AddGestureState(headName + "." + Constants.Gestures.DodgeRight, (int)(headX.Velocity - HeadGestureVelocity));
            if (isDodgingForward)
                AddGestureState(headName + "." + Constants.Gestures.DodgeForward, (int)(headZ.Velocity - HeadGestureVelocity));
            if (isDodgingBack)
                AddGestureState(headName + "." + Constants.Gestures.DodgeBack, (int)(headY.Velocity + HeadGestureVelocity));
            if (isJumping)
                AddGestureState(headName + "." + Constants.Gestures.Jump, (int)(headY.Velocity - HeadGestureVelocity));
            if (isDucking)
                AddGestureState(headName + "." + Constants.Gestures.Duck, (int)(headY.Velocity + HeadGestureVelocity));
        }
    }
}

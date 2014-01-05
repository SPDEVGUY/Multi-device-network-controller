using System.Collections.Generic;
using NetworkController.Logic.Plugin;
using NetworkController.Logic.Plugin.Attributes;
using SlimDX.XInput; //NOTE: Download from here http://slimdx.org/download.php

namespace NetworkController.Plugin.XboxControllers
{
    [DriverAbstracter]
    public class GamePadDriverAbstracter : DriverAbstracterBase
    {
        protected List<GamepadState> Pads;
        protected Dictionary<UserIndex, bool> Connected;

        //TODO: Add remove control for vibrators

        public GamePadDriverAbstracter()
            : base(Constants.ProviderName)
        { }

        protected override void CaptureCurrentState()
        {
            foreach (var pad in Pads)
            {
                if (Connected[pad.UserIndex])
                {
                    pad.Update();

                    var name = pad.UserIndex + ".";
                    AddButtonValue(name + Constants.Buttons.A, pad.A);
                    AddButtonValue(name + Constants.Buttons.B, pad.B);
                    AddButtonValue(name + Constants.Buttons.Back, pad.Back);
                    AddButtonValue(name + Constants.Buttons.LeftBumper, pad.LeftShoulder);
                    AddButtonValue(name + Constants.Buttons.LeftStick, pad.LeftStick.Clicked);
                    AddButtonValue(name + Constants.Buttons.LeftTrigger, pad.LeftTrigger >= 0.99);
                    AddButtonValue(name + Constants.Buttons.PadDown, pad.DPad.Down);
                    AddButtonValue(name + Constants.Buttons.PadLeft, pad.DPad.Left);
                    AddButtonValue(name + Constants.Buttons.PadRight, pad.DPad.Right);
                    AddButtonValue(name + Constants.Buttons.PadUp, pad.DPad.Up);
                    AddButtonValue(name + Constants.Buttons.RightBumper, pad.RightShoulder);
                    AddButtonValue(name + Constants.Buttons.RightStick, pad.RightStick.Clicked);
                    AddButtonValue(name + Constants.Buttons.RightTrigger, pad.RightTrigger >= 0.99);
                    AddButtonValue(name + Constants.Buttons.Start, pad.Start);
                    AddButtonValue(name + Constants.Buttons.X, pad.X);
                    AddButtonValue(name + Constants.Buttons.Y, pad.Y);

                    AddSliderValue(name + Constants.Sliders.LeftStickX, (int)(pad.LeftStick.Position.X*1000));
                    AddSliderValue(name + Constants.Sliders.LeftStickY, (int)(pad.LeftStick.Position.Y*1000));
                    AddSliderValue(name + Constants.Sliders.RightStickX, (int)(pad.RightStick.Position.X * 1000));
                    AddSliderValue(name + Constants.Sliders.RightStickX, (int)(pad.RightStick.Position.Y * 1000));
                    AddSliderValue(name + Constants.Sliders.RightTrigger, (int)(pad.RightTrigger * 1000));
                    AddSliderValue(name + Constants.Sliders.LeftTrigger, (int)(pad.LeftTrigger * 1000));
                }
                CheckDeviceState(pad);
            }
        }


        protected override void InitializeDriver()
        {
            Connected = new Dictionary<UserIndex, bool>();
            Connected[UserIndex.One] = false;
            Connected[UserIndex.Two] = false;
            Connected[UserIndex.Three] = false;
            Connected[UserIndex.Four] = false;

            Pads = new List<GamepadState>
                       {
                           new GamepadState(UserIndex.One),
                           new GamepadState(UserIndex.Two),
                           new GamepadState(UserIndex.Three),
                           new GamepadState(UserIndex.Four)
                       };
            foreach (var pad in Pads)
            {
                CheckDeviceState(pad);
            }
        }

        public void CheckDeviceState(GamepadState pad)
        {
            if (pad.Connected == Connected[pad.UserIndex]) return;
            
            SetDeviceState(pad.UserIndex,pad.Connected);
        }

        public void SetDeviceState(UserIndex index, bool isEnabled)
        {
            var name = index + ".";
            Connected[index] = isEnabled;
            AddDeviceStateValue(name + Constants.Buttons.A, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.B, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.Back, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.LeftBumper, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.LeftStick, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.LeftTrigger, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.PadDown, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.PadLeft, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.PadRight, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.PadUp, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.RightBumper, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.RightStick, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.RightTrigger, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.Start, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.X, isEnabled);
            AddDeviceStateValue(name + Constants.Buttons.Y, isEnabled);

            AddDeviceStateValue(name + Constants.Sliders.LeftStickX, isEnabled);
            AddDeviceStateValue(name + Constants.Sliders.LeftStickY, isEnabled);
            AddDeviceStateValue(name + Constants.Sliders.LeftTrigger, isEnabled);
            AddDeviceStateValue(name + Constants.Sliders.RightStickX, isEnabled);
            AddDeviceStateValue(name + Constants.Sliders.RightStickY, isEnabled);
            AddDeviceStateValue(name + Constants.Sliders.RightTrigger, isEnabled);
            
        }

        protected override void DisposeDriver()
        {
        }

        public override void ShowGui()
        {
            
        }
    }
}

using NetworkController.Logic.Plugin;
using NetworkController.Logic.Plugin.Attributes;
using NetworkController.Logic.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkController.Plugin.Mouse
{
    [DriverAbstracter]
    public class MouseDriverAbstracter : DriverAbstracterBase
    {

        public MouseDriverAbstracter()
            : base(Constants.ProviderName)
        { }

        protected override void CaptureCurrentState()
        {
            CaptureMousePosition();
            CaptureMouseButtons();
        }

        private void CaptureMouseButtons()
        {
            AddButtonValue(Constants.ButtonLeftInputName, MouseInterop.IsLeftMousePressed());
            AddButtonValue(Constants.ButtonRightInputName, MouseInterop.IsRightMousePressed());
            AddButtonValue(Constants.ButtonMiddleInputName, MouseInterop.IsMiddleMousePressed());
        }

        private void CaptureMousePosition()
        {
            var p = MouseInterop.GetCursorPosition();
            AddSliderValue(Constants.SliderXInputName, p.X, 0, p.X);
            AddSliderValue(Constants.SliderYInputName, p.Y, 0, p.Y);
        }

        protected override void InitializeDriver()
        {
        }

        protected override void DisposeDriver()
        {
        }
    }
}

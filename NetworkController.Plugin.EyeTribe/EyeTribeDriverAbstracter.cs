using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NetworkController.Logic.Plugin;
using NetworkController.Logic.Plugin.Attributes;

namespace NetworkController.Plugin.EyeTribe
{
    [DriverAbstracter]
    public class EyeTribeDriverAbstracter : DriverAbstracterBase
    {
        private Gui _gui;
        public EyeTribeProcessor Processor;


        public EyeTribeDriverAbstracter()
            : base(Constants.ProviderName)
        {
            Processor = new EyeTribeProcessor();
        }

        protected override void CaptureCurrentState()
        {
            var i = Processor.GazeInfo;

            if (i == null || i.Last == null || !i.IsUsable || !i.Last.IsUsable) return;

            var isTracking = i.Presence && i.Gaze && !i.Failed && !i.LostTracking;
            var wasTracking = i.Last.Presence && i.Last.Gaze && !i.Last.Failed && !i.Last.LostTracking;
            var leftEyeFound = i.LeftEye != null;
            var leftEyeWasFound = i.Last.LeftEye != null; 
            var rightEyeFound = i.RightEye != null;
            var rightEyeWasFound = i.Last.RightEye != null;
            

            if (isTracking != wasTracking)
            {
                AddDeviceStateValue("Tracking",isTracking);
                AddDeviceStateValue("LeftEye", leftEyeFound);
                AddDeviceStateValue("RightEye", rightEyeFound);
            }

            if (i.Failed || i.LostTracking) return;

            if (isTracking)
            {
                if(leftEyeFound != leftEyeWasFound) AddDeviceStateValue("LeftEye",leftEyeFound);
                if (rightEyeFound != rightEyeWasFound) AddDeviceStateValue("RightEye", rightEyeFound);

                if(i.IsFixated != i.Last.IsFixated && i.IsFixated) AddGestureValue("Fixated",1);

                if (i.Head != null)
                {
                    AddSliderValue("Head.X", (int) (i.Head.X*1000));
                    AddSliderValue("Head.Y", (int) (i.Head.Y*1000));
                    AddSliderValue("Head.Z", (int) (i.Head.Z*1000));
                }

                if (i.SmoothedCoordinates != null && i.SmoothedCoordinates.X != 0 && i.SmoothedCoordinates.Y != 0)
                {
                    AddSliderValue("Gaze.X", (int)(i.SmoothedCoordinates.X / Processor.CalibrationAreaSize.X * 1000));
                    AddSliderValue("Gaze.Y", (int)(i.SmoothedCoordinates.Y / Processor.CalibrationAreaSize.Y * 1000));
                }
            }
        }


        protected override void InitializeDriver()
        {
            Processor.Start();
        }

        protected override void DisposeDriver()
        {
            if (_gui != null && _gui.Visible)
            {
                if (_gui.InvokeRequired)
                    _gui.Invoke(new MethodInvoker(() => _gui.Close()));
                else _gui.Close();
            }

            Processor.Dispose();
        }

        public override void ShowGui()
        {
            if (_gui == null) _gui = new Gui(this);
            _gui.Abstracter = this;
            _gui.Show();
            _gui.Focus();
        }

    }
}

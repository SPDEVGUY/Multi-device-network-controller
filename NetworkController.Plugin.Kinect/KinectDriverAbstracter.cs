using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Kinect;
using NetworkController.Logic.Plugin;
using NetworkController.Logic.Plugin.Attributes;

namespace NetworkController.Plugin.Kinect
{
    [DriverAbstracter]
    public class KinectDriverAbstracter : DriverAbstracterBase
    {
        private Gui _gui;
        public KinectProcessor Processor;

        public bool SendLocalizedPositions = true;
        public bool SendPoints = true;
        public Dictionary<JointType, bool> SendJoint = new Dictionary<JointType, bool>();
        

        public KinectDriverAbstracter()
            : base(Constants.ProviderName)
        {
            var values = Enum.GetValues(typeof(JointType)) as JointType[];
            foreach (var value in values) SendJoint[value] = true;
        }

        protected override void CaptureCurrentState()
        {
            var bodies = Processor.GetBodies();

            var ix = 0;
            foreach (var body in bodies)
            {
                var name = "Body" + ix;
                if(!body.Position.IsTracked) AddDeviceStateValue(name, false);
                if (body.IsNew) AddDeviceStateValue(name, true);

                if (body.Position.IsTracked)
                {
                    AddSliderValue(name + ".X", (int) (body.Position.Position.X*1000.0));
                    AddSliderValue(name + ".Y", (int) (body.Position.Position.Y*1000.0));
                    AddSliderValue(name + ".Z", (int) (body.Position.Position.Z*1000.0));

                    if (SendPoints)
                    foreach (var point in body.Points)
                    {
                        if (SendJoint[point.Key])
                        {
                            var p = point.Value;
                            var pointName = name + "." + point.Key + ".";


                            if (p.IsTracked != p.WasTracked) AddDeviceStateValue(pointName, p.IsTracked);

                            if (SendLocalizedPositions)
                            {
                                AddSliderValue(pointName + "X", (int) (p.LocalizedPosition.X*1000.0));
                                AddSliderValue(pointName + "Y", (int) (p.LocalizedPosition.Y*1000.0));
                                AddSliderValue(pointName + "Z", (int) (p.LocalizedPosition.Z*1000.0));
                            }
                            else
                            {
                                AddSliderValue(pointName + "X", (int) (p.Position.X*1000.0));
                                AddSliderValue(pointName + "Y", (int) (p.Position.Y*1000.0));
                                AddSliderValue(pointName + "Z", (int) (p.Position.Z*1000.0));
                            }
                        }
                    }
                }

                ix++;
            }
        }

        private void CaptureButtons()
        {
           
        }

        protected override void InitializeDriver()
        {
            Processor = new KinectProcessor();
            
            Processor.Start(); //TODO: Move to gui.

            //Indicate our states are enabled
            AddDeviceStateValue(Constants.ProviderName,false);
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

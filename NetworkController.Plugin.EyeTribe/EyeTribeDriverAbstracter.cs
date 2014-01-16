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
            //TODO
            var i = Processor.GazeInfo;

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

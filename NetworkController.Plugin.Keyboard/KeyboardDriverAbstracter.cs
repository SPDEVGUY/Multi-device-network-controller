using System.Collections.Generic;
using System.Windows.Forms;
using NetworkController.Logic.Plugin;
using NetworkController.Logic.Plugin.Attributes;

namespace NetworkController.Plugin.Keyboard
{
    [DriverAbstracter]
    public class KeyboardDriverAbstracter : DriverAbstracterBase
    {
        private bool _isActive = false;
        private bool _isActiveChanged = false;
        private List<string> _activeKeys = new List<string>();
        private List<string> _stoppedKeys = new List<string>();
        private Gui _gui;

        public KeyboardDriverAbstracter()
            : base(Constants.ProviderName)
        { }

        protected override void CaptureCurrentState()
        {
            lock (this)
                if (_isActiveChanged)
                    AddDeviceStateValue(Constants.ProviderName,_isActive);

            CaptureButtons();
        }

        private void CaptureButtons()
        {
            lock (this)
            {
                foreach (var key in _activeKeys)
                {
                    AddButtonValue(key,true);   
                }

                foreach (var key in _stoppedKeys)
                {
                    AddButtonValue(key, false);
                }
                _stoppedKeys.Clear();
            }
        }

        protected override void InitializeDriver()
        {
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
        }

        public override void ShowGui()
        {
            if (_gui == null) _gui = new Gui(this);
            _gui.Abstracter = this;
            _gui.Show();
            _gui.Focus();
        }


        public void SetState(bool active)
        {
            lock (this)
            {
                _isActive = active;
                _isActiveChanged = true;
                _stoppedKeys.AddRange(_activeKeys);
                _activeKeys.Clear();
            }
        }

        public void SetKeyState(string key, bool isPressed)
        {
            lock (this)
            {
                if (isPressed)
                {
                    if (!_activeKeys.Contains(key)) _activeKeys.Add(key);
                }
                else
                {
                    if (_activeKeys.Contains(key)) _activeKeys.Remove(key);
                    if (!_stoppedKeys.Contains(key)) _stoppedKeys.Add(key);
                }
            }
        }
    }
}

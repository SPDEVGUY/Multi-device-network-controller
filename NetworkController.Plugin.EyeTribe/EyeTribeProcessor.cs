using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TETCSharpClient;
using TETCSharpClient.Data;
using TETWinControls;
using TETWinControls.Calibration;

namespace NetworkController.Plugin.EyeTribe
{
    public class EyeTribeProcessor : IDisposable, IGazeUpdateListener
    {
        public bool IsConnected;
        public bool IsCalibrated;
        public CalibrationRatingEnum CalibrationRating;
        public Point2D CalibrationAreaSize = new Point2D();
        private bool _gazeVisible;
        private GazeDot _gazeDot;
        public bool GazeVisible
        {
            get { return _gazeVisible; }
            set { 
                _gazeVisible = value;
                if (_gazeDot == null)
                {
                    _gazeDot = new GazeDot();
                }
                if(_gazeVisible) _gazeDot.Show();
                else _gazeDot.Hide();
            }
        }

        public GazeDelta GazeInfo;


        public void Start()
        {
            GazeManager.Instance.Activate(1, GazeManager.ClientMode.Push);
            GazeManager.Instance.AddGazeListener(this);

            IsConnected = GazeManager.Instance.IsConnected;
            IsCalibrated = IsConnected && GazeManager.Instance.IsCalibrated;
            CalibrationAreaSize.X = Screen.PrimaryScreen.Bounds.Width;
            CalibrationAreaSize.Y = Screen.PrimaryScreen.Bounds.Height;

        }

        public void Stop()
        {
            GazeVisible = false;
            GazeManager.Instance.Deactivate();
        }

        public void Dispose()
        {
            Stop();
        }

        public void RunCalibration(Form sourceForm)
        {
            //TODO: Code own calibration thing for remote screen calibration.

            Utility.Instance.RecordingScreen = Screen.FromHandle(sourceForm.Handle);
            var calRunner = new CalibrationRunner();
            IsCalibrated = calRunner.Start();
            CalibrationRating = calRunner.GetLatestCalibrationResult().GetRating();
            CalibrationAreaSize.X = calRunner.CalibrationAreaSize.Width;
            CalibrationAreaSize.Y = calRunner.CalibrationAreaSize.Height;
        }
        

        public void OnCalibrationStateChanged(bool isCalibrated)
        {
            IsCalibrated = isCalibrated;
        }

        public void OnGazeUpdate(GazeData gazeData)
        {
            if(GazeInfo == null) GazeInfo = new GazeDelta();
            GazeInfo.Apply(gazeData);
        }

        public void OnScreenIndexChanged(int screenIndex)
        {
            //?
        }
    }
}

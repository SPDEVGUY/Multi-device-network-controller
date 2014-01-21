using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;
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
        public Correction Correction ;

        private bool _gazeVisible;
        private GazeDot _gazeDot;
        private GazeDot _gazeDotCorrected;
        public bool GazeVisible
        {
            get { return _gazeVisible; }
            set
            {
                _gazeVisible = value;
                if (_gazeDot == null && value)
                {
                    _gazeDot = new GazeDot(System.Windows.Media.Color.FromRgb(255,0,0));
                    _gazeDotCorrected = new GazeDot(System.Windows.Media.Color.FromRgb(0, 255, 255));
                    _gazeDotCorrected.EnableCorrection = true;
                }
                if(_gazeDot != null) _gazeDot.ToggleVisibilitySafely(value);
                if (_gazeDotCorrected != null) _gazeDotCorrected.ToggleVisibilitySafely(value);
            }
        }

        private GazeDelta _gazeInfo;
        public GazeDelta GazeInfo
        {
            get
            {
                if (_gazeInfo == null) return null;
                return _gazeInfo.Clone();
            }
        }

        public void Start()
        {
            GazeManager.Instance.Activate(1, GazeManager.ClientMode.Push);
            GazeManager.Instance.AddGazeListener(this);
            Correction = new Correction();

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
            Correction.Dispose();
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
            if(_gazeInfo == null) _gazeInfo = new GazeDelta();
            _gazeInfo = _gazeInfo.Next(gazeData);
        }

        public void OnScreenIndexChanged(int screenIndex)
        {
            //?
        }
    }
}

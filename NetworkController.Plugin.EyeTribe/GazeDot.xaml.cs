using System;
using System.Windows.Media;
using System.Windows.Threading;
using TETCSharpClient;
using TETCSharpClient.Data;
using TETWinControls;

namespace NetworkController.Plugin.EyeTribe
{
	public partial class GazeDot : IGazeUpdateListener
	{
	    public bool EnableCorrection;

		public GazeDot(Color fillColor)
		{
			InitializeComponent();
			GazeManager.Instance.AddGazeListener(this);
		    Circle.Fill = new SolidColorBrush(fillColor);
		}

		public void OnScreenIndexChanged(int number)
		{
		}

		public void OnCalibrationStateChanged(bool val)
		{
		}

	    public void ToggleVisibilitySafely(bool value)
	    {
	        if (!Dispatcher.CheckAccess())
	        {
	            Dispatcher.BeginInvoke(
	                new Action(() => ToggleVisibilitySafely(value)));
	            return;
	        }
            if (value) Show();
            else Hide();
	    }


	    public void OnGazeUpdate(GazeData gazeData)
		{
			if (Dispatcher.CheckAccess() == false)
			{
				Dispatcher.BeginInvoke(new Action(() => OnGazeUpdate(gazeData)));
				return;
			}

			// Start or stop tracking lost animation
			if ((gazeData.State & GazeData.STATE_TRACKING_GAZE) == 0 &&
			    (gazeData.State & GazeData.STATE_TRACKING_PRESENCE) == 0) return;
			//Tracking coordinates
			var d = Utility.Instance.ScaleDpi;
			var x = Utility.Instance.RecordingPosition.X;
			var y = Utility.Instance.RecordingPosition.Y;

			//var gX = gazeData.RawCoordinates.X;
			//var gY = gazeData.RawCoordinates.Y;

	        var smoothedCoordinates = gazeData.SmoothedCoordinates;
            var gX = smoothedCoordinates.X;
            var gY = smoothedCoordinates.Y;

            if (EnableCorrection)
            {
                var diff = Correction.Instance.CorrectPoint(smoothedCoordinates);
                gX = diff.X;
                gY = diff.Y;
            }

			Left = d*x + d*gX - Width/2;
			Top = d*y + d*gY - Height/2;
		}
	}
}

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using TETCSharpClient;
using TETCSharpClient.Data;

namespace NetworkController.Plugin.EyeTribe
{
    public class Correction : IGazeUpdateListener,IDisposable
    {
        public static Correction Instance;

        public double ZInitial;
        public double ZCurrent;
        public double YGaze;
        public double YMouse;
        public double YDelta;
        public double YConstantOnFarZ;
        public double YConstantOnNearZ;


        public Correction()
        {
            Instance = this;
            GazeManager.Instance.AddGazeListener(this);
        }
        public void Dispose()
        {
            GazeManager.Instance.RemoveGazeListener(this);
            Instance = null;
        }

        /// <summary>
        /// When looking at a point on the screen and the dot using the normal eyetribe result is exactly on the point you are looking at.
        /// </summary>
        public void SetSweetSpot()
        {
            ZInitial = ZCurrent;
        }


        /// <summary>
        /// Assuming user is looking directly at the tip of mouse cursor the offsets can be calculated.
        /// The user should be leaned back or forward enough to make a noticable error.
        /// </summary>
        public void SetDeltas()
        {
            YDelta = YMouse - YGaze;

            //Perform calcs for constants
            //Yp= Sqr(Yd) / (Zc - Zi)
            if(ZCurrent < ZInitial) YConstantOnFarZ = Math.Sqrt(Math.Abs(YDelta))/(ZCurrent - ZInitial);
            else YConstantOnNearZ = Math.Sqrt(Math.Abs(YDelta))/(ZCurrent - ZInitial); //Different constant for near
        }


        public void OnGazeUpdate(GazeData gazeData)
        {

            var isValid = ((gazeData.State & GazeData.STATE_TRACKING_GAZE) != 0)
                          && ((gazeData.State & GazeData.STATE_TRACKING_PRESENCE) != 0)
                          && ((gazeData.State & GazeData.STATE_TRACKING_EYES) != 0)
                          && ((gazeData.State & GazeData.STATE_TRACKING_FAIL) == 0)
                          && ((gazeData.State & GazeData.STATE_TRACKING_LOST) == 0)
                          && gazeData.SmoothedCoordinates != null
                          && gazeData.SmoothedCoordinates.X != 0
                          && gazeData.SmoothedCoordinates.Y != 0
                ;
            if (!isValid) return;

            var headPosition = gazeData.HeadPosition();
            if (headPosition == null) return;

            //Now that we can suppose the data is probably valid we can make use of it.
            var cursorPoint = GetCursorPosition();
            var gazePoint = gazeData.SmoothedCoordinates;

            ZCurrent = headPosition.Z;
            YGaze = gazePoint.Y;
            YMouse = cursorPoint.Y;

        }

        public Point2D CorrectPoint(Point2D point)
        {
            var result = new Point2D(point);
            var y = (ZCurrent - ZInitial)* (ZCurrent<ZInitial ? YConstantOnFarZ : YConstantOnNearZ );
            result.Y = result.Y - (y*y); //Yd = ((Zc-Zi)*Yc)^2
            return result;
        }


        public void OnCalibrationStateChanged(bool isCalibrated) { }

        public void OnScreenIndexChanged(int screenIndex) { }

        #region Windows Mouse API
        
        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            //bool success = User32.GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }
        #endregion
    }

}

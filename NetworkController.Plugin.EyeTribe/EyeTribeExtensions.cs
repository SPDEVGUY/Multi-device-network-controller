using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TETCSharpClient.Data;

namespace NetworkController.Plugin.EyeTribe
{
    public static class EyeTribeExtensions
    {

        /// <summary>
        /// Minimum distance from sensor is about 1 foot
        /// </summary>
        public const double PUPIL_DISTANCE_MAX = 0.35d;

        /// <summary>
        /// Max distance is about 3-4 feet
        /// </summary>
        public const double PUPIL_DISTANCE_MIN = 0.10d;

        /// <summary>
        /// Returned when no pupil distance is not accurate enough to compute
        /// </summary>
        public const double PUPIL_DISTANCE_NOTACCURATE = -100d;

        /// <summary>
        /// Computed using pupil distance to return a value between 0 and 1 for z distance from sensor within 1-4 foot range.
        /// </summary>
        public static double HeadDistance(this GazeData data)
        {
            if ((data.State & GazeData.STATE_TRACKING_GAZE) == 0) return PUPIL_DISTANCE_NOTACCURATE;
            if ((data.State & GazeData.STATE_TRACKING_PRESENCE) == 0) return PUPIL_DISTANCE_NOTACCURATE;
            if ((data.State & GazeData.STATE_TRACKING_FAIL) != 0) return PUPIL_DISTANCE_NOTACCURATE;
            if ((data.State & GazeData.STATE_TRACKING_LOST) != 0) return PUPIL_DISTANCE_NOTACCURATE;


            if (data.LeftEye == null || data.RightEye == null) return PUPIL_DISTANCE_NOTACCURATE;

            var leftCenter = data.LeftEye.PupilCenterCoordinates;
            var rightCenter = data.RightEye.PupilCenterCoordinates;
            if (leftCenter == null || rightCenter == null) return PUPIL_DISTANCE_NOTACCURATE;

            var xDist = (rightCenter.X - leftCenter.X);
            var yDist = (rightCenter.Y - leftCenter.Y);

            if (xDist == 0 && yDist == 0) return PUPIL_DISTANCE_NOTACCURATE; //Sensor returning wonky values

            var dist = Math.Sqrt(xDist * xDist + yDist * yDist);
            if (dist > PUPIL_DISTANCE_MAX) return PUPIL_DISTANCE_NOTACCURATE; //Sensor returning wonky values

            dist -= PUPIL_DISTANCE_MIN;
            return dist / (PUPIL_DISTANCE_MAX - PUPIL_DISTANCE_MIN);
        }

        public static Point3D HeadPosition(this GazeData data)
        {
            if ((data.State & GazeData.STATE_TRACKING_GAZE) == 0) return null;
            if ((data.State & GazeData.STATE_TRACKING_PRESENCE) == 0) return null;
            if ((data.State & GazeData.STATE_TRACKING_FAIL) != 0) return null;
            if ((data.State & GazeData.STATE_TRACKING_LOST) != 0) return null;


            if (data.LeftEye == null || data.RightEye == null) return null;

            var leftCenter = data.LeftEye.PupilCenterCoordinates;
            var rightCenter = data.RightEye.PupilCenterCoordinates;
            if (leftCenter == null || rightCenter == null) return null;

            var xDist = (rightCenter.X - leftCenter.X);
            var yDist = (rightCenter.Y - leftCenter.Y);

            if (xDist == 0 && yDist == 0) return null; //Sensor returning wonky values

            var dist = Math.Sqrt(xDist * xDist + yDist * yDist);
            if (dist > PUPIL_DISTANCE_MAX) return null; //Sensor returning wonky values

            var result = new Point3D(
                (leftCenter.X + rightCenter.X)*0.5, 
                (leftCenter.Y + rightCenter.Y)*0.5,
                dist);
            return result;
        }
    }

    public class Point3D
    {
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double X;
        public double Y;
        public double Z;
    }
}

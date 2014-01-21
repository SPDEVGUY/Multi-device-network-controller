using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TETCSharpClient.Data;

namespace NetworkController.Plugin.EyeTribe
{
    public class GazeDelta
    {
        public bool IsUsable = false;

        //TODO: Potentially get head rotation from pupil size difference
        //TODO: fix Y axis misalignment with z distance calibration



        public GazeDelta Last;
        public bool IsFixated;
        public long TimeStamp;
        public bool Presence;
        public bool Gaze;
        public bool Failed;
        public bool LostTracking;
        public bool Eyes;
        public GazeEye LeftEye;
        public GazeEye RightEye;
        public Point2D RawCoordinates;
        public Point2D SmoothedCoordinates;
        public Point3D Head;



        public GazeDelta Next(GazeData data)
        {
            if (data == null) return null;
            var result =
                new GazeDelta
                    {
                        IsFixated = data.IsFixated,
                        TimeStamp = data.TimeStamp,
                        Presence = (data.State & GazeData.STATE_TRACKING_PRESENCE) != 0,
                        Gaze = (data.State & GazeData.STATE_TRACKING_PRESENCE) != 0,
                        Failed = (data.State & GazeData.STATE_TRACKING_FAIL) != 0,
                        LostTracking = (data.State & GazeData.STATE_TRACKING_LOST) != 0,
                        Eyes = (data.State & GazeData.STATE_TRACKING_EYES) != 0,

                        LeftEye = data.LeftEye,
                        RightEye = data.RightEye,

                        RawCoordinates = data.RawCoordinates,
                        SmoothedCoordinates = data.SmoothedCoordinates,

                        Head = data.HeadPosition(),

                        IsUsable = true,
                        Last = this
                    };
            Last = null;
            return result;
        }

        public GazeDelta Clone()
        {
            return new GazeDelta
            {
                IsFixated = IsFixated,
                TimeStamp = TimeStamp,
                Presence = Presence,
                Gaze = Gaze,
                Failed = Failed,
                LostTracking = LostTracking,
                Eyes = Eyes,

                LeftEye = LeftEye,
                RightEye = RightEye,

                RawCoordinates = RawCoordinates,
                SmoothedCoordinates = SmoothedCoordinates,

                Head = Head,

                IsUsable = true,
                Last = (Last !=null) ? Last.Clone() : null
            };
        }
    }
}

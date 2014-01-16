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

        public bool WasFixated;
        public long LastTimeStamp;
        public bool HadPresence;
        public bool HadGaze;
        public bool HadFailed;
        public bool HadLostTracking;
        public bool HadEyes;
        public GazeEye LastLeftEye;
        public GazeEye LastRigthEye;

        public Point2D LastRawCoordinates;
        public Point2D LastSmoothedCoordinates;

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


        private void Smoosh()
        {
            WasFixated = IsFixated;
            LastTimeStamp = TimeStamp;
            HadPresence = Presence;
            HadGaze = Gaze;
            HadFailed = Failed;
            HadLostTracking = LostTracking;
            HadEyes = Eyes;
            LastRigthEye = LeftEye;
            LastRawCoordinates = RawCoordinates;
            LastSmoothedCoordinates = SmoothedCoordinates;
        }


        public void Apply(GazeData data)
        {
            Smoosh();
            
            IsFixated = data.IsFixated;
            TimeStamp = data.TimeStamp;
            Presence = (data.State & GazeData.STATE_TRACKING_PRESENCE) != 0;
            Gaze = (data.State & GazeData.STATE_TRACKING_PRESENCE) != 0;
            Failed = (data.State & GazeData.STATE_TRACKING_FAIL) != 0;
            LostTracking = (data.State & GazeData.STATE_TRACKING_LOST) != 0;
            Eyes = (data.State & GazeData.STATE_TRACKING_EYES) != 0;

            LeftEye = data.LeftEye;
            RightEye = data.RightEye;

            RawCoordinates = data.RawCoordinates;
            SmoothedCoordinates = data.SmoothedCoordinates;

            IsUsable = true;
        }
    }
}

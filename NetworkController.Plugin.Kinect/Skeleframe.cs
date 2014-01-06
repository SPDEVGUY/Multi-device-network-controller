using System;
using System.Runtime.Serialization;
using Microsoft.Kinect;

namespace NetworkController.Plugin.Kinect
{

    [DataContract]
    public class Skeleframe
    {
        [DataMember]
        public Skeleton[] Skeletons;

        [DataMember]
        public Tuple<float, float, float, float> FloorClipPlane;

        [DataMember]
        public long TimeStamp;

        [DataMember]
        public long TimeDelta;

        [DataMember]
        public int SkeletonFrameNumber;

        [DataMember]
        public int SensorIndex;

        [DataMember]
        public string KinectId;
    }
}

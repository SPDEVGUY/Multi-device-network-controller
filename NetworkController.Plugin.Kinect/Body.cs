using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace NetworkController.Plugin.Kinect
{
    public class Body
    {
        public int SensorIndex;
        public int TrackingId;

        public int PlayerIndex;
        public bool IsNew;
        internal bool _IsAdding;
        public bool IsActive;
        public DeltaPoint Position;
        public Dictionary<JointType, DeltaPoint> Points;
        

        public Body(Skeleton skele)
        {
            var tracked = skele.TrackingState != SkeletonTrackingState.NotTracked;
            Position = new DeltaPoint(skele.Position, tracked);
            Points = new Dictionary<JointType, DeltaPoint>();

            SensorIndex = 0;
            TrackingId = skele.TrackingId;
            _IsAdding = true;

            ApplyFrame(skele);

        }

        public void ApplyFrame(Skeleton skele)
        {
            var tracked = skele.TrackingState != SkeletonTrackingState.NotTracked;

            Position.SetPosition(skele.Position,skele.Position);
            Position.SetTracking(tracked);

            foreach (Joint joint in skele.Joints)
            {
                var isJointTracked = joint.TrackingState == JointTrackingState.Tracked;

                if (!Points.ContainsKey(joint.JointType)) Points[joint.JointType] = new DeltaPoint(joint.Position,isJointTracked);

                var point = Points[joint.JointType];

                point.SetPosition(joint.Position,skele.Position);
                point.SetTracking(isJointTracked);
                point.SetRotations(skele.BoneOrientations[joint.JointType]);
            }
        }

        public class DeltaPoint
        {
            public SkeletonPoint Position;
            public SkeletonPoint LocalizedPosition;

            public BoneRotation RelativeRotationNow;
            public BoneRotation AbsoluteRotationNow;

            public bool IsTracked;
            public bool WasTracked;

            public DeltaPoint(SkeletonPoint point, bool isTracked)
             {
                 Position = point;
                 IsTracked = isTracked;
                 WasTracked = isTracked;
                 RelativeRotationNow = null;
                 AbsoluteRotationNow = null;
             }

            public void SetPosition(SkeletonPoint point, SkeletonPoint relativeTo)
            {
                Position = point;
                LocalizedPosition.X = point.X - relativeTo.X;
                LocalizedPosition.Y = point.Y - relativeTo.Y;
                LocalizedPosition.Z = relativeTo.Z - point.Z; //Kinect Z is screwy
            }

            public void SetTracking(bool isTracked)
            {
                WasTracked = IsTracked;
                IsTracked = isTracked;
            }

            internal void SetRotations(BoneOrientation boneOrientation)
            {
                RelativeRotationNow = boneOrientation.HierarchicalRotation;
                AbsoluteRotationNow = boneOrientation.AbsoluteRotation;
            }
        }


        
    }

    
}

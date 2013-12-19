using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace NetworkController.Plugin.Kinect
{
    /// <summary>
    /// This class helps to comine multiple kinects for incredibly acurrate tracking abilities.
    /// </summary>
    public class MultiKinectProcessor
    {
        public Skeleton Player1 = new Skeleton();
        public Skeleton Player2 = new Skeleton();
        public Skeleton Player3 = new Skeleton();
        public Skeleton Player4 = new Skeleton();

        public bool[] ActivePlayers = new bool[4];
        public Dictionary<int,List<SkeletonSensorState>> SensorStates;

        protected List<SkeletonSensorState> GetStatesForSensor(int sensorIndex)
        {
            if (!SensorStates.ContainsKey(sensorIndex)) SensorStates[sensorIndex] = new List<SkeletonSensorState>();
            return SensorStates[sensorIndex];
        }
        protected void PreApplyFrameToStates(List<SkeletonSensorState> states)
        {
            foreach (var s in states) s.WasTrackingLastFrame = false;
        }
        protected void ApplyFrameToStates(Skeleframe frame, List<SkeletonSensorState> states )
        {
            foreach (var sk in frame.Skeletons)
            {
                if (sk.TrackingState != SkeletonTrackingState.NotTracked)
                {
                    var state = states.Find(x => x.TrackingId == sk.TrackingId);
                    if (state == null)
                    {
                        state = new SkeletonSensorState
                        {
                            CurrentSkeleton = sk,
                            LastSkeleton = sk,
                            PlayerIndex = GetNextPlayerIndex(),
                            TrackingId = sk.TrackingId,
                            SensorIndex = frame.SensorIndex,
                            WasTrackingLastFrame = true
                        };

                        ActivePlayers[state.PlayerIndex] = true;
                        states.Add(state);
                        //TODO: Raise new player event?
                    }
                    state.LastSkeleton = state.CurrentSkeleton;
                    state.CurrentSkeleton = sk;
                    state.WasTrackingLastFrame = true;
                }
            }
        }
        
        protected void CleanUpStatesList(List<SkeletonSensorState> states)
        {
            var removeThese = states.Where(s => !s.WasTrackingLastFrame).ToList();
            foreach (var removeState in removeThese)
            {
                states.Remove(removeState);
                ActivePlayers[removeState.PlayerIndex] = false;
                //TODO: Event on player lost.
            }
            
        }

        public void ApplyFrame(Skeleframe frame)
        {
            var states = GetStatesForSensor(frame.SensorIndex);
            PreApplyFrameToStates(states);
            ApplyFrameToStates(frame, states);
            CleanUpStatesList(states);

            
        }

        public void Reset()
        {
            SensorStates = new Dictionary<int, List<SkeletonSensorState>>();
            ActivePlayers = new bool[4];
        }

        public int GetNextPlayerIndex()
        {
            for (var i = 0; i < 4; i++)
            {
                if (!ActivePlayers[i]) return i;
            }
            return -1;
        }
    }

    public class SkeletonSensorState
    {
        public int PlayerIndex;
        public int TrackingId;
        public int SensorIndex;
        public bool WasTrackingLastFrame;
        public Skeleton LastSkeleton;
        public Skeleton CurrentSkeleton;
    }

    public class ComputedSkeleton
    {
        public Dictionary<int, SkeletonSensorState> Skeletons;
        public SkeletonPoint Position;
        public Dictionary<JointType, SkeletonPoint> Joints;
        public Dictionary<JointType, bool> JointTracked;

        public void ApplySkeleton(SkeletonSensorState state)
        {
            //TODO: Computational mashing
        }

    }
}

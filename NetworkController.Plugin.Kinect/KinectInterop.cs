using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace NetworkController.Plugin.Kinect
{
    public class KinectInterop : IDisposable
    {
        public List<KinectSensor> ActiveSensors = new List<KinectSensor>();
        private long[] _lastTimeStamps;

        private bool _isRunning = false;
        public bool HasFrames = false;
        public TransformSmoothParameters Smoothing = new TransformSmoothParameters
        {
            Smoothing = 0.5f,
            Correction = 0.1f,
            Prediction = 0.1f,
            JitterRadius = 0.05f,
            MaxDeviationRadius = 0.05f
        };


        private Queue<Skeleframe> _frameQueue = new Queue<Skeleframe>();
        

        public void Start()
        {
            _isRunning = true;
            ActiveSensors = KinectSensor.KinectSensors.ToList();
            _lastTimeStamps = new long[ActiveSensors.Count];

            foreach (var sensor in ActiveSensors)
            {
                sensor.Start();
                sensor.SkeletonFrameReady += SensorFrameReady;
                sensor.SkeletonStream.Enable(Smoothing);
            }
        }

        public void Stop()
        {
            if (ActiveSensors != null && ActiveSensors.Count > 0)
            {
                foreach (var sensor in ActiveSensors)
                {
                    sensor.SkeletonStream.Disable();
                    sensor.Stop();
                    sensor.Dispose();
                }

                ActiveSensors.Clear();
            }
            _isRunning = false;
        }

        public void Dispose()
        {
            if(_isRunning) Stop();
            lock(_frameQueue) _frameQueue.Clear();
        }

        public List<Skeleframe> PopQueue()
        {
            List<Skeleframe> result;

            lock (_frameQueue)
            {
                result = new List<Skeleframe>(_frameQueue.ToArray());
                _frameQueue.Clear();
                HasFrames = false;
            }
            return result;
        }

        void SensorFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            var sensor = (KinectSensor) sender;
            var sensorIndex = ActiveSensors.IndexOf(sensor);
            Skeleframe frame;
            using (var skeleFrame = e.OpenSkeletonFrame())
            {
                if (skeleFrame == null) return;

                var timeDelta = skeleFrame.Timestamp - _lastTimeStamps[sensorIndex];
                _lastTimeStamps[sensorIndex] = skeleFrame.Timestamp;

                frame = new Skeleframe
                {
                    FloorClipPlane = skeleFrame.FloorClipPlane,
                    SkeletonFrameNumber = skeleFrame.FrameNumber,
                    TimeStamp = skeleFrame.Timestamp,
                    TimeDelta = timeDelta,
                    SensorIndex = sensorIndex,
                    Skeletons = new Skeleton[skeleFrame.SkeletonArrayLength],
                    KinectId = sensor.UniqueKinectId

                };
                skeleFrame.CopySkeletonDataTo(frame.Skeletons);

                lock (_frameQueue)
                {
                    _frameQueue.Enqueue(frame);
                    HasFrames = true;
                }
            }
        }
    }

}

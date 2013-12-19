using System.IO;
using System.Runtime.Serialization;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NetworkController.Plugin.Kinect
{
    //Made this class so I can easily record stuff from a kinect save to file and replay it.
    public class KinectRecorder : IDisposable
    {
        public delegate void FrameIncommingHandler(Skeleframe frame);
        public FrameIncommingHandler FrameIncomming;
        protected void RaiseFrameIncomming(Skeleframe frame)
        {
            if (FrameIncomming != null) FrameIncomming(frame);
        }

        public RecordedKinectFrames Recorded = new RecordedKinectFrames();
        protected List<KinectSensor> ActiveSensors = new List<KinectSensor>();
        public bool IsRecording { get; private set; }
        public bool IsPlaying  { get; private set; }
        public bool IsSensing { get; private set; }
        private Thread _playbackThread;

        public TransformSmoothParameters Smoother = new TransformSmoothParameters
        {
            Smoothing = 0.5f,
            Correction = 0.1f,
            Prediction = 0.1f,
            JitterRadius = 0.05f,
            MaxDeviationRadius = 0.05f
        };

        public void StartRecording()
        {
            if (IsRecording) return;
            if(IsPlaying) throw new Exception("Currently playing.  Cannot record.");
            IsRecording = true;
            Recorded = new RecordedKinectFrames();
            if(!IsSensing) StartSensing();
        }

        public void StopRecording(string fileName)
        {
            if (!IsRecording) return;

            IsRecording = false;
            using (var fs = new FileStream(fileName, FileMode.CreateNew))
            {
                var ds = new DataContractSerializer(typeof (RecordedKinectFrames));
                ds.WriteObject(fs, Recorded);
            }
            Recorded.Frames.Clear();
        }

        public bool LoadRecorded(string fileName)
        {
            if (IsPlaying) throw new Exception("Currently playing.  Cannot load.");
            if (IsRecording) throw new Exception("Currently recording.  Cannot load.");

            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                var ds = new DataContractSerializer(typeof(RecordedKinectFrames));
                Recorded = (RecordedKinectFrames)ds.ReadObject(fs) ?? new RecordedKinectFrames();
            }
            return Recorded.Frames.Count > 0;
        }

        public void StartPlaying()
        {
            if (IsPlaying) return;
            if (IsRecording) throw new Exception("Currently recording.  Cannot play.");
            if (IsSensing) throw new Exception("Currently sensing.  Cannot play.");

            if (Recorded.Frames.Count <= 1) return;
            IsPlaying = true;
            _playbackThread = new Thread(PlaybackThread) { Name="KinectRecorder.PlaybackThread"};
            _playbackThread.Start();
            
        }

        public void StopPlaying()
        {
            IsPlaying = false;
        }

        protected void PlaybackThread()
        {
            if (Recorded.Frames.Count > 1)
            {
                var index = 0;
                while (IsPlaying)
                {
                    var curFrame = Recorded.Frames[index];
                    var nextFrame = Recorded.Frames[index + 1];

                    RaiseFrameIncomming(curFrame);
                    
                    var frameDiff = (int)Math.Max(nextFrame.TimeStamp - curFrame.TimeStamp, 0);
                    Thread.Sleep(frameDiff);
                    index++;
                    if (index >= Recorded.Frames.Count - 1) index = 0;
                }
            }

            IsPlaying = false;
        }

        public void StartSensing()
        {
            if (IsSensing) return;
            if (IsPlaying) throw new Exception("Currently playing.  Cannot sense.");
            if (KinectSensor.KinectSensors.Count == 0) return;

            IsSensing = true;
            ActiveSensors = KinectSensor.KinectSensors.ToList();

            foreach (var sensor in ActiveSensors)
            {
                sensor.Start();
                sensor.SkeletonFrameReady += SensorFrameReady;
                sensor.SkeletonStream.Enable(Smoother);
            }
        }

        void SensorFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            if (IsPlaying) return;
            
            var sensorIndex = ActiveSensors.IndexOf((KinectSensor) sender);
            Skeleframe frame;
            using (var skeleFrame = e.OpenSkeletonFrame())
            {
                if (skeleFrame == null) return;
                
                frame = new Skeleframe
                                {
                                    FloorClipPlane = skeleFrame.FloorClipPlane,
                                    SkeletonFrameNumber = skeleFrame.FrameNumber,
                                    TimeStamp = skeleFrame.Timestamp,
                                    SensorIndex = sensorIndex,
                                    Skeletons = new Skeleton[skeleFrame.SkeletonArrayLength]
                                };
                skeleFrame.CopySkeletonDataTo(frame.Skeletons);

                if(IsRecording) Recorded.Frames.Add(frame);
            }

            RaiseFrameIncomming(frame);
        }

        public void StopSensing()
        {
            if (ActiveSensors != null && ActiveSensors.Count > 0)
            {
                foreach (var sensor in ActiveSensors)
                {
                    sensor.Stop();
                    sensor.Dispose();
                }

                ActiveSensors.Clear();
            }
            IsSensing = false;
        }
        
        public void Dispose()
        {
            IsPlaying = false;
            IsRecording =false;
            StopSensing();
            StopPlaying();
            Recorded.Frames.Clear();
        }
    }

    [DataContract]
    public class RecordedKinectFrames
    {
        [DataMember]
        public List<Skeleframe> Frames = new List<Skeleframe>();
    }

}

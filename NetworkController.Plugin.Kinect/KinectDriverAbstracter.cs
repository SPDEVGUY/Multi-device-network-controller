using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Kinect;
using NetworkController.Logic.Plugin.Attributes;
using NetworkController.Plugin.Mouse;

namespace NetworkController.Plugin.Kinect
{
    //Note: Kinect for windows SDK Source:
    //http://www.microsoft.com/en-us/kinectforwindows/develop/overview.aspx
    [DriverAbstracter]
    public class KinectDriverAbstracter : DriverAbstracterBase
    {
        private bool _isDisposing = false;
        private KinectRecorder _recorder = new KinectRecorder();
        private MultiKinectProcessor _processor = new MultiKinectProcessor();
        private bool _isInitialized = false;

        public KinectDriverAbstracter()
            : base("Kinect")
        { }

        protected override void CaptureCurrentState()
        {
            if (!_isInitialized && !_isDisposing)
            {
                InitializeDriver();
            }
            else
            {
                //Lazy ass code:
                var sk = _processor.Player1;
                if (sk.TrackingState != SkeletonTrackingState.NotTracked)
                {
                    var p = sk.Position;
                    AddPointToSliders("Player1", "Overall", p);
                }
            }
        }

        protected void AddPointToSliders(string player, string joint, SkeletonPoint p)
        {
            var jointPrefix = "Skeleton." + player + "." + joint + ".";
            AddSliderValue(
                jointPrefix  + "X",
                (int) p.X*100
                );
            AddSliderValue(
                jointPrefix + "Y",
                (int) (p.Y*100)
                );
            AddSliderValue(
                jointPrefix + "Z",
                (int) (p.Z*100)
                );
        }


        protected override void InitializeDriver()
        {
            _processor.Reset();
            _recorder.StartSensing();
            _recorder.FrameIncomming += _processor.ApplyFrame;
            _isInitialized = true;
            KinectSkeleViewer.CurrentRecorder = _recorder;
            KinectSkeleViewer.CurrentProcessor = _processor;
            KinectSkeleViewer.OpenWindow();
            
        }

        
        

        protected override void DisposeDriver()
        {
            _isDisposing = true;
            _recorder.StopSensing();
            _recorder.Dispose();
            if (KinectSkeleViewer.CurrentForm != null)
            {
                KinectSkeleViewer.CurrentRecorder = null;
                KinectSkeleViewer.CurrentForm.IsRunning = false;
            }
        }
    }
}

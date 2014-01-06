using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Kinect;

namespace NetworkController.Plugin.Kinect
{
    public class KinectProcessor : IDisposable
    {

        private bool _isRunning;
        private Thread processingThread;
        private Dictionary<int, Dictionary<int, Body>> _sortedBodies = new Dictionary<int, Dictionary<int, Body>>();
        private List<Body> _bodies = new List<Body>();

        public bool IsRunning { get { return _isRunning; } }

        public void Start()
        {
            if (_isRunning) return;

            processingThread = new Thread(KinectProcessingThread) { Name = "Kinect Processor Thread"};
            processingThread.Start();
        }

        public void Stop()
        {
            if (!_isRunning) return;
            _isRunning = false;
            Thread.Sleep(10);
        }

        public void Dispose()
        {
            if (_isRunning) Stop();
        }

        public List<Body> GetBodies(bool cleanUp = true)
        {
            if(cleanUp) CleanUpBodies();

            List<Body> result;
            lock(_bodies) result = new List<Body>(_bodies.ToArray());
            return result;
        }

        private void CleanUpBodies()
        {
            lock (_bodies)
            {
                var ix = 0;
                while (ix < _bodies.Count)
                {
                    var isRemoved = false;
                    var b = _bodies[ix];
                    
                    if (!b.IsActive)
                    {
                        if (b.Position.IsTracked) b.Position.IsTracked = false;
                        else
                        {
                            isRemoved = true;
                            _bodies.RemoveAt(ix);
                            _sortedBodies[b.SensorIndex].Remove(b.TrackingId);
                        }
                    } 

                    if (!isRemoved)
                    {
                        b.IsNew = false;
                        ix++;
                    }
                }
            }
        }
        


        private void KinectProcessingThread()
        {
            _isRunning = true;
            using (var kinect = new KinectInterop())
            {
                kinect.Start();
                
                while (_isRunning)
                {
                    if (kinect.HasFrames)
                    {
                        var frames = kinect.PopQueue();
                        foreach (var frame in frames)
                        {
                            //Find associated body and apply frame to it.
                            lock (_bodies)
                            {
                                //Mark existing bodies for sensor as untracked
                                for (var ix = 0; ix < _bodies.Count; ix++) if (_bodies[ix].SensorIndex == frame.SensorIndex) _bodies[ix].IsActive = false;

                                //detected bodies will be marked as active
                                var bodies = GetBodiesForFrame(frame);
                                foreach (var body in bodies)
                                {
                                    if (!_bodies.Contains(body)) _bodies.Add(body);

                                    body.IsActive = true;

                                }
                            }
                        }
                    }
                    Thread.Sleep(1);
                }

                kinect.Stop();
                kinect.Dispose();
            }
        }

        private List<Body> GetBodiesForFrame(Skeleframe frame)
        {
            List<Body> result = new List<Body>();

            if(!_sortedBodies.ContainsKey(frame.SensorIndex))
                _sortedBodies[frame.SensorIndex] =  new Dictionary<int, Body>();
            
            var sx = _sortedBodies[frame.SensorIndex];

            for (var ix = 0; ix<frame.Skeletons.Length;ix++)
            {
                if (frame.Skeletons[ix].TrackingId != 0)
                {
                    var body = GetBodyForSkeleton(sx, frame.Skeletons[ix]);
                    body.SensorIndex = frame.SensorIndex;
                    result.Add(body);
                }
                    
            }

            return result;
        }

        private Body GetBodyForSkeleton(Dictionary<int, Body> sensorSkeles, Skeleton skele)
        {
            var id = skele.TrackingId;

            Body body;

            if (!sensorSkeles.ContainsKey(id))
            {
                body = new Body(skele);
                sensorSkeles[id] = body;
            }
            else
            {
                body = sensorSkeles[id];
                body.ApplyFrame(skele);
            }

            return body;
        }

    }
}

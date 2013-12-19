using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace NetworkController.Plugin.Kinect
{
    public partial class KinectSkeleViewer : Form
    {
        public bool IsRunning = true;
        public static KinectSkeleViewer CurrentForm;
        public static KinectRecorder CurrentRecorder;
        public static MultiKinectProcessor CurrentProcessor;
        public Bitmap FrontView;
        public Bitmap SideView;
        public Bitmap TopView;
        

        public KinectSkeleViewer()
        {
            InitializeComponent();
            FrontView = new Bitmap(frontViewPB.Width, frontViewPB.Height);
            SideView = new Bitmap(sideViewPB.Width, sideViewPB.Height);
            TopView = new Bitmap(topVewPB.Width, topVewPB.Height);
            UpdateState();

            CurrentRecorder.FrameIncomming += delegate { RenderState(); };

            frontViewPB.Paint += (sender, args) =>
                                     {
                                         lock (FrontView)
                                             args.Graphics.DrawImageUnscaled(FrontView, 0, 0);
                                     };
            topVewPB.Paint += (sender, args) =>
                                     {
                                         lock (TopView)
                                             args.Graphics.DrawImageUnscaled(TopView, 0, 0);
                                     };
            sideViewPB.Paint += (sender, args) =>
                                     {
                                         lock (SideView)
                                             args.Graphics.DrawImageUnscaled(SideView, 0, 0);
                                     };
        }

        public void RenderState()
        {
            var backPen = new Pen(Color.White);
            lock (FrontView)
            {
                using (var g = Graphics.FromImage(FrontView))
                {
                    var rect = new Rectangle(new Point(), FrontView.Size);
                    g.DrawRectangle(backPen,rect);
                    DrawFrontView(g,CurrentProcessor.Player1,Color.Red,Color.Red,Color.DarkRed);
                }
            }
        }

        public void DrawFrontView(Graphics g, Skeleton sk, Color pointColor, Color lineColour, Color centerColor)
        {
            DrawPoint(g,sk.Position.X,sk.Position.Y,centerColor);
        }

        public void DrawPoint(Graphics g, float x, float y, Color color)
        {
            var positionPen = new Pen(color);
            g.DrawRectangle(positionPen, x-2, y-2,4,4);
        }

        public static Thread ViewerThread = new Thread(ViewerThreadMethod) { Name = "KinectSkeleViewer.ViewerThread" };
        
        protected static void ViewerThreadMethod()
        {
            if (CurrentForm != null) return;
            using (CurrentForm = new KinectSkeleViewer())
            {
                CurrentForm.Show();
                while (CurrentForm.IsRunning)
                {
                    Application.DoEvents();
                    Thread.Sleep(1);
                }
            }
            CurrentForm = null;
        }

        public static void OpenWindow()
        {
            ViewerThread.Start();
        }

        private void KinectSkeleViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsRunning = false;
        }

        private void UpdateState()
        {

            startRecordingB.Enabled = !CurrentRecorder.IsRecording && !CurrentRecorder.IsPlaying &&  CurrentRecorder.IsSensing;
            stopRecordingB.Enabled = CurrentRecorder.IsRecording;
            startSensingB.Enabled = !CurrentRecorder.IsSensing && !CurrentRecorder.IsPlaying;
            stopSensingB.Enabled = CurrentRecorder.IsSensing;
            startPlayingB.Enabled = !CurrentRecorder.IsPlaying && !CurrentRecorder.IsSensing && !CurrentRecorder.IsRecording;
            stopPlayingB.Enabled = CurrentRecorder.IsPlaying;
        }

        private void startRecordingB_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentRecorder.StartRecording();
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stopRecordingB_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentRecorder.StopRecording("Skeledata.txt");
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void startSensingB_Click(object sender, EventArgs e)
        {

            try
            {
                CurrentRecorder.StartSensing();
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stopSensingB_Click(object sender, EventArgs e)
        {

            try
            {
                CurrentRecorder.StopSensing();
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void startPlayingB_Click(object sender, EventArgs e)
        {

            try
            {
                CurrentRecorder.StartPlaying();
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stopPlayingB_Click(object sender, EventArgs e)
        {

            try
            {
                CurrentRecorder.StopPlaying();
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadRecordingB_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentRecorder.LoadRecorded("Skeledata.txt");
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
    
    
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TETCSharpClient.Data;

namespace NetworkController.Plugin.EyeTribe
{
    public partial class Gui : Form
    {
        public EyeTribeDriverAbstracter Abstracter;

        private Bitmap eyesImage = new Bitmap(200,200);
        private Bitmap gazeImage = new Bitmap(200,200);

        public Gui(EyeTribeDriverAbstracter abstracter)
        {
            Abstracter = abstracter;
            InitializeComponent();

            pbEyes.Paint += (x, y) =>
                                {
                                    lock(eyesImage)
                                        y.Graphics.DrawImageUnscaled(eyesImage,0,0);
                                };
            pbGaze.Paint += (x, y) =>
                                {
                                    lock (gazeImage)
                                        y.Graphics.DrawImageUnscaled(gazeImage, 0, 0);
                                };

            
            
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            lblConnected.Text = Abstracter.Processor.IsConnected ? "Connected" : "Not Connected";

            lblCalibrate.Text = Abstracter.Processor.IsCalibrated
                ? Abstracter.Processor.CalibrationRating.ToString()
                : "Click Here To Calibrate";

            var i = Abstracter.Processor.GazeInfo;
            if (i == null || !i.IsUsable) return;


            chkPresence.Checked = i.Presence;
            chkGaze.Checked = i.Gaze;
            chkFixated.Checked = i.IsFixated;
            chkFailed.Checked = i.Failed;
            chkLostTracking.Checked = i.LostTracking;
            chkLeftEye.Checked = i.LeftEye != null;
            chkRightEye.Checked = i.RightEye != null;

            if (i.Head != null)
            {
                lblHeadX.Text = i.Head.X.Round(2).ToString();
                lblHeadY.Text = i.Head.Y.Round(2).ToString();
                lblHeadZ.Text = i.Head.Z.Round(2).ToString();
            }

            if (i.RawCoordinates != null)
            {
                lblRawX.Text = i.RawCoordinates.X.ToString();
                lblRawY.Text = i.RawCoordinates.Y.ToString();
            }
            if (i.SmoothedCoordinates != null)
            {
                lblSmoothX.Text = i.SmoothedCoordinates.X.ToString();
                lblSmoothY.Text = i.SmoothedCoordinates.Y.ToString();
            }

            if (i.LeftEye != null)
            {
                lblLeftPupilSize.Text = i.LeftEye.PupilSize.ToString();
                lblLeftRawX.Text = i.LeftEye.RawCoordinates.X.Round(2).ToString();
                lblLeftRawY.Text = i.LeftEye.RawCoordinates.Y.Round(2).ToString();
                lblLeftSmoothX.Text = i.LeftEye.SmoothedCoordinates.X.Round(2).ToString();
                lblLeftSmoothY.Text = i.LeftEye.SmoothedCoordinates.Y.Round(2).ToString();
                lblLeftPupilX.Text = i.LeftEye.PupilCenterCoordinates.X.Round(2).ToString();
                lblLeftPupilY.Text = i.LeftEye.PupilCenterCoordinates.Y.Round(2).ToString();
            }
            if (i.RightEye != null)
            {
                lblRightPupilSize.Text = i.RightEye.PupilSize.ToString();
                lblRightRawX.Text = i.RightEye.RawCoordinates.X.Round(2).ToString();
                lblRightRawY.Text = i.RightEye.RawCoordinates.Y.Round(2).ToString();
                lblRightSmoothX.Text = i.RightEye.SmoothedCoordinates.X.Round(2).ToString();
                lblRightSmoothY.Text = i.RightEye.SmoothedCoordinates.Y.Round(2).ToString();
                lblRightPupilX.Text = i.RightEye.PupilCenterCoordinates.X.Round(2).ToString();
                lblRightPupilY.Text = i.RightEye.PupilCenterCoordinates.Y.Round(2).ToString();
            }

            DrawEyes();
            DrawGaze();

            pbEyes.Invalidate();
            pbGaze.Invalidate();
        }

        private void DrawGaze()
        {
            lock (gazeImage)
            {
                using (var g = Graphics.FromImage(gazeImage))
                {
                    
                    var i = Abstracter.Processor.GazeInfo;
                    var s = Abstracter.Processor.CalibrationAreaSize;

                    g.Clear(Color.Black);

                    var size = 5.0d;
                    var rawPen = new Pen(Color.Gray);
                    var smoothPen = new Pen(Color.Red);
                    
                    DrawCoordinates(g, i.Last.RawCoordinates, i.RawCoordinates, rawPen, s, size, 200.0d);
                    DrawCoordinates(g, i.Last.SmoothedCoordinates, i.SmoothedCoordinates, smoothPen, s, size, 200.0d);
                }
            }
        }

        private void DrawEyes()
        {
            lock (eyesImage)
            {
                using (var g = Graphics.FromImage(eyesImage))
                {

                    var i = Abstracter.Processor.GazeInfo;
                    var s = Abstracter.Processor.CalibrationAreaSize;

                    g.Clear(Color.Black);

                    if(i.Last != null) DrawEye(g, i.LeftEye, i.Last.LeftEye, s);
                    if(i.Last != null) DrawEye(g, i.RightEye, i.Last.RightEye, s);

                }
            }
        }

        private void DrawEye(Graphics g, GazeEye eye, GazeEye lastEye, Point2D calibrationAreaSize)
        {
            if (eye == null) return;

            var rawPen = new Pen(Color.Gray);
            var smoothPen = new Pen(Color.Red);
            var pupilPen = new Pen(Color.Green);

            Point2D lastRawCoordinates = null;
            Point2D lastSmoothedCoordinates = null;
            Point2D lastPupilCoordinate = null;

            if (lastEye != null) lastRawCoordinates = lastEye.RawCoordinates;
            if (lastEye != null) lastSmoothedCoordinates = lastEye.SmoothedCoordinates;
            if (lastEye != null) lastPupilCoordinate = lastEye.PupilCenterCoordinates;

            DrawCoordinates(g, lastRawCoordinates, eye.RawCoordinates, rawPen, calibrationAreaSize, eye.PupilSize,
                            200.0d);
            DrawCoordinates(g, lastSmoothedCoordinates, eye.SmoothedCoordinates, smoothPen, calibrationAreaSize,
                            eye.PupilSize, 200.0d);

            DrawCoordinates(g, lastPupilCoordinate, eye.PupilCenterCoordinates, pupilPen, eye.PupilSize, 200.0d);

        }

        private void DrawCoordinates(Graphics g, Point2D lastPoint, Point2D point, Pen pen, Point2D calibrationAreaSize, double scale, double boxSize)
        {
            if (point == null) return;

            var x = point.X / calibrationAreaSize.X * boxSize;
            var y = point.Y / calibrationAreaSize.Y * boxSize;
            if (lastPoint != null)
            {
                var _x = lastPoint.X / calibrationAreaSize.X * boxSize;
                var _y = lastPoint.Y / calibrationAreaSize.Y * boxSize;

                g.DrawLine(pen, (float)x, (float)y, (float)_x, (float)_y);
            }
            g.DrawEllipse(pen, (float)(x - scale / 2), (float)(y - scale / 2), (float)scale, (float)scale);
        }

        private void DrawCoordinates(Graphics g, Point2D lastPoint, Point2D point, Pen pen, double scale, double boxSize)
        {
            if (point == null) return;

            var x = point.X * boxSize;
            var y = point.Y  * boxSize;
            if (lastPoint != null)
            {
                var _x = lastPoint.X * boxSize;
                var _y = lastPoint.Y * boxSize;

                g.DrawLine(pen, (float)x, (float)y, (float)_x, (float)_y);
            }
            g.DrawEllipse(pen, (float)(x - scale / 2), (float)(y - scale / 2), (float)scale, (float)scale);
        }

        private void lblCalibrate_Click(object sender, EventArgs e)
        {
            Abstracter.Processor.RunCalibration(this);
        }

        private void chkShowGaze_CheckedChanged(object sender, EventArgs e)
        {
            Abstracter.Processor.GazeVisible = chkShowGaze.Checked;
        }

        private void Gui_Load(object sender, EventArgs e)
        {
            refreshTimer.Enabled = true;
        }

        private void Gui_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        


        private void btnSweetSpot_Click(object sender, EventArgs e)
        {
            Correction.Instance.SetSweetSpot();
            
        }

        private void btnOffset_Click(object sender, EventArgs e)
        {
            Correction.Instance.SetDeltas();
        }

        
    }

    public static class DoubleExt
    {
        public static Double Round(this double value, int places)
        {
            return Math.Round(value, places);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                lblLeftRawX.Text = i.LeftEye.RawCoordinates.X.ToString();
                lblLeftRawY.Text = i.LeftEye.RawCoordinates.Y.ToString();
                lblLeftSmoothX.Text = i.LeftEye.SmoothedCoordinates.X.ToString();
                lblLeftSmoothY.Text = i.LeftEye.SmoothedCoordinates.Y.ToString();
                lblLeftPupilX.Text = i.LeftEye.PupilCenterCoordinates.X.ToString();
                lblLeftPupilY.Text = i.LeftEye.PupilCenterCoordinates.Y.ToString();
            }
            if (i.RightEye != null)
            {
                lblRightPupilSize.Text = i.RightEye.PupilSize.ToString();
                lblRightRawX.Text = i.RightEye.RawCoordinates.X.ToString();
                lblRightRawY.Text = i.RightEye.RawCoordinates.Y.ToString();
                lblRightSmoothX.Text = i.RightEye.SmoothedCoordinates.X.ToString();
                lblRightSmoothY.Text = i.RightEye.SmoothedCoordinates.Y.ToString();
                lblRightPupilX.Text = i.RightEye.PupilCenterCoordinates.X.ToString();
                lblRightPupilY.Text = i.RightEye.PupilCenterCoordinates.Y.ToString();
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
                    if (i.RawCoordinates != null)
                    {
                        var x = i.RawCoordinates.X / s.X *200.0d;
                        var y = i.RawCoordinates.Y / s.Y * 200.0d;
                        if (i.LastRawCoordinates != null)
                        {
                            var _x = i.LastRawCoordinates.X / s.X * 200.0d;
                            var _y = i.LastRawCoordinates.Y / s.Y * 200.0d;

                            g.DrawLine(rawPen, (float) x, (float) y, (float) _x, (float) _y);
                        }
                        g.DrawEllipse(rawPen, (float) (x - size/2), (float) (y - size/2), (float) size, (float) size);
                    }

                    if (i.SmoothedCoordinates != null)
                    {
                        var x = i.SmoothedCoordinates.X / s.X * 200.0d;
                        var y = i.SmoothedCoordinates.Y / s.Y * 200.0d;
                        if (i.LastSmoothedCoordinates != null)
                        {
                            var _x = i.LastSmoothedCoordinates.X / s.X * 200.0d;
                            var _y = i.LastSmoothedCoordinates.Y / s.Y * 200.0d;
                            g.DrawLine(smoothPen, (float) x, (float) y, (float) _x, (float) _y);
                        }
                        g.DrawEllipse(smoothPen, (float) (x - size/2), (float) (y - size/2), (float) size, (float) size);
                    }
                }
            }
        }

        private void DrawEyes()
        {
            
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
    }
}

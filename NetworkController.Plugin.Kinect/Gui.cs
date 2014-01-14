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
using System.Runtime.Serialization;
using System.IO;

namespace NetworkController.Plugin.Kinect
{
    public partial class Gui : Form
    {
        public KinectDriverAbstracter Abstracter;
        public Bitmap FrontView;
        public Bitmap SideView;
        public Bitmap TopView;

        private JointType[] _jointTypes;
        

        public Gui(KinectDriverAbstracter abstracter)
        {
            Abstracter = abstracter;

            InitializeComponent();
            FrontView = new Bitmap(frontViewPB.Width, frontViewPB.Height);
            SideView = new Bitmap(sideViewPB.Width, sideViewPB.Height);
            TopView = new Bitmap(topVewPB.Width, topVewPB.Height);

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

            _jointTypes = Enum.GetValues(typeof(JointType)) as JointType[];


            var config = KinectConfig.Load();
            if(config != null) config.Apply(Abstracter);


            foreach (var value in _jointTypes) sendPoints.Items.Add(value, Abstracter.SendJoint[value]);
        }

        public void RenderState()
        {
            
            lock (FrontView)
            {
                using (var g = Graphics.FromImage(FrontView))
                {
                    g.Clear(Color.White);


                    var bodies = Abstracter.Processor.GetBodies(false);
                    if (bodies.Count == 0) return;

                    var player0 = bodies[0];

                    DrawPoint(g, player0.Position, Color.Red);
                    foreach (var p in player0.Points)
                    {
                        DrawPointWithZ(g, p.Value, Color.Green, FrontView.Width,FrontView.Height);
                    }
                }
            }
        }


        private void DrawPoint(Graphics g, Body.DeltaPoint deltaPoint, Color color)
        {
            var size = 2;
            var positionPen = new Pen(color);
            g.DrawRectangle(positionPen, deltaPoint.Position.X - size / 2, deltaPoint.Position.Y - size / 2, size, size);
        }

        private void DrawPointWithZ(Graphics g, Body.DeltaPoint deltaPoint, Color color, int width, int height)
        {
            var size = deltaPoint.LocalizedPosition.Z *50 +50;
            var positionPen = new Pen(color);
            g.DrawRectangle(positionPen, (int)(deltaPoint.Position.X * width * 0.75 + width / 2 - size / 2), height - (int)(deltaPoint.Position.Y * height * 0.75 + height / 2 - size / 2), size, size);
        }

        


        private void viewRefreshTimer_Tick(object sender, EventArgs e)
        {
            RenderState();
            frontViewPB.Invalidate();
            topVewPB.Invalidate();
            sideViewPB.Invalidate();
        }

        private void chkSendLocalizedPositions_CheckedChanged(object sender, EventArgs e)
        {
            Abstracter.SendLocalizedPositions = chkSendLocalizedPositions.Checked;
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Abstracter.SendPoints = chkEnabled.Checked;
        }

        private void Gui_Activated(object sender, EventArgs e)
        {
            chkEnabled.Checked = Abstracter.Processor.IsRunning;
            chkSendLocalizedPositions.Checked = Abstracter.SendLocalizedPositions;
        }

        private void sendPoints_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var joint = _jointTypes[e.Index];
            Abstracter.SendJoint[joint] = e.NewValue == CheckState.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var config = new KinectConfig(Abstracter);
            config.Save();
        }
    
    
        [DataContract]
        public class KinectConfig
        {
            [DataMember] public bool SendPoints;
            [DataMember] public bool SendLocalized;
            [DataMember] public List<JointType> SendingJoints;

            public KinectConfig(KinectDriverAbstracter abstracter)
            {
                SendPoints = abstracter.SendPoints;
                SendLocalized = abstracter.SendLocalizedPositions;
                SendingJoints= new List<JointType>();
                foreach (var joint in abstracter.SendJoint)
                {
                    if(joint.Value) SendingJoints.Add(joint.Key);
                }
            }

            public void Save()
            {
                var file = "KinectConfig.txt";
                if (File.Exists(file)) File.Delete(file);
                using (var fs = File.OpenWrite(file))
                {
                    var sr = new DataContractSerializer(typeof (KinectConfig));
                    sr.WriteObject(fs,this);
                    fs.Close();
                }
            }

            public static KinectConfig Load()
            {
                var file = "KinectConfig.txt";
                if (!File.Exists(file)) return null;

                
                KinectConfig result;
                using (var fs = File.OpenRead(file))
                {
                    try
                    {
                        var sr = new DataContractSerializer(typeof (KinectConfig));
                        result = sr.ReadObject(fs) as KinectConfig;
                    }
                    catch
                    {
                        result = null;
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
                return result;
            }

            public void Apply(KinectDriverAbstracter abstracter)
            {
                abstracter.SendPoints = SendPoints;
                abstracter.SendLocalizedPositions = SendLocalized;
                if(SendingJoints != null)
                foreach (var joint in SendingJoints)
                    abstracter.SendJoint[joint] = true;
            }
        }
    
    }
}

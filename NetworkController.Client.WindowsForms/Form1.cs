using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkController.Client.Logic.Controllers;
using NetworkController.Client.Logic.DataTypes.Interfaces;

namespace NetworkController.Client.WindowsForms
{
    public partial class Form1 : Form
    {
        public UdpReceiver Receiver;
        public Bitmap GraphBuffer;

        private Dictionary<string, ListViewItem> Gestures = new Dictionary<string, ListViewItem>(); 
        private Dictionary<string, ListViewItem> Buttons= new Dictionary<string, ListViewItem>(); 
        private Dictionary<string, ListViewItem> Sliders= new Dictionary<string, ListViewItem>(); 


        public Form1()
        {
            InitializeComponent();
            Receiver = new UdpReceiver();
            Receiver.StartListening();
            inputList.Items.Clear();
            GraphBuffer = new Bitmap(graphPicture.Width, graphPicture.Height);
            graphPicture.Image = GraphBuffer;

            graphPicture.Paint += delegate(object sender, PaintEventArgs args)
                                      {
                                          args.Graphics.DrawImageUnscaled(GraphBuffer,0,0);
                                      };
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            txtPackets.Text = Receiver.PacketsReceived.ToString();

            UpdateButtonStates();
            UpdateSliderStates();
            UpdateGestureStates();

            UpdatePicture();
            graphPicture.Invalidate();
        }

        private void UpdatePicture()
        {
            using (var graphics = Graphics.FromImage(GraphBuffer))
            {
                graphics.Clear(Color.Black);

                var xLocation = 0.5;
                var yLocation = 0.5;
                var zLocation = 0.5;
                var xFrom = xSelect.Text;
                var yFrom = ySelect.Text;
                var zFrom = zSelect.Text;

                if (Sliders.ContainsKey(xFrom))
                {
                    var li = Sliders[xFrom];
                    var slider = li.Tag as IDeltaSliderState;
                    xLocation = ((double)(slider.Value - slider.MinValue)/Math.Max(slider.MaxValue - slider.MinValue, 1));
                }
                if (Sliders.ContainsKey(yFrom))
                {
                    var li = Sliders[yFrom];
                    var slider = li.Tag as IDeltaSliderState;
                    yLocation = ((double)(slider.Value - slider.MinValue) / Math.Max(slider.MaxValue - slider.MinValue, 1));
                }
                if (Sliders.ContainsKey(zFrom))
                {
                    var li = Sliders[zFrom];
                    var slider = li.Tag as IDeltaSliderState;
                    zLocation = ((double)(slider.Value - slider.MinValue) / Math.Max(slider.MaxValue - slider.MinValue, 1));
                }
                var pen = new Pen(Color.Red);
                graphics.DrawRectangle(pen, (int)(xLocation * graphPicture.Width), (int)(yLocation * graphPicture.Height), (int)(zLocation * 40.0), (int)(zLocation * 40.0));
            }
        }

        private void UpdateGestureStates()
        {
            var ix = 0;
            var gestures = Receiver.GetAllGestureStates();
            foreach (var g in gestures)
            {
                var name = g.ProviderName + "." + g.InputName;
                if (!Gestures.ContainsKey(name))
                {
                    Gestures[name] = new ListViewItem(name, 0, inputList.Groups["Gestures"]);
                    inputList.Items.Add(Gestures[name]);
                }

                var li = Gestures[name];

                li.ImageIndex = GetGestureImageIndex(g);
                
                ix++;
            }
        }


        private void UpdateSliderStates()
        {
            var sliders = Receiver.GetAllSliderStates();
            foreach (var s in sliders)
            {
                var name = s.ProviderName + "." + s.InputName;
                if (!Sliders.ContainsKey(name))
                {
                    Sliders[name] = new ListViewItem(name, 0, inputList.Groups["Sliders"]);
                    inputList.Items.Add(Sliders[name]);

                    xSelect.Items.Add(name);
                    ySelect.Items.Add(name);
                    zSelect.Items.Add(name);
                }

                var li = Sliders[name];

                li.Tag = s;
                li.ImageIndex = GetSliderImageIndex(s);
            }
        }
        
        private void UpdateButtonStates()
        {
            var buttons = Receiver.GetAllButtonStates();
            foreach (var b in buttons)
            {
                var name = b.ProviderName + "." + b.InputName;
                if (!Buttons.ContainsKey(name))
                {
                    Buttons[name] = new ListViewItem(name, 0, inputList.Groups["Buttons"]);
                    inputList.Items.Add(Buttons[name]);
                }

                var li = Buttons[name];


                li.ImageIndex = GetButtonImageIndex(b);
            }
        }

        private int GetGestureImageIndex(IDeltaGestureState g)
        {
            if (!g.DetectedThisTick && !g.DetectedLastTick) return 4;
            if (g.DetectedThisTick) return 2;
            return 3;
        }
        private int GetSliderImageIndex(IDeltaSliderState s)
        {
            if (s.Velocity >= 1) return 0;
            if (s.Velocity <= -1) return 1;
            return 4;
        }
        private int GetButtonImageIndex(IDeltaButtonState b)
        {
            if (b.Velocity > 2) return 0;
            if (b.IsPressed) return 2;
            return 3;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Receiver.StopListening();
        }




    }
}

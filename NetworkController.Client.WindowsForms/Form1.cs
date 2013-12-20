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

        public Form1()
        {
            InitializeComponent();
            Receiver = new UdpReceiver();
            Receiver.StartListening();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            txtPackets.Text = Receiver.PacketsReceived.ToString();

            UpdateButtonStates();
            UpdateSliderStates();
            UpdateGestureStates();
        }

        private void UpdateGestureStates()
        {
            var ix = 0;
            var gestures = Receiver.GetAllGestureStates();
            foreach (var g in gestures)
            {
                ListViewItem li;
                if (lstGestures.Items.Count > ix)
                {
                    li = lstGestures.Items[ix];
                    li.BackColor = GetGestureStateColour(g);
                    li.Text = GetGestureLabel(g);
                }
                else
                {
                    li = new ListViewItem(GetGestureLabel(g));
                    li.BackColor = GetGestureStateColour(g);
                    lstGestures.Items.Add(li);
                }
                ix++;
            }
        }
        
        private void UpdateSliderStates()
        {
            var ix = 0;
            var sliders = Receiver.GetAllSliderStates();
            foreach (var s in sliders)
            {
                ListViewItem li;
                if (lstSliders.Items.Count > ix)
                {
                    li = lstSliders.Items[ix];
                    li.BackColor = GetSliderStateColour(s);
                    li.Text = GetSliderLabel(s);
                }
                else
                {
                    li = new ListViewItem(GetSliderLabel(s));
                    li.BackColor = GetSliderStateColour(s);
                    lstSliders.Items.Add(li);
                }
                ix++;
            }
        }

        private void UpdateButtonStates()
        {
            var ix = 0;
            var buttons = Receiver.GetAllButtonStates();
            foreach (var b in buttons)
            {
                ListViewItem li;
                if (lstButtonStates.Items.Count > ix)
                {
                    li = lstButtonStates.Items[ix];
                    li.BackColor = GetButtonStateColour(b);
                    li.Text = GetButtonLabel(b);
                }
                else
                {
                    li = new ListViewItem(GetButtonLabel(b));
                    li.BackColor = GetButtonStateColour(b);
                    lstButtonStates.Items.Add(li);
                }
                ix++;
            }
        }

        private string GetButtonLabel(IDeltaButtonState b)
        {
            return string.Format("[{0:00}] {3}{1}.{2}", b.Velocity, b.ProviderName, b.InputName, b.IsPressed ? "(*) " : "");
        }
        private string GetSliderLabel(IDeltaSliderState s)
        {
            var positionPercent = (s.Value / Math.Max(s.MaxValue - s.MinValue, 1.0) * 100.0);
            return string.Format("[{0:#0.0}%] {1}.{2}", positionPercent, s.ProviderName, s.InputName);
        }
        private string GetGestureLabel(IDeltaGestureState s)
        {
            return string.Format("[{0:#000}] {3}{1}.{2}", s.Intensity, s.ProviderName, s.InputName, s.DetectedThisTick ? "(*) " : "");
        }

        private Color GetButtonStateColour(IDeltaButtonState state)
        {
            var velocity = (int)state.Velocity; //Velocity = speed at which someone is mashing a button
            if (velocity <= -95) return Color.Black;
            if (velocity <= -75) return Color.Purple;
            if (velocity <= -50) return Color.BlueViolet;
            if (velocity <= -25) return Color.DarkBlue;
            if (velocity <= -10) return Color.Blue;
            if (velocity <= -5) return Color.DodgerBlue;
            if (velocity <= -1) return Color.CornflowerBlue;
            if (velocity == 0) return Color.White;
            if (velocity <= 1) return Color.LimeGreen;
            if (velocity <= 5) return Color.GreenYellow;
            if (velocity <= 10) return Color.Gold;
            if (velocity <= 25) return Color.Orange;
            if (velocity <= 75) return Color.DarkOrange;
            if (velocity <= 95) return Color.DarkRed;
            if (velocity >= 100) return Color.Red;

            return Color.SaddleBrown;
        }

        private Color GetSliderStateColour(IDeltaSliderState state)
        {
            var accelPercent = (int)(state.Acceleration/Math.Max(state.MaxValue - state.MinValue, 1.0)*100.0);
            if (accelPercent <= -95) return Color.Black;
            if (accelPercent <= -75) return Color.Purple;
            if (accelPercent <= -50) return Color.BlueViolet;
            if (accelPercent <= -25) return Color.DarkBlue;
            if (accelPercent <= -10) return Color.Blue;
            if (accelPercent <= -5) return Color.DodgerBlue;
            if (accelPercent <= -1) return Color.CornflowerBlue;
            if (accelPercent == 0) return Color.White;
            if (accelPercent <= 1) return Color.LimeGreen;
            if (accelPercent <= 5) return Color.GreenYellow;
            if (accelPercent <= 10) return Color.Gold;
            if (accelPercent <= 25) return Color.Orange;
            if (accelPercent <= 75) return Color.DarkOrange;
            if (accelPercent <= 95) return Color.DarkRed;
            if (accelPercent >= 100) return Color.Red;

            return Color.SaddleBrown;
        }
        private Color GetGestureStateColour(IDeltaGestureState state)
        {
            if (state.DetectedThisTick) return Color.LightSalmon;
            if (state.DetectedLastTick) return Color.LightGreen;
            return Color.White;
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkController.Server.Logic;

namespace NetworkController.Server.FormsApp
{
    public partial class MainForm : Form
    {
        private Broadcaster broadcaster;
        private long lastPacketCont;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            broadcaster = new Broadcaster();
            broadcaster.BroadcastPort = 9050;
            broadcaster.Start();

            //TODO: Dummy code, move into config or somethin
            broadcaster.VelocityRetentionFactor = 0.97; 
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(broadcaster.IsBroadcasting) broadcaster.Stop();
            broadcaster.Dispose();
            broadcaster = null;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (broadcaster == null) return;

            if (broadcaster.PacketsSent != lastPacketCont)
            {
                lblPackets.Text = broadcaster.PacketsSent.ToString();
                lblBandwidth.Text = Math.Round(broadcaster.PacketVolumeSent/1048576.0, 0).ToString();
                lastPacketCont = broadcaster.PacketsSent;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (broadcaster.IsBroadcasting)
            {
                broadcaster.Stop();
                btnStop.Text = "Start";
            }
            else
            {
                broadcaster.Start();
                btnStop.Text = "Stop";
            }
        }

        
    }
}

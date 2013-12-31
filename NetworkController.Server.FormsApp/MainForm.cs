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
using NetworkController.Server.Logic;
using NetworkController.Logic.Plugin.Interfaces;

namespace NetworkController.Server.FormsApp
{
    public partial class MainForm : Form
    {
        private Broadcaster broadcaster;
        private long lastPacketCont;
        private List<Button> driverButtons; 

        public MainForm()
        {
            driverButtons = new List<Button>();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            broadcaster = new Broadcaster();
            broadcaster.BroadcastPort = 9050;
            broadcaster.DriversReady +=
                delegate
                    {

                        if (pnlGuiButtons.InvokeRequired)
                        {
                            var drivers = broadcaster.GetDrivers();

                            foreach (var driver in drivers)
                            {
                                var button = new Button
                                                 {
                                                     Height = 24,
                                                     Width = pnlGuiButtons.Width/2-10,
                                                     Text = driver.ProviderName,
                                                     Tag = driver,
                                                     Visible = true
                                                 };
                                button.Click += (x, y) =>
                                                    {
                                                        var b = x as Button;
                                                        var t = b.Tag as IDriverAbstracter;
                                                        t.ShowGui();
                                                    };
                                driverButtons.Add(button);
                                pnlGuiButtons.Invoke(new MethodInvoker(() => pnlGuiButtons.Controls.Add(button)));
                            }
                        }
                    };


            broadcaster.Start();

            //TODO: Dummy code, move into config or somethin
            broadcaster.VelocityRetentionFactor = 0.97; 
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var b in driverButtons) b.Tag = null;

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
            var exceptions = broadcaster.PopExceptions();
            if (exceptions.Count > 0)
            {
                foreach (var exception in exceptions)
                {
                    txtExceptions.Text += exception.ToString() + Environment.NewLine + Environment.NewLine;
                }
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

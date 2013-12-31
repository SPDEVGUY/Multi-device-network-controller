using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkController.Plugin.YeiThreeSpace
{
    public partial class Gui : Form
    {
        public YeiDriverAbstracter Abstracter;

        public Gui(YeiDriverAbstracter abstracter)
        {
            Abstracter = abstracter;
            InitializeComponent();
            updateTimer.Enabled = true;

            
        }


        private void btnTare_Click(object sender, EventArgs e)
        {
            var item = lstSensors.SelectedItems[0];
            var sensor = item.Tag as YeiDriverAbstracter.SensorConfig;
            sensor.Sensor.Tare();
        }

        private void Gui_FormClosed(object sender, FormClosedEventArgs e)
        {
            updateTimer.Enabled = false;
            Abstracter = null;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (lstSensors.Items.Count < Abstracter.Sensors.Count)
            {
                foreach (var sensor in Abstracter.Sensors)
                {
                    var item = new ListViewItem(sensor.Sensor.PortName) { Tag = sensor };
                    lstSensors.Items.Add(item);
                }
            }

            foreach (var sensor in Abstracter.Sensors)
            {
                var item = lstSensors.Items[sensor.Index];
                item.BackColor = sensor.IsConnected ? Color.Transparent : Color.DarkRed;
                item.ForeColor = sensor.IsEnabled ? Color.Black : Color.Gray;
            }
        }

        private void lstSensors_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTare.Enabled
                = chkEnabled.Enabled
                = chkCompass.Enabled
                = chkGravity.Enabled
                = chkGyro.Enabled
                = chkRotation.Enabled
                = lstSensors.SelectedItems.Count > 0;

            if (btnTare.Enabled)
            {
                var item = lstSensors.SelectedItems[0];
                var sensor = item.Tag as YeiDriverAbstracter.SensorConfig;

                chkEnabled.Checked = sensor.IsEnabled;
                chkCompass.Checked = sensor.CompassEnabled;
                chkGravity.Checked = sensor.GravityEnabled;
                chkGyro.Checked = sensor.GyroEnabled;
                chkRotation.Checked = sensor.RotationEnabled;
            }
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            var item = lstSensors.SelectedItems[0];
            var sensor = item.Tag as YeiDriverAbstracter.SensorConfig;
            sensor.IsEnabled = chkEnabled.Checked;
        }

        private void chkGravity_CheckedChanged(object sender, EventArgs e)
        {

            var item = lstSensors.SelectedItems[0];
            var sensor = item.Tag as YeiDriverAbstracter.SensorConfig;
            sensor.GravityEnabled = chkGravity.Checked;
        }

        private void chkCompass_CheckedChanged(object sender, EventArgs e)
        {
            var item = lstSensors.SelectedItems[0];
            var sensor = item.Tag as YeiDriverAbstracter.SensorConfig;
            sensor.CompassEnabled = chkCompass.Checked;
        }

        private void chkGyro_CheckedChanged(object sender, EventArgs e)
        {
            var item = lstSensors.SelectedItems[0];
            var sensor = item.Tag as YeiDriverAbstracter.SensorConfig;
            sensor.GyroEnabled = chkGyro.Checked;
        }

        private void chkRotation_CheckedChanged(object sender, EventArgs e)
        {
            var item = lstSensors.SelectedItems[0];
            var sensor = item.Tag as YeiDriverAbstracter.SensorConfig;
            sensor.RotationEnabled = chkRotation.Checked;
        }
    }
}

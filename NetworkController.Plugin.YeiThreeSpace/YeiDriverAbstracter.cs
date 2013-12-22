using System.Collections.Generic;
using NetworkController.Logic.Plugin.Attributes;
using NetworkController.Plugin.Mouse;
using YEISensorLib.Sharped;

namespace NetworkController.Plugin.YeiThreeSpace
{
    [DriverAbstracter]
    public class YeiDriverAbstracter : DriverAbstracterBase
    {
        public List<SensorDevice> Sensors;
        public List<bool> IsConnected;

        public YeiDriverAbstracter()
            : base(Constants.ProviderName)
        {
            
        }

        protected override void CaptureCurrentState()
        {
            for(var i = 0; i<Sensors.Count;i++) 
            {
                if (IsConnected[i] && Sensors[i].IsConnected)
                {
                    CaptureSensorState(Sensors[i]);
                }
                else if(IsConnected[i])
                {
                    IsConnected[i] = false;
                    SetSensorState(Sensors[i].PortName,false);
                }
            }
        }

        private void CaptureSensorState(SensorDevice sensor)
        {
            sensor.GetEulerAngles();
            sensor.GetNormalizedSensorData();
            var port = sensor.PortName + ".";

            //TODO: Reduce noise from sensor to reduce network send traffic?

            AddSliderValue(port + Constants.AccelX, (int)(sensor.Accelerometer.X * 1000.0));
            AddSliderValue(port + Constants.AccelY, (int)(sensor.Accelerometer.Y * 1000.0));
            AddSliderValue(port + Constants.AccelZ, (int)(sensor.Accelerometer.Z * 1000.0));
            AddSliderValue(port + Constants.GyroX, (int)(sensor.Gyro.X * 1000.0));
            AddSliderValue(port + Constants.GyroY, (int)(sensor.Gyro.Y * 1000.0));
            AddSliderValue(port + Constants.GyroZ, (int)(sensor.Gyro.Z * 1000.0));
            AddSliderValue(port + Constants.CompassX, (int)(sensor.Compass.X * 1000.0));
            AddSliderValue(port + Constants.CompassY, (int)(sensor.Compass.Y * 1000.0));
            AddSliderValue(port + Constants.CompassZ, (int)(sensor.Compass.Z * 1000.0));
            AddSliderValue(port + Constants.DegreesX, (int)((sensor.Euler.X / 3.14159) * 360 * 1000.0));
            AddSliderValue(port + Constants.DegreesY, (int)((sensor.Euler.Y / 3.14159) * 360 * 1000.0));
            AddSliderValue(port + Constants.DegreesZ, (int)((sensor.Euler.Z / 3.14159) * 360 * 1000.0));
        }

        protected override void InitializeDriver()
        {
            Sensors = SensorDevices.GetDevices();
            IsConnected = new List<bool>(new bool[Sensors.Count]);
            
            //Indicate devise is enabled
            var i = 0;
            foreach (var sensor in Sensors)
            {
                IsConnected[i] = true;
                i++;
                SetSensorState(sensor.PortName,true);
                sensor.Tare();
            }
        }

        protected void SetSensorState(string port, bool isEnabled)
        {
            AddDeviceStateValue(port + "." + Constants.AccelX,isEnabled);
            AddDeviceStateValue(port + "." + Constants.AccelY,isEnabled);
            AddDeviceStateValue(port + "." + Constants.AccelZ,isEnabled);

            AddDeviceStateValue(port + "." + Constants.GyroX,isEnabled);
            AddDeviceStateValue(port + "." + Constants.GyroY,isEnabled);
            AddDeviceStateValue(port + "." + Constants.GyroZ,isEnabled);

            AddDeviceStateValue(port + "." + Constants.CompassX,isEnabled);
            AddDeviceStateValue(port + "." + Constants.CompassY,isEnabled);
            AddDeviceStateValue(port + "." + Constants.CompassZ,isEnabled);

            AddDeviceStateValue(port + "." + Constants.DegreesX,isEnabled);
            AddDeviceStateValue(port + "." + Constants.DegreesY,isEnabled);
            AddDeviceStateValue(port + "." + Constants.DegreesZ,isEnabled);
        }

        protected override void DisposeDriver()
        {
            foreach (var sensor in Sensors)
            {
                sensor.Dispose();
            }
        }
    }
}

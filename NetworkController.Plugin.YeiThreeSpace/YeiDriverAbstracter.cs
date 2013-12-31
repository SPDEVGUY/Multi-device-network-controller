using System.Collections.Generic;
using NetworkController.Logic.Plugin.Attributes;
using NetworkController.Plugin.Mouse;
using YEISensorLib.Sharped;

namespace NetworkController.Plugin.YeiThreeSpace
{
    [DriverAbstracter]
    public class YeiDriverAbstracter : DriverAbstracterBase
    {
        public List<SensorConfig> Sensors;
        private Gui _gui;

        public class SensorConfig
        {
            public int Index;
            public SensorDevice Sensor;
            public bool IsConnected = true;
            public bool IsEnabled = true;
            public bool GravityEnabled = true;
            public bool GyroEnabled = true;
            public bool CompassEnabled = true;
            public bool RotationEnabled = true;
        }

        public YeiDriverAbstracter()
            : base(Constants.ProviderName)
        {
            Sensors = new List<SensorConfig>();
        }

        public override void ShowGui()
        {
            if(_gui == null) _gui = new Gui(this);
            _gui.Show();
            _gui.Focus();
        }

        protected override void CaptureCurrentState()
        {
            foreach(var sensor in Sensors) 
            {
                if (sensor.IsEnabled && sensor.IsConnected && sensor.Sensor.IsConnected)
                {
                    CaptureSensorState(sensor);
                }
                else if (sensor.IsConnected && !sensor.Sensor.IsConnected)
                {
                    sensor.IsConnected = false;
                    SetSensorState(sensor);
                }
            }
        }

        private void CaptureSensorState(SensorConfig sensorConfig)
        {
            var sensor = sensorConfig.Sensor;
            
            if (sensorConfig.RotationEnabled) sensor.GetEulerAngles();

            if (sensorConfig.CompassEnabled || sensorConfig.GravityEnabled || sensorConfig.GyroEnabled) 
                sensor.GetNormalizedSensorData();
            
            var name = sensorConfig.Index + ".";

            //TODO: Reduce noise from sensor to reduce network send traffic?

            if (sensorConfig.GravityEnabled)
            {
                AddSliderValue(name + Constants.AccelX, (int) (sensor.Accelerometer.X*1000.0));
                AddSliderValue(name + Constants.AccelY, (int) (sensor.Accelerometer.Y*1000.0));
                AddSliderValue(name + Constants.AccelZ, (int) (sensor.Accelerometer.Z*1000.0));
            }
            if (sensorConfig.GyroEnabled)
            {
                AddSliderValue(name + Constants.GyroX, (int) (sensor.Gyro.X*1000.0));
                AddSliderValue(name + Constants.GyroY, (int) (sensor.Gyro.Y*1000.0));
                AddSliderValue(name + Constants.GyroZ, (int) (sensor.Gyro.Z*1000.0));
            }
            if (sensorConfig.CompassEnabled)
            {
                AddSliderValue(name + Constants.CompassX, (int) (sensor.Compass.X*1000.0));
                AddSliderValue(name + Constants.CompassY, (int) (sensor.Compass.Y*1000.0));
                AddSliderValue(name + Constants.CompassZ, (int) (sensor.Compass.Z*1000.0));
            }

            if (sensorConfig.RotationEnabled)
            {
                AddSliderValue(name + Constants.DegreesX, (int) ((sensor.Euler.X/3.14159)*180*1000.0));
                AddSliderValue(name + Constants.DegreesY, (int) ((sensor.Euler.Y/3.14159)*180*1000.0));
                AddSliderValue(name + Constants.DegreesZ, (int) ((sensor.Euler.Z/3.14159)*180*1000.0));
            }
        }

        protected override void InitializeDriver()
        {
            var sensors = SensorDevices.GetDevices();
            
            var i = 0;
            foreach (var sensor in sensors)
            {
                var sensorConfig = new SensorConfig { Sensor = sensor, Index =  i};
                Sensors.Add(sensorConfig);
               
                SetSensorState(sensorConfig);
                sensor.Tare();
                i++;
            }
        }

        protected void SetSensorState(SensorConfig sensorConfig)
        {
            var name = sensorConfig.Index + ".";
            AddDeviceStateValue(name + Constants.AccelX, sensorConfig.IsEnabled & sensorConfig.GravityEnabled);
            AddDeviceStateValue(name + Constants.AccelY, sensorConfig.IsEnabled & sensorConfig.GravityEnabled);
            AddDeviceStateValue(name + Constants.AccelZ, sensorConfig.IsEnabled & sensorConfig.GravityEnabled);

            AddDeviceStateValue(name + Constants.GyroX, sensorConfig.IsEnabled & sensorConfig.GyroEnabled);
            AddDeviceStateValue(name + Constants.GyroY, sensorConfig.IsEnabled & sensorConfig.GyroEnabled);
            AddDeviceStateValue(name + Constants.GyroZ, sensorConfig.IsEnabled & sensorConfig.GyroEnabled);

            AddDeviceStateValue(name + Constants.CompassX, sensorConfig.IsEnabled & sensorConfig.CompassEnabled);
            AddDeviceStateValue(name + Constants.CompassY, sensorConfig.IsEnabled & sensorConfig.CompassEnabled);
            AddDeviceStateValue(name + Constants.CompassZ, sensorConfig.IsEnabled & sensorConfig.CompassEnabled);

            AddDeviceStateValue(name + Constants.DegreesX, sensorConfig.IsEnabled & sensorConfig.RotationEnabled);
            AddDeviceStateValue(name + Constants.DegreesY, sensorConfig.IsEnabled & sensorConfig.RotationEnabled);
            AddDeviceStateValue(name + Constants.DegreesZ, sensorConfig.IsEnabled & sensorConfig.RotationEnabled);
        }

        protected override void DisposeDriver()
        {
            if (_gui != null)
            {
                
                    _gui.Close();
                
            }
            foreach (var sensor in Sensors)
            {
                sensor.Sensor.Dispose();
            }
            Sensors.Clear();
        }
    }
}

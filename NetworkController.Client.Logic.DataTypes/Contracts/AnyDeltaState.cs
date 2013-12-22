using NetworkController.Client.Logic.DataTypes.Interfaces;

namespace NetworkController.Client.Logic.DataTypes.Contracts
{
    /// <summary>
    /// Note: This contract is basically just a mash up of all the fields
    /// from all of the delta types, since JSON doesn't transmit types.
    /// </summary>
    public class AnyDeltaState : IDeltaState, IDeltaButtonState, IDeltaGestureState, IDeltaSliderState, IDeltaDeviceState
    {
        public byte DeltaType { get; set; }
        public int LastValue { get; set; }
        public int Value { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        public string InputName { get; set; }
        public string ProviderName { get; set; }
        public long TimeStampTicks { get; set; }
        public double Velocity { get; set; }
        public double Acceleration { get; set; }
        public long TimeDelta { get; set; }

        public int LastIntensity { get; set; }
        public bool DetectedLastTick { get; set; }
        public bool DetectedThisTick { get; set; }
        public int Intensity { get; set; }

        public int PressCount { get; set; }
        public int LastPressCount { get; set; }
        public bool IsPressed { get; set; }

        public bool WasEnabled { get; set; }
        public bool IsEnabled { get; set; }
    }
}

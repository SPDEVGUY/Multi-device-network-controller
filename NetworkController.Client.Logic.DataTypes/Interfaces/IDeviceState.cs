namespace NetworkController.Client.Logic.DataTypes.Interfaces
{
    public interface IDeviceState : IState
    {
        /// <summary>
        /// True if the device input is active and sending it's values.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}

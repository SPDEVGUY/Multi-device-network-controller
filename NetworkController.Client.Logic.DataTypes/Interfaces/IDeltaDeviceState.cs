namespace NetworkController.Client.Logic.DataTypes.Interfaces
{
    public interface IDeltaDeviceState : IDeviceState, IDeltaState
    {
        
        /// <summary>
        /// The previous active state
        /// </summary>
        bool WasEnabled { get; set; }

    }
}

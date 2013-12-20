using NetworkController.Client.Logic.DataTypes.Interfaces;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IServerDeltaDeviceState : IDeltaDeviceState, IServerDeltaState
    {
        /// <summary>
        /// Apply a new state to this delta
        /// </summary>
        /// <param name="newState"></param>
        void ApplyNewState(IDeviceState newState);
    }
}

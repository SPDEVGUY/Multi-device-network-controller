using NetworkController.Client.Logic.Interfaces;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IServerDeltaButtonState : IDeltaButtonState, IServerDeltaState
    {
        /// <summary>
        /// Apply a new button state to this delta
        /// </summary>
        /// <param name="newState"></param>
        void ApplyNewState(IButtonState newState);
    }
}

using NetworkController.Client.Logic.Interfaces;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IServerDeltaSliderState : IDeltaSliderState, IServerDeltaState
    {
        /// <summary>
        /// Apply a new state change to this delta
        /// </summary>
        /// <param name="newState"></param>
        void ApplyNewState(ISliderState newState);
    }
}

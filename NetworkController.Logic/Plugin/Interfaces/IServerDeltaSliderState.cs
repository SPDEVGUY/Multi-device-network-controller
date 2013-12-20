using NetworkController.Client.Logic.DataTypes.Interfaces;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IServerDeltaSliderState : IDeltaSliderState, IServerMeasuredDeltaState
    {
        /// <summary>
        /// Apply a new state change to this delta
        /// </summary>
        /// <param name="newState"></param>
        void ApplyNewState(ISliderState newState);
    }
}

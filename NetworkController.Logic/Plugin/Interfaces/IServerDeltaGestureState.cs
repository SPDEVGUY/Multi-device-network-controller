using NetworkController.Client.Logic.DataTypes.Interfaces;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IServerDeltaGestureState : IDeltaGestureState, IServerMeasuredDeltaState
    {

        /// <summary>
        /// Apply a gesture detection to this delta
        /// </summary>
        /// <param name="newState"></param>
        void ApplyNewState(IGestureState newState);
    }
}

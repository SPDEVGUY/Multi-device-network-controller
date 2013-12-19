using NetworkController.Client.Logic.Interfaces;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IServerDeltaGestureState : IDeltaGestureState, IServerDeltaState
    {

        /// <summary>
        /// Apply a gesture detection to this delta
        /// </summary>
        /// <param name="newState"></param>
        void ApplyNewState(IGestureState newState);
    }
}

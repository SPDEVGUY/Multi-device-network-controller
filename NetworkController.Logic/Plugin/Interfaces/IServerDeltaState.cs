using NetworkController.Client.Logic.Interfaces;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IServerDeltaState : IDeltaState
    {
        /// <summary>
        /// This method is called after all new states have been accumulated.
        /// </summary>
        void FinalizeDelta();
    }
}

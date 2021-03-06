﻿using NetworkController.Client.Logic.DataTypes.Interfaces;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IServerDeltaButtonState : IDeltaButtonState, IServerMeasuredDeltaState
    {
        /// <summary>
        /// Apply a new button state to this delta
        /// </summary>
        /// <param name="newState"></param>
        void ApplyNewState(IButtonState newState);
    }
}

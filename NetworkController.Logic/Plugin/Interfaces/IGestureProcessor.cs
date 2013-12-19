using System.Collections.Generic;
using NetworkController.Logic.Controller;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IGestureProcessor
    {
        /// <summary>
        /// This method is where you should add to CurrentGestureStates for any gestures detected.
        /// This is called after an input sweep.
        /// </summary>
        void PerformGestureDetection();

        /// <summary>
        /// The name of the provider
        /// </summary>
        string ProviderName { get; set; }

        InputDeltaObserver Observer { get; set; }
    }
}

using System.Collections.Generic;
using NetworkController.Logic.Controller;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IStateRemapper
    {
        /// <summary>
        /// Remap any state names / values here before the gesture processor is run on them.
        /// </summary>
        /// <param name="observer"></param>
        void PerformPreGestureRemapping(InputDeltaObserver observer);

        /// <summary>
        /// Remap any state names / values here after gesture processor is run on them.
        /// </summary>
        /// <param name="observer"></param>
        void PerformPostGestureRemapping(InputDeltaObserver observer);
    }
}

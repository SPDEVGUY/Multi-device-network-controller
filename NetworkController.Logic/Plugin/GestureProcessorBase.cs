using NetworkController.Client.Logic.Interfaces;
using NetworkController.Logic.Controller;
using NetworkController.Logic.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NetworkController.Logic.Plugin
{
    //Put this on your inheriting classes: [GestureProcessor]
    public abstract class GestureProcessorBase : IGestureProcessor
    {

        protected GestureProcessorBase(string providerName)
        {
            ProviderName = providerName;
        }


        public string ProviderName { get; set; }
        public InputDeltaObserver Observer { get; set; }

        protected void AddGestureState(string gestureName, int intensity)
        {
            Observer.GestureQueue.Add(new GestureState
            {
                ProviderName = ProviderName,
                InputName = gestureName,
                Intensity = intensity
            });
        }


        public abstract void PerformGestureDetection();
    }
}

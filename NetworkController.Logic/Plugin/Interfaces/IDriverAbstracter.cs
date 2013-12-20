using System;
using System.Collections.Generic;
using NetworkController.Client.Logic.DataTypes.Interfaces;

namespace NetworkController.Logic.Plugin.Interfaces
{
    public interface IDriverAbstracter : IDisposable
    {
        /// <summary>
        /// Name of the provider, like Kinect, XboxController, Mouse, etc...
        /// </summary>
        string ProviderName { get; set; }


        /// <summary>
        /// Method to retrieve the current slider states from the controller
        /// </summary>
        /// <returns>List of all slider states.</returns>
        List<ISliderState> PopSliderQueue();

        /// <summary>
        /// Method to retrieve the current gesture statues from the controller
        /// </summary>
        /// <returns>List of all the current detected gestures</returns>
        List<IGestureState> PopGestureQueue();
        
        /// <summary>
        /// Method to retrieve the current button states from the controller
        /// </summary>
        /// <returns>List of all button state changes since last access.</returns>
        List<IButtonState> PopButtonQueue();
        
        /// <summary>
        /// Method to retrieve the current button states from the controller
        /// </summary>
        /// <returns>List of all button state changes since last access.</returns>
        List<IDeviceState> PopDeviceStateQueue();

        /// <summary>
        /// Signal to driver to start capturing in it's own thread.
        /// This is where you should new up any related objects, and start a thread to monitor input.
        /// </summary>
        void StartCapturing();

        /// <summary>
        /// This method is a nicer way of saying stop whatever you are doing and dispose of stuff.
        /// Eventually IDisposable.Dispose is called, but this is where you should dispose in a cleaner sequential way.
        /// </summary>
        void StopCapturing();
    }
}

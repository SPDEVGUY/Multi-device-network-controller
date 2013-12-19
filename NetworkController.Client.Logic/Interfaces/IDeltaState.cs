namespace NetworkController.Client.Logic.Interfaces
{
    public interface IDeltaState : IState
    {
        /// <summary>
        /// Velocity of whatever state change occurred
        /// </summary>
        double Velocity { get; set; }

        /// <summary>
        /// Acceleration of state change since last measure
        /// </summary>
        double Acceleration { get; set; }

        /// <summary>
        /// Time stamp diff since last measure, IState.TimeStampTicks is always latest, 
        /// Retrieve last time stamp by IState.TimeStampTicks - IStateDelta.TimeDelta if needed.
        /// </summary>
        long TimeDelta { get; set; }


        /// <summary>
        /// Delta type, used for JSON reconstruction
        /// </summary>
        byte DeltaType { get; set; }

    }
}

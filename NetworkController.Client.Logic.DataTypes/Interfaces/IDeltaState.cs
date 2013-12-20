namespace NetworkController.Client.Logic.DataTypes.Interfaces
{
    public interface IDeltaState : IState
    {
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

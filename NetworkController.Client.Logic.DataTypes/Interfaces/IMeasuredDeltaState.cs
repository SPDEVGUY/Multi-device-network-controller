namespace NetworkController.Client.Logic.DataTypes.Interfaces
{
    public interface IMeasuredDeltaState : IDeltaState
    {
        /// <summary>
        /// Velocity of whatever state change occurred
        /// </summary>
        double Velocity { get; set; }

        /// <summary>
        /// Acceleration of state change since last measure
        /// </summary>
        double Acceleration { get; set; }
    }
}

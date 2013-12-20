namespace NetworkController.Client.Logic.DataTypes.Interfaces
{
    public interface IDeltaSliderState : ISliderState, IMeasuredDeltaState
    {
        /// <summary>
        /// The last value of the delta
        /// </summary>
        int LastValue { get; set; }
    }
}

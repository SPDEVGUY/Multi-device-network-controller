namespace NetworkController.Client.Logic.Interfaces
{
    public interface IDeltaSliderState : ISliderState, IDeltaState
    {
        /// <summary>
        /// The last value of the delta
        /// </summary>
        int LastValue { get; set; }
    }
}

namespace NetworkController.Client.Logic.Interfaces
{
    public interface IGestureState : IState
    {
        /// <summary>
        /// How verbose or eccentric was the gesture
        /// </summary>
        int Intensity { get; set; }
    }
}

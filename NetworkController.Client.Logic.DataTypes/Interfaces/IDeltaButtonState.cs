namespace NetworkController.Client.Logic.DataTypes.Interfaces
{
    public interface IDeltaButtonState : IButtonState, IMeasuredDeltaState
    {
        /// <summary>
        /// Number of times the button was depressed since last tick.
        /// </summary>
        int PressCount { get; set; }
        
        /// <summary>
        /// The previous last press count
        /// </summary>
        int LastPressCount { get; set; }

    }
}

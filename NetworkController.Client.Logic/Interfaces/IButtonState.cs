namespace NetworkController.Client.Logic.Interfaces
{
    public interface IButtonState : IState
    {
        /// <summary>
        /// True if the button is depressed currently.
        /// </summary>
        bool IsPressed { get; set; }
    }
}

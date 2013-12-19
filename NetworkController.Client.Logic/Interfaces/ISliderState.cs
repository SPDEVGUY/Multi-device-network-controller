namespace NetworkController.Client.Logic.Interfaces
{
    public interface ISliderState : IState
    {
        /// <summary>
        /// Current slider value
        /// </summary>
        int Value { get; set; }

        /// <summary>
        /// Max slider value (can be provided or observed)
        /// </summary>
        int MaxValue { get; set; }

        /// <summary>
        /// Min slider value (can be provided or observed)
        /// </summary>
        int MinValue { get; set; }
    }
}

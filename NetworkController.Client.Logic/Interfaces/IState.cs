namespace NetworkController.Client.Logic.Interfaces
{
    public interface IState
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        string InputName { get; set; }

        /// <summary>
        /// Provider name
        /// </summary>
        string ProviderName { get; set; }

        /// <summary>
        /// The timestamp of the state
        /// </summary>
        long TimeStampTicks { get; set; }
    }
}

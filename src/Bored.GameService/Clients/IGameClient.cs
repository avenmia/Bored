namespace Bored.GameService.Clients
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for the Game Client.
    /// </summary>
    public interface IGameClient
    {
        /// <summary>
        /// The method for clients to recieve the message.
        /// </summary>
        /// <param name="state">The serialized version of the state.</param>
        /// <returns>A task.</returns>
        Task ReceiveMessage(string state);
    }
}

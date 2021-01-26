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
        Task ReceiveState(string state);

        /// <summary>
        /// The method for clients to receive 
        /// the game id.
        /// </summary>
        /// <param name="gameId">The new game Id.</param>
        /// <returns>A task.</returns>
        Task ReceiveGameId(string gameId);

        /// <summary>
        /// Sends the error message back to the client.
        /// </summary>
        /// <param name="error">The error that occured.</param>
        /// <returns>A task.</returns>
        Task ReceiveError(string error);
    }
}

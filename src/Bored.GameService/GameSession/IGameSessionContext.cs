namespace Bored.GameService.GameSession
{
    using Bored.Common;

    /// <summary>
    /// The interface for the GameSessionContext.
    /// </summary>
    public interface IGameSessionContext
    {
        /// <summary>
        /// Gets the game state from the database based on the gameID.
        /// </summary>
        /// <param name="gameID">The game's ID.</param>
        /// <returns>The current state of the game.</returns>
        string GetGameState(string gameID);

        /// <summary>
        /// Adds the game state to the database.
        /// </summary>
        /// <param name="gameID">The gameID to add the state to.</param>
        /// <param name="state">The game state.</param>
        /// <returns>The current state of the game.</returns>
        string AddGameState(string gameID, IGameState state);
    }
}

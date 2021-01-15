namespace Bored.GameService.GameSession
{
    using Bored.Common;
    using Newtonsoft.Json;
    using StackExchange.Redis;

    /// <summary>
    /// The GameSessionContext. This is responsible for managing the database connection.
    /// </summary>
    public class GameSessionContext : IGameSessionContext
    {
        /// <summary>
        /// The connection multiplexer.
        /// </summary>
        private readonly IConnectionMultiplexer muxer;

        /// <summary>
        /// The database connection.
        /// </summary>
        private IDatabase db;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSessionContext"/> class.
        /// </summary>
        /// <param name="connMultiplexer">The connection multiplexer.</param>
        public GameSessionContext(IConnectionMultiplexer connMultiplexer)
        {
            muxer = connMultiplexer;
            db = muxer.GetDatabase();
        }

        /// <summary>
        /// Gets the game state from the database based on the gameID.
        /// </summary>
        /// <param name="gameID">The game's ID.</param>
        /// <returns>The current state of the game.</returns>
        public string GetGameState(string gameID)
        {
            return db.StringGet(gameID);
        }

        /// <summary>
        /// Adds the game state to the database.
        /// </summary>
        /// <param name="gameID">The gameID to add the state to.</param>
        /// <param name="state">The game state.</param>
        /// <returns>The current state of the game.</returns>
        public string AddGameState(string gameID, IGameState state)
        {
            var serializedState = JsonConvert.SerializeObject(state);
            return db.StringSet(gameID, serializedState) ? serializedState : null;
        }
    }
}

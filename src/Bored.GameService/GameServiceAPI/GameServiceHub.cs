namespace Bored.GameService.GameServiceAPI
{
    using System.Threading.Tasks;
    using Bored.GameService.Clients;
    using Bored.GameService.Factories;
    using Bored.GameService.GameSession;
    using Bored.GameService.Models;
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// The GameServiceHub. This is responsible for receiving messages from
    /// clients and responding to clients.
    /// </summary>
    public class GameServiceHub : Hub<IGameClient>
    {
        /// <summary>
        /// The GameContext deals with the database connection for the game.
        /// </summary>
        private readonly IGameSessionContext gameContext;

        /// <summary>
        /// The Factory deals with retrieving the correct game type,
        /// game move, and game state.
        /// </summary>
        private readonly IFactory Factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameServiceHub"/> class.
        /// </summary>
        /// <param name="context">The game session context.</param>
        /// <param name="factory">The factory.</param>
        public GameServiceHub(IGameSessionContext context, IFactory factory)
        {
            gameContext = context;
            Factory = factory;
        }

        /// <summary>
        /// Receives the message from the clients, runs the game logic,
        /// then sends the ending state back to the clients.
        /// </summary>
        /// <param name="message">The client message.</param>
        /// <returns>A task.</returns>
        public Task SendMessage(GameMessage message)
        {
            var gameState = gameContext.GetGameState(message.GameID);
            var deserializedGameState = Factory.GameStateFactory(message.Game, gameState);
            var game = Factory.GameFactory(message.Game, deserializedGameState);
            var gameMove = Factory.GameMoveFactory(message.Game, message.Move);
            var updatedGameState = game.MakeMove(gameMove);

            if (updatedGameState == null)
            {
                return Clients.All.ReceiveMessage("Invalid Move");
            }

            var serializedState = gameContext.AddGameState(message.GameID, updatedGameState);

            return Clients.All.ReceiveMessage(serializedState);
        }
    }
}

namespace Bored.GameService.GameServiceAPI
{
    using System;
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
        private readonly IFactory gameFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameServiceHub"/> class.
        /// </summary>
        /// <param name="context">The game session context.</param>
        /// <param name="factory">The factory.</param>
        public GameServiceHub(IGameSessionContext context, IFactory factory)
        {
            gameContext = context;
            gameFactory = factory;
        }

        /// <summary>
        /// Function that generates a gameId
        /// for the client.
        /// </summary>
        /// <returns> representing sending the new GameId to the clients.</returns>
        public Task GetNewGameID()
        {
            return Clients.All.ReceiveGameId(Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Function that initializes a new game.
        /// </summary>
        /// <param name="message">The client message.</param>
        /// <returns>A task.</returns>
        public Task InitializeGame(GameMessage message)
        {
            var gameState = gameContext.GetGameState(message.GameID);
            if (gameState != null)
            {
                return Clients.All.ReceiveError("Game has already been initialized.");
            }

            var deserializedGameState = gameFactory.GameStateFactory(message.Game, gameState);
            var serializedState = gameContext.AddGameState(message.GameID, deserializedGameState);
            return Clients.All.ReceiveState(serializedState);
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
            var deserializedGameState = gameFactory.GameStateFactory(message.Game, gameState);
            var game = gameFactory.GameFactory(message.Game, deserializedGameState);
            var gameMove = gameFactory.GameMoveFactory(message.Game, message.Move);
            var updatedGameState = game.MakeMove(gameMove);

            if (updatedGameState == null)
            {
                return Clients.All.ReceiveError("Invalid Move");
            }

            var serializedState = gameContext.AddGameState(message.GameID, updatedGameState);

            return Clients.All.ReceiveState(serializedState);
        }
    }
}

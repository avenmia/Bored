using Bored.Common;
using Bored.Game.TicTacToe;
using Bored.GameService.Clients;
using Bored.GameService.Factories;
using Bored.GameService.GameSession;
using Bored.GameService.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Bored.GameService.GameServiceAPI
{
    public class GameServiceHub : Hub<IGameClient>
    {
        private readonly IGameSessionContext GameContext;

        private readonly IFactory Factory;

        public GameServiceHub(IGameSessionContext context, IFactory factory)
        {
            GameContext = context;
            Factory = factory;
        }

        public Task SendMessage(GameMessage message)
        {
            var gameState = GameContext.GetGameState(message.GameID);
            var deserializedGameState = Factory.GameStateFactory(message.Game, gameState);
            var game = Factory.GameFactory(message.Game, deserializedGameState);
            var gameMove = Factory.GameMoveFactory(message.Game, message.Move);
            var updatedGameState = game.MakeMove(gameMove);

            if (updatedGameState == null)
            {
                return Clients.All.ReceiveMessage("Invalid Move");
            }

            var serializedState = GameContext.AddGameState(message.GameID, updatedGameState);

            return Clients.All.ReceiveMessage(serializedState);
        }
    }
}

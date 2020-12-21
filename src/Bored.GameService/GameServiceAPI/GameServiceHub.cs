using Bored.Common;
using Bored.Game.TicTacToe;
using Bored.GameService.Clients;
using Bored.GameService.GameSession;
using Bored.GameService.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Bored.GameService.GameServiceAPI
{
    public class GameServiceHub : Hub<IGameClient>
    {
        private IGameSessionContext _context;
        public GameServiceHub(IGameSessionContext context)
        {
            _context = context;
        }

        public Task SendMessage(GameMessage message)
        {
            // This should be done by .NET ?
            var gameTypeDef = new { GameType = "" };
            var gameState = JsonConvert.DeserializeAnonymousType(message.GameState, gameTypeDef);

            // Need a way to deserialize the game state to the resulting game type
            Assembly a = Assembly.Load("Bored.Game.TicTacToe");
            var resultingGameType = a.GetTypes().Where(typeName => typeName.Name == gameState.GameType).FirstOrDefault();

            return Clients.All.ReceiveMessage(message);
        }
    }
}

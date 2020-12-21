using Bored.Common;
using Bored.Game.TicTacToe;
using Bored.GameService.Clients;
using Bored.GameService.GameSession;
using Bored.GameService.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
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
            // Need a way to deserialize the game state to the resulting game type
            Assembly a = Assembly.Load("Bored.Game.TicTacToe");
            var resultingGameType = a.GetTypes().Where(typeName => typeName.Name == message.GameType).FirstOrDefault();

            var gameStateType = message.GameState.GetType();

            var deserialize = JsonConvert.DeserializeObject<TestState>(message.GameState);

            // This is slow
            var result = Activator.CreateInstance(resultingGameType, new object[] { message.GameState });
            var properties = result.GetType().GetProperties();

            //var jElement = (JsonElement)message.GameState;
            
            //foreach(var prop in properties)
            //{
            //    JsonElement pResult;
            //    var p = jElement.GetProperty(prop.Name);
            //    var strResult = p.GetString();
            //}

            return Clients.All.ReceiveMessage(message);
        }

    }
}

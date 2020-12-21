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
            // var gameMessage = DeserializeMessage(message);
            var state = _context.GetGameState(message.GameID);
            if (state == null)
            {
                state = InitializeGame(message);
                var result = _context.AddGameState(message.GameID, state);
                
                // TODO: Remove this
                return Clients.All.ReceiveMessage(message);
            }
            // IGameLogic = new TicTacToe(state);
            // var updatedState = IGameLogic.MakeMove(state.Move);
            // _context.UpdateGameSession(updatedState)
            // return updatedState
            return Clients.All.ReceiveMessage(message);
        }

        private string InitializeGame(GameMessage message)
        {
            string gameName = message.GameType.Replace("State", "");
            Assembly gameAssembly = Assembly.Load("Bored.Game." + gameName);
            var gameStateType = gameAssembly.GetTypes().Where(typeName => typeName.Name == message.GameType).FirstOrDefault();
            var initialGameState = Activator.CreateInstance(gameStateType);
            return JsonConvert.SerializeObject(initialGameState);
        }

        private object DeserializeMessage(GameMessage message)
        {
            string gameName = message.GameType.Replace("State", "");
            Assembly gameAssembly = Assembly.Load("Bored.Game." + gameName);
            var resultingGameType = gameAssembly.GetTypes().Where(typeName => typeName.Name == message.GameType).FirstOrDefault();
            var gameStateType = message.GameState.GetType();
            var gameState = JsonConvert.DeserializeObject(message.GameState, resultingGameType);
            return gameState;
        }

    }
}

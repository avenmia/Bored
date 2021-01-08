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
        
        public Type Game { get; set; }

        public Type GameState { get; set; }

        public Type GameMove { get; set; }

        public GameServiceHub(IGameSessionContext context)
        {
            _context = context;
        }

        public Task SendMessage(GameMessage message)
        {
            var state = _context.GetGameState(message.GameID);
            SetGameType(message);
            
            // If no current state exists initialize the game
            if (state == null)
            {
                state = InitializeGame();
                var result = _context.AddGameState(message.GameID, state);
                return Clients.All.ReceiveMessage(result);
            }

            var clientMove =  GetClientMove(message);
            var clientState = DeserializeGameState(state);
            var currentGameState = UpdateGame(clientState, clientMove);

            if (currentGameState == null)
            {
                throw new Exception("Invalid move");
            }

            // TODO: Check if this overrides the current state in the database
            currentGameState = _context.AddGameState(message.GameID, currentGameState);

            return Clients.All.ReceiveMessage(currentGameState);
        }

        private void SetGameType(GameMessage message)
        {
            string gameName = message.GameType.Replace("State", "");
            Assembly gameAssembly = Assembly.Load("Bored.Game." + gameName);
            this.Game = gameAssembly.GetTypes().Where(typeName => typeName.Name == gameName).FirstOrDefault();
            this.GameState = gameAssembly.GetTypes().Where(typeName => typeName.Name == message.GameType).FirstOrDefault();
            this.GameMove = gameAssembly.GetTypes().Where(typeName => typeName.Name == gameName + "Move").FirstOrDefault();
        }

        private string UpdateGame(object state, IGameMove move)
        {
            ConstructorInfo ctor = this.Game.GetConstructor(new[] { this.GameState });
            IGameLogic gameInstance = ctor.Invoke(new object[] { state }) as IGameLogic;
            return JsonConvert.SerializeObject(gameInstance.MakeMove(move));
        }

        private string InitializeGame()
        {
            var initialGameState = Activator.CreateInstance(this.GameState);
            return JsonConvert.SerializeObject(initialGameState);
        }


        private IGameMove GetClientMove(GameMessage message)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Error };
            return JsonConvert.DeserializeObject(message.Move, this.GameMove, settings) as IGameMove;
        }

        private object DeserializeGameState(string state)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Error };
            return JsonConvert.DeserializeObject(state, this.GameState, settings);
        }

    }
}

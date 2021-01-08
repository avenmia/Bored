using Bored.Common;
using Bored.Game.TicTacToe;
using Bored.GameService.Clients;
using Bored.GameService.GameSession;
using Bored.GameService.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public void SetGameType(GameMessage message)
        {
            string gameName = message.Game;
            try
            {
                Assembly gameAssembly = Assembly.Load("Bored.Game." + gameName);
                Game = gameAssembly.GetTypes().Where(typeName => typeName.Name == gameName).FirstOrDefault();
                GameState = gameAssembly.GetTypes().Where(typeName => typeName.Name == gameName + "State").FirstOrDefault();
                GameMove = gameAssembly.GetTypes().Where(typeName => typeName.Name == gameName + "Move").FirstOrDefault();
            }
            catch(Exception ex) when 
            (
                ex is ArgumentNullException
                || ex is ArgumentException
                || ex is FileNotFoundException
                || ex is FileLoadException
            )
            {
                throw new Exception("Invalid assembly");
            }

            if(Game == null || GameState == null || GameMove == null)
            {
                throw new Exception("Invalid service request");
            }
        }

        public string UpdateGame(object state, IGameMove move)
        {
            ConstructorInfo ctor = this.Game.GetConstructor(new[] { this.GameState });
            IGameLogic gameInstance = ctor.Invoke(new object[] { state }) as IGameLogic;
            return JsonConvert.SerializeObject(gameInstance.MakeMove(move));
        }

        public string InitializeGame()
        {
            var initialGameState = Activator.CreateInstance(this.GameState);
            return JsonConvert.SerializeObject(initialGameState);
        }


        public IGameMove GetClientMove(GameMessage message)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Error };
            return JsonConvert.DeserializeObject(message.Move, this.GameMove, settings) as IGameMove;
        }

        public object DeserializeGameState(string state)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Error };
            return JsonConvert.DeserializeObject(state, this.GameState, settings);
        }

    }
}

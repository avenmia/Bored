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
            var state = _context.GetGameState(message.GameID);
            if (state == null)
            {
                state = InitializeGame(message);
                var result = _context.AddGameState(message.GameID, state);
                
                // TODO: Remove this
                return Clients.All.ReceiveMessage(message);
            }

            var clientState =  DeserializeMessage(message);
            var currentGameState = UpdateGame(message, clientState);

            // TODO: Check for if currentGameState is null because it is invalid
            // TODO: Check if this overrides the current state in the database
            _context.AddGameState(message.GameID, currentGameState);

            // TODO: Remove this
            return Clients.All.ReceiveMessage(message);
        }

        private string UpdateGame(GameMessage message, ClientState state)
        {
            string gameName = message.GameType.Replace("State", "");

            // TODO: Replace with Factory Pattern?
            Assembly gameAssembly = Assembly.Load("Bored.Game." + gameName);
            var gameType = gameAssembly.GetTypes().Where(typeName => typeName.Name == gameName).FirstOrDefault();
            var gameStateType = gameAssembly.GetTypes().Where(typeName => typeName.Name == message.GameType).FirstOrDefault();
            ConstructorInfo ctor = gameType.GetConstructor(new[] { gameStateType });
            IGameLogic gameInstance = ctor.Invoke(new object[] { state.State }) as IGameLogic;
            return JsonConvert.SerializeObject(gameInstance.MakeMove(state.Move));
        }

        private string InitializeGame(GameMessage message)
        {
            string gameName = message.GameType.Replace("State", "");
            Assembly gameAssembly = Assembly.Load("Bored.Game." + gameName);
            var gameStateType = gameAssembly.GetTypes().Where(typeName => typeName.Name == message.GameType).FirstOrDefault();
            var initialGameState = Activator.CreateInstance(gameStateType);
            return JsonConvert.SerializeObject(initialGameState);
        }


        private ClientState DeserializeMessage(GameMessage message)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Error };
            string gameName = message.GameType.Replace("State", "");
            Assembly gameAssembly = Assembly.Load("Bored.Game." + gameName);
            var resultingGameType = gameAssembly.GetTypes().Where(typeName => typeName.Name == message.GameType).FirstOrDefault();
            var gameMoveType = gameAssembly.GetTypes().Where(typeName => typeName.Name == gameName + "Move").FirstOrDefault();
            var gameStateType = message.GameState.GetType();
            var gameState = JsonConvert.DeserializeObject(message.GameState, resultingGameType, settings);
            var gameMove = JsonConvert.DeserializeObject(message.Move, gameMoveType, settings);
            return new ClientState(gameState, (IGameMove)gameMove);
        }

    }
}

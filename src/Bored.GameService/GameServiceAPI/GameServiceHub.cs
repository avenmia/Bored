﻿using Bored.GameService.Clients;
using Bored.GameService.GameSession;
using Bored.GameService.Models;
using Microsoft.AspNetCore.SignalR;
using System;
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
            var state = _context.GetGameState();
            Console.WriteLine(state);
                // var gameState = context.GetGameState(context);
                // context.AddGameState(gameState);
            return Clients.All.ReceiveMessage(message);
        }
    }
}

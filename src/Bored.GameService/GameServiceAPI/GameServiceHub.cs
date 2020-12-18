using Bored.GameService.Clients;
using Bored.GameService.GameSession;
using Bored.GameService.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Bored.GameService.GameServiceAPI
{
    public class GameServiceHub : Hub<IGameClient>
    {
        public Task SendMessage(GameMessage message)
        {
            using (var context = new GameSessionContext())
            {
                // var gameState = context.GetGameState();
                // context.AddGameState(gameState);
                // Clients.All.ReceiveMessage(gameState);
            }
            return Clients.All.ReceiveMessage(message);
        }
    }
}

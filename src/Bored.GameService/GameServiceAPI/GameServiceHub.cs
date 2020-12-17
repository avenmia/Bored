using Bored.GameService.Clients;
using Bored.GameService.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Bored.GameService.GameServiceAPI
{
    public class GameServiceHub : Hub<IGameClient>
    {
        public Task SendMessage(GameMessage message)
        {
            return Clients.All.ReceiveMessage(message);
        }
    }
}

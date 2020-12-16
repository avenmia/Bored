using Bored.GameService.Clients;
using Bored.GameService.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Bored.GameService.GameServiceAPI
{
    public class GameServiceHub : Hub<IClient>
    {
        public Task SendMessage(IGameMessage message)
        {
            return Clients.All.ReceiveMessage(message);
        }
    }
}

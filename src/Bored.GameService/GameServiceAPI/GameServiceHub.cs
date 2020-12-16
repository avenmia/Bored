using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Bored.GameService.GameServiceAPI
{
    public class GameServiceHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

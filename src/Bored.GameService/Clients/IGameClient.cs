using Bored.Common;
using Bored.GameService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bored.GameService.Clients
{
    public interface IGameClient
    {
        Task ReceiveMessage(string state);
    }
}

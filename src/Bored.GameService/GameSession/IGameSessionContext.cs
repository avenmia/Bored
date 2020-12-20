using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bored.GameService.GameSession
{
    public interface IGameSessionContext
    {
        string GetGameState(string gameID);

        object AddGameState(string gameID, object state);
    }
}

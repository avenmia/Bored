using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bored.GameService.GameSession
{
    public interface IGameSession
    {
        void GetGameState();

        void AddGameState();

        void ReturnGameState();
    }
}

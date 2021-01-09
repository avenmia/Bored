using Bored.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bored.GameService.Factories
{
    public interface IFactory
    {
        IGameLogic GameFactory(string gameName, IGameState state);

        IGameState GameStateFactory(string gameName, string state);

        IGameMove GameMoveFactory(string gameName, string move);
    }
}

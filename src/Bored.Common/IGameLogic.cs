using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bored.Common
{
    public interface IGameLogic
    {
        public IGameState? MakeMove(IGameMove move);
    }
}

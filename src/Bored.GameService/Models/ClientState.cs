using Bored.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bored.GameService.Models
{
    public record ClientState
    {
        public object State { get; }
        public IGameMove Move { get; }
        public ClientState(object state, IGameMove move) => (State, Move) = (state, move);
    }
}

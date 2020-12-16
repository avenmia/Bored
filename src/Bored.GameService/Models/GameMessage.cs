using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bored.GameService.Models
{
    public class GameMessage : IGameMessage
    {
        public string User { get; set; }

        public string Message { get; set; }
    }
}

using Bored.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bored.GameService.Models
{
    public class GameMessage : IGameMessage
    {
        public string Game { get; set; }

        public string GameID { get; set; }

        public string Move { get; set; }
    }
}

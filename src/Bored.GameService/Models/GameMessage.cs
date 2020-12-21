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
        [JsonProperty]
        public string GameState { get; set; }
    }
}

using Bored.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bored.GameService.Models
{
    public interface IGameMessage
    {
        string Game { get; set; }

        string GameID { get; set; }

        string Move { get; set; }
    }
}

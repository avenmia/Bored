namespace Bored.GameService.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bored.Common;
    using Newtonsoft.Json;

    public interface IGameMessage
    {
        string Game { get; set; }

        string GameID { get; set; }

        string Move { get; set; }
    }
}

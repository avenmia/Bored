using Bored.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bored.Common
{
    public interface IGameState
    {
        string GameID { get; set; }

        IPlayer? Winner { get; set; }

        IBoard Board { get; set; }

        IPlayer Turn { get; set; }

        GameStatus Status { get; set; }
    }
}

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
        string GameType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bored.GameService.Models
{
    public interface IGameMessage
    {
        string User { get; set; }

        string Message { get; set; }
    }
}

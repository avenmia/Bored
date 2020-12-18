using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bored.Common.Models
{
    public interface IPlayer
    {
        public IPiece Piece { get; set; }
    }
}

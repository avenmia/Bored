using Bored.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bored.Game.TicTacToe
{
    public class TestState : IGameState
    {
        public TestState()
        {

        }

        public TestState(dynamic obj)
        {
            GameID = obj.GameID;
            TotalWins = obj.TotalWins;
            Winner = obj.Winner;
            Turn = obj.Turn;
        }

        public string GameID { get; set; }

        public string Turn { get; set; }

        public string Winner { get; set; }

        public int TotalWins { get; set; }
    }
}

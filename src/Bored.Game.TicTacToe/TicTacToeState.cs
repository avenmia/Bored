using Bored.Common;
using Bored.Common.Models;

namespace Bored.Game.TicTacToe
{
    public class TicTacToeState : IGameState
    {
        public TicTacToePlayer? Winner = null;
        public TicTacToePlayer?[,] Cells = new TicTacToePlayer?[3, 3];
        public TicTacToePlayer Turn = TicTacToePlayer.X;
        public GameStatus Status = GameStatus.IN_PROGRESS;
    }
}
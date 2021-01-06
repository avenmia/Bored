using Bored.Common;
using System.Text.Json.Serialization;

namespace Bored.Game.TicTacToe
{
    public record TicTacToeCell
    {
        public int Row { get; }
        public int Col { get; }
        public TicTacToeCell(int row, int col) => (Row, Col) = (row, col);
    }
    public class TicTacToeMove : IGameMove
    {
        public TicTacToePlayer Player;
        public TicTacToeCell Cell;

        public TicTacToeMove(TicTacToePlayer _player, TicTacToeCell _cell)
        {
            Player = _player;
            Cell = _cell;
        }
    }
}
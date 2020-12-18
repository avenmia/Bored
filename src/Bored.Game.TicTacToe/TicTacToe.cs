using Bored.Common;
using Bored.Common.Models;

namespace Bored.Game.TicTacToe
{
    public class TicTacToe : GameLogic<TicTacToeState, TicTacToeMove>
    {
        public TicTacToe(TicTacToeState GameState) : base(GameState)
        {
        }

        public override TicTacToeState? MakeMove(TicTacToeMove move)
        {
            if (move.Player == this.State.Turn && this.Move(move.Cell))
            {
                return this.State;
            }
            return null;
        }

        private bool IsValidMove((byte row, byte col) cell)
        {
            if (this.State.Status == GameStatus.FINISHED)
            {
                return false;
            }
            else if (cell.row < 0 || cell.row > 2 || cell.col < 0 || cell.col > 2)
            {
                return false;
            }
            else if (this.State.Cells[cell.row, cell.col] != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsWinningMove(TicTacToePlayer player, (byte row, byte col) cell)
        {
            return (
              (this.State.Cells[cell.row, 0]?.Value == player && // 3-in-the-row
                this.State.Cells[cell.row, 1]?.Value == player &&
                this.State.Cells[cell.row, 2]?.Value == player) ||
              (this.State.Cells[0, cell.col]?.Value == player && // 3-in-the-column
                this.State.Cells[1, cell.col]?.Value == player &&
                this.State.Cells[2, cell.col]?.Value == player) ||
              (cell.row == cell.col && // 3-in-the-diagonal
                this.State.Cells[0, 0]?.Value == player &&
                this.State.Cells[1, 1]?.Value == player &&
                this.State.Cells[2, 2]?.Value == player) ||
              (cell.row + cell.col == 2 && // 3-in-the-opposite-diagonal
                this.State.Cells[0, 2]?.Value == player &&
                this.State.Cells[1, 1]?.Value == player &&
                this.State.Cells[2, 0]?.Value == player)
            );
        }

        public bool Move((byte row, byte col) cell)
        {
            if (this.IsValidMove(cell))
            {
                this.State.Cells[cell.row, cell.col] = new TicTacToeCell(this.State.Turn);
                if (this.IsWinningMove(this.State.Turn, cell))
                {
                    this.State.Status = GameStatus.FINISHED;
                    this.State.Winner = this.State.Turn;
                }
                else
                {
                    this.State.Turn = this.State.Turn == TicTacToePlayer.X ? TicTacToePlayer.O : TicTacToePlayer.X;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
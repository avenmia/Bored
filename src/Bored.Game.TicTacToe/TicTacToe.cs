using Bored.Common;

namespace Bored.Game.TicTacToe
{
    public class TicTacToe : GameLogic<TicTacToeState, TicTacToeMove>
    {
        public TicTacToe(TicTacToeState GameState) : base(GameState) { }
        public override TicTacToeState GetNextState(TicTacToeState previousState, TicTacToeMove move)
        {
            if (move.Player == this.GameState.Turn && this.Move(move.Cell))
            {
                return this.GameState;
            }
            throw new System.Exception("Invalid move");
        }
        private bool IsValidMove((byte row, byte col) cell)
        {

            if (this.GameState.Status == GameStatus.FINISHED)
            {
                return false;
            }
            else if (cell.row < 0 || cell.row > 2 || cell.col < 0 || cell.col > 2)
            {
                return false;
            }
            else if (this.GameState.Cells[cell.row][cell.col] != null)
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
              (this.GameState.Cells[cell.row][0].Value == player && // 3-in-the-row
                this.GameState.Cells[cell.row][1].Value == player &&
                this.GameState.Cells[cell.row][2].Value == player) ||
              (this.GameState.Cells[0][cell.col].Value == player && // 3-in-the-column
                this.GameState.Cells[1][cell.col].Value == player &&
                this.GameState.Cells[2][cell.col].Value == player) ||
              (cell.row == cell.col && // 3-in-the-diagonal
                this.GameState.Cells[0][0].Value == player &&
                this.GameState.Cells[1][1].Value == player &&
                this.GameState.Cells[2][2].Value == player) ||
              (cell.row + cell.col == 2 && // 3-in-the-opposite-diagonal
                this.GameState.Cells[0][2].Value == player &&
                this.GameState.Cells[1][1].Value == player &&
                this.GameState.Cells[2][0].Value == player)
            );
        }

        public bool Move((byte row, byte col) cell)
        {
            if (this.IsValidMove(cell))
            {
                this.GameState.Cells[cell.row][cell.col].Value = this.GameState.Turn;
                if (this.IsWinningMove(this.GameState.Turn, cell))
                {
                    this.GameState.Status = GameStatus.FINISHED;
                    this.GameState.Winner = this.GameState.Turn;
                }
                else
                {
                    this.GameState.Turn = this.GameState.Turn == TicTacToePlayer.X ? TicTacToePlayer.O : TicTacToePlayer.X;
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

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
            if (IsValidMove(move.Player, move.Cell))
            {
                State.Cells[move.Cell.row, move.Cell.col] = State.Turn;
                if (IsWinningMoveByPlayer(State.Turn, move.Cell))
                {
                    State.Status = GameStatus.FINISHED;
                    State.Winner = State.Turn;
                }
                else
                {
                    FlipTurn();
                }
                return State;
            }
            return null;
        }

        private void FlipTurn() =>
            State.Turn = State.Turn == TicTacToePlayer.X ? TicTacToePlayer.O : TicTacToePlayer.X;
        private bool IsValidPlayer(TicTacToePlayer player) =>
            player == State.Turn;
        private bool IsGameFinished() =>
            State.Status == GameStatus.FINISHED;
        private bool IsOutOfBounds((byte row, byte col) cell) =>
            cell.row > 2 || cell.col > 2;
        private bool IsCellOccopied((byte row, byte col) cell) =>
            State.Cells[cell.row, cell.col] != null;
        private bool IsValidMove(TicTacToePlayer player, (byte row, byte col) cell) =>
            IsValidPlayer(player) && !IsGameFinished() && !IsOutOfBounds(cell) && !IsCellOccopied(cell);
        private bool IsThreeInTheRow(TicTacToePlayer player, (byte row, byte col) cell) =>
            State.Cells[cell.row, 0] == player &&
            State.Cells[cell.row, 1] == player &&
            State.Cells[cell.row, 2] == player;
        private bool IsThreeInTheColumn(TicTacToePlayer player, (byte row, byte col) cell) =>
            State.Cells[0, cell.col] == player &&
            State.Cells[1, cell.col] == player &&
            State.Cells[2, cell.col] == player;
        private bool IsThreeInTheDiagonal(TicTacToePlayer player, (byte row, byte col) cell) =>
            cell.row == cell.col &&
            State.Cells[0, 0] == player &&
            State.Cells[1, 1] == player &&
            State.Cells[2, 2] == player;
        private bool IsThreeInTheOtherDiagonal(TicTacToePlayer player, (byte row, byte col) cell) =>
            cell.row + cell.col == 2 &&
            State.Cells[0, 2] == player &&
            State.Cells[1, 1] == player &&
            State.Cells[2, 0] == player;
        private bool IsWinningMoveByPlayer(TicTacToePlayer player, (byte row, byte col) cell) =>
            IsThreeInTheRow(player, cell) ||
            IsThreeInTheColumn(player, cell) ||
            IsThreeInTheDiagonal(player, cell) ||
            IsThreeInTheOtherDiagonal(player, cell);
    }
}
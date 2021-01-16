namespace Bored.Game.TicTacToe
{
    using Bored.Common;
    using Bored.Common.Models;

    /// <summary>
    /// The TicTacToe class gives isntances of the TicTacToe game logic.
    /// </summary>
    public class TicTacToe : GameLogic<TicTacToeState, TicTacToeMove>, IGameLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicTacToe"/> class.
        /// </summary>
        /// <param name="gameState">The state of the game. </param>
        public TicTacToe(TicTacToeState gameState)
            : base(gameState)
        {
        }

        /// <summary>
        /// Handles a TicTacToe move.
        /// </summary>
        /// <param name="gameMove">The game move to be played. </param>
        /// <returns>The updated game state. </returns>
        public override IGameState? MakeMove(IGameMove gameMove)
        {
            TicTacToeMove? move = gameMove as TicTacToeMove;

            if (move != null && IsValidMove(move.Player, move.Cell))
            {
                State.Cells[move.Cell.Row, move.Cell.Col] = State.Turn;
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

        private bool IsOutOfBounds(TicTacToeCell cell) =>
            cell.Row > 2 || cell.Col > 2;

        private bool IsCellOccopied(TicTacToeCell cell) =>
            State.Cells[cell.Row, cell.Col] != null;

        private bool IsValidMove(TicTacToePlayer player, TicTacToeCell cell) =>
            IsValidPlayer(player) && !IsGameFinished() && !IsOutOfBounds(cell) && !IsCellOccopied(cell);

        private bool IsThreeInTheRow(TicTacToePlayer player, TicTacToeCell cell) =>
            State.Cells[cell.Row, 0] == player &&
            State.Cells[cell.Row, 1] == player &&
            State.Cells[cell.Row, 2] == player;

        private bool IsThreeInTheColumn(TicTacToePlayer player, TicTacToeCell cell) =>
            State.Cells[0, cell.Col] == player &&
            State.Cells[1, cell.Col] == player &&
            State.Cells[2, cell.Col] == player;

        private bool IsThreeInTheDiagonal(TicTacToePlayer player, TicTacToeCell cell) =>
            cell.Row == cell.Col &&
            State.Cells[0, 0] == player &&
            State.Cells[1, 1] == player &&
            State.Cells[2, 2] == player;

        private bool IsThreeInTheOtherDiagonal(TicTacToePlayer player, TicTacToeCell cell) =>
            cell.Row + cell.Col == 2 &&
            State.Cells[0, 2] == player &&
            State.Cells[1, 1] == player &&
            State.Cells[2, 0] == player;

        private bool IsWinningMoveByPlayer(TicTacToePlayer player, TicTacToeCell cell) =>
            IsThreeInTheRow(player, cell) ||
            IsThreeInTheColumn(player, cell) ||
            IsThreeInTheDiagonal(player, cell) ||
            IsThreeInTheOtherDiagonal(player, cell);
    }
}
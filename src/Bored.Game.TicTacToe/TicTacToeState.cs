namespace Bored.Game.TicTacToe
{
    using Bored.Common;
    using Bored.Common.Models;

    /// <summary>
    /// The TicTacToe state.
    /// </summary>
    public class TicTacToeState : IGameState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicTacToeState"/> class.
        /// </summary>
        public TicTacToeState()
        {
            Winner = null;
            Cells = new TicTacToePlayer?[3, 3];
            Turn = TicTacToePlayer.X;
            Status = GameStatus.IN_PROGRESS;
        }

        /// <summary>
        /// Gets or sets the winner if there is a winning player, else null.
        /// </summary>
        public TicTacToePlayer? Winner { get; set; }

        /// <summary>
        /// Gets or sets the current state of the TicTacToe cells.
        /// </summary>
        public TicTacToePlayer?[,] Cells { get; set; }

        /// <summary>
        /// Gets or sets the which player's turn it is as part of this state.
        /// </summary>
        public TicTacToePlayer Turn { get; set; }

        /// <summary>
        /// Gets or sets the game's status.
        /// </summary>
        public GameStatus Status { get; set; }
    }
}
namespace Bored.Game.TicTacToe
{
    using Bored.Common;

    public record TicTacToeCell
    {
        /// <summary>
        /// Gets the TicTacToe row.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Gets the TicTacToe column.
        /// </summary>
        public int Col { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TicTacToeCell"/> class.
        /// </summary>
        /// <param name="row">The TicTacToe row.</param>
        /// <param name="col">The TicTacToe column.</param>
        public TicTacToeCell(int row, int col) => (Row, Col) = (row, col);
    }

    /// <summary>
    /// The TicTacToeMove class.
    /// </summary>
    public class TicTacToeMove : IGameMove
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicTacToeMove"/> class.
        /// </summary>
        /// <param name="player">The TicTacToe player.</param>
        /// <param name="cell">The TicTacToe cell being marked.</param>
        public TicTacToeMove(TicTacToePlayer player, TicTacToeCell cell)
        {
            Player = player;
            Cell = cell;
        }

        /// <summary>
        /// Gets or sets the TicTacToe player making the move.
        /// </summary>
        public TicTacToePlayer Player { get; set; }

        /// <summary>
        /// Gets or sets the TicTacToe cell that is being marked.
        /// </summary>
        public TicTacToeCell Cell { get; set; }
    }
}
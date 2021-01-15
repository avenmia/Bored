namespace Bored.Game.TicTacToe
{
    /// <summary>
    /// The TicTacToe game cell.
    /// </summary>
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
}

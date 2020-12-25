namespace Bored.Game.TicTacToe
{
    public record TicTacToeMove
    {
        public TicTacToePlayer Player { get; }
        public (byte row, byte col) Cell { get; }
        public TicTacToeMove(TicTacToePlayer player, (byte row, byte col) cell) => (Player, Cell) = (player, cell);
    }
}
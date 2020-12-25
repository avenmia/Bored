namespace Bored.Game.TicTacToe
{
    public class TicTacToeMove
    {
        public TicTacToePlayer Player;
        public (byte row, byte col) Cell;

        public TicTacToeMove(TicTacToePlayer player, (byte row, byte col) cell) => (Player, Cell) = (player, cell);
    }
}
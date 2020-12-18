namespace Bored.Game.TicTacToe
{
    public class TicTacToeCell
    {
        public TicTacToePlayer Value { get; set; }

        public TicTacToeCell(TicTacToePlayer player)
        {
            this.Value = player;
        }
    }
}
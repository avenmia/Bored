namespace Bored.Game.TicTacToe
{
    public class TicTacToeState
    {
        public TicTacToePlayer? Winner = null;
        public TicTacToeCell[,] Cells = new TicTacToeCell[3, 3];
        public TicTacToePlayer Turn = TicTacToePlayer.X;
        public GameStatus Status = GameStatus.IN_PROGRESS;

        public TicTacToeState()
        {
            this.Cells.Initialize();
        }
    }
}
namespace Bored.Game.TicTacToe
{
    public class TicTacToeState
    {
        public TicTacToePlayer? Winner = null;
        public TicTacToePlayer?[,] Cells = new TicTacToePlayer?[3, 3];
        public TicTacToePlayer Turn = TicTacToePlayer.X;
        public GameStatus Status = GameStatus.IN_PROGRESS;
    }
}
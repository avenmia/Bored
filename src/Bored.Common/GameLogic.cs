namespace Bored.Common
{
    public abstract class GameLogic<GameState, GameMove>
    {
        public GameState State { get; protected set; }

        public GameLogic(GameState _State)
        {
            this.State = _State;
        }

        public abstract GameState? MakeMove(GameMove Move);
    }
}
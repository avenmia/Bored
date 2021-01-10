namespace Bored.Common
{
    public abstract class GameLogic<GameState, GameMove> where GameState : IGameState
    {
        public GameState State { get; protected set; }

        public GameLogic(GameState _State)
        {
            State = _State;
        }

        public abstract IGameState? MakeMove(IGameMove Move);
    }
}
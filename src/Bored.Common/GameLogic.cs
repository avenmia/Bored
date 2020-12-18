namespace Bored.Common
{
    public abstract class GameLogic<State, Move>
    {
        public State GameState { get; private set; }

        public GameLogic(State GameState)
        {
            this.GameState = GameState;
        }

        public abstract State GetNextState(State previousState, Move move);
    }
}

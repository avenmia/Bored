namespace Bored.Common
{
    /// <summary>
    /// Base class for game logic.
    /// </summary>
    /// <typeparam name="TGameState">The type of game state.</typeparam>
    /// <typeparam name="TGameMove">The type of game move.</typeparam>
    public abstract class GameLogic<TGameState, TGameMove>
        where TGameState : IGameState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic{TGameState, TGameMove}"/> class.
        /// </summary>
        /// <param name="state">The game state.</param>
        public GameLogic(TGameState state)
        {
            State = state;
        }

        /// <summary>
        /// Gets or sets the game state.
        /// </summary>
        public TGameState State { get; protected set; }

        /// <summary>
        /// Handles a TicTacToe move.
        /// </summary>
        /// <param name="gameMove">The game move to be played. </param>
        /// <returns>The updated game state. </returns>
        public abstract IGameState? MakeMove(IGameMove gameMove);
    }
}
namespace Bored.GameService.Models
{
    using Bored.Common;

    public record ClientState
    {
        /// <summary>
        /// Gets the client state.
        /// </summary>
        public object State { get; }

        /// <summary>
        /// Gets the game move.
        /// </summary>
        public IGameMove Move { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientState"/> class.
        /// </summary>
        /// <param name="state">The game state.</param>
        /// <param name="move">The game move.</param>
        public ClientState(object state, IGameMove move) => (State, Move) = (state, move);
    }
}

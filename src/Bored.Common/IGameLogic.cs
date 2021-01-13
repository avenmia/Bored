namespace Bored.Common
{
    /// <summary>
    /// Interface for the game logic.
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// The MakeMove method will make a move for a certain game.
        /// </summary>
        /// <param name="move">The move to make.</param>
        /// <returns>The updated game state.</returns>
        public IGameState? MakeMove(IGameMove move);
    }
}

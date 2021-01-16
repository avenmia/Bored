namespace Bored.GameService.Factories
{
    using Bored.Common;

    /// <summary>
    /// Interface for the Factory class.
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// GameFactory returns the instance of the game being played.
        /// </summary>
        /// <param name="gameName">The type of game.</param>
        /// <param name="state">The game state.</param>
        /// <returns>The game logic of that game type.</returns>
        IGameLogic GameFactory(string gameName, IGameState state);

        /// <summary>
        /// GameStateFactory returns the instance of the GameState for the game being played.
        /// </summary>
        /// <param name="gameName">The name of the game being played.</param>
        /// <param name="state">A serialized version of the game state.</param>
        /// <returns>The game state for that game.</returns>
        IGameState GameStateFactory(string gameName, string state);

        /// <summary>
        /// GameMoveFactory returns an instance of that game's move type.
        /// </summary>
        /// <param name="gameName">The game being played.</param>
        /// <param name="move">A serialized version of the game's move.</param>
        /// <returns>The game move.</returns>
        IGameMove GameMoveFactory(string gameName, string move);
    }
}

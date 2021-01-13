namespace Bored.GameService.Factories
{
    using System;
    using Bored.Common;
    using Bored.Game.TicTacToe;
    using Newtonsoft.Json;

    /// <summary>
    /// Factory class that gives instances of Game, GameMove, and GameState objects
    /// </summary>
    public class Factory : IFactory
    {
        /// <summary>
        /// GameFactory returns the instance of the game being played.
        /// </summary>
        /// <param name="gameName">The type of game.</param>
        /// <param name="state">The game state.</param>
        /// <returns>The game logic of that game type.</returns>
        public IGameLogic GameFactory(string gameName, IGameState state)
        {
            return gameName switch
            {
                "TicTacToe" => new TicTacToe(state as TicTacToeState),
                _ => throw new Exception("Invalid game."),
            };
        }

        /// <summary>
        /// GameStateFactory returns the instance of the GameState for the game being played.
        /// </summary>
        /// <param name="gameName">The name of the game being played.</param>
        /// <param name="state">A serialized version of the game state.</param>
        /// <returns>The game state for that game.</returns>
        public IGameState GameStateFactory(string gameName, string state)
        {
            return gameName switch
            {
                "TicTacToe" => state == null ? new TicTacToeState() : JsonConvert.DeserializeObject<TicTacToeState>(state),
                _ => throw new Exception("Invalid game state."),
            };
        }

        /// <summary>
        /// GameMoveFactory returns an instance of that game's move type.
        /// </summary>
        /// <param name="gameName">The game being played.</param>
        /// <param name="move">A serialized version of the game's move.</param>
        /// <returns>The game move.</returns>
        public IGameMove GameMoveFactory(string gameName, string move)
        {
            return gameName switch
            {
                "TicTacToe" => JsonConvert.DeserializeObject<TicTacToeMove>(move),
                _ => throw new Exception("Invalid game move."),
            };
        }
    }
}

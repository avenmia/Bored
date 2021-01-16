namespace Bored.GameService.Models
{
    /// <summary>
    /// The interface that defines how the game data moves in the application.
    /// </summary>
    public interface IGameMessage
    {
        /// <summary>
        /// Gets or sets the serialized version of the game.
        /// </summary>
        string Game { get; set; }

        /// <summary>
        /// Gets or sets the game ID.
        /// </summary>
        string GameID { get; set; }

        /// <summary>
        /// Gets or sets the serialized version of the game move.
        /// </summary>
        string Move { get; set; }
    }
}

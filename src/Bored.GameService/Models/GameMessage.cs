namespace Bored.GameService.Models
{
    /// <summary>
    /// The GameMessage represents which
    /// properties the client will send
    /// to the server.
    /// </summary>
    public class GameMessage : IGameMessage
    {
        /// <summary>
        /// Gets or sets the name of the game being played.
        /// </summary>
        public string Game { get; set; }

        /// <summary>
        /// Gets or sets the game ID.
        /// </summary>
        public string GameID { get; set; }

        /// <summary>
        /// Gets or sets the move.
        /// </summary>
        public string Move { get; set; }
    }
}

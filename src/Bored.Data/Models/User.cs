namespace Bored.Data.Models
{
    /// <summary>
    /// The User model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets he user's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's games won.
        /// </summary>
        public int GamesWon { get; set; }

        /// <summary>
        /// Gets or sets the user's games lost.
        /// </summary>
        public int GamesLost { get; set; }
    }
}

namespace Bored.Data.Data
{
    using Bored.Data.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The db context.
    /// </summary>
    public class GameDbContext : DbContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDbContext"/> class.
        /// </summary>
        /// <param name="options">The DbContext options.</param>
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Users DBset.
        /// </summary>
        public DbSet<User> Users { get; set; }

    }
}

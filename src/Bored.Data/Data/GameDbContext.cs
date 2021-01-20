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
        /// Gets or sets the Users DBset.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}

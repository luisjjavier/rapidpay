using Core.AppUsers;
using Core.Cards;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence
{
    /// <summary>
    /// Represents the main database context for the RapidPay application.
    /// </summary>
    public class RapidPayDbContext : IdentityDbContext<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RapidPayDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public RapidPayDbContext(DbContextOptions<RapidPayDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for managing Card entities.
        /// </summary>
        public DbSet<Card> Cards { get; set; } = null!;

        /// <summary>
        /// Overrides the default behavior of model creation, applying additional configurations.
        /// </summary>
        /// <param name="builder">The builder used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply entity configurations from the assembly containing RapidPayDbContext.
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(RapidPayDbContext))!);
        }
    }

}

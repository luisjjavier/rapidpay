using Core.Cards;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistence
{
    public class RapidPayDbContext : IdentityDbContext<IdentityUser>
    {
        public RapidPayDbContext(DbContextOptions<RapidPayDbContext> options) : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(RapidPayDbContext))!);

        }
    }
}

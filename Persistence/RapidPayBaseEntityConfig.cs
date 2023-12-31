using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{    /// <summary>
    /// Entity config base class
    /// </summary>
    /// <typeparam name="TEntity">The entity to configure</typeparam>
    public abstract class RapidPayBaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        private readonly string? _tableName;

        /// <summary>
        /// Instantiate the Entity Config
        /// </summary>
        protected RapidPayBaseEntityConfig()
        {

        }

        /// <summary>
        /// Instantiate the Entity Config
        /// </summary>
        /// <param name="tableName">The table name of the entity in database</param>
        protected RapidPayBaseEntityConfig(string tableName)
        {
            _tableName = tableName;
        }

        /// <summary>
        /// Configure the property in base entity
        /// </summary>
        /// <param name="builder">The entity type builder instance</param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (!string.IsNullOrEmpty(_tableName))
                builder.Metadata.SetTableName(_tableName);

            builder.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(e => e.Created).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            builder.Property(e => e.Updated).HasConversion(v => v, v => v != null ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null);
        }
    }
}

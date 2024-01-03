using Core;
using Core.AppUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence
{
    /// <summary>
    /// Generic repository implementation for managing entities in the RapidPay application.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
    /// <remarks>
    /// This repository provides common CRUD operations and handles concurrency during updates.
    /// </remarks>
    public class RapidPayRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// The DbContext associated with the repository.
        /// </summary>
        protected readonly DbContext Db;

        private readonly ILogger<RapidPayRepository<TEntity>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RapidPayRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="db">The DbContext used for database interactions.</param>
        /// <param name="logger">The logger for capturing repository-related logs.</param>
        public RapidPayRepository(RapidPayDbContext db, ILogger<RapidPayRepository<TEntity>> logger)
        {
            Db = db;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<TEntity> CreateAsync(TEntity entity, AppUser appUser,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Set creation-related properties.
            entity.Created = DateTime.Now;
            entity.CreatedBy = appUser.UserName!;

            // Add the entity to the DbSet and save changes.
            Db.Set<TEntity>().Add(entity);
            await Db.SaveChangesAsync(cancellationToken);

            return entity;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(TEntity entity, AppUser appUser, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var retryCount = 0;
            bool success = false;

            do
            {
                try
                {
                    // Set update-related properties.
                    entity.Updated = DateTime.Now;
                    entity.UpdatedBy = appUser.UserName!;
                    entity.ConcurrencyStamp = Guid.NewGuid().ToString();

                    // Mark the entity as modified and save changes.
                    Db.Entry(entity).State = EntityState.Modified;
                    await Db.SaveChangesAsync(cancellationToken);

                    success = true;
                }
                catch (Exception ex)
                {
                    if (ex is DbUpdateConcurrencyException)
                    {
                        _logger.LogWarning(ex,
                            $"Concurrency error occurred when trying to update {nameof(entity)} record {entity.Id}");
                        break;
                    }

                    _logger.LogError(ex, $"Error occurred when trying to update {nameof(entity)} record {entity.Id}");

                    retryCount++;
                }

            } while (!success && retryCount < 3);
        }

        /// <inheritdoc />
        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Retrieve and return the entity with the specified id.
            return await Db.Set<TEntity>().AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }
    }

}

using Core;
using Core.AppUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence
{
    public class RapidPayRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// The Db Context of the repository
        /// </summary>
        protected readonly DbContext Db;
        private readonly ILogger<RapidPayRepository<TEntity>> _logger;

        public RapidPayRepository(RapidPayDbContext db, ILogger<RapidPayRepository<TEntity>> logger)
        {
            Db = db;
            _logger = logger;
        }

        public async Task<TEntity> CreateAsync(TEntity entity, AppUser appUser, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            entity.Created = DateTime.Now;
            entity.CreatedBy = appUser.UserName!;
            Db.Set<TEntity>().Add(entity);
            await Db.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task UpdateAsync(TEntity entity, AppUser appUser, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var retryCount = 0;
            bool success = false;
            do
            {
                try
                {
                    entity.Updated = DateTime.Now;
                    entity.UpdatedBy = appUser.UserName!;
                    entity.ConcurrencyStamp = Guid.NewGuid().ToString();

                    Db.Entry(entity).State = EntityState.Modified;


                    await Db.SaveChangesAsync(cancellationToken);
                    success = true;
                }
                catch (Exception ex)
                {
                    if (ex is DbUpdateConcurrencyException)
                    {
                        _logger.LogWarning(ex, $"Concurrency error occurred when trying to update {nameof(entity)} record {entity.Id}");
                        break;
                    }
                    else
                    {
                        _logger.LogError(ex, $"Error occurred when trying to update {nameof(entity)} record {entity.Id}");
                    }

                    retryCount++;
                }

            } while (!success && retryCount < 3);

        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Db.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }

    }
}

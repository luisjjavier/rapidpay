using Core;
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

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            entity.Created = DateTime.Now;

            Db.Set<TEntity>().Add(entity);
            await Db.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}

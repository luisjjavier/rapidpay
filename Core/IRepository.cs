using Core.AppUsers;

namespace Core
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity, AppUser appUser, CancellationToken  cancellationToken  = default);
        Task UpdateAsync(T entity, AppUser appUser, CancellationToken  cancellationToken  = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken  cancellationToken  = default);
    }
}

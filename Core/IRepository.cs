namespace Core
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity, CancellationToken  cancellationToken  = default);
    }
}

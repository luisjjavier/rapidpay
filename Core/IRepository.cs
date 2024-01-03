using Core.AppUsers;

namespace Core
{
    /// <summary>
    /// Represents a generic repository interface for CRUD operations on entities inheriting from <see cref="BaseEntity"/>.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Asynchronously creates a new entity.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <param name="appUser">The user initiating the creation.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The created entity.</returns>
        Task<T> CreateAsync(T entity, AppUser appUser, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <param name="appUser">The user initiating the update.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        Task UpdateAsync(T entity, AppUser appUser, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The retrieved entity or <c>null</c> if not found.</returns>
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }

}

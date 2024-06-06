using WebAPI.Model;

namespace WebAPI.DataAccess;

/// <summary>
/// Represents a generic repository interface for CRUD operations on entities.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
public interface IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Asynchronously retrieves an entity with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains the entity if found; otherwise, <c>null</c>.
    /// </returns>

    /// <summary>
    /// Returns an IQueryable of type TEntity representing all rows of an IEntity table.
    /// </summary>
    IQueryable<T> GetAll();

    Task<T?> GetAsync(Guid id);

    /// <summary>
    /// Adds a new entity to the data source.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    Task AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the data source.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Deletes an existing entity from the data source.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    Task DeleteAsync(T entity);
}
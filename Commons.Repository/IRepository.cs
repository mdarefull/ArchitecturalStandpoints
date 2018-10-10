using System.Collections.Generic;
using System.Threading.Tasks;

using Commons.OperationResult;

namespace Commons.Repository
{
    /// <summary>
    /// Defines the contract for the Generic Repository pattern.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity that this repository will manage.</typeparam>
    /// <typeparam name="TId">Type of the <typeparamref name="TEntity"/>'s id.</typeparam>
    public interface IRepository<TEntity, TId>
    {
        /// <summary>
        /// Retrieves the entity identified by the given <paramref name="id"/> from the store asynchronously.
        /// </summary>
        /// <param name="id">Id of the entity to retrieve.</param>
        /// <returns>
        /// A <see cref="Task"/> that, when finished, will contain a representation of the
        /// result of executing the operation, with the expected entity if success.
        /// </returns>
        Task<IResult<TEntity>> GetByIdAsync(TId id);
        /// <summary>
        /// Retrieves all the entities from the data store asynchronously.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that, when finished, will contain a representation of the
        /// result of executing the operation, with the list of entities if success.
        /// </returns>
        Task<IResult<IEnumerable<TEntity>>> GetAllAsync();
        /// <summary>
        /// Adds the given entity to the data store asynchronously.
        /// </summary>
        /// <param name="entity"><typeparamref name="TEntity"/>'s instance to be added.</param>
        /// <returns>
        /// A <see cref="Task"/> that, when finished, will contain a representation of the
        /// result of executing the operation with the added entity if success.
        /// </returns>
        Task<IResult<TEntity>> AddAsync(TEntity entity);
        /// <summary>
        /// Updates (put) the given entity to the data store asynchronously.
        /// </summary>
        /// <param name="entity"><typeparamref name="TEntity"/>'s instance to be updated (put).</param>
        /// <returns>
        /// A <see cref="Task"/> that, when finished, will contain a representation of the
        /// result of executing the operation with the updated entity if success.
        /// </returns>
        Task<IResult<TEntity>> UpdateAsync(TEntity entity);
        /// <summary>
        /// Removes the entity identified by the given <paramref name="id"/> from the data store asynchronously.
        /// </summary>
        /// <param name="id">Id of the entity to remove.</param>
        /// <returns>
        /// A <see cref="Task"/> that, when finished, will contain a representation of the 
        /// result of executing the operation with the removed entity if success.
        /// </returns>
        Task<IResult<TEntity>> RemoveByIdAsync(TId id);
        /// <summary>
        /// Removes the given entity from the data store asynchronously.
        /// </summary>
        /// <param name="entity"><typeparamref name="TEntity"/>'s instance to be removed.</param>
        /// <returns>
        /// A <see cref="Task"/> that, when finished, will contain a representation of the 
        /// result of executing the operation with the removed entity if success.
        /// </returns>
        Task<IResult<TEntity>> RemoveAsync(TEntity entity);
    }

    /// <summary>
    /// Defines the contract for the Generic Repository Pattern.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity that this repository will manage.</typeparam>
    /// <remarks>
    /// It is a typed extension of <see cref="IRepository{TEntity, TId}"/> with <code>long</code>
    /// as its id type.
    /// </remarks>
    public interface IRepository<TEntity> : IRepository<TEntity, long> { }
}

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
        Task<IResult<TEntity>> GetByIdAsync(TId id);
        Task<IResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<IResult<TEntity>> AddAsync(TEntity entity);
        Task<IResult<TEntity>> UpdateAsync(TEntity entity);
        Task<IResult<TEntity>> RemoveByIdAsync(TId id);
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

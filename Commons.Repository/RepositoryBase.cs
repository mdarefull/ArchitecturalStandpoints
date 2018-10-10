using System.Collections.Generic;
using System.Threading.Tasks;

using Commons.OperationResult;

namespace Commons.Repository
{
    /// <summary>
    /// Basic implementation of an <see cref="IRepository{TEntity, TId}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity that this repository will manage.</typeparam>
    /// <typeparam name="TId">Type of the <typeparamref name="TEntity"/>'s id.</typeparam>
    public abstract class RepositoryBase<TEntity, TId> : IRepository<TEntity, TId>
    {
        /// <summary>
        /// Gets the service that implements the Unit of Work pattern
        /// that will be used to perform the operations on this repository.
        /// </summary>
        protected virtual IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// Creates a new instance of <see cref="RepositoryBase{TEntity, TId}"/>.
        /// </summary>
        /// <param name="unitOfWork">
        /// Service that implements the Unit of Work pattern that will be used to perform the operations on this repository.
        /// </param>
        public RepositoryBase(IUnitOfWork unitOfWork) => UnitOfWork = unitOfWork;

        /// <inheritdoc />
        public abstract Task<IResult<TEntity>> GetByIdAsync(TId id);
        /// <inheritdoc />
        public abstract Task<IResult<IEnumerable<TEntity>>> GetAllAsync();
        /// <inheritdoc />
        public abstract Task<IResult<TEntity>> AddAsync(TEntity entity);
        /// <inheritdoc />
        public abstract Task<IResult<TEntity>> UpdateAsync(TEntity entity);
        /// <inheritdoc />
        public abstract Task<IResult<TEntity>> RemoveByIdAsync(TId id);
        /// <inheritdoc />
        public abstract Task<IResult<TEntity>> RemoveAsync(TEntity entity);
    }

    /// <summary>
    /// Basic implementation of an <see cref="IRepository{TEntity, TId}"/> with id of type <code>long</code>.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity that this repository will manage.</typeparam>
    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, long>
    {
        /// <inheritdoc />
        public RepositoryBase(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}

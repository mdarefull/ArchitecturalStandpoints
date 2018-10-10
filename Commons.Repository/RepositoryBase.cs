using Commons.OperationResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commons.Repository
{
    public abstract class RepositoryBase<TEntity, TId> : IRepository<TEntity, TId>
    {
        protected virtual IUnitOfWork UnitOfWork { get; set; }
        public RepositoryBase(IUnitOfWork unitOfWork) => UnitOfWork = unitOfWork;

        public abstract Task<IResult<TEntity>> GetByIdAsync(TId id);
        public abstract Task<IResult<IEnumerable<TEntity>>> GetAllAsync();
        public abstract Task<IResult<TEntity>> AddAsync(TEntity entity);
        public abstract Task<IResult<TEntity>> UpdateAsync(TEntity entity);
        public abstract Task<IResult<TEntity>> RemoveByIdAsync(TId id);
        public abstract Task<IResult<TEntity>> RemoveAsync(TEntity entity);
    }

    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, long>
    {
        public RepositoryBase(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}

using Commons.OperationResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commons.Repository
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        protected virtual IUnitOfWork UnitOfWork { get; set; }
        public Repository(IUnitOfWork unitOfWork) => UnitOfWork = unitOfWork;

        public abstract Task<IResult<TEntity>> GetByIdAsync(TId id);
        public abstract Task<IResult<IEnumerable<TEntity>>> GetAllAsync();
        public abstract Task<IResult<TEntity>> AddAsync(TEntity entity);
        public abstract Task<IResult<TEntity>> UpdateAsync(TEntity entity);
        public abstract Task<IResult<TEntity>> RemoveByIdAsync(TId id);
        public abstract Task<IResult<TEntity>> RemoveAsync(TEntity entity);
    }

    public abstract class Repository<TEntity> : Repository<TEntity, long> where TEntity : IEntity
    {
        public Repository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}

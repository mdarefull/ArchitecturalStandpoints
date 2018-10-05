using Commons.OperationResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commons.Repository
{
    public interface IRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        Task<IResult<TEntity>> GetByIdAsync(TId id);
        Task<IResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<IResult<TEntity>> AddAsync(TEntity entity);
        Task<IResult<TEntity>> UpdateAsync(TEntity entity);
        Task<IResult<TEntity>> RemoveByIdAsync(TId id);
        Task<IResult<TEntity>> RemoveAsync(TEntity entity);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : IEntity { }
}

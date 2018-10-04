using System.Linq;

namespace Commons.Repository
{
    public interface IRepository<TEntity, TId> where TEntity : IEntity<TId>
                                               where TId : struct
    {
        TEntity GetById(TId id);
        IQueryable<TEntity> GetAll();
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity RemoveById(TId id);
        TEntity Remove(TEntity entity);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : IEntity { }
}

using System.Linq;

namespace Commons.Repository
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity<TId>
                                                                               where TId : struct
    {
        protected virtual IUnitOfWork UnitOfWork { get; set; }
        public Repository(IUnitOfWork unitOfWork) => UnitOfWork = unitOfWork;

        public abstract TEntity Add(TEntity entity);
        public abstract IQueryable<TEntity> GetAll();
        public abstract TEntity GetById(TId id);
        public abstract TEntity Remove(TEntity entity);
        public abstract TEntity RemoveById(TId id);
        public abstract TEntity Update(TEntity entity);
    }

    public abstract class Repository<TEntity> : Repository<TEntity, long> where TEntity : IEntity
    {
        public Repository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}

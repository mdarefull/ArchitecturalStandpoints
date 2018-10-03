using System.Linq;

namespace Commons.Repository
{
    public interface IRepository<TModel>
    {
        TModel GetById(int id);

        IQueryable<TModel> GetAll();

        TModel Insert(TModel model);

        TModel Update(TModel model);
    }
}

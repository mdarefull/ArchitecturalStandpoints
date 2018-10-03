using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Repository
{
    public class EntityFrameworkRepository<TModel> : IRepository<TModel>
    {
        public EntityFrameworkRepository(DbContext context)
        {

        }

        public IQueryable<TModel> GetAll() => throw new NotImplementedException();
        public TModel GetById(int id) => throw new NotImplementedException();
        public TModel Insert(TModel model) => throw new NotImplementedException();
        public TModel Update(TModel model) => throw new NotImplementedException();
    }
}

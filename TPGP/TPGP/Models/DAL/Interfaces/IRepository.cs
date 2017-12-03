using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace TPGP.Models.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetBy(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
                             IOrderedQueryable<T>> orderBy = null,
                             string includeProperties = "");

        T GetById(long id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(long id);
    }
}
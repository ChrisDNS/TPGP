using PagedList;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace TPGP.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetBy(Expression<Func<T, bool>> filter);

        T GetById(long id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(long id);

        void SaveChanges();
    }
}
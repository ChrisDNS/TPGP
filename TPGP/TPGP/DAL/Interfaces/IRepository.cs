using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TPGP.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetBy(Expression<Func<T, bool>> filter);

        T GetById(long id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(long id);

        void SaveChanges();

        IEnumerable<T> Pagination(int page, int itemsPerPage, out int totalCount);
    }
}
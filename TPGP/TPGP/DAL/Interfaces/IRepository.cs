using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace TPGP.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> GetAll();

        IEnumerable<T> GetByFilter(Expression<Func<T, bool>> filter);

        T GetById(long id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(long id);

        void SaveChanges();

        IEnumerable<T> Pagination<TKey>(Expression<Func<T, TKey>> sort, int noPage, int itemsPerPage, out int total);

        IEnumerable<T> Pagination<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sort, int noPage, int itemsPerPage, out int total);
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using TPGP.Context;
using TPGP.DAL.Interfaces;

namespace TPGP.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected TPGPContext dbContext;
        private DbSet<T> dbSet;

        public Repository(TPGPContext ctx)
        {
            dbContext = ctx;
            dbSet = dbContext.Set<T>();
        }

        public DbSet<T> GetAll() => dbSet;

        public IEnumerable<T> GetByFilter(Expression<Func<T, bool>> filter) => dbSet.Where(filter);

        public T GetById(long id) => dbSet.Find(id);

        public void Insert(T obj) => dbSet.Add(obj);

        public void Update(T entityToUpdate) => dbContext.Entry(entityToUpdate).State = EntityState.Modified;

        public void Delete(long id) => dbSet.Remove(GetById(id));

        public void SaveChanges() => dbContext.SaveChanges();

        public IEnumerable<T> Pagination<TKey>(Expression<Func<T, TKey>> sort, int noPage, int itemsPerPage, out int total)
        {
            total = dbContext.Set<T>().Count();

            return dbContext.Set<T>().OrderBy(sort).Skip(itemsPerPage * noPage).Take(itemsPerPage);
        }

        public IEnumerable<T> Pagination<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sort, int noPage, int itemsPerPage, out int total)
        {
            var pagedList = dbContext.Set<T>().Where(filter).OrderBy(sort).Skip(itemsPerPage * noPage).Take(itemsPerPage);
            total = dbContext.Set<T>().Where(filter).Count();

            return pagedList;
        }
    }
}
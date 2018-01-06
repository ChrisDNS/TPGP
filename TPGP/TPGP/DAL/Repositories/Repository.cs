using System;
using System.Data.Entity;
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

        public IQueryable<T> GetAll() => dbSet.AsQueryable();

        public IQueryable<T> GetBy(Expression<Func<T, bool>> filter) => dbSet.Where(filter);

        public T GetById(long id) => dbSet.Find(id);

        public void Insert(T obj) => dbSet.Add(obj);

        public void Update(T entityToUpdate) => dbContext.Entry(entityToUpdate).State = EntityState.Modified;

        public void Delete(long id) => dbSet.Remove(GetById(id));

        public void SaveChanges() => dbContext.SaveChanges();
    }
}
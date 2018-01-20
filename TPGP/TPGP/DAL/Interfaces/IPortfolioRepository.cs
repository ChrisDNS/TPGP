using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Interfaces
{
    public interface IPortfolioRepository : IRepository<Portfolio>
    {
        IEnumerable<Portfolio> GetPortfoliosByUserScope<TKey>(long userId, Expression<Func<Portfolio, TKey>> sort, int noPage, int itemsPerPage, out int total);
    }
}
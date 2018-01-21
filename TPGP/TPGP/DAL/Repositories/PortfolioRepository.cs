using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Repositories
{
    public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(TPGPContext ctx) : base(ctx)
        {
        }

        public IEnumerable<Portfolio> GetPortfoliosByUserScope<TKey>(long userId, Expression<Func<Portfolio, TKey>> sort, int noPage, int itemsPerPage, out int total)
        {
            var query = dbContext.Portfolios.Join(dbContext.Scopes,
                                                   p => p.Id,
                                                   s => s.PortfolioId,
                                                  (p, s) => new { user = s.UserId, portfolio = p })
                                            .Where(ps => ps.user == userId);

            total = query.Count();

            var res = new List<Portfolio>();
            foreach (var item in query)
            {
                res.Add(item.portfolio);
            }

            //orderBy
            return res.Skip(itemsPerPage * noPage).Take(itemsPerPage);
        }
    }
}
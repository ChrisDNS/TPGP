using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Portfolio> GetPortfoliosByUserScope(long userId)
        {
            var query = dbContext.Portfolios.Join(dbContext.Scopes,
                                                   p => p.Id,
                                                   s => s.PortfolioId,
                                                  (p, s) => new { user = s.UserId, portfolio = p })
                                            .Where(ps => ps.user == userId);

            //total = query.Count();

            var res = new List<Portfolio>();
            foreach (var item in query)
            {
                res.Add(item.portfolio);
            }

            return res;
        }
    }
}
using System.Linq;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Repositories
{
    public class ScopeRepository : Repository<Scope>, IScopeRepository
    {
        public ScopeRepository(TPGPContext ctx) : base(ctx)
        {
        }

        public bool GetScopeByPortfolio(long portfolioId)
        {
            var portfolio = dbContext.Scopes.Where(s => s.PortfolioId == portfolioId).FirstOrDefault();

            return portfolio.Initial;
        }
    }
}
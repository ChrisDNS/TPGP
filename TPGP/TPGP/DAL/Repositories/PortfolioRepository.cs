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

        public override IEnumerable<Portfolio> Pagination(int page, int itemsPerPage, out int totalCount)
        {
            IEnumerable<Portfolio> portfolios = GetAll();
            totalCount = portfolios.Count();

            return portfolios.OrderBy(p => p.Sector).Skip(itemsPerPage * page).Take(itemsPerPage).ToList();
        }
    }
}
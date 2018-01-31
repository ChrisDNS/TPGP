using System.Collections.Generic;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Interfaces
{
    public interface IPortfolioRepository : IRepository<Portfolio>
    {
        IEnumerable<Portfolio> GetPortfoliosByUserScope(long userId);
    }
}
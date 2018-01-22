using TPGP.Models.Jobs;

namespace TPGP.DAL.Interfaces
{
    public interface IScopeRepository : IRepository<Scope>
    {
        bool GetScopeByPortfolio(long portfolioId);
    }
}
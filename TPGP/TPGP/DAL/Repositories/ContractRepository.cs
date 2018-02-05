using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.DAL.Repositories;
using TPGP.Models.Jobs;

namespace TPGP.Models.DAL.Repositories
{
    public class ContractRepository : Repository<Contract>, IContractRepository
    {
        public ContractRepository(TPGPContext ctx) : base(ctx)
        {
        }

        public IEnumerable<Contract> GetAllFromPortfolio(long id)
        {
            return GetByFilter(c => c.PortfolioId == id);
        }

        public IEnumerable<Contract> GetContractsByUserScope(long userId)
        {
            var query = dbContext.Contracts.Join(dbContext.Scopes,
                                                   p => p.Id,
                                                   c => c.PortfolioId,
                                                  (p, s) => new { user = s.UserId, contract = p })
                                            .Where(ps => ps.user == userId);

            //total = query.Count();

            var res = new List<Contract>();
            foreach (var item in query)
            {
                res.Add(item.contract);
            }

            res.ForEach(c => Debug.WriteLine(c.Name));

            return res;
        }
    }
}
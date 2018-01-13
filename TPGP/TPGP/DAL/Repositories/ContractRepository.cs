using System.Collections.Generic;
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
    }
}
using System.Collections.Generic;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Interfaces
{
    public interface IContractRepository : IRepository<Contract>
    {
        IEnumerable<Contract> GetAllFromPortfolio(long id);
    }
}
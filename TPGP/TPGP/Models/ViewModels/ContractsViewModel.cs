using PagedList;
using System.Collections.Generic;
using TPGP.Models.Jobs;

namespace TPGP.Models.ViewModels
{
    public class ContractsViewModel
    {
        public IPagedList<Contract> Contracts { get; set; }
        public Portfolio Portfolio { get; set; }

        public IEnumerable<Portfolio> Portfolios { get; set; }
       
        public bool IsListEmpty;

        public ContractsViewModel()
        {
        }
    }
}
using System.Collections.Generic;
using TPGP.Models.Jobs;

namespace TPGP.ViewModels
{
    public class ContractViewModel
    {
        public Contract Contract { get; set; }

        public ContractViewModel(Contract c)
        {
            this.Contract = c;
        }
    }
}
using System.Collections.Generic;
using System.Web.Mvc;
using TPGP.Models.Jobs;

namespace TPGP.ViewModels
{
    public class ContractViewModel
    {
        public Contract Contract { get; set; }
        public IEnumerable<SelectListItem> Portfolios { get; set; }

        public ContractViewModel()
        {
        }

        public ContractViewModel(Contract c)
        {
            this.Contract = c;
        }
    }
}
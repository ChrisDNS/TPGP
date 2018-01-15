using System.Collections.Generic;
using System.Web.Mvc;
using TPGP.Models.Jobs;
using TPGP.Models.ViewModels;

namespace TPGP.ViewModels
{
    public class ContractViewModel
    {
        public Contract Contract { get; set; }
        public IEnumerable<SelectListItem> Portfolios { get; set; }
        public IEnumerable<AssignedGeographicalZoneData> Zones { get; set; }

        public ContractViewModel()
        {            
        }
    }
}
﻿using System.Collections.Generic;
using System.Web.Mvc;
using TPGP.Models.Jobs;

namespace TPGP.Models.ViewModels
{
    public class ContractViewModel
    {
        public Contract Contract { get; set; }
        public string Portfolio { get; set; }

        public IEnumerable<SelectListItem> Portfolios { get; set; }

        public IEnumerable<GeographicalZone> Zones { get; set; }
        public IEnumerable<long> ZonesIds { get; set; }

        public ContractViewModel()
        {            
        }
    }
}
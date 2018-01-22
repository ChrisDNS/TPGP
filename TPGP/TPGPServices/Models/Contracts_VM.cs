using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPGP.Models.Jobs;

namespace TPGPServices.Models
{
    public class Contracts_VM
    {
        public long IDContract { get; set; }
        public string Name { get; set; }
        public DateTime InitDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Bonus { get; set; }
        public string Company { get; set; }
        public string Sector { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPGP.Models.Jobs;
using TPGPServices.View_Model;

namespace TPGPServices.Models
{
    public class Contract_VM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string InitDate { get; set; }
        public string EndDate { get; set; }
        public double Bonus { get; set; }
        public string Company { get; set; }
        public string Sector { get; set; }
        public List<string> Zones { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPGP.Models.Jobs
{
    public class Portfolio
    {
        public long Id { get; set; }

        public string Sector { get; set; }

        public ICollection<Contract> Contracts { get; set; }

        public Portfolio()
        {
        }
    }
}
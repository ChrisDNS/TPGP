using System.Collections.Generic;

namespace TPGP.Models.Jobs
{
    public class Portfolio
    {
        public long Id { get; set; }

        public string Sector { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }

        public Portfolio()
        {
        }
    }
}
using System;
using System.Collections.Generic;

namespace TPGP.Models.Jobs
{
    public class Contract
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public DateTime InitDate { get; set; }
        public double Bonus { get; set; }

        public long PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        public Contract()
        {  
        }

        public Contract(long id, string name, DateTime initDate, double bonus)
        {
            Id = id;
            Name = name;
            InitDate = initDate;
            Bonus = bonus;
        }
    }
}
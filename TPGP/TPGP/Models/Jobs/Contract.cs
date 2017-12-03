using System;
using System.Collections.Generic;

namespace TPGP.Models.Jobs
{
    public class Contract
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Sector { get; set; }
        public DateTime InitDate { get; set; }
        public double Bonus { get; set; }

        public Contract()
        {  
        }

        public Contract(long id, string name, string sector, DateTime initDate, double bonus)
        {
            Id = id;
            Name = name;
            Sector = sector;
            InitDate = initDate;
            Bonus = bonus;
        }

        public override bool Equals(object obj)
        {
            var contract = obj as Contract;

            return contract != null &&
                   Id == contract.Id &&
                   Sector == contract.Sector &&
                   InitDate == contract.InitDate &&
                   Bonus == contract.Bonus;
        }

        public override int GetHashCode()
        {
            var hashCode = -162179230;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Sector);
            hashCode = hashCode * -1521134295 + InitDate.GetHashCode();
            hashCode = hashCode * -1521134295 + Bonus.GetHashCode();

            return hashCode;
        }
    }
}
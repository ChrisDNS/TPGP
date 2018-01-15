using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPGP.Models.Jobs
{
    public class Contract
    {
        public long Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Display(Name = "Begin Date")]
        [Required(ErrorMessage = "The Begin Date field is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime InitDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "The End Date field is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Bonus")]
        [Required(ErrorMessage = "The Bonus field is required.")]
        public double Bonus { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Area")]
        public virtual ICollection<GeographicalZone> Zones { get; set; }

        [Required(ErrorMessage = "The Sector field is required.")]
        public long PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        public Contract()
        {
        }

        public Contract(long id, string name, DateTime initDate, double bonus, string company)
        {
            Id = id;
            Name = name;
            InitDate = initDate;
            Bonus = bonus;
            Company = company;
        }
    }
}
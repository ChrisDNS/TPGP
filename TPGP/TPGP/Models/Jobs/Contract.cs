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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MMM/dd}")]
        public DateTime InitDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "The End Date field is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MMM/dd}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Bonus")]
        [Required(ErrorMessage = "The Bonus field is required.")]
        public double Bonus { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "The Company field is required.")]
        public string Company { get; set; }

        [Display(Name = "Zones")]
        public virtual ICollection<GeographicalZone> Zones { get; set; }

        [Required(ErrorMessage = "The Sector field is required.")]
        public long PortfolioId { get; set; }
        public virtual Portfolio Portfolio { get; set; }

        public Contract()
        {
        }
    }
}
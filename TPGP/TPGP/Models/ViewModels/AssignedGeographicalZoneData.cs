using System.Web.Mvc;

namespace TPGP.Models.ViewModels
{
    public class AssignedGeographicalZoneData : SelectListItem
    {
        public long Id { get; set; }

        public string Label { get; set; }
        public bool Assigned { get; set; }
    }
}
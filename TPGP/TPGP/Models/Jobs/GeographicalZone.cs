using System.Collections.Generic;

namespace TPGP.Models.Jobs
{
    public class GeographicalZone
    {
        public long Id { get; set; }

        public string Label { get; set; }

        public long? ParentId { get; set; }
        public virtual GeographicalZone Parent { get; set; }
        public virtual ICollection<GeographicalZone> Children { get; set; }

        public GeographicalZone()
        {
        }
    }
}
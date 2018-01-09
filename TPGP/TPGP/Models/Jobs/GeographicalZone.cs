using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPGP.Models.Jobs
{
    public class GeographicalZone
    {
        [Key, ForeignKey("Parent")]
        public long Id { get; set; }

        public string Label { get; set; }

        public long ParentId { get; set; }
        public virtual GeographicalZone Parent { get; set; }
        public virtual ICollection<GeographicalZone> Children { get; set; }

        public GeographicalZone()
        {
        }
    }
}
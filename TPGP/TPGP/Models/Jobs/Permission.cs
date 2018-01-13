using System.Collections.Generic;

namespace TPGP.Models.Jobs
{
    public class Permission
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
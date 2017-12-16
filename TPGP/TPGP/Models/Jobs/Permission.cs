using System.Collections.Generic;

namespace TPGP.Models.Jobs
{
    public class Permission
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
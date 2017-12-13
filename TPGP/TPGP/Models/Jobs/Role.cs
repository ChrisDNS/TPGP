using System.Collections.Generic;

namespace TPGP.Models.Jobs
{
    public class Role
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public List<Permission> Permissions { get; set; }

        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }

        public Role()
        {
        }
    }
}
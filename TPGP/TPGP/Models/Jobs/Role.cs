using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TPGP.Models.Enums;

namespace TPGP.Models.Jobs
{
    public class Role
    {
        public long Id { get; set; }
        [Display(Name="Role")]
        public Roles RoleName { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Role()
        {        
        }
    }
}
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

        public ICollection<Permission> Permissions { get; set; }
        public ICollection<User> Users { get; set; }

        public bool IsBeingProcessed;

        public Role()
        {
        }
    }
}
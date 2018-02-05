using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TPGP.Models.Enums;

namespace TPGP.Models.Jobs
{
    public class User
    {
        public long Id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username field is required.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [NotMapped]
        public string Password { get; set; }

        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public long ZoneId { get; set; }
        [Display(Name = "Zones")]
        public virtual GeographicalZone Zone { get; set; }

        public long RoleId { get; set; }
        [Display(Name = "Role")]
        public virtual Role Role { get; set; }

        public long DesiredRoleId { get; set; }
        public Roles DesiredRoleName { get; set; }
        public bool IsBeingProcessed { get; set; }

        public File File { get; set; }

        public User()
        {
        }
    }
}
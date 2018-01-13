using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPGP.Models.Jobs
{
    public class User
    {
        public long Id { get; set; }
        [Display(Name ="Username")]
        [Required(ErrorMessage = "Username field is required.")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        public string Password { get; set; }
        [Display(Name ="First name")]
        public string Firstname { get; set; }
        [Display(Name ="Last name")]
        public string Lastname { get; set; }
        public string Email { get; set; }

        public long RoleId { get; set; }
        public virtual Role Role { get; set; }

        public File file { get; set; }

        public User()
        {
        }

        public User(string username, string firstname, string lastname, string email, Role role)
        {
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Role = role;
        }

        public void Edit(User user)
        {
            this.Username = user.Username;
            this.Firstname = user.Firstname;
            this.Lastname = user.Lastname;
            this.Email = user.Email;
            this.RoleId = user.RoleId;
        }
    }
}
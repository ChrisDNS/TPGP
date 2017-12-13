using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPGP.Models.Jobs
{
    public class User
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Username field is required.")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        public string Password { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public bool IsAdmin { get; set; }
        public Role Role { get; set; }

        public User()
        {
        }

        public User(string username, string firstname, string lastname, string email, bool isAdmin, Role role)
        {
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            IsAdmin = isAdmin;
            Role = role;
        }
    }
}
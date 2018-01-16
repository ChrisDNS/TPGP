using System.Collections.Generic;
using System.Web.Mvc;
using TPGP.Models.Jobs;

namespace TPGP.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        public UserViewModel()
        {
        }

        public UserViewModel(User u)
        {
            User = u;
        }
    }
}
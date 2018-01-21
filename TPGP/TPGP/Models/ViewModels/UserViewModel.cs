using System.Collections.Generic;
using System.Web.Mvc;
using TPGP.Models.Jobs;

namespace TPGP.Models.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        public UserViewModel()
        {
        }
    }
}
using TPGP.Models.Jobs;

namespace TPGP.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }

        public UserViewModel()
        {
        }

        public UserViewModel(User u)
        {
            User = u;
        }
    }
}
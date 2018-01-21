using PagedList;
using TPGP.Models.Jobs;

namespace TPGP.Models.ViewModels
{
    public class UsersViewModel
    {
        public IPagedList<User> Users { get; set; }

        public UsersViewModel()
        {
        }
    }
}
using PagedList;
using TPGP.Models.Jobs;

namespace TPGP.ViewModels
{
    public class UsersViewModel
    {
        public IPagedList<User> Users { get; set; }

        public UsersViewModel()
        {
        }
    }
}
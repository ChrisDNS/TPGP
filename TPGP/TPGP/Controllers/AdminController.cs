using System.Web.Mvc;
using TPGP.ActionFilters;
using System.Linq;
using TPGP.DAL.Interfaces;
using TPGP.Models.ViewModels;
using PagedList;
using TPGP.Models.Jobs;
using TPGP.Utils;

namespace TPGP.Controllers
{
    [CustomAuthorize]
    public class AdminController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public AdminController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            int noPage = (page ?? 1) - 1;

            if (searchString != null)
                page = 1;

            var users = userRepository.Pagination<string>(p => p.Username, noPage, Constants.ITEMS_PER_PAGE, out int total);

            if (!string.IsNullOrEmpty(searchString))
                users = users.Where(c => c.Username.Contains(searchString));

            var usersViewModels = new UsersViewModel
            {
                Users = new StaticPagedList<User>(users, noPage + 1, Constants.ITEMS_PER_PAGE, total)
            };

            return View(usersViewModels);
        }

        public ActionResult Edit(long id)
        {
            var userViewModel = new UserViewModel
            {
                User = userRepository.GetById(id),
                Roles = new SelectList(roleRepository.GetAll(), dataValueField: "Id", dataTextField: "RoleName")
            };

            return View(userViewModel);
        }

        public ActionResult Save(UserViewModel uvm)
        {
            var usr = userRepository.GetByFilter(u => u.Id == uvm.User.Id).FirstOrDefault();

            if (usr != null)
            {
                usr.RoleId = uvm.User.RoleId;
                usr.Role.IsBeingProcessed = false;
            }

            userRepository.Update(usr);
            userRepository.SaveChanges();

            return View("Index");
        }
    }
}
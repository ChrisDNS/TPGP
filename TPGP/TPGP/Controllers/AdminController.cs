using System.Web.Mvc;
using TPGP.ActionFilters;
using System.Linq;
using TPGP.DAL.Interfaces;
using TPGP.ViewModels;

namespace TPGP.Controllers
{
    [CustomAuthorize]
    public class AdminController : Controller
    {
        private readonly IUserRepository userRepository;

        public AdminController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(userRepository.GetAll());
        }

        public ActionResult Edit(long id)
        {
            return View(new UserViewModel(userRepository.GetById(id)));
        }

        public ActionResult Save(UserViewModel uvm)
        {
            var usr = userRepository.GetByFilter(u => u.Id == uvm.User.Id).FirstOrDefault();

            if (usr != null)
            {
                usr.Username = uvm.User.Username;
                usr.Role.RoleName = uvm.User.Role.RoleName;
            }

            userRepository.Update(usr);
            userRepository.SaveChanges();

            return View("Index", userRepository.GetAll());
        }
    }
}
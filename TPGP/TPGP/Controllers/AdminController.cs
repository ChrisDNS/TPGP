using System.Web.Mvc;
using TPGP.ActionFilters;
using System.Linq;
using TPGP.DAL.Interfaces;
using TPGP.Models.ViewModels;

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

        // GET: Admin
        public ActionResult Index()
        {
            return View(userRepository.GetAll());
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
            }

            userRepository.Update(usr);
            userRepository.SaveChanges();

            return View("Index", userRepository.GetAll());
        }
    }
}
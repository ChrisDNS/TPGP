using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.Models.Jobs;
using System.Linq;
using TPGP.DAL.Interfaces;

namespace TPGP.Controllers
{
    [CustomAuthorizeAdmin]
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
            return View(userRepository.GetById(id));
        }

        public ActionResult Save(User user)
        {
            var usr = userRepository.GetBy(u => u.Id == user.Id).FirstOrDefault();

            if (usr != null)
            {
                usr.Username = user.Username;
                usr.Role.RoleName = user.Role.RoleName;
            }

            userRepository.Update(usr);
            userRepository.SaveChanges();

            return View("Index", userRepository.GetAll());
        }
    }
}
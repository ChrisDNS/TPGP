using LDAP;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using TPGP.DAL.Interfaces;
using TPGP.Models.Enums;
using TPGP.Models.Jobs;
using TPGP.ViewModels;

namespace TPGP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public HomeController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public ActionResult Index()
        {
            if (Session["username"] != null)
                return View("_AlreadyLoggedIn");

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel uvm)
        {
            if (ModelState.IsValid)
            {
                LDAPUser ldapUserDetails = LDAPService.Instance.AuthenticationAndIdentification(uvm.User.Username, uvm.User.Password);
                if (ldapUserDetails == null)
                {
                    ModelState.AddModelError(string.Empty, "Wrong username or password.");
                    return View("Index", uvm);
                }

                var user = userRepository.GetByFilter(u => u.Username == uvm.User.Username).FirstOrDefault();
                if (user == null)
                {
                    var newUser = new User
                    {
                        Firstname = ldapUserDetails.Firstname,
                        Lastname = ldapUserDetails.Lastname,
                        Username = ldapUserDetails.Username,
                        Email = ldapUserDetails.Email,
                        //Zone = ldapUserDetails.Zone,
                        Role = roleRepository.GetByFilter(r => r.RoleName == Roles.COLLABORATOR).FirstOrDefault()
                    };

                    userRepository.Insert(newUser);
                    userRepository.SaveChanges();

                    Session["username"] = newUser.Username;
                    Session["role"] = newUser.Role.RoleName.ToString("g");
                }
                else
                {
                    user.Role = roleRepository.GetById(user.RoleId);

                    Session["username"] = user.Username;
                    Session["role"] = user.Role.RoleName.ToString("g");

                    if (user.Role.RoleName == Roles.ADMIN)
                        return RedirectToAction("Index", "Admin");
                    else
                        return RedirectToAction("Index", "Portfolio");
                }

                return View("Index");
            }

            return View("Index");
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
            Session["role"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
using LDAP;
using System.Linq;
using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.Enums;
using TPGP.Models.Jobs;
using TPGP.Models.ViewModels;

namespace TPGP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IGeographicalZoneRepository zoneRepository;

        public HomeController(IUserRepository userRepository, IRoleRepository roleRepository,
                                                              IGeographicalZoneRepository zoneRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.zoneRepository = zoneRepository;
        }

        public ActionResult Index()
        {
            using (var ctx = new TPGPContext())
            {
                ctx.Roles.Count();
            }

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
                        Zone = zoneRepository.GetByFilter(z => z.Label == ldapUserDetails.Zone).FirstOrDefault(),
                        Role = roleRepository.GetByFilter(r => r.RoleName == Roles.COLLABORATOR).FirstOrDefault()
                    };

                    userRepository.Insert(newUser);
                    userRepository.SaveChanges();

                    Session["username"] = newUser.Username;
                    Session["role"] = newUser.Role.RoleName.ToString("g");
                    Session["id"] = newUser.Id;
                    Session["Zone"] = newUser.Zone.Label;
                }
                else
                {
                    user.Role = roleRepository.GetById(user.RoleId);

                    Session["username"] = user.Username;
                    Session["role"] = user.Role.RoleName.ToString("g");
                    Session["id"] = user.Id;
                    Session["Zone"] = user.Zone.Label;

                    if (this.IsAdmin())
                        return RedirectToAction("Index", "Admin");
                }

                return RedirectToAction("Index", "Portfolio");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
            Session["role"] = null;
            Session["id"] = null;
            Session["Zone"] = null;

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
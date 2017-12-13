using LDAP;
using System.Linq;
using System.Web.Mvc;
using TPGP.Models.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;

        public HomeController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: Home
        public ActionResult Index()
        {
            User user = new User("Sarra", "Sarra", "Sarra", "sarra@sarra.sarra", false, null);
            userRepository.Insert(user);
            userRepository.SaveChanges();

            return View();
        }

        [HttpPost]
        public ActionResult Login(User userModel)
        {
            if (ModelState.IsValid)
            {
                LDAPUser ldapUserDetails = LDAPService.Instance.AuthenticationAndIdentification(userModel.Username, userModel.Password);
                if (ldapUserDetails == null)
                {
                    ModelState.AddModelError(string.Empty, "Wrong username or password.");
                    return View("Index", userModel);
                }

                bool userAlreadyExists = userRepository.GetBy(u => u.Username == userModel.Username).ToList().Count > 0;
                if (!userAlreadyExists)
                {
                    User newUser = new User()
                    {
                        Firstname = ldapUserDetails.Firstname,
                        Lastname = ldapUserDetails.Lastname,
                        Username = ldapUserDetails.Username,
                        Email = ldapUserDetails.Email,
                        IsAdmin = false,
                        Role = new Role()
                    };

                    userRepository.Insert(newUser);
                    userRepository.SaveChanges();
                }

                return View("USER AJOUTE");
            }

            return View("USER DEJA LA");
        }
    }
}
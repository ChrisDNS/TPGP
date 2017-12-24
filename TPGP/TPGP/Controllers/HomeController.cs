using LDAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPGP.Context;
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
        private readonly IContractRepository contractRepository;

        public HomeController(IUserRepository userRepository, IRoleRepository roleRepository,
                                                              IContractRepository contractRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.contractRepository = contractRepository;
        }

        // GET: Home
        public ActionResult Index()
        {
            //juste pour créer la database
            using (var ctx = new TPGPContext())
            {
                var rolesCount = ctx.Roles.Count();
            }
            //juste pour créer la database

            if (System.Web.HttpContext.Current.Request.Cookies["user"] != null)
                return RedirectToAction("Index", "Contract");

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

                HttpCookie cookie = new HttpCookie("user")
                {
                    Expires = DateTime.Now.AddHours(1)
                };

                User user = userRepository.GetBy(u => u.Username == userModel.Username).First();
                if (user == null)
                {
                    User newUser = new User()
                    {
                        Firstname = ldapUserDetails.Firstname,
                        Lastname = ldapUserDetails.Lastname,
                        Username = ldapUserDetails.Username,
                        Email = ldapUserDetails.Email,
                        Role = new Role() { RoleName = Roles.COLLABORATOR, IsAdmin = false }
                    };

                    userRepository.Insert(newUser);
                    userRepository.SaveChanges();

                    cookie.Values["username"] = user.Username;
                    cookie.Values["role"] = user.Role.RoleName.ToString("g");
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    user.Role = roleRepository.GetById(user.RoleId);

                    cookie.Values["username"] = user.Username;
                    cookie.Values["role"] = user.Role.RoleName.ToString("g");
                    Response.Cookies.Add(cookie);

                    if (user.Role.RoleName == Roles.ADMIN)
                        return RedirectToAction("Index", "Admin");
                    else if (user.Role.RoleName == Roles.COLLABORATOR)
                        return RedirectToAction("Index", "Contract");
                }

                return View("Index");
            }

            return View("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPGP.Models.DAL.Context;
using TPGP.Models.DAL.Interfaces;
using TPGP.Models.DAL.Repositories;
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
            System.Diagnostics.Debug.WriteLine("AAAAAAAAA");

            return View();
        }

        [HttpPost]
        public ActionResult Login(User userModel)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                var userDetails = userRepository.GetBy(u => u.Username == userModel.Username && u.Username == userModel.Username).FirstOrDefault();
                System.Diagnostics.Debug.WriteLine(userDetails);
                if (userDetails == null)
                {
                    ModelState.AddModelError(string.Empty, "Wrong username or password.");
                    return View("Index", userModel);
                }


            }

            return View("ghbngh");
        }
    }
}
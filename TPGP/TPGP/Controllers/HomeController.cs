using System.Collections.Generic;
using System.Web.Mvc;
using TPGP.Models.DAL.Context;
using TPGP.Models.DAL.Repositories;
using TPGP.Models.Jobs;

namespace TPGP.Controllers
{
    public class HomeController : Controller
    {

        private UserRepository ur = new UserRepository(new TPGPContext());

        // GET: Home
        public ActionResult Index()
        {
            User user = new User(1, "caca", "caca", "caca", "", "", "", "");
            ur.Insert(user);
            ur.context.SaveChanges();

            IEnumerable<User> i = ur.GetAll();
            foreach (var uuu in i)
            {
                ViewBag.Firstname = uuu.Firstname;
            }

            ur.context.SaveChanges();

            return View();
        }
    }
}
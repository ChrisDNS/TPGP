using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.Models.Jobs;
using TPGP.Context;
using System.Collections.Generic;
using System.Linq;

namespace TPGP.Controllers
{
   // [CustomAuthorizeAdmin]
    public class AdminController : Controller
    {
        TPGPContext context;
        public AdminController(/*[FromService] TPGPContext context*/)
        { // context.users.Skip(4).take(5).tolist() // saut de users
            // ff.Include(p=>p.cate) inclure les lazy
            context = new TPGPContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            IEnumerable<User> us = context.Users.ToList();
            return View("AdminView", us);
        }

        public ActionResult Edit(int id)
        {
           
            User us = context.Users
                .Where(p => p.Id.Equals(id))
                .FirstOrDefault<User>();

            return View("edit",us);
        }

        public ActionResult Save(User user)
        {

            User us = context.Users
                .Where(p => p.Username.Equals(user.Username))
                .FirstOrDefault<User>();
            if (us != null)
            {
                us.Username = user.Username;
                us.Role.RoleName = user.Role.RoleName;
            }
            context.SaveChanges();
            IEnumerable<User> users = context.Users.ToList();
            
            return View("AdminView", users);
        }
    }
}
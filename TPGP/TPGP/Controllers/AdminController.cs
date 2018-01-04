using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.Models.Jobs;
using TPGP.Context;
using System.Collections.Generic;
using System.Linq;
using TPGP.DAL.Repositories;
using TPGP.DAL.Interfaces;

namespace TPGP.Controllers
{
   // [CustomAuthorizeAdmin]
    public class AdminController : Controller
    {
        TPGPContext context;
        IUserRepository userrepository;
        public AdminController(IUserRepository usr)
        { // context.users.Skip(4).take(5).tolist() // saut de users
            // ff.Include(p=>p.cate) inclure les lazy
            context = new TPGPContext();
            userrepository = usr;
        }
        // GET: Admin
        public ActionResult Index()
        {
            
            IEnumerable<User> us = userrepository.GetAll();
            return View("AdminView", us);
        }

        public ActionResult Edit(long id)
        {
            var us = userrepository.GetById(id);
           /* User us = context.Users
                .Where(p => p.Id.Equals(id))
                .FirstOrDefault<User>();*/

            return View("edit",us);
        }

        public ActionResult Save(User user)
        {

            var us = userrepository.GetAll().Where(p => p.Username.Equals(user.Username))
                .FirstOrDefault<User>();
           /* User us = context.Users
                .Where(p => p.Username.Equals(user.Username))
                .FirstOrDefault<User>();*/
            if (us != null)
            {
                us.Username = user.Username;
                us.Role.RoleName = user.Role.RoleName;
            }
            userrepository.SaveChanges();
            IEnumerable<User> users = context.Users.ToList();
            
            return View("AdminView", users);
        }
    }
}
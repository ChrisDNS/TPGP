using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.Controllers
{
    public class AccountController : Controller
    {
        private TPGPContext db = new TPGPContext();
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            
        }

        // GET: Account
        public ActionResult Index()
        {
            string username = (string)Session["username"];
            User user = userRepository.GetByFilter(u => u.Username == username).First();
          
           
                return View("index",user);
            
          

        }

        // GET: Account/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Account/Create
       public ActionResult Create()
        {
            return View();
        }
       

        // POST: Account/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TPGP.Models.Jobs.File f, HttpPostedFileBase file)
        {
            string username = (string)Session["username"];
            User user = userRepository.GetByFilter(u => u.Username == username).First();
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/pdf"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                var newFile = new Models.Jobs.File
                {
                    Id = f.Id,
                    FilePath = "~/pdf/" + file.FileName
                };
                db.Files.Add(newFile);
                db.SaveChanges();
                user.file = newFile;
                user.Role.IsBeingProcessed = true;
            }
                return View("Index", user);
            
        }


        // GET: Account/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // POST: Account/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Firstname,Lastname,Email,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

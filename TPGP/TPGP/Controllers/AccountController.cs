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
       
        private readonly IUserRepository userRepository;
        private readonly IFileRepository fileRepository;
        private readonly IRoleRepository roleRepository;

        public AccountController(IUserRepository userRepository, IFileRepository fileRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.fileRepository = fileRepository;
            this.roleRepository = roleRepository;
        }

        // GET: Account
        public ActionResult Index()
        {
            string username = (string)Session["username"];
            User user = userRepository.GetByFilter(u => u.Username == username).First();
           
            ViewBag.User = user;

            return View("index",user);



        }

        // GET: Account/Details/5
        /*  public ActionResult Details(long? id)
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
          }*/

        // GET: Account/Create
        public ActionResult Create()
        {
            string username = (string)Session["username"];
            User user = userRepository.GetByFilter(u => u.Username == username).First();
            List<Models.Enums.Roles> roles = new List<Models.Enums.Roles>
            {
                Models.Enums.Roles.ACTUARY,
                Models.Enums.Roles.COLLABORATOR,
                Models.Enums.Roles.MANAGER,
                Models.Enums.Roles.SUBSCRIBER,
            };
            roles.Remove(user.Role.RoleName);
            ViewBag.Roles = new SelectList(roles);
            return View("create", user);
        }

        //GET
        /*  public ActionResult Download()
          {
              var dir = new System.IO.DirectoryInfo(Server.MapPath("~/pdf_status/"));
              System.IO.FileInfo[] filename = dir.GetFiles("status");
              return View(filename[0].Name);

          }*/

        public FileResult Download()
        {
            var FileVirtualePath = "~/pdf_download/status" + ".pdf";
            return File(FileVirtualePath, "application/force-download", Path.GetFileName(FileVirtualePath));
        }


        // POST: Account/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TPGP.Models.Jobs.File f, HttpPostedFileBase file, TPGP.Models.Enums.Roles roleChoisi)
        {
            string username = (string)Session["username"];
            User user = userRepository.GetByFilter(u => u.Username == username).First();
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/pdf_upload"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                var newFile = new Models.Jobs.File
                {
                    Id = f.Id,
                    FilePath = "~/pdf_upload/" + file.FileName
                };
               
                fileRepository.Insert(newFile);
                fileRepository.SaveChanges();
                user.file = newFile;
                user.Role.IsBeingProcessed = true;
                user.Role.DesiredRole = roleChoisi;
                userRepository.Update(user);
                userRepository.SaveChanges();
            }
           
            ViewBag.User = user;
            return View("Index",user);

        }


        // GET: Account/Edit/5
        /* public ActionResult Edit(long? id)
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
         }*/

        // POST: Account/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
       /* [HttpPost]
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
         }*/

        // GET: Account/Delete/5
        /* public ActionResult Delete(long? id)
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
         }*/

        // POST: Account/Delete/5
        /* [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public ActionResult DeleteConfirmed(long id)
         {
             User user = db.Users.Find(id);
             db.Users.Remove(user);
             db.SaveChanges();
             return RedirectToAction("Index");
         }
         */
        /* protected override void Dispose(bool disposing)
         {
             if (disposing)
             {
                 db.Dispose();
             }
             base.Dispose(disposing);
         }*/
    }
}

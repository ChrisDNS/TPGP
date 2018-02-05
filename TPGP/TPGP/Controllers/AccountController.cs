using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.DAL.Interfaces;
using TPGP.Models.Enums;
using TPGP.Models.ViewModels;

namespace TPGP.Controllers
{
    [CustomAuthorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IFileRepository fileRepository;
        private readonly IRoleRepository roleRepository;

        public AccountController(IUserRepository userRepository, IFileRepository fileRepository,
                                                                 IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.fileRepository = fileRepository;
            this.roleRepository = roleRepository;
        }

        public ActionResult Index()
        {
            string username = (string)Session["username"];

            var uvm = new UserViewModel
            {
                User = userRepository.GetByFilter(u => u.Username == username).FirstOrDefault()
            };

            return View(uvm);
        }

        public ActionResult ChangeStatus()
        {
            string username = (string)Session["username"];

            var user = userRepository.GetByFilter(u => u.Username == username).FirstOrDefault();
            var roles = roleRepository.GetAll();

            var uvm = new UserViewModel
            {
                User = user,
                Roles = new SelectList(roles, dataValueField: "Id", dataTextField: "RoleName")
            };

            return View(uvm);
        }

        [HttpPost]
        public ActionResult ChangeStatus(UserViewModel uvm, Models.Jobs.File f, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.GetByFilter(u => u.Username == uvm.User.Username).FirstOrDefault();

                string path = Path.Combine(Server.MapPath("~/pdf_upload"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

                var newFile = new Models.Jobs.File
                {
                    UserId=user.Id,
                    Id = f.Id,
                    FilePath = "~/pdf_upload/" + file.FileName
                };

                fileRepository.Insert(newFile);
                fileRepository.SaveChanges();

                user.File = newFile;
                user.IsBeingProcessed = true;
                user.DesiredRoleId = uvm.User.DesiredRoleId;
                user.DesiredRoleName = roleRepository.GetByFilter(r => user.DesiredRoleId == r.Id).FirstOrDefault().RoleName;

                userRepository.Update(user);
                userRepository.SaveChanges();          
            }

            return RedirectToAction("Index", "Account", uvm);
        }

        public FileResult Download()
        {
            var fileVirtualePath = "~/pdf_download/status" + ".pdf";

            return File(fileVirtualePath, "application/force-download", Path.GetFileName(fileVirtualePath));
        }
    }
}
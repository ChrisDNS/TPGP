using System.Web.Mvc;
using TPGP.ActionFilters;

namespace TPGP.Controllers
{
    [CustomAuthorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}
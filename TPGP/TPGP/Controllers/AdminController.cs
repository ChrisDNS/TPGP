using System.Web.Mvc;
using TPGP.ActionFilters;

namespace TPGP.Controllers
{
    [CustomAuthorizeAdmin]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}
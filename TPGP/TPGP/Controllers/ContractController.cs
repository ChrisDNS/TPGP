using System.Web.Mvc;
using TPGP.ActionFilters;

namespace TPGP.Controllers
{
    [CustomAuthorizeOthers]
    public class ContractController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
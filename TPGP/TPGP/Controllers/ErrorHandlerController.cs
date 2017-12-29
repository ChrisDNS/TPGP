using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TPGP.Controllers
{
    public class ErrorHandlerController : Controller
    {
        public ActionResult Index()
        {
            return View("Error");
        }
    }
}
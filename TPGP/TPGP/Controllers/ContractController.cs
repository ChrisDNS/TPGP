using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.DAL.Interfaces;

namespace TPGP.Controllers
{
    [CustomAuthorizeOthers]
    public class ContractController : Controller
    {
        private readonly IContractRepository contractRepository;

        public ContractController(IContractRepository contractRepository)
        {
            this.contractRepository = contractRepository;
        }

        public ActionResult Index(string currentFilter, string searchString)
        {
            return View();
        }

        public ActionResult Details(long id)
        {
            return View(contractRepository.GetById(id));
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}
using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;
using TPGP.ViewModels;

namespace TPGP.Controllers
{
    [CustomAuthorize]
    public class ContractController : Controller
    {
        private readonly IContractRepository contractRepository;
        private readonly IPortfolioRepository portfolioRepository;

        public ContractController(IContractRepository contractRepository,
                                  IPortfolioRepository portfolioRepository)
        {
            this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
        }

        public ActionResult Index(string currentFilter, string searchString)
        {
            return View();
        }

        public ActionResult Details(long id)
        {
            var cvm = new ContractViewModel
            {
                Contract = contractRepository.GetById(id)
            };

            return View(cvm.Contract);
        }

        public ActionResult Create()
        {
            var contractViewModel = new ContractViewModel
            {
                Portfolios = new SelectList(portfolioRepository.GetAll(), dataValueField: "Id", dataTextField: "Sector")
            };

            return View(contractViewModel);
        }

        [HttpPost]
        public ActionResult Save(ContractViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var contract = new Contract
                {
                    Name = cvm.Contract.Name,
                    InitDate = cvm.Contract.InitDate,
                    Bonus = cvm.Contract.Bonus,
                    PortfolioId = cvm.Contract.PortfolioId
                };

                contractRepository.Insert(contract);
                contractRepository.SaveChanges();

                return RedirectToAction("Index", "Portfolio");
            }

            return View();
        }
    }
}
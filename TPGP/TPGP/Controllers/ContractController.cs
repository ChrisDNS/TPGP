using System.Collections.Generic;
using System.Web.Mvc;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;
using TPGP.ViewModels;

namespace TPGP.Controllers
{
    public class ContractController : Controller
    {
        private readonly IContractRepository contractRepository;
        private readonly IPortfolioRepository portfolioRepository;

        public ContractController(IContractRepository contractRepository, IPortfolioRepository portfolioRepository)
        {
            this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
        }

        public ActionResult Index()
        {
            PortfoliosViewModel pfv = new PortfoliosViewModel
            {
                Portfolios = (List<Portfolio>)portfolioRepository.GetAll()
            };

            return View(pfv);
        }
    }
}
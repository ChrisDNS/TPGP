using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;
using TPGP.Utils;
using TPGP.ViewModels;

namespace TPGP.Controllers
{
    [CustomAuthorizeOthers]
    public class PortfolioController : Controller
    {
        private readonly IContractRepository contractRepository;
        private readonly IPortfolioRepository portfolioRepository;

        public PortfolioController(IContractRepository contractRepository, IPortfolioRepository portfolioRepository)
        {
            this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
        }

        public ActionResult Index(int? page, string sortOrder)
        {
            int noPage = (page ?? 1);
            var portfoliosViewModels = new PortfoliosViewModel
            {
                Portfolios = Util.Pagination<Portfolio, string>(portfolioRepository.GetAll(),
                                                                p => p.Sector,
                                                                noPage,
                                                                Constants.ITEMS_PER_PAGE)
            };

            return View(portfoliosViewModels);
        }

        public ActionResult Contracts(long id, int? page, string sortOrder, string currentFilter, string searchString)
        {
            int noPage = (page ?? 1);
            var contractsViewModels = new ContractsViewModel
            {
                CurrentSort = sortOrder
            };

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            contractsViewModels.CurrentFilter = searchString;

            var contracts = contractRepository.GetAllFromPortfolio(id);
            if (contracts.Count() == 0)
            {
                contractsViewModels.IsListEmpty = true;
                return View(contractsViewModels);
            }

            if (!string.IsNullOrEmpty(searchString))
                contracts = contracts.Where(c => c.Name.Contains(searchString));

            contractsViewModels.Contracts = Util.Pagination<Contract, string>(contracts, p => p.Name,
                                                                             noPage,
                                                                             Constants.ITEMS_PER_PAGE);

            return View(contractsViewModels);
        }
    }
}
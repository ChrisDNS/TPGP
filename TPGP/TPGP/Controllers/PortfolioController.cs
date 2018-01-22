using PagedList;
using System.Linq;
using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;
using TPGP.Models.ViewModels;
using TPGP.Utils;

namespace TPGP.Controllers
{
    [CustomAuthorize]
    public class PortfolioController : Controller
    {
        private readonly IContractRepository contractRepository;
        private readonly IPortfolioRepository portfolioRepository;
        private readonly IScopeRepository scopeRepository;

        public PortfolioController(IContractRepository contractRepository, IPortfolioRepository portfolioRepository,
                                                                           IScopeRepository scopeRepository)
        {
            this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
            this.scopeRepository = scopeRepository;
        }

        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            int noPage = (page ?? 1) - 1;

            if (searchString != null)
                page = 1;

            var portfolios = portfolioRepository.GetPortfoliosByUserScope((long)Session["id"], p => p.Sector, noPage, Constants.ITEMS_PER_PAGE, out int total);
            portfolios.ToList().ForEach(p => p.Scope = scopeRepository.GetScopeByPortfolio(p.Id) ? "Initial" : "Extent");

            if (portfolios.Count() == 0)
                portfolios = portfolioRepository.Pagination(p => p.Sector, noPage, Constants.ITEMS_PER_PAGE, out total);

            if (!string.IsNullOrEmpty(searchString))
                portfolios = portfolios.Where(c => c.Sector.Contains(searchString));

            var portfoliosViewModels = new PortfoliosViewModel
            {
                Portfolios = new StaticPagedList<Portfolio>(portfolios, noPage + 1, Constants.ITEMS_PER_PAGE, total)
            };

            return View(portfoliosViewModels);
        }

        public ActionResult Contracts(long id, int? page, string sortOrder, string searchString)
        {
            int noPage = (page ?? 1) - 1;
            var contractsViewModels = new ContractsViewModel
            {
                Portfolio = portfolioRepository.GetById(id)
            };

            if (searchString != null)
                page = 1;

            var contracts = contractRepository.Pagination(p => p.PortfolioId == id, c => c.Name, noPage, Constants.ITEMS_PER_PAGE, out int total);
            contractsViewModels.Portfolio.Scope = scopeRepository.GetScopeByPortfolio(id) ? "Initial" : "Extent";

            if (contracts.Count() == 0)
            {
                contractsViewModels.IsListEmpty = true;
                return View(contractsViewModels);
            }

            if (!string.IsNullOrEmpty(searchString))
                contracts = contracts.Where(c => c.Name.Contains(searchString));

            contractsViewModels.Contracts = new StaticPagedList<Contract>(contracts, noPage + 1, Constants.ITEMS_PER_PAGE, total);

            return View(contractsViewModels);
        }
    }
}
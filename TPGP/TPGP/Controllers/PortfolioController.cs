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
        private readonly IGeographicalZoneRepository zoneRepository;

        public PortfolioController(IContractRepository contractRepository, IPortfolioRepository portfolioRepository,
                                                                           IScopeRepository scopeRepository,
                                                                           IGeographicalZoneRepository zoneRepository)
        {
            this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
            this.scopeRepository = scopeRepository;
            this.zoneRepository = zoneRepository;
        }

        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            int noPage = (page ?? 1) - 1;
            int total = 0;

            if (searchString != null)
                page = 1;

            var portfolios = portfolioRepository.GetPortfoliosByUserScope((long)Session["id"]);
            portfolios.ToList().ForEach(p => p.Scope = scopeRepository.GetScopeByPortfolio(p.Id) ? "Initial" : "Extent");

            if (portfolios.Count() == 0)
                portfolios = portfolioRepository.Pagination(p => p.Sector, noPage, Constants.ITEMS_PER_PAGE, out total);

            if (!string.IsNullOrEmpty(searchString))
                portfolios = portfolios.Where(c => c.Sector.ToLower().Contains(searchString.ToLower()));

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

            string zoneUser = (string)Session["zone"];
            contracts = zoneRepository.GetAll().Include("Contracts").Where(z => z.Label == zoneUser).SelectMany(z => z.Contracts);

            if (contracts.Count() == 0)
            {
                contractsViewModels.IsListEmpty = true;
                return View(contractsViewModels);
            }

            if (!string.IsNullOrEmpty(searchString))
                contracts = contracts.Where(c => c.Name.ToLower().Contains(searchString.ToLower()));

            contractsViewModels.Contracts = new StaticPagedList<Contract>(contracts, noPage + 1, Constants.ITEMS_PER_PAGE, total);

            return View(contractsViewModels);
        }
    }
}
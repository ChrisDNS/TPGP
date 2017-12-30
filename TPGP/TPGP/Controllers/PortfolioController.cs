using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;
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
            const int itemsPerPage = 5;
            int noPage = (page ?? 1);

            IEnumerable<Portfolio> portfolios = portfolioRepository.GetAll();
            switch (sortOrder)
            {
                default:
                    portfolios = portfolios.OrderBy(p => p.Sector);
                    break;
            }

            List<PortfolioViewModel> portfoliosViews = new List<PortfolioViewModel>();
            foreach (Portfolio p in portfolios)
                portfoliosViews.Add(new PortfolioViewModel(p));

            return View(portfoliosViews.ToPagedList(noPage, itemsPerPage));
        }

        public ActionResult Contracts(long id, int? page, string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.IsListEmpty = false;
            ViewBag.CurrentSort = sortOrder;
            const int itemsPerPage = 3;
            int noPage = (page ?? 1);

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            List<ContractViewModel> contractsViews = new List<ContractViewModel>();
            IEnumerable<Contract> contracts = contractRepository.GetAllFromPortfolio(id);
            if(contracts.Count() == 0)
            {
                ViewBag.IsListEmpty = true;
                return View(contractsViews.ToPagedList(noPage, itemsPerPage));
            }

            if (!string.IsNullOrEmpty(searchString))
                contracts = contracts.Where(c => c.Name.Contains(searchString));

            switch (sortOrder)
            {
                default:
                    contracts = contracts.OrderBy(p => p.Name);
                    break;
            }

            foreach (Contract c in contracts)
                contractsViews.Add(new ContractViewModel(c));

            return View(contractsViews.ToPagedList(noPage, itemsPerPage));
        }
    }
}
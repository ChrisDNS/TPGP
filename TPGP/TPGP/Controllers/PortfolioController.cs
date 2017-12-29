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
            const int itemsPerPage = 3;
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

        public ActionResult Contracts(int? page, long? id, string sortOrder)
        {
            const int itemsPerPage = 3;
            int noPage = (page ?? 1);

            IEnumerable<Contract> contracts = contractRepository.GetAllFromPortfolio(id);
            switch (sortOrder)
            {
                default:
                    contracts = contracts.OrderBy(p => p.Name);
                    break;
            }

            List<ContractViewModel> contractsViews = new List<ContractViewModel>();
            foreach (Contract c in contracts)
                contractsViews.Add(new ContractViewModel(c));

            return View(contractsViews.ToPagedList(noPage, itemsPerPage));
        }
    }
}
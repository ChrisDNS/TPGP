using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;
using TPGP.ViewModels;

namespace TPGP.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IContractRepository contractRepository;
        private readonly IPortfolioRepository portfolioRepository;

        public PortfolioController(IContractRepository contractRepository, IPortfolioRepository portfolioRepository)
        {
            this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
        }

        public ActionResult Index(int? page)
        {
            const int itemsPerPage = 3;
            int noPage = (page ?? 1) - 1;
            IEnumerable<Portfolio> portfolios = portfolioRepository.Pagination(noPage, itemsPerPage, out int totalCount);

            List<PortfolioViewModel> portfoliosViews = new List<PortfolioViewModel>();
            foreach (Portfolio p in portfolios)
                portfoliosViews.Add(new PortfolioViewModel(p));

            IPagedList<PortfolioViewModel> list = new StaticPagedList<PortfolioViewModel>(portfoliosViews, noPage + 1, itemsPerPage, totalCount);

            return View(list);
        }
    }
}
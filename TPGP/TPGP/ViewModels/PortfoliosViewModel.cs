using PagedList;
using TPGP.Models.Jobs;

namespace TPGP.ViewModels
{
    public class PortfoliosViewModel
    {
        public IPagedList<Portfolio> Portfolios { get; set; }

        public PortfoliosViewModel()
        {
        }
    }
}
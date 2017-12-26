using TPGP.Models.Jobs;

namespace TPGP.ViewModels
{
    public class PortfolioViewModel
    {
        public Portfolio Portfolio { get; set; }

        public PortfolioViewModel(Portfolio p)
        {
            this.Portfolio = p;
        }
    }
}
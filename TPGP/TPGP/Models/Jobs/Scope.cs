namespace TPGP.Models.Jobs
{
    public class Scope
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public long PortfolioId { get; set; }

        public Scope()
        {
        }

        public Scope(long userId, long portfolioId)
        {
            UserId = userId;
            PortfolioId = portfolioId;
        }
    }
}
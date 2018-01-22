namespace TPGP.Models.Jobs
{
    public class Scope
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public long PortfolioId { get; set; }
        public bool Initial { get; set; }

        public Scope()
        {
        }

        public Scope(long userId, long portfolioId, bool initial)
        {
            UserId = userId;
            PortfolioId = portfolioId;
            Initial = initial;
        }
    }
}
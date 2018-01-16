namespace TPGP.Models.Jobs
{
    public class Scope
    {
        public long Id { get; set; }

        public User User { get; set; }
        public Portfolio Portfolio { get; set; }

        public Scope(long id, User user, Portfolio portfolio)
        {
            Id = id;
            User = user;
            Portfolio = portfolio;
        }
    }
}
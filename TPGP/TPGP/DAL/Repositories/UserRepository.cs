using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TPGPContext ctx) : base(ctx)
        {
        }
    }
}
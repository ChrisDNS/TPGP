using System.Collections.Generic;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(TPGPContext ctx) : base(ctx)
        {
        }
    }
}

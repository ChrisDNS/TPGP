using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGP.Models.DAL.Context;
using TPGP.Models.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.Models.DAL.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(TPGPContext ctx) : base(ctx)
        {
        }
    }
}

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

        public override IEnumerable<Role> Pagination(int page, int itemsPerPage, out int totalCount) => throw new System.NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPGP.Models.DAL.Context;
using TPGP.Models.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.Models.DAL.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(TPGPContext ctx) : base(ctx)
        {
        }

        public List<Permission> GetPermissions(string username)
        {
            throw new NotImplementedException();
        }

        public Role GetUserRole(string username)
        {
            throw new NotImplementedException();
        }
    }
}
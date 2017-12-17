using System;
using System.Collections.Generic;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Repositories
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
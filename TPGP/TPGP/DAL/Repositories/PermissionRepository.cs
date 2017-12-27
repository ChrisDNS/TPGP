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
    }
}
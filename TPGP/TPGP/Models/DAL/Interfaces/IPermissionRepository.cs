using System.Collections.Generic;
using TPGP.Models.Jobs;

namespace TPGP.Models.DAL.Interfaces
{
    interface IPermissionRepository
    {
        Role GetUserRole(string username);
        List<Permission> GetPermissions(string username);
    }
}

using System.Collections.Generic;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Interfaces
{
    public interface IPermissionRepository
    {
        Role GetUserRole(string username);
        List<Permission> GetPermissions(string username);
    }
}

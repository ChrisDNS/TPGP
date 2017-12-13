using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPGP.Models
{
    public class RBACUser
    {
        public int User_Id { get; set; }
        public bool IsSysAdmin { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }

        public RBACUser(string _username)
        {
            this.Username = _username;
            this.IsSysAdmin = false;
            Role = new UserRole();
            GetDatabaseUserRolesPermissions();
        }

        private void GetDatabaseUserRolesPermissions()
        {
            //Get user roles and permissions from database tables...      
        }

        public bool HasPermission(string requiredPermission) => Role.Permissions.Where(
                                                                p => p.PermissionDescription == requiredPermission).ToList().Count > 0;

        public bool HasRole(string role) => Role.RoleName == role;
    }

    public class UserRole
    {
        public long Id { get; set; }

        public string RoleName { get; set; }
        public List<RolePermission> Permissions = new List<RolePermission>();
    }

    public class RolePermission
    {
        public long Id { get; set; }

        public string PermissionDescription { get; set; }
    }
}
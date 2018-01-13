using System.Collections.Generic;
using System.Linq;
using TPGP.Context;
using TPGP.Models.Enums;

namespace TPGP.ActionFilters
{
    public class RBACUser
    {
        public long Id { get; set; }

        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }

        public RBACUser(string username)
        {
            Username = username;
            IsAdmin = false;

            GetUserRolePermissions();
        }

        public bool HasPermission(string requiredPermission) => Role.Permissions.Where(
                                                                p => p.PermissionName == requiredPermission).ToList().Count > 0;

        public bool HasRole(Roles role) => Role.RoleName == role;

        private void GetUserRolePermissions()
        {
            using (TPGPContext ctx = new TPGPContext())
            {
                var user = ctx.Users.Where(u => u.Username == Username).FirstOrDefault();
                if (user != null)
                {
                    user.Role.Permissions = ctx.Roles.Where(r => r.Id == user.RoleId).SelectMany(r => r.Permissions).ToList();

                    Id = user.Id;
                    Role = new UserRole { Id = user.Role.Id, RoleName = user.Role.RoleName };

                    user.Role.Permissions.ToList().ForEach(p => Role.Permissions.Add(new RolePermission
                    {
                        Id = p.Id,
                        PermissionName = p.Name
                    }));

                    IsAdmin = user.Role.IsAdmin;
                }
            }
        }
    }

    public class UserRole
    {
        public long Id { get; set; }

        public Roles RoleName { get; set; }
        public List<RolePermission> Permissions = new List<RolePermission>();
    }

    public class RolePermission
    {
        public long Id { get; set; }

        public string PermissionName { get; set; }
    }
}
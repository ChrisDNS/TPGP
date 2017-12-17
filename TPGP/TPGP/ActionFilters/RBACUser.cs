using System.Collections.Generic;
using System.Linq;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.DAL.Repositories;
using TPGP.Models.Enums;
using TPGP.Models.Jobs;

namespace TPGP.ActionFilters
{
    public class RBACUser
    {
        public long Id { get; set; }

        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }

        private readonly IUserRepository userRepository = new UserRepository(new TPGPContext());
        private readonly IRoleRepository roleRepository = new RoleRepository(new TPGPContext());

        public RBACUser(string username)
        {
            this.Username = username;
            this.IsAdmin = false;

            GetUserRolePermissions();
        }

        public bool HasPermission(string requiredPermission) => Role.Permissions.Where(
                                                                p => p.PermissionName == requiredPermission).ToList().Count > 0;

        public bool HasRole(Roles role) => Role.RoleName == role;

        private void GetUserRolePermissions()
        {
            User user = userRepository.GetBy(u => u.Username == Username).First();
            Role role = roleRepository.GetById(user.RoleId);
            if (user != null && role != null)
            {
                Id = user.Id;
                UserRole userRole = new UserRole { Id = role.Id, RoleName = role.RoleName };
//                foreach (var permission in role.Permissions)
//                {
//                    userRole.Permissions.Add(new RolePermission { Id = permission.Id, PermissionName = permission.Name});
//                }

                Role = userRole;

                if (!IsAdmin)
                    IsAdmin = role.IsAdmin;
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
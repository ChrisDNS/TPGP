using System.Collections.Generic;
using System.Linq;
using TPGP.Models.DAL.Context;
using TPGP.Models.DAL.Interfaces;
using TPGP.Models.DAL.Repositories;
using TPGP.Models.Enums;
using TPGP.Models.Jobs;

namespace TPGP.Models
{
    public class RBACUser
    {
        public long Id { get; set; }

        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }

        private readonly IUserRepository userRepository;

        public RBACUser(string username)
        {
            this.Username = username;
            this.IsAdmin = false;

            userRepository = new UserRepository(new TPGPContext());

            GetUserRolePermissions();
        }

        public bool HasPermission(string requiredPermission) => Role.Permissions.Where(
                                                                p => p.PermissionName == requiredPermission).ToList().Count > 0;

        public bool HasRole(Roles role) => Role.RoleName == role;

        private void GetUserRolePermissions()
        {
            User user = userRepository.GetBy(u => u.Username == Username).FirstOrDefault();
            if (user != null)
            {
                Id = user.Id;
                UserRole userRole = new UserRole { Id = user.Role.Id, RoleName = user.Role.RoleName };
                foreach (var permission in user.Role.Permissions)
                {
                    userRole.Permissions.Add(new RolePermission { Id = permission.Id, PermissionName = permission.Name});
                }

                Role = userRole;

                if (!IsAdmin)
                    IsAdmin = user.Role.IsAdmin;
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
using System.Collections.Generic;
using System.Data.Entity;
using TPGP.Context;
using TPGP.Models.Enums;
using TPGP.Models.Jobs;

namespace TPGP
{
    public class TPGPDbInitializer : DropCreateDatabaseAlways<TPGPContext>
    {
        public TPGPDbInitializer()
        {
        }

        protected override void Seed(TPGPContext context)
        {
            IList<Permission> permissions = new List<Permission>
            {
                new Permission { Name = "admin" }
            };
            IList<Role> roles = new List<Role>
            {
                new Role { RoleName = Roles.ADMIN, IsAdmin = true, Permissions = permissions },
                new Role { RoleName = Roles.COLLABORATOR, IsAdmin = false }
            };
            IList<User> users = new List<User>
            {
                new User { Username = "Sarra", Firstname = "Sarra", Lastname = "Sarra", Email = "sarra@sarra.sarra", Role = roles[0] },
                new User { Username = "Pierre", Firstname = "Pierre", Lastname = "Pierre", Email = "pierre@pierre.pierre", Role = roles[1] }
            };

            foreach (var role in roles)
                context.Roles.Add(role);

            foreach (var user in users)
                context.Users.Add(user);

            foreach (var permission in permissions)
                context.Permissions.Add(permission);

            base.Seed(context);
        }
    }
}
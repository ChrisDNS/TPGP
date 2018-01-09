using System;
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

            IList<Portfolio> portfolios = new List<Portfolio>
            {
                new Portfolio { Sector = "Health"},
                new Portfolio { Sector = "Transport"},
                new Portfolio { Sector = "Automobile"},
                new Portfolio { Sector = "Pharmaceutical Industry"},
                new Portfolio { Sector = "Textile/Dressing/Shoe"},
                new Portfolio { Sector = "Communication and Multimedia"},
                new Portfolio { Sector = "Studies and advices"},
                new Portfolio { Sector = "Metalworking Industry"},
                new Portfolio { Sector = "Chemistry and Parachemistry"},
                new Portfolio { Sector = "Bank/Insurance"},
                new Portfolio { Sector = "Food-processing Industry"}
            };

            IList<Contract> contracts = new List<Contract>
            {
                new Contract { Name = "Contract-1", InitDate = DateTime.Now, Bonus = 125.445, Portfolio = portfolios[0] },
                new Contract { Name = "Contract-2", InitDate = DateTime.Now, Bonus = 1201.45, Portfolio = portfolios[0] },
                new Contract { Name = "Contract-3", InitDate = DateTime.Now, Bonus = 201.15, Portfolio = portfolios[1] },
                new Contract { Name = "Contract-4", InitDate = DateTime.Now, Bonus = 2221.74, Portfolio = portfolios[2] }
            };

            GeographicalZone world = new GeographicalZone
            {
                Label = "World",
                Parent = null
            };

            IList<GeographicalZone> continents = new List<GeographicalZone>
            {
                new GeographicalZone { Label = "Europe", Parent = world },
                new GeographicalZone { Label = "Asie", Parent = world },
                new GeographicalZone { Label = "Océanie", Parent = world },
                new GeographicalZone { Label = "Afrique", Parent = world },
                new GeographicalZone { Label = "Amérique", Parent = world },
                new GeographicalZone { Label = "Antarctique", Parent = world }
            };

            IList<GeographicalZone> countries = new List<GeographicalZone>
            {
                new GeographicalZone { Label = "France", Parent = continents[0] },
                new GeographicalZone { Label = "Chine", Parent = continents[1] },
                new GeographicalZone { Label = "Algérie", Parent = continents[3] },
                new GeographicalZone { Label = "Canada", Parent = continents[4] },
            };

            foreach (var role in roles)
                context.Roles.Add(role);

            foreach (var user in users)
                context.Users.Add(user);

            foreach (var permission in permissions)
                context.Permissions.Add(permission);

            foreach (var portfolio in portfolios)
                context.Portfolios.Add(portfolio);

            foreach (var contract in contracts)
                context.Contracts.Add(contract);

            /**geographical zones**/
            context.Zones.Add(world);

            foreach (var continent in continents)
                context.Zones.Add(continent);

            //foreach (var country in countries)
              //  context.Zones.Add(country);

            base.Seed(context);
        }
    }
}
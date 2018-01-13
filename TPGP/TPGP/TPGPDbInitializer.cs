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
        public TPGPDbInitializer() : base()
        {
        }

        protected override void Seed(TPGPContext context)
        {
            IList<Permission> adminPermissions = new List<Permission>
            {
                new Permission { Name = "Admin-Index" },
                new Permission { Name = "Admin-Edit" },
                new Permission { Name = "Admin-Save" }
            };

            IList<Permission> collaboratorPermissions = new List<Permission>
            {
                new Permission { Name = "Portfolio-Index" }
            };

            IList<Permission> subscriberPermissions = new List<Permission>
            {
                new Permission { Name = "Portfolio-Index" },
                new Permission { Name = "Portfolio-Contracts" },
                new Permission { Name = "Contract-Index" },
                new Permission { Name = "Contract-Details" },
                new Permission { Name = "Contract-Create" },
                new Permission { Name = "Contract-Save" }
            };

            IList<Role> roles = new List<Role>
            {
                new Role { RoleName = Roles.ADMIN, IsAdmin = true, Permissions = adminPermissions },
                new Role { RoleName = Roles.COLLABORATOR, IsAdmin = false, Permissions = collaboratorPermissions },
                new Role { RoleName = Roles.SUBSCRIBER, IsAdmin = false, Permissions = subscriberPermissions }
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
                new Contract { Name = "Contract-1", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(214), Bonus = 125.445, Portfolio = portfolios[0] },
                new Contract { Name = "Contract-2", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(546), Bonus = 1201.45, Portfolio = portfolios[0] },
                new Contract { Name = "Contract-3", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(111), Bonus = 201.15, Portfolio = portfolios[1] },
                new Contract { Name = "Contract-4", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2221.74, Portfolio = portfolios[2] },
                new Contract { Name = "Contract-5", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2221.74, Portfolio = portfolios[3] },
                new Contract { Name = "Contract-6", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2221.74, Portfolio = portfolios[4] },
                new Contract { Name = "Contract-7", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2121.74, Portfolio = portfolios[4] },
                new Contract { Name = "Contract-8", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2221.74, Portfolio = portfolios[4] },
                new Contract { Name = "Contract-123", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 231.74, Portfolio = portfolios[4] },
                new Contract { Name = "Contract-10", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2781.74, Portfolio = portfolios[4] },
                new Contract { Name = "Contract-11", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(141), Bonus = 27961.74, Portfolio = portfolios[4] },
                new Contract { Name = "Contract-12", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(167), Bonus = 24541.74, Portfolio = portfolios[5] },
                new Contract { Name = "Contract-13", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(896), Bonus = 24521.74, Portfolio = portfolios[6] },
                new Contract { Name = "Contract-14", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(855), Bonus = 78921.74, Portfolio = portfolios[1] },
                new Contract { Name = "Contract-15", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(201), Bonus = 67.74, Portfolio = portfolios[2] },
                new Contract { Name = "Contract-16", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(104), Bonus = 45221.74, Portfolio = portfolios[3] },
                new Contract { Name = "Contract-17", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(758), Bonus = 7921.78, Portfolio = portfolios[1] },
                new Contract { Name = "Contract-18", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(459), Bonus = 2221.74, Portfolio = portfolios[2] },
                new Contract { Name = "Contract-19", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(566), Bonus = 251.74, Portfolio = portfolios[6] },
                new Contract { Name = "Contract-20", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(764), Bonus = 2421.74, Portfolio = portfolios[10] },
                new Contract { Name = "Contract-21", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(76), Bonus = 621.71, Portfolio = portfolios[7] },
                new Contract { Name = "Contract-22", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(766), Bonus = 621.74, Portfolio = portfolios[6] },
                new Contract { Name = "Contract-23", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(777), Bonus = 91.94, Portfolio = portfolios[9] },
                new Contract { Name = "Contract-24", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7897), Bonus = 281.74, Portfolio = portfolios[9] },
                new Contract { Name = "Contract-25", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(765), Bonus = 278.74, Portfolio = portfolios[8] },
                new Contract { Name = "Contract-26", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1174), Bonus = 291.74, Portfolio = portfolios[9] }
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

            IList<User> users = new List<User>
            {
                new User { Username = "Sarra", Firstname = "Sarra", Lastname = "Sarra", Email = "sarra@sarra.sarra", Zone = countries[0], Role = roles[0] },
                new User { Username = "Pierre", Firstname = "Pierre", Lastname = "Pierre", Email = "pierre@pierre.pierre", Zone = countries[0], Role = roles[1] },
                new User { Username = "Sidi", Firstname = "Sidi", Lastname = "Sidi", Email = "sidi@sid.sidi", Zone = countries[3], Role = roles[2] }
            };

            foreach (var user in users)
                context.Users.Add(user);

            foreach (var portfolio in portfolios)
                context.Portfolios.Add(portfolio);

            foreach (var contract in contracts)
                context.Contracts.Add(contract);

            /**geographical zones**/
            context.Zones.Add(world);

            foreach (var continent in continents)
                context.Zones.Add(continent);

            foreach (var country in countries)
                context.Zones.Add(country);

            base.Seed(context);
        }
    }
}
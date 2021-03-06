﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using TPGP.Context;
using TPGP.Models.Enums;
using TPGP.Models.Jobs;

namespace TPGP
{
    public sealed class TPGPDbInitializer : DropCreateDatabaseAlways<TPGPContext>
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
                new Permission { Name = "Admin-Save" },
                new Permission { Name = "Admin-Download" },

                new Permission { Name = "Account-Index" }
            };

            IList<Permission> collaboratorPermissions = new List<Permission>
            {
                new Permission { Name = "Portfolio-Index" },

                new Permission { Name = "Account-Index" },
                new Permission { Name = "Account-ChangeStatus" },
                new Permission { Name = "Account-Download" }
            };

            IList<Permission> subscriberPermissions = new List<Permission>
            {
                new Permission { Name = "Portfolio-Index" },
                new Permission { Name = "Portfolio-Contracts" },

                new Permission { Name = "Contract-Index" },
                new Permission { Name = "Contract-Details" },
                new Permission { Name = "Contract-Create" },
                new Permission { Name = "Contract-Save" },
                new Permission { Name = "Contract-Edit" },
                new Permission { Name = "Contract-Update" },

                new Permission { Name = "Account-Index" },
                new Permission { Name = "Account-ChangeStatus" },
                new Permission { Name = "Account-Download" }
            };

            IList<Permission> managerPermissions = new List<Permission>
            {
                new Permission { Name = "Portfolio-Index" },
                new Permission { Name = "Portfolio-Contracts" },

                new Permission { Name = "Contract-Index" },
                new Permission { Name = "Contract-Details" },

                new Permission { Name = "Account-Index" },
                new Permission { Name = "Account-ChangeStatus" },
                new Permission { Name = "Account-Download" }
            };

            IList<Role> roles = new List<Role>
            {
                new Role { RoleName = Roles.ADMIN, IsAdmin = true, Permissions = adminPermissions },
                new Role { RoleName = Roles.COLLABORATOR, IsAdmin = false, Permissions = collaboratorPermissions },
                new Role { RoleName = Roles.SUBSCRIBER, IsAdmin = false, Permissions = subscriberPermissions },
                new Role { RoleName = Roles.MANAGER, IsAdmin = false, Permissions = managerPermissions }
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
                new GeographicalZone { Label = "Allemagne", Parent = continents[0] },
                new GeographicalZone { Label = "Chine", Parent = continents[1] },
                new GeographicalZone { Label = "Algérie", Parent = continents[3] },
                new GeographicalZone { Label = "Canada", Parent = continents[4] },
                new GeographicalZone { Label = "Pologne", Parent = continents[0] },
                new GeographicalZone { Label = "Hollande", Parent = continents[0] },
                new GeographicalZone { Label = "Luxembourg", Parent = continents[0] },
                new GeographicalZone { Label = "Autriche", Parent = continents[0] },
                new GeographicalZone { Label = "Bolivie", Parent = continents[4] },
                new GeographicalZone { Label = "Pérou", Parent = continents[4] },
                new GeographicalZone { Label = "Argentine", Parent = continents[4] },
                new GeographicalZone { Label = "Brésil", Parent = continents[4] },
                new GeographicalZone { Label = "Japon", Parent = continents[1] },
                new GeographicalZone { Label = "Roumanie", Parent = continents[0] },
                new GeographicalZone { Label = "Suède", Parent = continents[0] },
                new GeographicalZone { Label = "Cambodge", Parent = continents[1] }
            };

            IList<GeographicalZone> countriesContracts1 = new List<GeographicalZone>();

            countriesContracts1.Add(countries[0]);

            IList<Contract> contracts = new List<Contract>
            {
                new Contract { Name = "Contract-1", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(214), Bonus = 125.445, Company = "Nike", Portfolio = portfolios[0], Zones = countriesContracts1 },
                new Contract { Name = "Contract-2", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(546), Bonus = 1201.45, Company = "Auchan", Portfolio = portfolios[0] },
                new Contract { Name = "Contract-3", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(111), Bonus = 201.15, Company = "Smarties", Portfolio = portfolios[1] },
                new Contract { Name = "Contract-4", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2221.74, Company = "Playboy", Portfolio = portfolios[3] },
                new Contract { Name = "Contract-5", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2221.74, Company = "Monsanto", Portfolio = portfolios[3] },
                new Contract { Name = "Contract-6", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2221.74, Company = "Adidas", Portfolio = portfolios[4] },
                new Contract { Name = "Contract-7", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2121.74, Company = "Ellesse", Portfolio = portfolios[4] },
                new Contract { Name = "Contract-8", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2221.74, Company = "Fila", Portfolio = portfolios[4] },
                new Contract { Name = "Contract-9", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 231.74, Company = "EDF", Portfolio = portfolios[4] },
                new Contract { Name = "Contract-10", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(799), Bonus = 2781.74, Company = "Engie", Portfolio = portfolios[4] },
                new Contract { Name = "Contract-11", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(141), Bonus = 27961.74, Company = "General Motors", Portfolio = portfolios[4] },
                new Contract { Name = "Contract-12", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(167), Bonus = 24541.74, Company = "BNP", Portfolio = portfolios[5] },
                new Contract { Name = "Contract-13", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(896), Bonus = 24521.74, Company = "Nokia", Portfolio = portfolios[6] },
                new Contract { Name = "Contract-14", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(855), Bonus = 78921.74, Company = "Apple", Portfolio = portfolios[1] },
                new Contract { Name = "Contract-15", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(201), Bonus = 67.74, Company = "Samsung", Portfolio = portfolios[7] },
                new Contract { Name = "Contract-16", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(104), Bonus = 45221.74, Company = "Wiko", Portfolio = portfolios[3] },
                new Contract { Name = "Contract-17", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(758), Bonus = 7921.78, Company = "Mercedes", Portfolio = portfolios[1] },
                new Contract { Name = "Contract-18", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(459), Bonus = 2221.74, Company = "BMW", Portfolio = portfolios[9] },
                new Contract { Name = "Contract-19", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(566), Bonus = 251.74, Company = "Porsche", Portfolio = portfolios[6] },
                new Contract { Name = "Contract-20", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(764), Bonus = 2421.74, Company = "Koenigsegg", Portfolio = portfolios[10] },
                new Contract { Name = "Contract-21", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(76), Bonus = 621.71, Company = "Seat", Portfolio = portfolios[7] },
                new Contract { Name = "Contract-22", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(766), Bonus = 621.74, Company = "Sanofi", Portfolio = portfolios[6] },
                new Contract { Name = "Contract-23", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(777), Bonus = 91.94, Company = "Lindt & Sprüngli", Portfolio = portfolios[9] },
                new Contract { Name = "Contract-24", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7897), Bonus = 281.74, Company = "Google", Portfolio = portfolios[9] },
                new Contract { Name = "Contract-25", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(765), Bonus = 278.94, Company = "Yahoo", Portfolio = portfolios[8] },
                new Contract { Name = "Contract-26", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(117), Bonus = 291.74, Company = "Nvidia", Portfolio = portfolios[9] },
                new Contract { Name = "Contract-27", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(174), Bonus = 291.74, Company = "AMD", Portfolio = portfolios[2] },
                new Contract { Name = "Contract-28", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(114), Bonus = 231.78, Company = "BitFenix", Portfolio = portfolios[10] },
                new Contract { Name = "Contract-29", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(174), Bonus = 391.74, Company = "Aston Martin", Portfolio = portfolios[1] },
                new Contract { Name = "Contract-30", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1174), Bonus = 291.74, Company = "Michelin", Portfolio = portfolios[0] },
                new Contract { Name = "Contract-31", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(119), Bonus = 291.74, Company = "Norauto", Portfolio = portfolios[3] },
                new Contract { Name = "Contract-32", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1178), Bonus = 2471.24, Company = "Leclerc", Portfolio = portfolios[4] },
                new Contract { Name = "Contract-33", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(174), Bonus = 491.74, Company = "Serpentard", Portfolio = portfolios[5] },
                new Contract { Name = "Contract-34", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1174), Bonus = 291.74, Company = "UHU", Portfolio = portfolios[5] },
                new Contract { Name = "Contract-35", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(164), Bonus = 2911.74, Company = "Ferrero Rocher", Portfolio = portfolios[5] },
                new Contract { Name = "Contract-36", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2174), Bonus = 91.784, Company = "Nutella", Portfolio = portfolios[6] },
                new Contract { Name = "Contract-37", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1074), Bonus = 2951.74, Company = "Ford", Portfolio = portfolios[9] },
                new Contract { Name = "Contract-38", InitDate = DateTime.Now, EndDate = DateTime.Now.AddDays(114), Bonus = 2991.741, Company = "Sony", Portfolio = portfolios[9] }
            };

            IList<User> users = new List<User>
            {
                new User { Username = "Sarra", Firstname = "Sarra", Lastname = "Sarra", Email = "sarra@sarra.sarra", Zone = countries[0], Role = roles[0] },
                new User { Username = "Pierre", Firstname = "Pierre", Lastname = "Pierre", Email = "pierre@pierre.pierre", Zone = countries[1], Role = roles[1] },
                new User { Username = "Sidi", Firstname = "Sidi", Lastname = "Sidi", Email = "sidi@sidi.sidi", Zone = countries[4], Role = roles[2] },
                new User { Username = "Sidi2", Firstname = "Sidi2", Lastname = "Sidi2", Email = "sidi2@sidi2.sidi2", Zone = countries[2], Role = roles[2] },
                new User { Username = "Chris", Firstname = "Chris", Lastname = "Chris", Email = "chris@chris.chris", Zone = countries[1], Role = roles[3] }
            };

            IList<Scope> scopes = new List<Scope>
            {
                new Scope { UserId = 3, PortfolioId = 1, Initial = true },
                new Scope { UserId = 3, PortfolioId = 3, Initial = false },
                new Scope { UserId = 3, PortfolioId = 2, Initial = false },
                new Scope { UserId = 3, PortfolioId = 4, Initial = false },
                new Scope { UserId = 3, PortfolioId = 5, Initial = false },

                new Scope { UserId = 5, PortfolioId = 1, Initial = true },
                new Scope { UserId = 5, PortfolioId = 10, Initial = false },
                new Scope { UserId = 5, PortfolioId = 9, Initial = false }
            };

            foreach (var user in users)
                context.Users.Add(user);

            foreach (var portfolio in portfolios)
                context.Portfolios.Add(portfolio);

            foreach (var scope in scopes)
                context.Scopes.Add(scope);

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
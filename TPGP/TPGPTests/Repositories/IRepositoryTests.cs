using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.DAL.Repositories;
using TPGP.Models.Jobs;

namespace TPGPTests.Repositories
{
    [TestClass]
    public class IRepositoryTests
    {
        [TestMethod]
        public void GetById_ListOf4Contracts_ReturnsTrue()
        {
            var mock = GetFakeContractRepository();

            Assert.IsNotNull(mock.GetById(1));
        }

        [TestMethod]
        public void Insert_ListOf4Contracts_Returns4ForTheListSize()
        {
            var mock = GetFakeContractRepository();
            mock.Insert(new Contract { Name = "Contract-2", InitDate = DateTime.Now, Bonus = 121.445, Portfolio = null });
            mock.Insert(new Contract { Name = "Contract-1", InitDate = DateTime.Now, Bonus = 126.445, Portfolio = null });
            mock.Insert(new Contract { Name = "Contract-3", InitDate = DateTime.Now, Bonus = 129.145, Portfolio = null });
            mock.Insert(new Contract { Name = "Contract-4", InitDate = DateTime.Now, Bonus = 123.445, Portfolio = null });

            Assert.IsTrue(mock.GetAll().ToList().Count == 4);
        }

        private static IContractRepository GetFakeContractRepository()
        {
            var contracts = new List<Contract>();
            var mock = new Mock<IContractRepository>();
            mock.Setup(x => x.Insert(It.IsAny<Contract>())).Callback((Contract c) =>
            {
                contracts.Add(c);
            });
            mock.Setup(x => x.GetAll()).Returns(contracts);

            return mock.Object;
        }
    }
}
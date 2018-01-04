using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPGP.Controllers;
using System.Web.Mvc;

namespace TPGPTests.ContractControllerTests
{
    [TestClass]
    public class ContractControllerTests
    {
        [TestMethod]
        public void Details_ReturnViewResult(long id)
        {
            ContractController cc = new ContractController();
            ViewResult vr = cc.Details(id) as ViewResult;

            Assert.IsNotNull(vr);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Routing;
using TPGP;

namespace TPGPTests.RouteTests
{
    [TestClass]
    public class RouteTester
    {
        [TestMethod]
        public void Routes_HomePage_ReturnHomeControllerAndIndexMethod()
        {
            RouteData routeData = CreateURL("~/");
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            //Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        private static RouteData CreateURL(string url)
        {
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns(url);
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteData routeData = routes.GetRouteData(mockContext.Object);

            return routeData;
        }
    }
}

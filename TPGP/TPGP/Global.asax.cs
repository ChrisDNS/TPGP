using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TPGP.App_Start;
using Unity;

namespace TPGP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterTypes(new UnityContainer());
            ViewEngines.Engines.Add(new RazorViewEngine());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TPGP.DAL.Interfaces;
using TPGP.DAL.Repositories;
using TPGP.Models.DAL.Repositories;
using TPGPServices.App_Start;
using Unity;
using Unity.Lifetime;

namespace TPGPServices
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Register(GlobalConfiguration.Configuration);
           
        }

        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IContractRepository, ContractRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPortfolioRepository, PortfolioRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoleRepository, RoleRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolv(container);
        }
    }
}

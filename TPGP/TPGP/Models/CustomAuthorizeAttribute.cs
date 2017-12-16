using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using TPGP.Models;

namespace LDAP
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                                                                 filterContext.ActionDescriptor.ActionName);
            RBACUser authUser = new RBACUser(filterContext.RequestContext.HttpContext.User.Identity.Name);
            if (/*!authUser.HasPermission(requiredPermission) & */!authUser.IsAdmin)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "action", "Index" },
                    { "controller", "Unauthorized" }
                });
            }
        }
    }
}
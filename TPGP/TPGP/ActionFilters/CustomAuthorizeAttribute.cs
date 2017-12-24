using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TPGP.ActionFilters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                                                                 filterContext.ActionDescriptor.ActionName);

            RBACUser authUser = new RBACUser(HttpContext.Current.Request.Cookies["user"]["username"]);
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
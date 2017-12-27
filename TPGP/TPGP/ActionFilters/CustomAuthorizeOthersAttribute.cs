using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TPGP.ActionFilters
{
    public class CustomAuthorizeOthersAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                                                                 filterContext.ActionDescriptor.ActionName);

            string username = null;
            RBACUser authUser = null;
            if (HttpContext.Current.Session["username"] != null)
            {
                username = HttpContext.Current.Session["username"].ToString();
                authUser = new RBACUser(username);
                if (/*!authUser.HasPermission(requiredPermission) & */authUser.IsAdmin)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "action", "Index" },
                        { "controller", "Unauthorized" }
                    });
                }
            }
            else
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
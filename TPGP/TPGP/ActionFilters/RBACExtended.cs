using System.Web;
using System.Web.Mvc;
using TPGP.Models.Enums;

namespace TPGP.ActionFilters
{
    public static class RBACExtended
    {
        public static bool HasRole(this ControllerBase controller, Roles role)
        {
            return new RBACUser(HttpContext.Current.Session["username"].ToString()).HasRole(role);
        }

        public static bool HasPermission(this ControllerBase controller, string permission)
        {
            return new RBACUser(HttpContext.Current.Session["username"].ToString()).HasPermission(permission);
        }

        public static bool IsAdmin(this ControllerBase controller)
        {
            return new RBACUser(HttpContext.Current.Session["username"].ToString()).IsAdmin;
        }
    }
}
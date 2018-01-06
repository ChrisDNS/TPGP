using System.Web;
using System.Web.Optimization;

namespace TPGP.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/scripts/jquery-ui-1.12.1.js"));
            bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
                   "~/Content/themes/base/jquery-ui.css"));
        }
    }
}
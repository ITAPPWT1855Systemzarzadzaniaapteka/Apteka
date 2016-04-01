using System.Web;
using System.Web.Optimization;

namespace Apteka {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/js/dist").Include(
                        "~/Scripts/lib/jquery/*.js",
                        "~/Scripts/lib/*.js",                        
                        "~/Scripts/*.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/css/dist").Include("~/Content/lib/*.css"));

            bundles.Add(new LessBundle("~/bundles/less").Include(
                    "~/Content/less/*.less"
            ));

            #region login
            bundles.Add(new ScriptBundle("~/bundles/login/js").Include(
                        "~/Scripts/lib/jquery/jquery.min.js",
                        "~/Scripts/lib/bootstrap.min.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/login/css").Include(
                      "~/Content/lib/bootstrap.min.css"
            ));

            bundles.Add(new LessBundle("~/bundles/login/less").Include(
                      "~/Content/less/login/*.less"
            ));
            #endregion

            //BundleTable.EnableOptimizations = true;
        }
    }
}

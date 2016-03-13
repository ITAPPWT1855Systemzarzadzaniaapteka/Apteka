using System.Web;
using System.Web.Optimization;

namespace Apteka {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/login/js").Include(
                        "~/bower_components/jquery/dist/jquery.min.js",
                        "~/bower_components/bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/login/css").Include(
                      "~/bower_components/bootstrap/dist/css/bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js/dist").Include(
                        "~/bower_components/jquery/dist/jquery.min.js",
                        "~/bower_components/bootstrap/dist/js/bootstrap.min.js",
                        "~/bower_components/datatables/media/js/jquery.dataTables.min.js",
                        "~/bower_components/datatables-responsive/js/datatables.responsive.js",
                        "~/bower_components/metisMenu/dist/metisMenu.min.js",
                        "~/bower_components/jquery-validation/dist/jquery.validate.min.js",
                        "~/Scripts/sb-admin-2.js"));

            bundles.Add(new StyleBundle("~/bundles/css/dist").Include(
                      "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/bower_components/metisMenu/dist/metisMenu.min.css",
                      "~/bower_components/datatables/dist/dataTables.bootstrap.min.css",
                      "~/bower_components/datatables/dist/jquery.dataTables.min.css",
                      "~/Content/sb-admin-2.css")
                      .Include("~/bower_components/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));
        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace Apteka {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/js/dist").Include(
                        "~/bower_components/jquery/dist/jquery.min.js",
                        "~/bower_components/requirejs/require.js",
                        "~/bower_components/bootstrap/dist/js/bootstrap.min.js",
                        "~/bower_components/datatables/media/js/jquery.dataTables.min.js",
                        "~/bower_components/datatables-responsive/js/datatables.responsive.js",
                        "~/bower_components/metisMenu/dist/metisMenu.min.js",
                        "~/bower_components/modernizr/src/modernizr-*",
                        "~/bower_components/angular/angular.min.js",
                        "~/bower_components/angular-route/angular-route.min.js", 
                        "~/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.js",
                        "~/bower_components/jquery-autocomplete/jquery.autocomplete.js",
                        //"~/bower_components/jquery-validation/dist/jquery.validate.min.js",
                        "~/Scripts/*.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/css/dist").Include(
                      "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/bower_components/metisMenu/dist/metisMenu.min.css",
                      "~/bower_components/datatables/dist/dataTables.bootstrap.min.css",
                      "~/bower_components/datatables/dist/jquery.dataTables.min.css",
                      "~/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.css",
                        "~/bower_components/jquery-autocomplete/autocomplete.css")
                      .Include("~/bower_components/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()
            ));

            bundles.Add(new LessBundle("~/bundles/less").Include(
                    "~/Content/less/*.less"
            ));

            #region datetimepicker
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
          "~/Scripts/bootstrap-datetimepicker.min.js"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                      "~/Content/bootstrap-datetimepicker.min.css"));
            #endregion
            #region login
            bundles.Add(new ScriptBundle("~/bundles/login/js").Include(
                        "~/bower_components/jquery/dist/jquery.min.js",
                        "~/bower_components/bootstrap/dist/js/bootstrap.min.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/login/css").Include(
                      "~/bower_components/bootstrap/dist/css/bootstrap.min.css"
            ));

            bundles.Add(new LessBundle("~/bundles/login/less").Include(
                      "~/Content/less/login/*.less"
            ));
            #endregion

            //BundleTable.EnableOptimizations = true;
        }
    }
}

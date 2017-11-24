using System.Web;
using System.Web.Optimization;

namespace LeXPro
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",                     
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
            "~/Scripts/jquery.uniform.js"
            , "~/Scripts/chosen.jquery.js"
            , "~/Scripts/moment.js"
            , "~/Scripts/bootstrap-multiselect.js"
            , "~/Scripts/bootstrap-datetimepicker.js"));
            bundles.Add(new StyleBundle("~/Content/vendors").Include(
            "~/Content/bootstrap-datetimepicker.css"
           , "~/Content/bootstrap-multiselect.css"
           , "~/Content/datepicker.fixes.css"
           , "~/Content/uniform.default.min.css"
           , "~/Contentuniform.default.fixes.css"
           , "~/Content/chosen.min.css"
           ));
        }
    }
}

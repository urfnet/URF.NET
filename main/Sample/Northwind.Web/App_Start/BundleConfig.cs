using System.Web.Optimization;

namespace Northwind.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-migrate-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      "~/Scripts/angular.min.js",
                      "~/Scripts/kendo/2014.1.318/kendo.web.min.js",
                      "~/Scripts/angular-kendo.min.js",
                      "~/Scripts/app/app.js"));
            
            //Styles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                      "~/Content/kendo/2014.1.318/kendo.common.min.css",
                      "~/Content/kendo/2014.1.318/kendo.bootstrap.min.css"));

            // Set EnableOptimizations to false for debugging. For more information, visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
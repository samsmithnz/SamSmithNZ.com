using System.Web;
using System.Web.Optimization;

namespace SamSmithNZ2017
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/globalJS").Include(
                        "~/Scripts/3rdParty/jquery-{version}.js",
                        "~/Scripts/3rdParty/jquery.validate*",
                        "~/Scripts/3rdParty/modernizr-*",
                        "~/Scripts/3rdParty/bootstrap.js",
                        "~/Scripts/3rdParty/respond.js",
                        "~/Scripts/3rdParty/angular.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/servicesJS").IncludeDirectory(
                        "~/Scripts/Services/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/controllersJS").IncludeDirectory(
                        "~/Scripts/Controllers/", "*.js", searchSubdirectories: true
                ));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/3rdParty/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/3rdParty/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/3rdParty/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/3rdParty/bootstrap.js",
            //          "~/Scripts/3rdParty/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}

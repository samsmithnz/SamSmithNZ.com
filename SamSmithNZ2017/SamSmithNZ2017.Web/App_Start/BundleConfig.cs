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
                        "~/Scripts/3rdParty/angular.js",
                        "~/Scripts/3rdParty/moment.js",
                        "~/Scripts/3rdParty/angular-moment.js",
                        "~/Scripts/3rdParty/angular-sanitize.js",
                        "~/Scripts/app.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/steamServicesJS").IncludeDirectory(
                        "~/Scripts/SteamServices/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/steamControllersJS").IncludeDirectory(
                        "~/Scripts/SteamControllers/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/guitarTabServicesJS").IncludeDirectory(
                        "~/Scripts/GuitarTabServices/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/guitarTabControllersJS").IncludeDirectory(
                        "~/Scripts/GuitarTabControllers/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/iTunesServicesJS").IncludeDirectory(
                        "~/Scripts/ITunesServices/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/iTunesControllersJS").IncludeDirectory(
                        "~/Scripts/ITunesControllers/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/fooFightersServicesJS").IncludeDirectory(
                        "~/Scripts/FooFightersServices/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/fooFightersControllersJS").IncludeDirectory(
                        "~/Scripts/FooFightersControllers/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/intFootballServicesJS").IncludeDirectory(
                        "~/Scripts/IntFootballServices/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/intFootballControllersJS").IncludeDirectory(
                        "~/Scripts/IntFootballControllers/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/mandMCounterServicesJS").IncludeDirectory(
                        "~/Scripts/MandMCounterServices/", "*.js", searchSubdirectories: true
                ));

            bundles.Add(new ScriptBundle("~/bundles/mandMCounterControllersJS").IncludeDirectory(
                        "~/Scripts/MandMCounterControllers/", "*.js", searchSubdirectories: true
                ));


            

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/3rdParty/modernizr-*"));

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

            bundles.Add(new StyleBundle("~/Content/globalCSS").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}

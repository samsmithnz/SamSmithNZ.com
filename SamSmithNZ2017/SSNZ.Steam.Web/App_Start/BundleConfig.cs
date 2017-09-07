using System.Web;
using System.Web.Optimization;

namespace SSNZ.Steam.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
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

            bundles.Add(new StyleBundle("~/Content/globalCSS").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/site.css"));
        }
    }
}

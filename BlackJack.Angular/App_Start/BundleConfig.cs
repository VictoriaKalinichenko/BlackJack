using System.Web.Optimization;

namespace BlackJack.Angular
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/libs/runtime.js",
                "~/Scripts/libs/polyfills.js",
                "~/Scripts/libs/styles.js",
                "~/Scripts/libs/vendor.js",
                "~/Scripts/libs/main.js",
                "~/Scripts/libs/common.js",
                "~/Scripts/libs/app-authorized-user-authorized-user-game-game-module.js",
                "~/Scripts/libs/app-authorized-user-authorized-user-module.js"));
        }
    }
}

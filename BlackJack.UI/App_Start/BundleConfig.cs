using System.Web.Optimization;

namespace BlackJack.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/KendoUI/css").Include(
                "~/Content/kendoui/styles/kendo.common.min.css",
                "~/Content/kendoui/styles/kendo.default.min.css",
                "~/Content/kendoui/styles/kendo.default.mobile.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/KendoUI/js").Include(
                "~/Scripts/kendoui/js/jquery.min.js",
                "~/Scripts/kendoui/js/kendo.all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/KendoUI/GameHistory").Include(
                "~/Scripts/GameHistory/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/RoundHandler").Include(
                "~/Scripts/Start/initialize.js"));
        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace UniAppSpel
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/UniAppSpelApp")
                   .IncludeDirectory("~/Scripts/Controllers", "*.js")
                   .Include("~/Scripts/UniAppSpelApp.js"));

            bundles.Add(new ScriptBundle("~/bundles/UniAppSpelServices")
                .IncludeDirectory("~/Scripts/Services", "*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
               "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
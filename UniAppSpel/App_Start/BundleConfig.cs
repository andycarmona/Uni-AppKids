using System.Web;
using System.Web.Optimization;

namespace UniAppSpel
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {           
            bundles.Add(new ScriptBundle("~/bundles/UniAppSpelServices")
                .Include("~/Scripts/Services/userService.js"));

            bundles.Add(new ScriptBundle("~/bundles/UniAppSpelApp")
                   .IncludeDirectory("~/Scripts/Controllers", "*.js")
                   .Include("~/Scripts/UniAppSpelApp.js"));



            bundles.Add(new ScriptBundle("~/bundles/Scripts")
          .IncludeDirectory("~/Scripts", "*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap")
                .Include(
               "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-responsive.min.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
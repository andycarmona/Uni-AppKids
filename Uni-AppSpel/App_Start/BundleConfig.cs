﻿using System.Web;
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
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
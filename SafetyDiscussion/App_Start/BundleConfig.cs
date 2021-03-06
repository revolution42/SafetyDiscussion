﻿using System.Web;
using System.Web.Optimization;

namespace PrototypeApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            var siteScriptBundle = new ScriptBundle("~/bundles/site-js");
            siteScriptBundle.Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-animate.min.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                      "~/Scripts/respond.js");
            siteScriptBundle.IncludeDirectory("~/Scripts/app/", "*.js");

            bundles.Add(siteScriptBundle);
        
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}

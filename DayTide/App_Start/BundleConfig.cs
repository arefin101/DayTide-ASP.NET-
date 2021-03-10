using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace DayTide.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/css")
                .IncludeDirectory("~/bundle/css", "*.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
                    .IncludeDirectory("~/Scripts/", "*.js", true));

            //the following creates bundles in debug mode;
            //BundleTable.EnableOptimizations = true;
        }
    }
}
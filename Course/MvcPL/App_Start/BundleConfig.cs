using System.Web.Optimization;

namespace MvcPL
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").
                Include("~/Scripts/jquery-{version}.js").
                Include("~/Scripts/jquery-ui-{version}.js").
                Include("~/Scripts/jquery.fancybox.js").
                Include("~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryOnly").
                Include("~/Scripts/jquery-{version}.js").
                Include("~/Scripts/jquery-ui-{version}.js").
                Include("~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/search").
                Include("~/Scripts/search.js"));

            bundles.Add(new ScriptBundle("~/bundles/popover").
                Include("~/Scripts/jquery-{version}.js").
                Include("~/Scripts/jquery-ui-{version}.js").
                Include("~/Scripts/popover.js").
                Include("~/Scripts/photoDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/createAd").
                Include("~/Scripts/calculate.js"));

           bundles.Add(new ScriptBundle("~/bundles/uploadImage").
               Include("~/Scripts/uploadImage.js"));

            bundles.Add(new ScriptBundle("~/bundles/profile").
                Include("~/Scripts/profilePost.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").
                Include("~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/css").
                Include("~/Content/Site.css").
                Include("~/Content/bootstrap.css").
                Include("~/Content/jquery.fancybox.css").
                Include("~/Content/ionicons.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").
                Include("~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/bundles/payform").
                Include("~/Scripts/jquery.payform.min.js").
                Include("~/Scripts/payFormValidate.js"));

            bundles.Add(new StyleBundle("~/Content/payform/css").
                Include("~/Content/demo.css").
                Include("~/Content/creditCared.css"));
        }
    }
}
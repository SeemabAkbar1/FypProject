using System.Web;
using System.Web.Optimization;
using PUCIT.AIMRL.WebAppName.MainApp;
using PUCIT.AIMRL.WebAppName.UI.Common;

namespace PUCIT.AIMRL.WebAppName.MainApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            if (!HttpContext.Current.IsDebuggingEnabled)
                BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-1.8.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jqueryui/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                                            "~/Scripts/jquery-plugins/date.js",
                                            "~/Scripts/jquery-plugins/style-select.js",
                                            "~/Scripts/jquery-plugins/jquery.tooltip.js"));

            bundles.Add(new ScriptBundle("~/bundles/tooltip").Include(
                             "~/Scripts/jquery-plugins/jquery.tooltip.js"));

            bundles.Add(new ScriptBundle("~/bundles/sharedplugins").Include(
                        "~/Scripts/jquery-plugins/jquery.cookie.js",
                        "~/Scripts/jquery-plugins/Watermark/jquery.watermark.js",
                        "~/Scripts/spinner/spin.js",
                        "~/Scripts/spinner/jquery.spin.js",
                        "~/Scripts/spinner/ui.spinner.js",
                        "~/Scripts/spinner/ui.spinner.min.js,",
                        "~/Scripts/jquery-plugins/modalpopup/jquery.bsmodal.js",
                        "~/Scripts/jquery-plugins/Livequery/jquery.livequery.js"));


            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        new string[] { 
                            //"~/Scripts/common/servicespath.js", 
                            "~/Scripts/common/Global*", 
                            "~/Scripts/common/utilities.js", 
                            "~/Scripts/common/json2.js" }));

            bundles.Add(new ScriptBundle("~/bundles/mywebappmain").Include(
                        "~/Scripts/mywebapp/core/mywebapp.js",
                        "~/Scripts/mywebapp/core/mywebapp.ui.enums.js",
                        "~/Scripts/mywebapp/core/mywebapp.globals.js",
                        "~/Scripts/mywebapp/core/mywebapp.resources.js",
                        "~/Scripts/mywebapp/core/mywebapp.debugger.js",
                        "~/Scripts/mywebapp/core/mywebapp.ui.js",
                        "~/Scripts/mywebapp/core/mywebapp.ui.main.js",
                        "~/Scripts/mywebapp/core/mywebapp.ui.common.js",
                        "~/Scripts/mywebapp/core/mywebapp.ui.datamanager.js"));

            

            //Context Menu Component CSS
            var stBundle = new StyleBundle("~/Content/contextmenu");
            stBundle.Include("~/Content/style/contextmenu/jquery.contextMenu.css", new RewriteUrlTransform());
            stBundle.Include("~/Content/style/contextmenu/screen.css", new RewriteUrlTransform());
            stBundle.Include("~/Content/style/contextmenu/prettify.css", new RewriteUrlTransform());
            bundles.Add(stBundle);

            //Context Menu Component JavaScript
            bundles.Add(new ScriptBundle("~/bundles/contextmenu").Include(
                       "~/Scripts/jquery-plugins/Contextmenu/screen.js",
                       "~/Scripts/jquery-plugins/Contextmenu/src/jquery.contextMenu.js",
                       "~/Scripts/jquery-plugins/Contextmenu/src/jquery.ui.position.js"));

            bundles.Add(new ScriptBundle("~/bundles/SliderJs").Include(
                       "~/Scripts/jquery-plugins/Slider/jQuery-1.10.2.js",
                       "~/Scripts/jquery-plugins/Slider/jQuery-Ui.js"));

            stBundle = new StyleBundle("~/Content/SliderCss");

            stBundle.Include("~/Content/Slider/Slider.css", new RewriteUrlTransform());
            stBundle.Include("~/Content/Slider/SliderStyle.css", new RewriteUrlTransform());
            bundles.Add(stBundle);

            bundles.Add(new ScriptBundle("~/bundles/hicharts").Include("~/Scripts/jquery-plugins/hicharts/highcharts.src.js", 
                "~/Scripts/jquery-plugins/hicharts/draggable-points.js"));

            //stBundle = new StyleBundle("~/Content/css");

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //stBundle.Include("~/Content/mywebapp.css", new RewriteUrlTransform());
            //stBundle.Include("~/Content/style/reset.css", new RewriteUrlTransform());
            //stBundle.Include("~/Content/style/gs-24col.css", new RewriteUrlTransform());
            //stBundle.Include("~/Content/style/media-queries.css", new RewriteUrlTransform());
            //stBundle.Include("~/Content/style/default.css", new RewriteUrlTransform());
            //stBundle.Include("~/Content/bsmodal/jquery.bsmodal.css", new RewriteUrlTransform());
            //bundles.Add(stBundle);

            bundles.Add(new StyleBundle("~/Content/bsmodal").Include("~/Content/bsmodal/jquery.bsmodal.css"));

            stBundle = new StyleBundle("~/Content/themes/base/css");
            stBundle.Include("~/Content/themes/base/jquery.ui.core.css", new RewriteUrlTransform());
            stBundle.Include("~/Content/themes/base/jquery.ui.resizable.css", new RewriteUrlTransform());
            stBundle.Include("~/Content/NotificationMenu/style_light.css", new RewriteUrlTransform());
            stBundle.Include("~/Content/jCrumb/jCrumb.css", new RewriteUrlTransform());
            stBundle.Include("~/Content/style/ui.spinner.css", new RewriteUrlTransform());
            stBundle.Include("~/Content/themes/base/jquery.ui.datepicker.css", new RewriteUrlTransform());
            bundles.Add(stBundle);


            stBundle = new StyleBundle("~/Content/autocomplete1");
            stBundle.Include("~/Content/autoComplete/autocomplete.css", new RewriteUrlTransform());
            stBundle.Include("~/Content/autoComplete/autocomplete.jquery.ui.override.css", new RewriteUrlTransform());
            bundles.Add(stBundle);


            bundles.Add(new ScriptBundle("~/bundles/autocomplete").Include(
                        "~/Scripts/jquery-plugins/AutoComplete/jquery.ui.autocomplete.js"
                        , "~/Scripts/jquery-plugins/AutoComplete/jquery.ui.autocomplete.wrapper.js"));

            bundles.Add(new ScriptBundle("~/bundles/JqueryTemplate").Include(
                        "~/Scripts/jquery-plugins/jquery.tmpl.js"));

            //Table Sorter plugin + its widgets
            bundles.Add(new ScriptBundle("~/bundles/plugins/tablesorter").Include(
                        "~/Scripts/jquery-plugins/tablesorter/jquery.tablesorter.js",
                        "~/Scripts/jquery-plugins/tablesorter/jquery.tablesorter.staticrow.js"));

            //Login Page
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                       "~/scripts/mywebapp/user/mywebapp.ui.logon.js"));

            bundles.Add(new ScriptBundle("~/bundles/LoginAsOtherUsers").Include(
                       "~/scripts/mywebapp/Admin/mywebapp.ui.LoginAsOtherUsers.js"));

            //////Time Picker JavaScript
            bundles.Add(new ScriptBundle("~/bundles/timepicker").Include(
                        "~/Scripts/common/jquery-ui-timepicker-addon.js"));

            bundles.Add(new ScriptBundle("~/bundles/newtimepicker").Include(
            "~/Scripts/jquery-plugins/Timepicker/js/bootstrap-timepicker.js"
            , "~/Scripts/mywebapp/Core/mywebapp.ui.timepickercommon.js"));

            //////Time Picker css
            stBundle = new StyleBundle("~/Content/timepicker");
            stBundle.Include("~/Content/style/jquery-ui-timepicker-addon.css", new RewriteUrlTransform());
            bundles.Add(stBundle);
            stBundle = new StyleBundle("~/Content/newtimepicker");
            stBundle.Include("~/Content/style/bootstrap-timepicker.css", new RewriteUrlTransform());
            bundles.Add(stBundle);
           

            //for calender scheduler 
            bundles.Add(new ScriptBundle("~/bundles/fullcalender").Include(
                        "~/Scripts/jquery-plugins/FullCalenderScheduler/fullcalendar.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/daterangepicker").Include(
                       "~/Scripts/jquery-plugins/DateRangePicker/moment.min.js",
                       "~/Scripts/jquery-plugins/DateRangePicker/daterangepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/fineuploader").Include(
            "~/Scripts/jquery-plugins/FineUploader/fineuploader.js",
            "~/Scripts/jquery-plugins/jquery.tooltip.js"));

            //for calender scheduler 
            stBundle = new StyleBundle("~/Content/fullcalender");
            stBundle.Include("~/Content/style/FullCalenderScheduler/fullcalendar.css", new RewriteUrlTransform());
            stBundle.Include("~/Content/style/FullCalenderScheduler/fullcalendar.print.css", new RewriteUrlTransform());
            bundles.Add(stBundle);

            //for multiselect script bundle
            bundles.Add(new ScriptBundle("~/bundles/multiselect").Include(
                        "~/Scripts/jquery-plugins/MultiSelect/jquery.multiselect.js",
                       "~/Scripts/jquery-plugins/MultiSelect/jquery.multiselect.filter.min.js"));

            //for multiselect css bundle
            stBundle = new StyleBundle("~/Content/multiselect");
            stBundle.Include("~/Scripts/jquery-plugins/MultiSelect/assets/style.css", new RewriteUrlTransform());
            stBundle.Include("~/Scripts/jquery-plugins/MultiSelect/jquery.multiselect.filter.css", new RewriteUrlTransform());
            stBundle.Include("~/Scripts/jquery-plugins/MultiSelect/jquery.multiselect.css", new RewriteUrlTransform());
            bundles.Add(stBundle);

            //for multiselect css bundle
            stBundle = new StyleBundle("~/Content/daterangepicker");
            stBundle.Include("~/Scripts/jquery-plugins/DateRangePicker/daterangepicker.css", new RewriteUrlTransform());
            bundles.Add(stBundle);

            //for fineuploader css bundle
            stBundle = new StyleBundle("~/Content/fineuploader");
            stBundle.Include("~/Content/style/FineUploader/fineuploader.css", new RewriteUrlTransform());
            bundles.Add(stBundle);


            bundles.Add(new ScriptBundle("~/bundles/mywebapp.ui.forms").Include(
                        "~/Scripts/mywebapp/Forms/mywebapp.ui.forms.js"));
           
        }
    }

    public class RewriteUrlTransform : System.Web.Optimization.IItemTransform
    {
        CssRewriteUrlTransform tran = new CssRewriteUrlTransform();
        public string Process(string includedVirtualPath, string input)
        {
            var input1 = tran.Process(includedVirtualPath, input);
            input1 = input1.Replace("url(/Content", string.Format("url({0}/Content", GlobalDataManager.BasePath));
            return input1;
        }
    }
}
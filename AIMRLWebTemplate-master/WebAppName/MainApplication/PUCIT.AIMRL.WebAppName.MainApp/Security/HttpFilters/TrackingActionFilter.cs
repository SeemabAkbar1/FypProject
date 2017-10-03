using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Controllers;
using System.Diagnostics;
using PUCIT.AIMRL.WebAppName.MainApp.Util;
using PUCIT.AIMRL.WebAppName.UI.Common;

namespace PUCIT.AIMRL.WebAppName.MainApp.Utils.HttpFilters
{
    public class TrackingActionFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        //private Stopwatch stopwatch = new Stopwatch();

        public void OnException(ExceptionContext filterContext)
        {
            Util.Utility.HandleException(filterContext.Exception);
        }
        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext filterContext)
        {
            //this.stopwatch.Stop();
            //Util.Utility.LogData("Time Taken by Action Execution:" + this.stopwatch.ElapsedMilliseconds);
        }
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //Util.Utility.LogData(String.Format("URL:{0}",filterContext.Request.RequestUri.PathAndQuery));
            //this.stopwatch.Start();

            if (!SessionManager.IsUserLoggedIn)
            {
                filterContext.ControllerContext.RouteData.Values["Controller"] = "Login";
                filterContext.ControllerContext.RouteData.Values["Action"] = "Index";
                
            }

        }//end of OnActionExecuting

        

    }
}
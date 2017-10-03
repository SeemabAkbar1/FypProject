using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Controllers;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace PUCIT.AIMRL.WebAppName.MainApp.Utils.HttpFilters
{
    
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Util.Utility.HandleException(context.Exception);
            
            HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An unhandled exception was thrown by Custom Web API controller."),
                    ReasonPhrase = "An unhandled exception was thrown by Custom Web API controller."
                };

            context.Response = msg;
        }
    }

}
using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using PUCIT.AIMRL.WebAppName.MainApp.Util;
using System.Web;
using System.Net.Http;
using System.Net;
using PUCIT.AIMRL.WebAppName.UI.Common;

namespace PUCIT.AIMRL.WebAppName.MainApp.Security
{
    /// <summary>
    /// Class is used so that users see a custom not autorized page when they select to view a resource that requires 
    /// elevated role priviliges. Rather then redirecting to the logon page, they see the NotAuthorized page.
    /// </summary>
    public class AuthorizedForWebAPI : System.Web.Http.AuthorizeAttribute
    {
        public AuthorizedForWebAPI()
        {
            View = "notauthorized";
            Master = "notauthorized";
        }

        public String View { get; set; }
        public String Master { get; set; }

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            base.OnAuthorization(filterContext);
            CheckIfUserIsAuthenticated(filterContext);
        }
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (SessionManager.IsUserLoggedIn == false)
            {
                return false;
            }
            return true;
        }

        private void CheckIfUserIsAuthenticated(HttpActionContext actionContext)
        {
            // If Result is null, we're OK: the user is authenticated and authorized.
            if (actionContext.ActionArguments == null)
                return;

            if (SessionManager.IsUserLoggedIn == false)
            {
                //Abandons the current session and redirect to Login Page
                SessionManager.AbandonSession();

                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.NotAcceptable);

                return;
            }


        }
    }
}
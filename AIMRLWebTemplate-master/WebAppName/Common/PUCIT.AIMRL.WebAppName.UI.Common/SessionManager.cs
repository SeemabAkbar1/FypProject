using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace PUCIT.AIMRL.WebAppName.UI.Common
{
    public static class SessionManager
    {
        #region Private Data

        private static String USER_KEY = "user";

        #endregion

        public static Boolean LogsInAsOtherUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["LogsInAsOtherUser"] != null)
                    return Convert.ToBoolean(System.Web.HttpContext.Current.Session["LogsInAsOtherUser"]);
                else
                    return false;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LogsInAsOtherUser"] = value;
            }
        }
        public static int ActualUserUserID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["ActualUserUserID"] != null)
                    return Convert.ToInt32(System.Web.HttpContext.Current.Session["ActualUserUserID"]);
                else
                    return 0;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ActualUserUserID"] = value;
            }
        }
        public static String ActualUserLoginID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["ActualUserLoginID"] != null)
                    return Convert.ToString(System.Web.HttpContext.Current.Session["ActualUserLoginID"]);
                else
                    return "";
            }
            set
            {
                System.Web.HttpContext.Current.Session["ActualUserLoginID"] = value;
            }
        }

        public static PUCIT.AIMRL.WebAppName.Entities.Security.SecUserDTO CurrentUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session[USER_KEY] != null)
                    return System.Web.HttpContext.Current.Session[USER_KEY] as PUCIT.AIMRL.WebAppName.Entities.Security.SecUserDTO;
                else
                    return null;
            }
            set
            {
                System.Web.HttpContext.Current.Session[USER_KEY] = value;
            }
        }

        public static Int32 SessionTimeout
        {
            get
            {
                return System.Web.HttpContext.Current.Session.Timeout;
            }
        }

        public static String GetUserFullName()
        {
            if (SessionManager.CurrentUser != null)
                return SessionManager.CurrentUser.UserFullName;
            else
                return null;
        }
        public static String GetUserLogin()
        {
            if (SessionManager.CurrentUser != null)
                return SessionManager.CurrentUser.Login;
            else
                return null;
        }

        public static int GetLoggedInUserId()
        {
            if (SessionManager.CurrentUser != null)
                return SessionManager.CurrentUser.UserId;
            else
                return 0;
        }
        public static Boolean IsUserLoggedIn
        {
            get
            {
                if (SessionManager.CurrentUser != null)
                    return true;
                else
                    return false;
            }

        }

        #region Methods
        public static void AbandonSession()
        {
            LogsInAsOtherUser = false;
            ActualUserUserID = 0;
            ActualUserLoginID = "";
            for (int i = 0; i < System.Web.HttpContext.Current.Session.Count; i++)
            {
                System.Web.HttpContext.Current.Session[i] = null;
            }
            System.Web.HttpContext.Current.Session.Abandon();
        }

        #endregion



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;
using PUCIT.AIMRL.Common.Logger;
using PUCIT.AIMRL.WebAppName.UI.Common;
using PUCIT.AIMRL.WebAppName.DAL;



namespace PUCIT.AIMRL.WebAppName.MainApp.Util
{

    
    public static class Utility
    {

        public static String GetRequestedPageName()
        {
            String pageName = "";

            String[] completeURL = System.Web.HttpContext.Current.Request.ServerVariables["URL"].ToString().Split('/');
            pageName = completeURL[completeURL.Length - 1].ToString();

            return pageName;
        }
        public static String GetUserIPAddress()
        {
            return HttpContext.Current.Request.UserHostAddress.ToString();
        }

        /// <summary>
        /// Writes a log entry for an exception to file, database and email as specified in configuration
        /// </summary>
        /// <param name="pEx">Exception object</param>
        public static void HandleException(Exception pEx)
        {
            PUCIT.AIMRL.Common.Logger.LogHandler.WriteLog(GetUserNameForLogging(), pEx.Message, Common.Logger.LogType.ErrorMsg, pEx);
        }

        public static void LogData(String pLogEntry)
        {
            PUCIT.AIMRL.Common.Logger.LogHandler.WriteLog(GetUserNameForLogging(), pLogEntry, Common.Logger.LogType.InfoMsg);
        }

        private static String GetUserNameForLogging()
        {
            var userName = "";
            var dto = SessionManager.CurrentUser;
            if (dto != null)
                userName = dto.Login;

            if (HttpContext.Current.Request.UserHostAddress != null)
                userName += "-IP:" + HttpContext.Current.Request.UserHostAddress.ToString();

            if (HttpContext.Current.Request.Url != null && HttpContext.Current.Request.Url.PathAndQuery != null)
                userName += "-URL: " + HttpContext.Current.Request.Url.AbsoluteUri;

            return userName;
        }



        public static String GetFileSizeFromBytes(int sizeInBytes)
        {
            if (sizeInBytes < 1024)
                return sizeInBytes + " B";
            else if (sizeInBytes >= 1024 && sizeInBytes < (1024 * 1024))
                return Math.Round((sizeInBytes / 1024.0), 2) + " KB";
            else if (sizeInBytes >= (1024 * 1024))
                return Math.Round((sizeInBytes / (1024.0 * 1024.0)), 2) + " MB";
            return "";
        }

        public static void LoadApplicationSettingFromWebConfig()
        {
            Boolean flag = false;
            Boolean.TryParse(ConfigurationManager.AppSettings["IsCSEncrypted"], out flag);
            GlobalDataManager.IsCSEncrypted = flag;
            GlobalDataManager.PageTitlePrefix = ConfigurationManager.AppSettings["PageTitlePrefix"];
            GlobalDataManager.BuildVersion = ConfigurationManager.AppSettings["BuildVersion"];

        }
        public static void LoadGlobalSettings()
        {
            LoadApplicationSettingFromWebConfig();

            var dal = new PRMDataService();

            //GlobalDataManager.MessagesList = dal.GetAllNotificationMessages();

        }


    }


}


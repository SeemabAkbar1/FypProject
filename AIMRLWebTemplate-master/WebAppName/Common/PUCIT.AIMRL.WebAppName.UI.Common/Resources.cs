
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PUCIT.AIMRL.WebAppName.UI.Common
{
    public static class EmailNotificationConstants
    {
        public const String FROM_ADDRESS = "noreply@pucit.aimrl.com";
        public const String DEFAULT_TO_ADDRESS = "test@pucit.aimrl.com";
    }

    public static class Resources
    {
        public const String CREATED_BY_FOR_NEW_REGIS_UNLOGGEDIN_USER = "guest";        
        public const String MESSAGES_SESSION_EXPIRED_MSG = "Session has been expired!";
        public const String IMAGES_INITIAL_PATH = "~/docs/images/";
        public const String RESUMES_INITIAL_PATH = "~/docs/resumes/";
        public const String APPLICATION_DEFAULT_TIMEZONE_CODE = "EST";
        
        public const String PAGES_DEFAULT_LOGIN_PAGE = "~/Login";
        public const String PAGES_MANAGERS_DEFAULT_HOME_PAGE = "~/Home";
        public const String PAGES_SIGNOUT_PAGE_URL = "~/signout.aspx";
        public const String PAGES_DEFAULT_PAGE_TO_REDIRECT = "~";
        public const String PAGES_ERROR = "~/Error.aspx";

        
        public const String ALLOWED_EXTENSIONS = @"(.*\.(jpg|jpeg|gif|png)$)";

        public const String TEMPLATES_RTA_ALERTS = "~/EmailTemplates/Alerts.html";

        public const String IMPORT_EMPLOYEE_EXCEL_UPLOADING_FILE_PATH = "~/TempFiles/test";


        //Path of Images for Email Templates
        public const String EMAIL_IMAGE_HEADER_PART = "~/EmailTemplates/img-header.png";
        public const String EMAIL_IMAGE_HEADER_LOGO_PART = "~/EmailTemplates/img-logo.png"; 


        public static String GetCompletePath(String pVirtualPath)
        {
            string initialPath = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            String compPath = initialPath + VirtualPathUtility.ToAbsolute(pVirtualPath);
            return compPath;
        }
        public static String LogoPhysicalPath
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath(VirtualPathUtility.ToAbsolute("~/EmailTemplates/logo.png"));
            }
        }
        public static String GetPhysicalPathOfImg(String pVirtualPath)
        {
            return System.Web.HttpContext.Current.Server.MapPath(VirtualPathUtility.ToAbsolute(pVirtualPath));
        }
    }

    
}

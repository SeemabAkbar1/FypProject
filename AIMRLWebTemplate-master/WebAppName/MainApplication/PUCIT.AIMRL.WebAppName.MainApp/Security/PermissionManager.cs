using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUCIT.AIMRL.WebAppName.MainApp.Util;
using PUCIT.AIMRL.WebAppName.Entities.Enum;

namespace PUCIT.AIMRL.WebAppName.MainApp.Security
{
    public static class PermissionManager
    {

        public static Boolean perCanLoginAsOtherUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["perCanLoginAsOtherUser"] != null)
                    return Convert.ToBoolean(System.Web.HttpContext.Current.Session["perCanLoginAsOtherUser"]);
                else
                    return false;
            }
            set
            {
                System.Web.HttpContext.Current.Session["perCanLoginAsOtherUser"] = value;
            }
        }
        public static Boolean perManageSecurityUsers
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["perManageSecurityUsers"] != null)
                    return Convert.ToBoolean(System.Web.HttpContext.Current.Session["perManageSecurityUsers"]);
                else
                    return false;
            }
            set
            {
                System.Web.HttpContext.Current.Session["perManageSecurityUsers"] = value;
            }
        }
        public static Boolean perManageSecurityRoles
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["perManageSecurityRoles"] != null)
                    return Convert.ToBoolean(System.Web.HttpContext.Current.Session["perManageSecurityRoles"]);
                else
                    return false;
            }
            set
            {
                System.Web.HttpContext.Current.Session["perManageSecurityRoles"] = value;
            }
        }
        public static Boolean perManageSecurityPermissions
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["perManageSecurityPermissions"] != null)
                    return Convert.ToBoolean(System.Web.HttpContext.Current.Session["perManageSecurityPermissions"]);
                else
                    return false;
            }
            set
            {
                System.Web.HttpContext.Current.Session["perManageSecurityPermissions"] = value;
            }
        }
        public static Boolean perViewLoginHistoryReport
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["perViewLoginHistoryReport"] != null)
                    return Convert.ToBoolean(System.Web.HttpContext.Current.Session["perViewLoginHistoryReport"]);
                else
                    return false;
            }
            set
            {
                System.Web.HttpContext.Current.Session["perViewLoginHistoryReport"] = value;
            }
        }


        public static void HandlePermissions(List<string> permissionStrList)
        {
            //Set all permissions to false

            PermissionManager.perManageSecurityPermissions = false;
            PermissionManager.perManageSecurityRoles = false;
            PermissionManager.perManageSecurityUsers = false;
            PermissionManager.perViewLoginHistoryReport = false;
            PermissionManager.perCanLoginAsOtherUser = false;


            //Now check if permission list contains specific string, then make relevant boolean permission true
            if (permissionStrList.Contains("PERMANAGESECURITYPERMISSIONS"))
                PermissionManager.perManageSecurityPermissions = true;

            if (permissionStrList.Contains("PERMANAGESECURITYROLES"))
                PermissionManager.perManageSecurityRoles = true;

            if (permissionStrList.Contains("PERMANAGESECURITYUSERS"))
                PermissionManager.perManageSecurityUsers = true;

            if (permissionStrList.Contains("PERVIEWLOGINHISTORYREPORT"))
                PermissionManager.perViewLoginHistoryReport = true;

            if (permissionStrList.Contains("PERCANLOGINASOTHERUSER"))
                PermissionManager.perCanLoginAsOtherUser = true;

        }
    }
}
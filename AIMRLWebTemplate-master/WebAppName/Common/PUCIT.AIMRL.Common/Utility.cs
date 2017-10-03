using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PUCIT.AIMRL.Common
{
    public static class WinUtility
    {
        public static String WinBasepath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);

        public static String GetPhysicalPathByRelativePath(String pRelativePath)
        {
            return Path.Combine(WinBasepath, pRelativePath);
        }
    }

    public static class WebUtility
    {
        public static String GetPhysicalPathByVirtualPath(String pVirtualPath)
        {
            return System.Web.HttpContext.Current.Server.MapPath(VirtualPathUtility.ToAbsolute(pVirtualPath));
        }
    }

    public static class HelperMethods
    {
        public static String ConvertDTToStr(DateTime dt)
        {
            return dt.ToString("dd-MM-yy h:mm:ss tt");
        }

        public static String ConvertOnlyDateToStr(DateTime dt)
        {
            return dt.Date.ToString("dd-MM-yy");
        }

        public static String ChangeDTFormat (DateTime dt)
        {
            return dt.ToString("dd-MM-yy hh:mm tt");
        }
    }

    public static class MyDateTimeExtension
    {
        

        public static String YYYYMMDD(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public static DateTime ToTimeZoneTime(this DateTime time, string timeZoneId = "Pakistan Standard Time")
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return time.ToTimeZoneTime(tzi);
        }
        public static DateTime ToTimeZoneTime(this DateTime time, TimeZoneInfo tzi)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time, tzi);
        }
    }
}

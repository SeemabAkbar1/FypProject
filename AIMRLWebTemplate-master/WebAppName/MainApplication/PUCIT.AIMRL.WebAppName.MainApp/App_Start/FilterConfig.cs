using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.WebAppName.MainApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
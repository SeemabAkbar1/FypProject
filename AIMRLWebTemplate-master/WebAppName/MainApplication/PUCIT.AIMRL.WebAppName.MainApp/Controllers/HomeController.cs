using MvcSiteMapProvider.Web.Mvc.Filters;
using PUCIT.AIMRL.WebAppName.Entities;
using PUCIT.AIMRL.WebAppName.MainApp.Models;
using PUCIT.AIMRL.WebAppName.MainApp.Util;
using PUCIT.AIMRL.WebAppName.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PUCIT.AIMRL.WebAppName.MainApp.Controllers
{
    public class HomeController : BaseController
    {

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View("Dashboard");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}

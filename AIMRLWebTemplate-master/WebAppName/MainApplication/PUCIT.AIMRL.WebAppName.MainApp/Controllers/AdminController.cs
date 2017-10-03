using PUCIT.AIMRL.WebAppName.DAL;
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
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult LoginAs()
        {
            if (PUCIT.AIMRL.WebAppName.MainApp.Security.PermissionManager.perCanLoginAsOtherUser==false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("LoginAsAnotherUser");
            }
        }
    }
}

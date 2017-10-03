using PUCIT.AIMRL.WebAppName.MainApp.Util;
using PUCIT.AIMRL.WebAppName.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.WebAppName.MainApp.Controllers
{
    public class SecurityController : BaseController
    {
        //
        // GET: /Admin/
        public ActionResult Users()
        {
            if (PUCIT.AIMRL.WebAppName.MainApp.Security.PermissionManager.perManageSecurityUsers== false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Roles()
        {
            if (PUCIT.AIMRL.WebAppName.MainApp.Security.PermissionManager.perManageSecurityRoles == false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Id = 1;
                return View();
            }
        }
        public ActionResult Permissions()
        {
            if (PUCIT.AIMRL.WebAppName.MainApp.Security.PermissionManager.perManageSecurityPermissions == false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Id = 2;
                return View();
            }
        }
    }
}

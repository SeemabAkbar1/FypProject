using PUCIT.AIMRL.Common;
using PUCIT.AIMRL.WebAppName.DAL;
using PUCIT.AIMRL.WebAppName.MainApp.Models;
using PUCIT.AIMRL.WebAppName.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.WebAppName.MainApp.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title =PUCIT.AIMRL.WebAppName.UI.Common.GlobalDataManager.PageTitlePrefix + "Login";
            return View("login");
        }

        public ActionResult Index2()
        {
            ViewBag.Title = PUCIT.AIMRL.WebAppName.UI.Common.GlobalDataManager.PageTitlePrefix + "Login";
            return View("login");
        }
        public ActionResult ResetPassword1(string rt)
        {
            //rt = HttpUtility.UrlDecode(rt);
        var emailaddress = EncryptDecryptUtility.Decrypt(rt);

            ViewBag.data = rt;

            return View();
        }
        public ActionResult ForgotPassword()
        {
            return PartialView("ForgotPassword");
        }
        public ActionResult LoginPanel()
        {
            return PartialView("LoginPanel");
        }
        public ActionResult AdminLogin()
        {
            return View("AdminLogin");
        }
        public ActionResult test()
        {
            return View("test");
        }

        public ActionResult SignOut()
        {

            if (SessionManager.IsUserLoggedIn && SessionManager.LogsInAsOtherUser)
            {
                UserInfoRepository repo = new UserInfoRepository();
                repo.ValidateUser(SessionManager.ActualUserLoginID,"",true,false);
                
                SessionManager.ActualUserUserID = 0;
                SessionManager.LogsInAsOtherUser = false;
                SessionManager.ActualUserLoginID = "";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                SessionManager.AbandonSession();
                return RedirectToAction("Index");
            }
        }
    }
}

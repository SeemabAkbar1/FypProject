using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Collections;
using PUCIT.AIMRL.WebAppName.DAL;
using PUCIT.AIMRL.WebAppName.MainApp.Util;
using PUCIT.AIMRL.WebAppName.UI.Common;
using PUCIT.AIMRL.WebAppName.Entities.Security;
using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using PUCIT.AIMRL.WebAppName.MainApp.Security;
using PUCIT.AIMRL.WebAppName.Entities;
using PUCIT.AIMRL.Common;


namespace PUCIT.AIMRL.WebAppName.MainApp.Models
{
    public class UserInfoRepository
    {
        private PRMDataService _dataService;
        public UserInfoRepository()
        {
        }
        private PRMDataService DataService
        {
            get
            {
                if (_dataService == null)
                    _dataService = new PRMDataService();

                return _dataService;
            }
        }

        

        public Object ValidateUser(String login, String pPassword,Boolean pIgnorePassword,Boolean pLoginAsOtherUser)
        {
            Object dataToReturn = null;
            //Check to see if the user is provided the rights on the application
            try
            {

                var ipAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                var currTime = DateTime.UtcNow;

                if (pLoginAsOtherUser == true && SessionManager.IsUserLoggedIn == true)
                {
                    SessionManager.LogsInAsOtherUser = true;
                    SessionManager.ActualUserUserID = SessionManager.CurrentUser.UserId;
                    SessionManager.ActualUserLoginID = SessionManager.CurrentUser.Login;
                }
                else
                {
                    SessionManager.LogsInAsOtherUser = false;
                    SessionManager.ActualUserUserID = 0;
                    SessionManager.ActualUserLoginID = "";
                }

                var secUserForSession = DataService.ValidateUserSP(login, pPassword, currTime, ipAddress, pIgnorePassword, SessionManager.ActualUserLoginID);

                if (secUserForSession != null)
                {
                    if (secUserForSession.IsActive==false)
                    {
                        Utility.LogData("User Is Inactive, can't log in");
                        SessionManager.CurrentUser = null;
                        dataToReturn = new
                        {
                            success = false,
                            error = "Your account is not active, Please Contact Administrator"
                        };
                      
                    }
                    else
                    {
                        PermissionManager.HandlePermissions(secUserForSession.Permissions);
                        secUserForSession.Permissions = null;

                        //if (secUserForSession.ApproverDesignations.Count > 0 && secUserForSession.IsContributor)
                        //{
                        //    var desig = secUserForSession.ApproverDesignations.First();
                        //    secUserForSession.CurrentApproverID = desig.ApproverID;
                        //    secUserForSession.Desingtation = desig.Designation;
                        //}

                        SessionManager.CurrentUser = secUserForSession;

                        var RedirectURl = Resources.PAGES_MANAGERS_DEFAULT_HOME_PAGE;
                        RedirectURl = RedirectURl.Replace("~/", "");
                        dataToReturn = new
                        {
                            redirect = RedirectURl,
                            success = true,
                            error = ""
                        };
                    }
                }

                else
                {
                    //If the user was not detected as an authorized user
                    Utility.LogData("Invalid Login: " + login + " Password: " + pPassword);
                    SessionManager.CurrentUser = null;
                    dataToReturn = new
                    {
                        success = false,
                        error = "Invalid Login/Password"
                    };
                }
                return (dataToReturn);
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
                SessionManager.CurrentUser = null;
                var exception = new
                {
                    success = false,
                    error = "Some problem has occurred, Please Try again!"
                };
                return (exception);
            }
        }
        
        //public Object UpdateDesign(int aid)
        //{
        //    var returnObj = (new
        //    {
        //        success = false,
        //        error = "Invalid Request"
        //    });

        //    try
        //    {

        //        Boolean flag = false;
        //        var secUserForSession = SessionManager.CurrentUser;

        //        if (secUserForSession.ApproverDesignations.Count > 0 && secUserForSession.IsContributor)
        //        {
        //            var desig = secUserForSession.ApproverDesignations.Where(p => p.ApproverID == aid).FirstOrDefault();
        //            if (desig != null)
        //            {
        //                var rolesList = new List<String>();
        //                var permList = new List<String>();

        //                permList = DataService.GetRolePermissionById(aid, out rolesList);

        //                PermissionManager.HandlePermissions(permList);

        //                secUserForSession.Roles = rolesList;

                        
        //                flag = true;
        //            }
        //        }

        //        if (flag)
        //        {
        //            SessionManager.CurrentUser = secUserForSession;
        //            return (new
        //            {
        //                success = true,
        //                error = ""
        //            });
        //        }
        //        else
        //        {
        //            return returnObj;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return returnObj;
        //    }
        //}

        public Object sendEmail(string emailAddress)
        {
            if (PUCIT.AIMRL.WebAppName.UI.Common.SessionManager.LogsInAsOtherUser == true)
            {
                return (new
                {
                    success = false,
                    error = "You Are Not Allowed"
                });
            }
            try
            {
                var userObj = DataService.GetUserByEmail(emailAddress);

                if (userObj != null)
                {
                    string token = "";
                    token = HttpUtility.UrlEncode(EncryptDecryptUtility.Encrypt(emailAddress));

                    String url = PUCIT.AIMRL.WebAppName.UI.Common.Resources.GetCompletePath("~/Login/ResetPassword1");
                    url = String.Format("{0}?rt={1}", url, token);

                    String subject = "Reset Password";
                    String msg = String.Format("Click the link below to reset your password \n {0}", url);

                    EmailHandler.SendEmail(emailAddress, subject, msg);
               }
                else
                {
                    return (new
                    {
                        success = false,
                        error = "email not correct"
                    });
                }


                return (new
                {
                    data = new
                    {
                        Id = emailAddress
                    },
                    success = true,
                    error = ""
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "email not correct"
                });
            }
        }

        public Object SignOut(Boolean pManualEclockLogout)
        {
            try
            {
                SessionManager.CurrentUser = null;
                SessionManager.AbandonSession();
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddSeconds(-1));
                HttpContext.Current.Response.Cache.SetNoStore();

                if (HttpContext.Current.Request.Cookies["breadcrumbs"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("breadcrumbs");
                    myCookie.Expires = DateTime.UtcNow.AddDays(-1d);
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }

                var result = new
                {
                    success = true,
                    error = ""
                };

                return (result);

            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }

        public Object resetPassword(PasswordEntity pass)
        {
            if (PUCIT.AIMRL.WebAppName.UI.Common.SessionManager.LogsInAsOtherUser == true)
            {
                return (new
                {
                    success = false,
                    error = "You Are Not Allowed"
                });
            }
            try
            {
                var emailid = EncryptDecryptUtility.Decrypt(pass.ID);
                var password = pass.NewPassword;

                var id = DataService.resetPassword(emailid, password);

                return (new
                {
                    data = new
                    {
                        Id = id
                    },
                    success = true,
                    error = "Password Reset"
                });
            }
            catch (Exception ex)
            {
                return (new
                {
                    success = false,
                    error = "Some Error has occurred"
                });
            }
        }
    }
}
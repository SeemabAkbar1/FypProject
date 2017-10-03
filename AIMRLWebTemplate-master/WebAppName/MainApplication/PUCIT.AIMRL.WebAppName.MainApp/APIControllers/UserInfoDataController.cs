
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using PUCIT.AIMRL.WebAppName.MainApp.Models;
using PUCIT.AIMRL.WebAppName.MainApp.Security;
using PUCIT.AIMRL.WebAppName.MainApp.Utils.HttpFilters;
using PUCIT.AIMRL.WebAppName.Entities;

namespace PUCIT.AIMRL.WebAppName.MainApp.APIControllers
{
    public class UserInfoDataController : ApiController
    {
        public class Login
        {
            public String UserName { get; set; }
            public String Password { get; set; }
        }

        private readonly UserInfoRepository _repository;
        public UserInfoDataController()
        {
            _repository = new UserInfoRepository();
        }

        private UserInfoRepository Repository
        {
            get
            {
                return _repository;
            }
        }

        [HttpPost]
        public Object ValidateUser(Login pLogin)
        {
            try
            {
                Util.Utility.LogData("Going to validate Login:" + pLogin.UserName);
                return Repository.ValidateUser(pLogin.UserName, pLogin.Password,false,false);
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
        //[AuthorizedForWebAPI]
        //public Object GetUsername()
        //{
        //    return Repository.getUsername();
        //}
        [HttpGet]
        public Object sendEmail(string emailAddress)
        {
            return Repository.sendEmail(emailAddress);
        }
        //[AuthorizedForWebAPI]
        public Object resetPassword(PasswordEntity pass)
        {
            return Repository.resetPassword(pass);
        }

        [AuthorizedForWebAPI]
        [HttpGet]
        public Object ChangeDesig(int aid)
        {
            return null;
            //return Repository.UpdateDesign(aid);
        }
        
        
    }
}
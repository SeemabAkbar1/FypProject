using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using PUCIT.AIMRL.WebAppName.MainApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PUCIT.AIMRL.WebAppName.MainApp.APIControllers
{

    //    ->DB side paging(DB Level):
    //        Pass page index, page size & search criteria to your SP and get specific records only by using FETCH, OFFSET feature

    //    ->Background: In admin panels, there is mostly an option for admins to login by another user(without providing password). It is to check how system is working for a specific user.
    //Task: We need to design a screen where we'll provide
    //Autocomplete box(to search any user by name, login or by email id)
    //User will click on "Login as" button and same login process will work as it works for normal user on lo



    public class AdminController : BaseDataController
    {
        //
        // GET: /Admin/

        private readonly AdminRepository _repository;

        public AdminController()
        {
            _repository = new AdminRepository();
        }
        private AdminRepository Repository
        {
            get
            {
                return _repository;
            }
        }
        
        
        
        public Object searchUsers(User u)
        {
            return Repository.SearchUsers(u);
        }

        [HttpPost]
        public Object ValidateUser(Login pLogin)
        {
            try
            {
                UserInfoRepository userInfoRepo = new UserInfoRepository();
                return userInfoRepo.ValidateUser(pLogin.UserName, "", true, true);
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

        public class Login
        {
            public String UserName { get; set; }
        }

        [HttpGet]
        public Object SearchUser(string key)
        {

            return Repository.SearchUser(key);
        }
    }
}

using System.Web;
using PUCIT.AIMRL.WebAppName.DAL;
using PUCIT.AIMRL.WebAppName.Entities;
using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using System.Web.Security;
using System.Configuration;
using System;
using System.Linq;
using PUCIT.AIMRL.WebAppName.UI.Common;
using System.Collections.Generic;
using System.IO;
using PUCIT.AIMRL.WebAppName.MainApp.Util;
using PUCIT.AIMRL.WebAppName.Entities.Security;
using PUCIT.AIMRL.WebAppName.MainApp.Security;
using PUCIT.AIMRL.Common;
using System.Collections;


namespace PUCIT.AIMRL.WebAppName.MainApp.Models
{
    public class AdminRepository
    {
        private PRMDataService _dataService;
        public AdminRepository()
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


        
        
        
        public Object SearchUsers(User pSearchParam)
        {


            try
            {


                var List = DataService.SearchUsers(pSearchParam);


                return (new
                {
                    data = new
                    {
                        UserList = List
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
                    error = "Some Error has occurred"
                });
            }
        }
        public Object SearchUser(string key)
        {
            try
            {

                var list = DataService.SearchUser(key);

                var result = (from p in list
                              select new
                              {
                                  ID = p.UserId,
                                  Login = p.Login,
                                  Name = p.Name
                              }).ToList();
                return result;
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
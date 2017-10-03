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
using PUCIT.AIMRL.WebAppName.Entities;
using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using PUCIT.AIMRL.WebAppName.Entities.Enum;

namespace PUCIT.AIMRL.WebAppName.MainApp.Models
{
    public class DashboardRepository
    {
        private PRMDataService _dataService;
        public DashboardRepository()
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

        public Object changePassword(PasswordEntity pass)
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
                //  var emailid = EncryptDecryptUtility.Decrypt(pass.ID);

                var id = DataService.changePassword(pass);
                if (id == 0)
                {
                    return (new
                    {
                        data = new
                        {
                            Id = id
                        },
                        success = false,
                        error = "Wrong Password"
                    });
                }
                else
                {
                    return (new
                    {
                        data = new
                        {
                            Id = id
                        },
                        success = true,
                        error = "Password Changed"
                    });
                }
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PUCIT.AIMRL.WebAppName.MainApp.Models;
using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using PUCIT.AIMRL.WebAppName.Entities;

namespace PUCIT.AIMRL.WebAppName.MainApp.APIControllers
{
    public class DashboardController : BaseDataController
    {
        private readonly DashboardRepository _repository;
        public DashboardController()
        {
            _repository = new DashboardRepository();
        }

        private DashboardRepository Repository
        {
            get
            {
                return _repository;
            }
        }
        
        [HttpGet]
        public Object changePassword(PasswordEntity pass)
        {
            return Repository.changePassword(pass);
        }
    }
}


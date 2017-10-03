using PUCIT.AIMRL.WebAppName.DAL;
using PUCIT.AIMRL.WebAppName.Entities;
using PUCIT.AIMRL.WebAppName.Entities.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
namespace PUCIT.AIMRL.WebAppName.MainApp.APIControllers
{
    class ReportsRepository
    {
        private PRMDataService _dataService;
        public ReportsRepository()
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

        
        public object getLoginHistory()
        {

            try
            {
                var List = DataService.GetLoginHistory();
                return (new
                {
                    data = new
                    {
                        LoginHistoryList = List
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

    }
}
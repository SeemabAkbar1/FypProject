using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using PUCIT.AIMRL.WebAppName.MainApp.Security;

namespace PUCIT.AIMRL.WebAppName.MainApp.APIControllers
{

    [AuthorizedForWebAPI]
    public class BaseDataController : ApiController
    {        

    }
}
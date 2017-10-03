using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUCIT.AIMRL.WebAppName.Entities.DBEntities
{
   
    public class PermissionsWithRoleID : Permissions
    {
        public int RoleId { get; set; }

    }
}

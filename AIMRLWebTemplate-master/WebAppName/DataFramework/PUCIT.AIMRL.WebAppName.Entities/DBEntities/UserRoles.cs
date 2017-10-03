using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUCIT.AIMRL.WebAppName.Entities.DBEntities
{
    [Table("dbo.UserRoles")]
    public class UserRoles
    {
        [Key]
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PUCIT.AIMRL.WebAppName.Entities.DBEntities
{

    [Table("dbo.Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public Boolean IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }

    public class Approver
    {
        public int UserId { get; set; }
        public String Login { get; set; }
        public String Name { get; set; }
        public String Designation { get; set; }
        public String Email { get; set; }

        public int WorkFlowStatus { get; set; }
    }
}

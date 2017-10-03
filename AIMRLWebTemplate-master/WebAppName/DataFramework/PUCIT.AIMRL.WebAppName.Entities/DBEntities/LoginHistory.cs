using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PUCIT.AIMRL.Common;

namespace PUCIT.AIMRL.WebAppName.Entities.DBEntities
{
    [Table("dbo.LoginHistory")]
    public class LoginHistory
    {
        [Key]
        public Int64 LoginHistoryID { get; set; }
        public int UserID { get; set; }
        public String LoginID { get; set; }
        public String MachineIP { get; set; }
        public DateTime LoginTime { get; set; }
        [NotMapped]
        public String LoginTimeStr
        {
            get
            {
                return HelperMethods.ConvertDTToStr(this.LoginTime);
            }
        }
    }
}
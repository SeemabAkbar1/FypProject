using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PUCIT.AIMRL.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace PUCIT.AIMRL.WebAppName.Entities
{
    [Table("dbo.EmailRequests")]
    public class EmailRequest
    {
        [Key]
        public long EmailRequestID {get;set;}
        public String Subject {get;set;}
        public String MessageBody {get;set;}
        public String MessageParameters {get;set;}
        public String EmailTo {get;set;}
        public String EmailCC {get;set;}
        public String EmailBCC {get;set;}
        public String EmailTemplate {get;set;}
        public int ScheduleType {get;set;}
        public DateTime? ScheduleTime {get;set;}
        public int EmailRequestStatus {get;set;}
        public DateTime? EntryTime {get;set;}

        public String UniqueID { get; set; }
    }

   
}

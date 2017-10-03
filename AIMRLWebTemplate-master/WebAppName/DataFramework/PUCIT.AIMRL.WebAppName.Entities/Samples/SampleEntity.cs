using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUCIT.AIMRL.WebAppName.Entities
{

    [Table("dbo.SampleStudents")]
    public class SampleStudent
    {
        [Key]
        public Int32 StudentId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Gender { get; set; }
        public Int32 Education { get; set; }
        public Boolean? IsActive { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public String ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

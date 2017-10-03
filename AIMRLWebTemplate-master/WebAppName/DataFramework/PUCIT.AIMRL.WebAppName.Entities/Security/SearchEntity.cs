using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUCIT.AIMRL.WebAppName.Entities
{
    public class SearchEntity
    {
        public SearchEntity()
        {
            SDate = new DateTime(1900, 1, 1);
            EDate = DateTime.MaxValue;
            Type = 0;
            Status = 0;
            RollNO = "";
            Name = "";
        }
        public String RollNO { get; set; }
        public String Name { get; set; }
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public String DiaryNo { get; set; }
        public int DiaryNoInt { get; set; }

    }
}

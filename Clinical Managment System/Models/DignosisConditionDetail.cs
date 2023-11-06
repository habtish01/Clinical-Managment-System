using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical_Managment_System.Models
{
    public class DignosisConditionDetail
    {
        public string ConditionDetail { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Action { get; set; }
    }
}

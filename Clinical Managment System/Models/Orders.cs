using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical_Managment_System.Models
{
    public class Sample
    {
        public int Id { get; set; }
        public int OrderTypeId { get; set; }
        public string SampleName { get; set; }

    }

    public class Panels
    {
        public int Id { get; set; } 
        public int SampleId { get; set; }    
        public string PanelName { get; set; }
    }
}

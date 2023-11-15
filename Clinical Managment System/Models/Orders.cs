using DevExpress.XtraEditors;
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
        public bool isClicked { get; set; }=false;
        public bool isLoadFromDb { get; set; } = true; 


    }

    public class Panels
    {
        public int Id { get; set; } 
        public int SampleId { get; set; }    
        public string PanelName { get; set; }
        public bool isClicked { get; set; } = true;
     
    }
    public class Tests{
        public int Id { get; set; }
        public int PanelId { get; set; }
        public string TestName { get; set; }
        public string TestType { get; set; }
    }

    public class SampleElements
    {
       
        public int SampleId { get; set; }
        public int PanelId { get; set; }
        public int TestId { get; set; }
        public string PanelName { get; set; }
        public string TestName { get; set; }
        public string TestType { get; set; }
       
        public bool isClicked { get; set; } = true;
    }

    public class LabOrderExtended
    {
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

    }


    public class ListofButtons
    {
        public SimpleButton TestButton { get; set; }
        public int Id { get; set; }
        public bool isClicked { get; set; } = false;
    }
}

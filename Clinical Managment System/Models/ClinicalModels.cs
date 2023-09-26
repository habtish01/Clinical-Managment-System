using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical_Managment_System.Models
{
    public class ConsultationNote
    {
        public int PatientId { get; set; }
        public string Note { get; set; }
        public string AddedBy { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
    }

    public class DignosisModel
    {
        public int Patient_Id { get; set; }
        public int DiseasisTypeId { get; set; }
        public string Order { get; set; }
        public string Certainity { get; set; }
        public string Satus { get; set; }
        public string AddedBy { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

    }
    public class DispositionModel
    {
        public int? Patient_Id { get; set; }
        public int? Room_Id { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public string Added_By { get; set; }
    }
    public class ComboxData
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}

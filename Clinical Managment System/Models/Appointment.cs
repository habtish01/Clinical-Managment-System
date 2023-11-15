using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical_Managment_System.Models
{
    public class Appointment
    {
           public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string ServiceType { get; set; }
            public string VistLocation { get; set; }
            public string AppointmentNote { get; set; }
            public string OrderdBy { get; set; }
            public DateTime OrderedDate { get; set; }
            public bool Status { get; set; }
            public string Remark { get; set; }
        
    }
    public class AppointmmentDto
    {
        public string PatientId { get; set; }
        public int AppointmentTypeId  { get; set; }
        public int VisitLocationId { get; set; }
        public string OrderedBy{ get; set; }
        public string AppointmentDescription { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool Status { get; set; } = false;
        public string  Remark { get; set; }         
    }

  

    public class ServiceType
    {
        public int id { get; set; }
        public string service_description { get; set; }
    }

    public class VisitLocation
    {
        public int id { get; set; }
        public string description { get; set; }
    }

}

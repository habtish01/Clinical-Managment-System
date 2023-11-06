using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical_Managment_System.Models
{
    public class PatientModel
    {
        public string ID { get; set; }
        public string FirstName { get; set; } 
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
    }
    public class PatientsForHomeDisplay
    {
        public string PatientID { get; set; }
        public string FullName { get; set; }
    }
}

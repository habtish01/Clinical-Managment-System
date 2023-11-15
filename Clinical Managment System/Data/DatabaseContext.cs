using Clinical_Managment_System.Models;
using DevExpress.XtraEditors.Filtering.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Clinical_Managment_System.Data_Access_Layer
{
    public class DatabaseContext
    {
        string connectionString = ConfigurationManager.ConnectionStrings["HahcConnection"].ConnectionString;
        public int loadPatientId(string personId)
        {
            int patientId=0;
            SqlConnection connection=new SqlConnection(connectionString);
            string query = "select id from general.patient where person_id=@id";
            connection.Open();  
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", personId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                patientId = reader.GetInt32(0);
                return patientId;   
            }
            return patientId;
        }
        public bool insertConsultation(ConsultationNote note)
        {
            SqlConnection connection=new SqlConnection(connectionString);
            connection.Open();
            string insertQuery = "insert  into patient_consultation (patient_id,consultation,added_by,given_date,remark) values (@id,@consultation,@added_by,@given_date,@remark)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@id", note.PatientId);
            insertCommand.Parameters.AddWithValue("@consultation", note.Note);
            insertCommand.Parameters.AddWithValue("@added_by", note.AddedBy);
            insertCommand.Parameters.AddWithValue("@given_date", note.Date);
            insertCommand.Parameters.AddWithValue("@remark", note.Remark);
            int rowsAffected=insertCommand.ExecuteNonQuery();  
            connection.Close(); 
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }


      
        public bool InsertDignosis(DignosisModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "insert into patient_diagnosis (patient_id,diseases_type_id,diseases_order,certainity,added_by,description,added_date,status) values (@id,@diseases_type_id,@order,@certainity,@added_by,@description,@date,@status)";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", model.Patient_Id);
            command.Parameters.AddWithValue("@diseases_type_id", model.DiseasisTypeId);
            command.Parameters.AddWithValue("@order", model.Order);
            command.Parameters.AddWithValue("@certainity", model.Certainity);
            command.Parameters.AddWithValue("@added_by", model.AddedBy);
            command.Parameters.AddWithValue("@description", model.Description);
            command.Parameters.AddWithValue("@date", model.Date);
            command.Parameters.AddWithValue("@status", model.Satus);
          
                    

            int rows=command.ExecuteNonQuery();
            if(rows > 0)    return true;    
            return false;
        }
        public List<ComboxData> LoadDiseasesType()
        {
            List<ComboxData> comboxDatas = new List<ComboxData>();  
           
            SqlConnection conn=new SqlConnection(connectionString);
            conn.Open();
            string query = "select *from clinical.diagnosis_diseases_type";
            SqlCommand command=new SqlCommand(query, conn);
            SqlDataReader reader=command.ExecuteReader();
            while (reader.Read())
            {
                ComboxData data = new ComboxData
                    {
                      Id = reader.GetInt32(0),
                      Description = reader.GetString(1)
                    };
                comboxDatas.Add(data);
            }
            

            return comboxDatas;
        }

        public bool InsertDisposition(DispositionModel model)
        {
            string query = "insert into patient_room_addmission (patient_id,room_id,addmission_date,addmitted_by,status) values (@patient_id,@room_id,@addmission_date,@addmitted_by,@status)";
            
            SqlConnection conn=new SqlConnection( connectionString);
            conn.Open();
            SqlCommand command=new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@patient_id", model.Patient_Id);
            command.Parameters.AddWithValue("@room_id", model.Room_Id);
            command.Parameters.AddWithValue("@addmission_date", model.Date);
            command.Parameters.AddWithValue("@addmitted_by", model.Added_By);
            command.Parameters.AddWithValue("@status", model.Status);

            int rows=command.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                return true;
            }
            return false;
        }
        public bool UpdateDisposition(DispositionModel model)
        {
            string query = "update patient_room_addmission set discharged_date=@date,discharged_by=@user,status=@status where patient_id=@id";
            
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", model.Patient_Id);
        
            command.Parameters.AddWithValue("@date", model.Date);
            command.Parameters.AddWithValue("@user", model.Added_By);
            command.Parameters.AddWithValue("@status", model.Status);

            int rows = command.ExecuteNonQuery();
            conn.Close ();  
            if (rows > 0)
            {
                return true;
            }
            return false;
        }
        public List<string> LoadDispositionType(int id)
        {
            string Dispositiontype="Nothing";
            bool type=false;
            var CheckPatient=string.Empty;
            List<string> list=new List<string>();
        
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "select status from patient_room_addmission where patient_id=@id";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                type = reader.GetBoolean(0);
                CheckPatient = "Exit";  
               
            }
            if (CheckPatient == string.Empty)
            {
               Dispositiontype = "Addmit Patient";
                list.Add("---Select---");
                list.Add(Dispositiontype);
                return list;
            }
            if (type)
            {
                Dispositiontype = "Discharge Patient";
                list.Add("---Select---");
                list.Add(Dispositiontype);  
                return list;
            }
            list.Add(Dispositiontype);

            return list;
        }
    }
}

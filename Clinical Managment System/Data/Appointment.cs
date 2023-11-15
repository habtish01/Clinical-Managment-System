using Clinical_Managment_System.Models;
using Dapper;
using DevExpress.XtraTreeMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical_Managment_System.Data
{
    public class AppointmentDbContext
    {
        string connectionString = ConfigurationManager.ConnectionStrings["HahcConnection"].ConnectionString;
        // for the appointments
        public List<Appointment> loadAppointmentSummary()
        {

            List<Appointment> appointments = new List<Appointment>();
            string query = "SELECT *from dbo.AppointmentList";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                Appointment appointment = new Appointment
                {
                    FirstName = reader["first_name"].ToString(),
                    MiddleName = reader["middile_name"].ToString(),
                    LastName = reader["last_name"].ToString(),
                    ServiceType = reader["service_description"].ToString(),
                    VistLocation = reader["description"].ToString(),
                    AppointmentNote = reader["note"].ToString(),
                    OrderdBy = reader["appointed_by"].ToString(),
                    OrderedDate = DateTime.Parse(reader["date"].ToString()),
                    Status = bool.Parse(reader["status"].ToString()),
                    Remark = reader["remark"].ToString(),
                };

                appointments.Add(appointment);
            }

            return appointments;

        }

       

        public List<ServiceType> getServiceType()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var sql = "SELECT [id],[service_description] FROM [appointment].[appointment_type]";

            var serviceTypes = connection.Query<ServiceType>(sql);
            return serviceTypes.ToList();
        }
        public List<VisitLocation> getVisitLocation()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var sql = "SELECT [id],[description] FROM [general].[location]";

            var visitLocations = connection.Query<VisitLocation>(sql);
            return visitLocations.ToList();
        }

        public bool addAppointment(AppointmmentDto appointmmentDto)
        {
            try
            {
                int row = 0;
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                string query = "INSERT INTO [appointment].[appointment]([patient_id],[appointment_type_id] ,[location_id],[appointed_by] ,[date],[status],[note],[remark])VALUES (@patientID,@serviceTypeId,@locationId,@orderedBy,@date,@status,@note,@remark)";
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@patientID", appointmmentDto.PatientId);
                command.Parameters.AddWithValue("@serviceTypeId", appointmmentDto.AppointmentTypeId);
                command.Parameters.AddWithValue("@locationId", appointmmentDto.VisitLocationId);
                command.Parameters.AddWithValue("@orderedBy", appointmmentDto.OrderedBy);
                command.Parameters.AddWithValue("@date", appointmmentDto.AppointmentDate);
                command.Parameters.AddWithValue("@status", appointmmentDto.Status);
                command.Parameters.AddWithValue("@note", appointmmentDto.AppointmentDescription);
                command.Parameters.AddWithValue("@remark", appointmmentDto.Remark);
                row = command.ExecuteNonQuery();

                sqlConnection.Close();
                if (row > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;   
            
        }
    }
}

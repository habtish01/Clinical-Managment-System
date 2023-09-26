﻿using Clinical_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical_Managment_System.Data_Access_Layer
{

    public class AccessAllPateints
    {
        string connectionString = ConfigurationManager.ConnectionStrings["HahcConnection"].ConnectionString;
        public List<PatientModel> LoadAllPatients()
        {
            List<PatientModel> patients = new List<PatientModel>();
            int type_id = 1;
            SqlConnection connection=new SqlConnection(connectionString);
            connection.Open();
            string query = "select Id,first_name,middile_name,last_name,gender,age,phone,date_registered from general.person where type_id=@id and active=@status";
            SqlCommand cmd = new SqlCommand(query, connection);
            
            cmd.Parameters.AddWithValue("@id", type_id);
            cmd.Parameters.AddWithValue("@status", true);
            SqlDataReader reader = cmd.ExecuteReader(); 
            while (reader.Read())
            {
                PatientModel model = new PatientModel
                {
                    Id = reader["Id"].ToString(),
                    FirstName = reader["first_name"].ToString(),
                    MiddleName = reader["middile_name"].ToString(),
                    LastName = reader["last_name"].ToString(),
                    Gender = reader["gender"].ToString(),
                    Age = int.Parse(reader["age"].ToString()),
                    PhoneNumber = reader["phone"].ToString(),
                    Date = DateTime.Parse(reader["date_registered"].ToString())

                };
                patients.Add(model);
            }
            connection.Close();
            return patients;
        }

    }
}

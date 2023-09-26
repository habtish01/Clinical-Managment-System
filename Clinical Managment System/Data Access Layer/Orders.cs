using Clinical_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical_Managment_System.Data_Access_Layer
{
    public class Orders
    {
        string connectionString = ConfigurationManager.ConnectionStrings["HahcConnection"].ConnectionString;

        public List<Sample> GetSamples()
        {
            List<Sample> samples = new List<Sample>();
            string query = "select id,type_id,description from [order].order_category";
            SqlConnection connection=new SqlConnection(connectionString);
            connection.Open();  
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader(); 
            while (reader.Read())
            {
                Sample sample = new Sample
                {
                    Id = reader.GetInt32(0),
                    OrderTypeId = reader.GetInt32(1),
                    SampleName = reader.GetString(2),
                };
                samples.Add(sample);    
            }
            return samples; 
        }

    }
}

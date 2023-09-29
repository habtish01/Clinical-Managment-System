using Clinical_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public List<Panels> GetPanels(int sampleId)
        {
            List<Panels> panels = new List<Panels>();
            string query = "select id,description from [order].order_class where category_id=@id";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", sampleId);    
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Panels panel = new Panels
                {
                    Id = reader.GetInt32(0),
                    
                    PanelName = reader.GetString(1),
                };
                panels.Add(panel);
            }
            return panels;

        }

        public List<Tests> GetTests(int panelId)
        {
            List<Tests> tests = new List<Tests>();
            string query = "select id,description,type from [order].order_test where class_id=@id";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", panelId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Tests test = new Tests
                {
                    Id = reader.GetInt32(0),                   
                    TestName = reader.GetString(1),
                    TestType = reader.GetString(2),
                };
                tests.Add(test);
            }
            return tests;

        }

    }
}

using Clinical_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
            string query = "select id,category_id,description from [order].order_class where category_id=@id";
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
                    SampleId = reader.GetInt32(1),
                    PanelName = reader.GetString(2),
                };
                panels.Add(panel);
            }
            return panels;

        }

        public List<Tests> GetTests(List<int> panelId)
        {
            List<Tests> tests = new List<Tests>();
            foreach (int id in panelId)
            {
                string query = "select id,description,class_id,type from [order].order_test where class_id=@id";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tests test = new Tests
                    {
                        Id = reader.GetInt32(0),
                        TestName = reader.GetString(1),
                        PanelId=reader.GetInt32(2),
                        TestType = reader.GetString(3),
                    };
                    tests.Add(test);
                }
            }
            return tests;

        }

        public List<SampleElements> getPanelAandTests(int sampleId)
        {
            List<SampleElements> panelwithTests = new List<SampleElements>();
            string query = "select [order].order_class.category_id,[order].order_test.class_id,[order].order_class.description,[order].order_test.id,[order].order_test.description,[order].order_test.type from[order].order_class inner join[order].order_test on[order].order_class.id = [order].order_test.class_id where[order].order_class.category_id =@sampleId";
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@sampleId", sampleId);
            SqlDataReader reader = cmd.ExecuteReader(); 
            while (reader.Read()) 
            {
                SampleElements sampleElements = new SampleElements
                {
                    SampleId = reader.GetInt32(0),
                    PanelId = reader.GetInt32(1),
                    PanelName = reader.GetString(2),
                    TestId = reader.GetInt32(3),
                    TestName = reader.GetString(4),
                    TestType = reader.GetString(5),
                };
                panelwithTests.Add(sampleElements);


            }
            return panelwithTests;

        }

        public bool saveLabOrders(List<SampleElements> testElements, LabOrderExtended orderExtended)
        {
            int row = 0;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string query = "insert into [order].patient_order (order_test_id,patient_id,date,total) values (@test_id,@patient_id,@date,@total)";
            sqlConnection.Open();
            foreach (var element in testElements)
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@test_id", element.TestId);
                command.Parameters.AddWithValue("@patient_id", orderExtended.PatientId);
                command.Parameters.AddWithValue("@date", orderExtended.Date);
                command.Parameters.AddWithValue("@total", testElements.Count);
                row = command.ExecuteNonQuery();
               
                
            }
            sqlConnection.Close();  
            if(row > 0)
            {
                return true;
            }
            return false;
        }

    }
}

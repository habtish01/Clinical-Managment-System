

using Clinical_Managment_System.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Clinical_Managment_System.Data_Access_Layer
{
    public class ControlsModelDbAcess
    {
        string connectionString = ConfigurationManager.ConnectionStrings["HahcConnection"].ConnectionString;
        public List<ControlsModel> getAllControls()
        {
            List<ControlsModel> allcontrols= new List<ControlsModel>(); 
            SqlConnection connection=new SqlConnection(connectionString);
            string query = "select *from  [clinical].definition_detail";
            SqlCommand command=new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(); 
            while (reader.Read())
            {
                ControlsModel model = new ControlsModel
                {
                    Id = reader.GetInt32(0),
                    ExtensionId = reader.GetInt32(1),
                    Description = reader.GetString(2),
                    Type = reader.GetString(3),
                    Order = reader.GetInt32(4),
                    ParentId = reader.GetInt32(5),
                };
                allcontrols.Add(model);
            }
            
            return allcontrols; 
        }

    }
}

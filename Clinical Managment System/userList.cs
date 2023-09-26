using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    internal class userList
    {
        public int id { get; set; }
        public int person_id { get; set; }
        public string first_name { get; set; }
        public string middile_name { get; set; }
        public string last_name { get; set;}
        public string gender { get; set; }
        public DateTime date_registered { get; set; }
        public int age { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public bool active { get; set; }
    }
}

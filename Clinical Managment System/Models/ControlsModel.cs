using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinical_Managment_System.Models
{
    public class ControlsModel
    {
        public int Id { get; set; }
        public int ExtensionId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
        public int ParentId { get; set; }
    }
}

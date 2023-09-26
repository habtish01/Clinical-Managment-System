using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinical_Managment_System.Models
{
    public class Controls
    {
        public ComboBox DiseasesType { get; set; }
        public CheckBox Primary { get; set; }
        public CheckBox Secondary { get; set; }
        public CheckBox Confirmed { get; set; }
        public CheckBox Persumed { get; set; }
        public CheckBox Sataus { get; set; }
        public Label DescriptionTitle { get; set; }
        public RichTextBox Description { get; set; }
    }
}

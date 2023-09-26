using Clinical_Managment_System.Data_Access_Layer;
using Clinical_Managment_System.Models;
using Clinical_Managment_System.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraPrinting.Native.PageSizeInfo;

namespace Clinical_Managment_System
{
    public partial class HomeClinicalSystem : Form
    {
        AccessAllPateints context =new AccessAllPateints();
        List<PatientModel> patientModels;
        List<Button> buttons=new List<Button>();
      
        public HomeClinicalSystem()
        {
            InitializeComponent();
            patientModels = context.LoadAllPatients();
            var patientsQuery = patientModels.AsQueryable();
            var patients=patientModels.Skip(0).Take(36).ToList();   
            int x = 0;
            int y=0; 
            int length=patients.Count;
            int countButton = 0;
            int z = 0;  
            txtPageNumber.Enabled = false;

            foreach(var patient in patients)
            {
                countButton++;   
                Button button = new Button();
                button.Text=patient.FirstName + " " +patient.MiddleName+" " +patient.LastName+ "\n" +"\n" + patient.Id;
                button.Tag = patient;   
                button.Font= new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                button.BackColor = Color.LightGray;
                  
                //button.DrawBorder(button.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
            
            button.Size = new Size(210, 85);
                button.Location = new Point(x, y);
                x = countButton%6==0 ? 0 : x+220;
                if(countButton%6==0)
                {
                    y = y + 90;
                }
             
                button.Click += Button_Click;
                button.MouseEnter += button_MouseEnter;
                button.MouseLeave += button_MouseLeave;
                panel1.Controls.Add(button);
                buttons.Add(button);


            }
               
            
        }
        private void button_MouseEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.LightBlue; 

        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.LightGray; 

        }
        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                // Retrieve the associated data from the sender's Tag property
                if (button.Tag is PatientModel patients)
                {
                    ClinicalManagmentSystemForm clinicalSystem = new ClinicalManagmentSystemForm();
                    clinicalSystem.patient = patients;
                    clinicalSystem.Show();
                    clinicalSystem.DisplayReceivedData();
                    //MessageBox.Show("Button clicked: " + button.Text + "\nData: " + patient.PhoneNumber);
                }
            }
           
        }

        private void HomeClinicalSystem_Load(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
           

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            foreach(var item in buttons)
            {
                panel1.Controls.Remove(item);   
            }
            List<Button> nextButtons= new List<Button>();   
            foreach(var item in nextButtons)
            {
                panel1.Controls.Remove(item);
            }
            var pageNumber=int.Parse(txtPageNumber.Text.Trim())+1;
            txtPageNumber.Text = pageNumber.ToString(); 
            var pageSize = 36;
            var skipWalks = (pageNumber - 1) * pageSize;
            var patientsQuery = patientModels.AsQueryable();
            var patients = patientModels.Skip(skipWalks).Take(pageSize).ToList();
            int x = 0;
            int y = 0;
            int length = patients.Count;
            int countButton = 0;
            int z = 0;
            txtPageNumber.Enabled = false;

            foreach (var patient in patients)
            {
                countButton++;
                Button button = new Button();
                button.Text = patient.FirstName + " " + patient.MiddleName + " " + patient.LastName + "\n" + "\n" + patient.Id;
                button.Tag = patient;
                button.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                button.BackColor = Color.LightGray;

                //button.DrawBorder(button.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);

                button.Size = new Size(210, 85);
                button.Location = new Point(x, y);
                x = countButton % 6 == 0 ? 0 : x + 220;
                if (countButton % 6 == 0)
                {
                    y = y + 90;
                }

                button.Click += Button_Click;
                button.MouseEnter += button_MouseEnter;
                button.MouseLeave += button_MouseLeave;
                panel1.Controls.Add(button);
                nextButtons.Add(button);    


            }
        }
    }
}

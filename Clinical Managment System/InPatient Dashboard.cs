using Clinical_Managment_System.Models;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinical_Managment_System
{
    public partial class InPatient_DashboardForm : Form
    {
        public PatientModel patient { get; set; }
        public InPatient_DashboardForm()
        {
            InitializeComponent();            
        }
        public void DisplayReceivedData()
        {
            patientName.Text = patient.FirstName + " " + patient.MiddleName + " " + patient.LastName;
            patientGender.Text = patient.Gender;
            patientAge.Text = patient.Age.ToString();
            patientId.Text = patient.ID.ToString();
            patientPhone.Text = patient.PhoneNumber.ToString();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void patientId_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelImpatientDashboard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoggedUser_Click(object sender, EventArgs e)
        {

        }

        private void LogoName_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxLogo_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxLogedUser_Click(object sender, EventArgs e)
        {

        }

        private void panelForPatient_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void patientPhone_Click(object sender, EventArgs e)
        {

        }

        private void patientGender_Click(object sender, EventArgs e)
        {

        }

        private void patientAge_Click(object sender, EventArgs e)
        {

        }

        private void patientName_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panelDashbordCollection_Paint(object sender, PaintEventArgs e)
        {

        }

        private void footerPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl12_Paint(object sender, PaintEventArgs e)
        {
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            PatientModel clickedObject = patient;
            ClinicalManagmentSystemForm clinicalSystem = new ClinicalManagmentSystemForm();
            clinicalSystem.patient = clickedObject;
            clinicalSystem.Show();
            clinicalSystem.DisplayReceivedData();
        }
    }
}

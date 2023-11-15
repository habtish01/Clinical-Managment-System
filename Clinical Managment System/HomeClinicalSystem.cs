using Clinical_Managment_System.Data_Access_Layer;
using Clinical_Managment_System.Models;
using Clinical_Managment_System.Properties;
using DevExpress.XtraRichEdit.Model;
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
        AccessAllPateints context = new AccessAllPateints();//instance of the database context
        List<PatientModel> patientModels = new List<PatientModel>();

        public HomeClinicalSystem()
        {
            InitializeComponent();

        }


        private void HomeClinicalSystem_Load(object sender, EventArgs e)
        {
            //calls the database and gets all active patients
            //then convert to list of object for display purpose 
            patientModels = context.LoadAllPatients();
            
            List<PatientsForHomeDisplay> partialFieldsListAsClass = patientModels.Select(obj => new PatientsForHomeDisplay
            {
                FullName = obj.FirstName + " " + obj.MiddleName + " " + obj.LastName,
                PatientID = obj.ID

            }).ToList();
            //data grid for displaying the active patients in the layput view
            HomeControl.DataSource = partialFieldsListAsClass;

        }
        //click event for each card in the layout view
        private void layoutView1_CardClick(object sender, DevExpress.XtraGrid.Views.Layout.Events.CardClickEventArgs e)
        {
            var rowIndex = e.RowHandle;
            List<PatientsForHomeDisplay> dataSource = (List<PatientsForHomeDisplay>)layoutViewHomeView.GridControl.DataSource;
            PatientsForHomeDisplay clickedObject = dataSource[rowIndex];
            PatientModel model = patientModels.FirstOrDefault(x => x.ID == clickedObject.PatientID);
            InPatient_DashboardForm dashboard = new InPatient_DashboardForm();
            dashboard.patient = model;
            dashboard.Show();
            dashboard.DisplayReceivedData();

        }
        //exists  the app
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
    }
}

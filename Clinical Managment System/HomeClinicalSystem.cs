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
        AccessAllPateints context =new AccessAllPateints();
        List<PatientModel> patientModels=new List<PatientModel>();
        List<PatientModel> patients = new List<PatientModel>();
        List<Button> nextButtons = new List<Button>();
        List<Button> buttons=new List<Button>();
        List<Button> prevButtons = new List<Button>();
        List<Button> searchedButton = new List<Button>();
      
        int x = 0;
        int y = 0;
        
        int countButton = 0;
       
        public HomeClinicalSystem()
        {
            InitializeComponent();
           
        }
        //public void getFirstPageOfPatients()
        //{
        //     x = 0;
        //     y = 0;
        //    foreach (var patient in patients)
        //    {
        //        countButton++;
        //        Button button = new Button();
        //        button.Text = patient.FirstName + " " + patient.MiddleName + " " + patient.LastName + "\n" + "\n" + patient.ID;
        //        button.Tag = patient;
        //        button.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //        button.BackColor = Color.LightGray;
        //        button.Size = new Size(210, 85);
        //        button.Location = new Point(x, y);
        //        x = countButton % 6 == 0 ? 0 : x + 220;
        //        if (countButton % 6 == 0)
        //        {
        //            y = y + 90;
        //        }

        //        button.Click += Button_Click;
        //        button.MouseEnter += button_MouseEnter;
        //        button.MouseLeave += button_MouseLeave;
        //        panelHomePage.Controls.Add(button);
        //        buttons.Add(button);
        //        btnPrevious.Enabled = false;
        //        btnNext.Enabled = true;


        //    }
        //}
        //private void button_MouseEnter(object sender, EventArgs e)
        //{
        //    Button button = (Button)sender;
        //    button.BackColor = Color.LightBlue; 

        //}

        //private void button_MouseLeave(object sender, EventArgs e)
        //{
        //    Button button = (Button)sender;
        //    button.BackColor = Color.LightGray; 

        //}
        //private void Button_Click(object sender, EventArgs e)
        //{
        //    if (sender is Button button)
        //    {
        //        // Retrieve the associated data from the sender's Tag property
        //        if (button.Tag is PatientModel patients)
        //        {
        //            ClinicalManagmentSystemForm clinicalSystem = new ClinicalManagmentSystemForm();
        //            clinicalSystem.patient = patients;
        //            clinicalSystem.Show();
        //            clinicalSystem.DisplayReceivedData();
        //            //MessageBox.Show("Button clicked: " + button.Text + "\nData: " + patient.PhoneNumber);
        //        }
        //    }
           
        //}

        private void HomeClinicalSystem_Load(object sender, EventArgs e)
        {
            patientModels = context.LoadAllPatients();

            var partialFieldsListAsClass = patientModels.Select(obj => new PatientsForHomeDisplay
            {
                PatientID = obj.ID,
                FullName = obj.FirstName+ obj.LastName,
              
            }).ToList();
         
            HomeControl.DataSource= patientModels;
          
           



        }
        private void layoutView1_CardClick(object sender, DevExpress.XtraGrid.Views.Layout.Events.CardClickEventArgs e)
        {
            var rowIndex = e.RowHandle;
            List<PatientModel> dataSource = (List<PatientModel>)layoutViewHomeView.GridControl.DataSource;
            PatientModel clickedObject = dataSource[rowIndex];
            InPatient_DashboardForm dashboard = new InPatient_DashboardForm();
            dashboard.patient = clickedObject;
            dashboard.Show();
            dashboard.DisplayReceivedData();

            string name = clickedObject.FirstName.ToString();

            txtSearch.Text = name;
        }

        private void HomeControl_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      
        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    foreach(var item in buttons)
        //    {
        //        panelHomePage.Controls.Remove(item);   
        //    }
        //    foreach (var item in prevButtons)
        //    {
        //        panelHomePage.Controls.Remove(item);
        //    }

        //    foreach (var item in nextButtons)
        //    {
        //        panelHomePage.Controls.Remove(item);
        //    }
        //    var pageNumber=int.Parse(txtPageNumber.Text.Trim())+1;
        //    txtPageNumber.Text = pageNumber.ToString(); 
        //    var pageSize = 36;
        //    var skipWalks = (pageNumber - 1) * pageSize;
        //    var patientsQuery = patientModels.AsQueryable();
        //    var patients = patientModels.Skip(skipWalks).Take(pageSize).ToList();
        //    int x = 0;
        //    int y = 0;
        //    int length = patients.Count;
        //    int countButton = 0;
                     
        //    foreach (var patient in patients)
        //    {
        //        countButton++;
        //        Button button = new Button();
        //        button.Text = patient.FirstName + " " + patient.MiddleName + " " + patient.LastName + "\n" + "\n" + patient.ID;
        //        button.Tag = patient;
        //        button.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //        button.BackColor = Color.LightGray;
        //        button.Size = new Size(210, 85);
        //        button.Location = new Point(x, y);
        //        x = countButton % 6 == 0 ? 0 : x + 220;
        //        if (countButton % 6 == 0)
        //        {
        //            y = y + 90;
        //        }

        //        button.Click += Button_Click;
        //        button.MouseEnter += button_MouseEnter;
        //        button.MouseLeave += button_MouseLeave;
        //        panelHomePage.Controls.Add(button);
        //        nextButtons.Add(button);    


        //    }
        //    int TotalPatients = patientModels.Count;
        //   int TotalPages = (int)Math.Ceiling((double)TotalPatients / 36);
        //    if (txtPageNumber.Text.Trim() == TotalPages.ToString()) { btnNext.Enabled = false; }
        //    else { btnPrevious.Enabled = true; }
        //    btnPrevious.Enabled = txtPageNumber.Text.Trim() == "1" ? false : true;

        //}

        //private void btnPrevious_Click(object sender, EventArgs e)
        //{
        //    foreach (var item in nextButtons)
        //    {
        //        panelHomePage.Controls.Remove(item);
        //    }
        //    foreach (var item in prevButtons)
        //    {
        //        panelHomePage.Controls.Remove(item);
        //    }
        //    var pageNumber = int.Parse(txtPageNumber.Text.Trim()) - 1;
        //    txtPageNumber.Text = pageNumber.ToString();
        //    var pageSize = 36;
        //    var skipWalks = (pageNumber - 1) * pageSize;
        //    var patientsQuery = patientModels.AsQueryable();
        //    var patients = patientModels.Skip(skipWalks).Take(pageSize).ToList();
        //    int x = 0;
        //    int y = 0;
        //    int length = patients.Count;
        //    int countButton = 0;

        //    txtPageNumber.Enabled = false;

        //    foreach (var patient in patients)
        //    {
        //        countButton++;
        //        Button button = new Button();
        //        button.Text = patient.FirstName + " " + patient.MiddleName + " " + patient.LastName + "\n" + "\n" + patient.ID;
        //        button.Tag = patient;
        //        button.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //        button.BackColor = Color.LightGray;

        //        //button.DrawBorder(button.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);

        //        button.Size = new Size(210, 85);
        //        button.Location = new Point(x, y);
        //        x = countButton % 6 == 0 ? 0 : x + 220;
        //        if (countButton % 6 == 0)
        //        {
        //            y = y + 90;
        //        }

        //        button.Click += Button_Click;
        //        button.MouseEnter += button_MouseEnter;
        //        button.MouseLeave += button_MouseLeave;
        //        panelHomePage.Controls.Add(button);
        //        prevButtons.Add(button);
        //    }
        //    btnPrevious.Enabled = txtPageNumber.Text.Trim() == "1" ? false : true;
        //    btnNext.Enabled = true;

        //}

        //private void btnSearchPatient_Click(object sender, EventArgs e)
        //{
        //    var searchtext=txtSearch.Text.Trim();           
        //    var patient=patientModels.Find(x=>x.ID.TrimEnd() == searchtext ||x.PhoneNumber.TrimEnd()==searchtext);
        //    if (patient != null) 
        //    {
        //        panelHomePage.Controls.Clear();    
        //        Button button = new Button();
        //        button.Text = patient.FirstName + " " + patient.MiddleName + " " + patient.LastName + "\n" + "\n" + patient.ID;
        //        button.Tag = patient;
        //        button.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //        button.BackColor = Color.LightGray;
        //        button.Size = new Size(210, 85);
        //        button.Location = new Point(0, 0);
        //        button.Click += Button_Click;
        //        button.MouseEnter += button_MouseEnter;
        //        button.MouseLeave += button_MouseLeave;
        //        panelHomePage.Controls.Add(button);
        //        searchedButton.Add(button); 
        //    }
        //}

        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void btnBackHome_Click(object sender, EventArgs e)
        //{
        //    txtPageNumber.Text = "1";
        //    //foreach (var button in searchedButton) 
        //    //{ 
        //    //    panelHomePage.Controls.Remove(button);  
        //    //}
        //    panelHomePage.Controls.Clear();
        //    getFirstPageOfPatients();
        //}

        //private void HomeControl_Click(object sender, EventArgs e)
        //{

        //}

      
    }
}

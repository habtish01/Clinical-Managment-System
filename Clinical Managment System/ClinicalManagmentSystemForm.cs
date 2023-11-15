using Clinical_Managment_System.Data;
using Clinical_Managment_System.Data_Access_Layer;
using Clinical_Managment_System.Models;
using Clinical_Managment_System.Properties;
using Clinical_Managment_System.Validation;
using DevExpress.Utils.Extensions;
using DevExpress.Utils.ScrollAnnotations;
using DevExpress.XtraCharts.Design;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static DevExpress.Data.Mask.Internal.MaskSettings<T>;

namespace Clinical_Managment_System
{
    public partial class ClinicalManagmentSystemForm : Form
    {
        
        DatabaseContext context=new DatabaseContext();
        ConsultationNote consultation=new ConsultationNote();
       // List<Panel> panelCollection = new List<Panel>();
        Orders orders = new Orders();
        List<Panels> panels = new List<Panels>();
        List<Panels> allpanelsinAllSamples = new List<Panels>();
        List<Tests> tests = new List<Tests>();
        List<SampleElements> sampleElements = new List<SampleElements>();
        List<SampleElements> allTests=new List<SampleElements>();
        List<SampleElements> allTestsinAllSamples=new List<SampleElements>();
        List<Label> OrderedPanelsLabel= new List<Label>();
       
     
        public PatientModel patient {  get; set; }
        int patientID;
      
        int dignosisPanelX = 217;
      
        List<ListofButtons> testButtonList = new List<ListofButtons>();
  
        bool checkStatus = true;
        Controls dignosisControl = new Controls();
        List<Controls> listControls = new List<Controls>();
        List<Panel> dignosisPanel=new List<Panel>();
        DevExpress.XtraGrid.GridControl gridControlConditionDisplay = new DevExpress.XtraGrid.GridControl();     
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClinicalManagmentSystemForm));
        AppointmentDbContext dbContext = new AppointmentDbContext();
        List<DignosisConditionDetail> conditionDetails = new List<DignosisConditionDetail>();
        

        public ClinicalManagmentSystemForm()
        {
            InitializeComponent();



        }
        public void DisplayReceivedData()
        {
            patientName.Text = patient.FirstName + " " + patient.MiddleName + " " + patient.LastName;
            patientGender.Text = patient.Gender;
            patientAge.Text = patient.Age.ToString();
            lblPatientId.Text = patient.ID.ToString();
            patientPhone.Text = patient.PhoneNumber.ToString();
        }

        private void ClinicalManagmentSystemForm_Load(object sender, EventArgs e)
        {
            //Load Combo Box List For Diseases Type
           List<ComboxData> comboList=context.LoadDiseasesType();
           
            dropDownDiseases.Properties.DataSource=comboList;
            dropDownDiseases.EditValue = 1;
            dropDownDiseases.Properties.DisplayMember = "Description";
            //dropDownDiseases.Properties.ValueMember = "Id";
            dropDownDiseases.Properties.PopulateViewColumns();
            //////////////////////////////////////////////////
            searchLookUpEditCondition.Properties.DataSource=comboList;
            searchLookUpEditCondition.EditValue = 1;
           // searchLookUpEditCondition.Properties.ValueMember = "Id";
            searchLookUpEditCondition.Properties.DisplayMember = "Description";
            searchLookUpEditCondition.Properties.PopulateViewColumns();

           
            //load patint id based on person id from patient table
            // patientID = context.loadPatientId(patient.ID.ToString());
            List<String> type = context.LoadDispositionType(patientID);
            dropDownSelectAction.DataSource = type;

            //for Dignosis Desccription
           
            diagnosisDescription.Visible = false; 
            descriptionTitle.Visible = false;
            //ende for the above action
            ///appointment
            var appointmentList = dbContext.loadAppointmentSummary();
            appointmentList.OrderBy(x=>dateComparision(x.OrderedDate)).ToList();    
            gridControlAppointmentDocument.DataSource = dbContext.loadAppointmentSummary();
            loadAppointmentFields();
            ////make the default date value for patient condition date filed
            dateEditCondition.DateTime = DateTime.Today;            
            dateEditCondition.Properties.MaxValue = DateTime.Today;
            appointmentDate.DateTime = DateTime.Today;
            appointmentDate.Properties.MinValue = DateTime.Today;
            //////////////////////////Lab Order//////////
            createSamples();

        }

   
        

     

       
        #region Consultation
        private void btnSaveConsultation_Click(object sender, EventArgs e)
        {
            var note = richtxtConsultation.Text.Trim();
            ConsultationValidation validation = new ConsultationValidation();
            if (!validation.isConsultationValid(note))
            {
                richtxtConsultation.BackColor = Color.LightPink;
                richtxtConsultation.Focus();
                return;
            }
            consultation.PatientId = patientID;
            consultation.Note = note;   
            consultation.AddedBy = "Habtish";
            consultation.Date= DateTime.Now;
            consultation.Remark = "Consultated";

            if (context.insertConsultation(consultation))
            {
                MessageBox.Show("Patient Consulation Added","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);      
            }
            else
            {
                MessageBox.Show("Patient Consulation Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void btnNewConsultation_Click(object sender, EventArgs e)
        {
            richtxtConsultation.Text = String.Empty;
        }

        private void richtxtConsultation_TextChanged(object sender, EventArgs e)
        {
            richtxtConsultation.BackColor = SystemColors.Window;
        }
        #endregion
        #region Back and Exit Buttons
        private void btnExit_Click(object sender, EventArgs e)
        {
           Application.Exit();  
        }

        private void btnBackHome_Click(object sender, EventArgs e)
        {
            HomeClinicalSystem homeClinicalSystem = new HomeClinicalSystem();
            homeClinicalSystem.Show();
        }
        public void createConsultationControls()
        {
            ControlsModelDbAcess controlsModelDb = new ControlsModelDbAcess();
            var controls = controlsModelDb.getAllControls();


        }

         #endregion       
        #region Dignosis
        private void btnSaveDiagnosis_Click(object sender, EventArgs e)
        {
            ///validation
            if (!(dropDownDiseases.EditValue is ComboxData data) || data.Id <= 1)
            {
                dropDownDiseases.BackColor = Color.LightPink;
                dropDownDiseases.Focus();
                return;
            }
            if (!(checkBoxprimary.Checked || checkBoxSecondary.Checked))
            {
                checkBoxprimary.BackColor = Color.LightPink;
                checkBoxprimary.Focus();
                checkBoxSecondary.BackColor = Color.LightPink;
                checkBoxSecondary.Focus();
                return;
            }
            if (!(checkBoxPersumed.Checked || checkBoxConfirmed.Checked))
            {
                checkBoxConfirmed.BackColor = Color.LightPink;
                checkBoxConfirmed.Focus();
                checkBoxPersumed.BackColor = Color.LightPink;
                checkBoxPersumed.Focus();
                return;
            }

            foreach (Controls control in listControls)
            {
                ComboxData data1=control.DiseasesType.EditValue as ComboxData;   

                if (data1 is null || data1.Id<=1)
                {
                    control.DiseasesType.BackColor = Color.LightPink;
                    control.DiseasesType.Focus();

                    return;
                }

                if (!(control.Primary.Checked || control.Secondary.Checked))
                {
                    control.Primary.BackColor = Color.LightPink;
                    control.Primary.Focus();
                    control.Secondary.BackColor = Color.LightPink;
                    control.Secondary.Focus();


                    return;
                }

                if (!(control.Confirmed.Checked || control.Persumed.Checked))
                {
                    control.Confirmed.BackColor = Color.LightPink;
                    control.Confirmed.Focus();
                    control.Persumed.BackColor = Color.LightPink;
                    control.Persumed.Focus();


                    return;
                }
                /////action
                DignosisModel dignosis1 = new DignosisModel();
                ComboxData slectedItem1 =control.DiseasesType.EditValue as ComboxData;
           

                dignosis1.Patient_Id = patientID;
                dignosis1.DiseasisTypeId = slectedItem1.Id;
                dignosis1.Order = control.Primary.Checked ? "Primary" : "Secondary";
                dignosis1.Certainity = control.Persumed.Checked ? "Confirmed" : "Persumed";
                dignosis1.Satus = control.Sataus.Checked ? "InActive" : "Active";
                dignosis1.AddedBy = "Habtish";
                dignosis1.Date = DateTime.Now;
                dignosis1.Description = control.Description.Text.Trim();

                if (context.InsertDignosis(dignosis1))
                {
                     checkStatus = true;

                }
                else
                {
                    checkStatus = false;    

                }



            }

            /////action
            DignosisModel dignosis = new DignosisModel();
            ComboxData diagnosisId = dropDownDiseases.EditValue as ComboxData;

            dignosis.Patient_Id = patientID;
            dignosis.DiseasisTypeId = diagnosisId.Id;
            dignosis.Order = checkBoxprimary.Checked ? "Primary" : "Secondary";
            dignosis.Certainity = checkBoxPersumed.Checked ? "Confirmed" : "Persumed";
            dignosis.Satus = checkBoxStatus.Checked ? "InActive" : "Active";
            dignosis.AddedBy = "Habtish";
            dignosis.Date = DateTime.Now;
            dignosis.Description = diagnosisDescription.Text.Trim();

            if (context.InsertDignosis(dignosis) && checkStatus)
            {

                MessageBox.Show("Dignosis Diseases Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Dignosis Diseases Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        bool btnDescriptionButton = true;
        private void addDescription_Click(object sender, EventArgs e)
        {
            if (btnDescriptionButton)
            {
                diagnosisDescription.Visible = true;
                descriptionTitle.Visible = true;
                addDescription.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
                paneldDignosis.Size = new Size(paneldDignosis.Width, 150);
                btnDescriptionButton = !btnDescriptionButton;

            }
            else if (!btnDescriptionButton)
            {
                diagnosisDescription.Visible = false;
                descriptionTitle.Visible = false;
                addDescription.Image = ((System.Drawing.Image)(resources.GetObject("addDescription.ImageOptions.Image")));
                paneldDignosis.Size = new Size(paneldDignosis.Width, 70);
                btnDescriptionButton = !btnDescriptionButton;

            }

        }
        private void paneldDignosis_SizeChanged(object sender, EventArgs e)
        {
            var firstPanel = true;
            int index = 0;
            if (dignosisPanel.Count != 0)
            {
                foreach (var panel in dignosisPanel)
                {

                    if (firstPanel)
                    {
                        panel.Location = new Point(0, paneldDignosis.Location.Y + paneldDignosis.Height + 10);

                        firstPanel = false;
                        index++;
                        continue;
                    }

                    panel.Location = new Point(0, dignosisPanel[index - 1].Location.Y + dignosisPanel[index - 1].Height + 10);

                    index++;
                }

                panelDiagnosisCondition.Location = new Point(0, dignosisPanel[dignosisPanel.Count - 1].Location.Y +
                    dignosisPanel[dignosisPanel.Count - 1].Height + 20);
                gridControlConditionDisplay.Location = new Point(0, panelDiagnosisCondition.Location.Y + panelDiagnosisCondition.Height + 20);


            }
            else
            {
                panelDiagnosisCondition.Location = new Point(0, paneldDignosis.Location.Y + paneldDignosis.Height + 20);
                gridControlConditionDisplay.Location = new Point(0, panelDiagnosisCondition.Location.Y + panelDiagnosisCondition.Height + 20);

            }

        }
        private void DignosisPanelSizeChanged(object sender, EventArgs e)//for new panel
        {
            var firstPanel = true;
            int index = 0;
            foreach (var panel in dignosisPanel)
            {
                if (firstPanel)
                {
                    firstPanel = false;
                    index++;
                    continue;
                }

                panel.Location = new Point(0, dignosisPanel[index - 1].Location.Y + dignosisPanel[index - 1].Height + 10);
                index++;
            }
            panelDiagnosisCondition.Location = new Point(0, dignosisPanel[dignosisPanel.Count - 1].Location.Y +
                dignosisPanel[dignosisPanel.Count - 1].Height + 20);
            gridControlConditionDisplay.Location = new Point(0, panelDiagnosisCondition.Location.Y + panelDiagnosisCondition.Height + 20);

        }
        private void addDignosis_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = true;
            Panel newPanel = new Panel();
            newPanel.Size = new Size(1360, 80);
            if (dignosisPanel.Count == 0)
            {
                newPanel.Location = new Point(0, paneldDignosis.Location.Y + paneldDignosis.Height + 10);
            }
            else
            {
                newPanel.Location = new Point(0, dignosisPanel[dignosisPanel.Count - 1].Location.Y + dignosisPanel[dignosisPanel.Count - 1].Height + 10);
            }
            panelDiagnosisCondition.Location = new Point(0, newPanel.Location.Y + newPanel.Height + 20);

            // Adjust the location as needed
            newPanel.BackColor = Color.FromArgb(240, 240, 240);
            newPanel.SizeChanged += DignosisPanelSizeChanged;
            dignosisPanel.Add(newPanel);
            panelDignosis.Controls.Add(newPanel);
           
            Button button = new Button();
            //button.Text = "Add";
            button.Image = ((System.Drawing.Image)(resources.GetObject("addDescription.ImageOptions.Image")));
            button.Location = new Point(1210, 9);
            button.Size = new Size(51, 46);
            button.BackColor = Color.White;
            button.Click += new EventHandler(ButtonForEachPanel_Click);
            newPanel.Controls.Add(button);

            Button btn = new Button();
            btn.Location = new Point(270, 18);
            btn.Text = "Accept";
            btn.Size = new Size(76, 32);
            btn.BackColor = Color.WhiteSmoke;
            btn.ForeColor = Color.Black;
            btn.Click += new EventHandler(AcceptButtonForEachDiagnosis_Click);
            btn.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            newPanel.Controls.Add(btn);
            //ConditionPanel.Controls.Add(newPanel);

            dignosisControl.DiseasesType = new SearchLookUpEdit();
            dignosisControl.DiseasesType.EditValue = 1;
            dignosisControl.DiseasesType.Properties.DataSource = context.LoadDiseasesType();
            dignosisControl.DiseasesType.Size = new Size(210, 28);
            dignosisControl.DiseasesType.Location = new Point(40, 23);
            dignosisControl.DiseasesType.Font = new Font("Times New Roman", 12.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dignosisControl.DiseasesType.Properties.DisplayMember = "Description";
            // dignosisControl.DiseasesType.Properties.ValueMember = "Id";
            dignosisControl.DiseasesType.Properties.PopulateViewColumns();



            dignosisControl.DiseasesType.TextChanged += DiseasesType_TextChanged;

            dignosisControl.Primary = new CheckBox();
            dignosisControl.Primary.Text = "Primary";
            dignosisControl.Primary.Size = new Size(81, 23);
            dignosisControl.Primary.Location = new Point(471, 23);
            dignosisControl.Primary.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            dignosisControl.Primary.CheckedChanged += primary_CheckedChanged;

            dignosisControl.Secondary = new CheckBox();
            dignosisControl.Secondary.Text = "Secondary";
            dignosisControl.Secondary.Size = new Size(98, 23);
            dignosisControl.Secondary.Location = new Point(556, 23);
            dignosisControl.Secondary.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            dignosisControl.Secondary.CheckedChanged += secondary_CheckedChanged;


            dignosisControl.Confirmed = new CheckBox();
            dignosisControl.Confirmed.Text = "Confirmed";
            dignosisControl.Confirmed.Size = new Size(98, 23);
            dignosisControl.Confirmed.Location = new Point(748, 16);
            dignosisControl.Confirmed.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            dignosisControl.Confirmed.CheckedChanged += confirmed_CheckedChanged;

            dignosisControl.Persumed = new CheckBox();
            dignosisControl.Persumed.Text = "Persumed";
            dignosisControl.Persumed.Size = new Size(94, 23);
            dignosisControl.Persumed.Location = new Point(850, 16);
            dignosisControl.Persumed.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            dignosisControl.Persumed.CheckedChanged += persumed_CheckedChanged;

            dignosisControl.Sataus = new CheckBox();
            dignosisControl.Sataus.Text = "InActive";
            dignosisControl.Sataus.Size = new Size(85, 23);
            dignosisControl.Sataus.Location = new Point(1092, 16);
            dignosisControl.Sataus.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            dignosisControl.DescriptionTitle = new Label();
            dignosisControl.DescriptionTitle.Text = "Description(optional)";
            dignosisControl.DescriptionTitle.Size = new Size(149, 19);
            dignosisControl.DescriptionTitle.Location = new Point(68, 83);
            dignosisControl.DescriptionTitle.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            dignosisControl.Description = new RichTextBox();
            dignosisControl.Description.Size = new Size(1098, 65);
            dignosisControl.Description.Location = new Point(72, 97);
            dignosisControl.Description.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));


            newPanel.Controls.Add(dignosisControl.DiseasesType);
            newPanel.Controls.Add(dignosisControl.Primary);
            newPanel.Controls.Add(dignosisControl.Secondary);
            newPanel.Controls.Add(dignosisControl.Confirmed);
            newPanel.Controls.Add(dignosisControl.Persumed);
            newPanel.Controls.Add(dignosisControl.Sataus);
            newPanel.Controls.Add(dignosisControl.DescriptionTitle);
            newPanel.Controls.Add(dignosisControl.Description);



            dignosisPanelX = dignosisPanelX + 190;

            listControls.Add(dignosisControl);


        }
        private void description_TextChanged(object sender, EventArgs e)
        {
            description.BackColor = SystemColors.Window;
        }
        private void AcceptButtonForEachDiagnosis_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        bool btnDiagnosisDescriptionCount = true;
        private void ButtonForEachPanel_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Panel parentPanel = (Panel)clickedButton.Parent;

            if (btnDiagnosisDescriptionCount)
            {
                clickedButton.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
                btnDiagnosisDescriptionCount = !btnDiagnosisDescriptionCount;
                parentPanel.Size = new Size(parentPanel.Width, 180);
                // parentPanel.Location=new Point()
            }
            else if (!btnDiagnosisDescriptionCount)
            {
                btnDiagnosisDescriptionCount = !btnDiagnosisDescriptionCount;
                clickedButton.Image = ((System.Drawing.Image)(resources.GetObject("addDescription.ImageOptions.Image")));
                parentPanel.Size = new Size(parentPanel.Width, 80);
            }
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (dignosisPanel.Count > 0)
            {
               // Panel lastPanel = panelCollection[panelCollection.Count - 1];
                Panel lastPanel = dignosisPanel[dignosisPanel.Count - 1];
                //panelCollection.Remove(lastPanel);
                 //ConditionPanel.Controls.Remove(lastPanel);
                dignosisPanel.Remove(lastPanel);
                panelDignosis.Controls.Remove(lastPanel);   
                dignosisPanelX = dignosisPanelX - 190;
                if (dignosisPanel.Count != 0)
                {
                    panelDiagnosisCondition.Location = new Point(0, dignosisPanel[dignosisPanel.Count - 1].Location.Y 
                        + dignosisPanel[dignosisPanel.Count - 1].Height + 20);

                }
                else
                {
                    panelDiagnosisCondition.Location = new Point(0, paneldDignosis.Location.Y + paneldDignosis.Height + 10);
                }

            }

        }



        private void DiseasesType_TextChanged(object sender, EventArgs e)
        {
            foreach (var control in listControls)
            {
                control.DiseasesType.BackColor = SystemColors.Window;

            }
        }
        private void primary_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in listControls)
            {
                if (control.Primary.Checked)
                {
                    control.Secondary.Checked = false;
                    control.Secondary.BackColor = SystemColors.Window;
                }
                control.Primary.BackColor = SystemColors.Window;
            }
        }

        private void secondary_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in listControls)
            {
                if (control.Secondary.Checked)
                {
                    control.Primary.Checked = false;
                    control.Primary.BackColor = SystemColors.Window;
                }
                control.Secondary.BackColor = SystemColors.Window;
            }
        }
        private void confirmed_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in listControls)
            {
                if (control.Confirmed.Checked)
                {
                    control.Persumed.Checked = false;
                    control.Persumed.BackColor = SystemColors.Window;
                }
                control.Confirmed.BackColor = SystemColors.Window;
            }
        }

        private void persumed_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in listControls)
            {
                if (control.Persumed.Checked)
                {
                    control.Confirmed.Checked = false;
                    control.Confirmed.BackColor = SystemColors.Window;
                }
                control.Persumed.BackColor = SystemColors.Window;
            }
        }

        private void checkBoxprimary_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxprimary.Checked)
            {
                checkBoxSecondary.Checked = false;
                checkBoxSecondary.BackColor = SystemColors.Window;
            }
            checkBoxprimary.BackColor = SystemColors.Window;
        }

        private void checkBoxSecondary_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSecondary.Checked)
            {
                checkBoxprimary.Checked = false;
                checkBoxprimary.BackColor = SystemColors.Window;
            }
            checkBoxSecondary.BackColor = SystemColors.Window;
        }

        private void checkBoxConfirmed_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxConfirmed.Checked)
            {
                checkBoxPersumed.Checked = false;
                checkBoxPersumed.BackColor = SystemColors.Window;
            }
            checkBoxConfirmed.BackColor = SystemColors.Window;
        }

        private void checkBoxPersumed_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPersumed.Checked)
            {
                checkBoxConfirmed.Checked = false;
                checkBoxConfirmed.BackColor = SystemColors.Window;
            }
            checkBoxPersumed.BackColor = SystemColors.Window;
        }



        private void btnNewDiagnosis_Click(object sender, EventArgs e)
        {
            dropDownDiseases.Text = string.Empty;
            checkBoxprimary.Checked = false;
            checkBoxSecondary.Checked = false;
            checkBoxPersumed.Checked = false;
            checkBoxConfirmed.Checked = false;
            checkBoxStatus.Checked = false;
        }

        private void dropDownDiseases_TextChanged(object sender, EventArgs e)
        {
            dropDownDiseases.BackColor = SystemColors.Window;
        }
        private void dropDownDiseases_EditValueChanged(object sender, EventArgs e)
        {
            dropDownDiseases.BackColor = Color.White;

        }

        bool btnConditionDescriptionAddChecker = true;
        private void btnConditionDescriptionAdd_Click(object sender, EventArgs e)
        {
            if (btnConditionDescriptionAddChecker)
            {
                panelDiagnosisCondition.Size = new Size(panelDiagnosisCondition.Width, 185);
                btnConditionDescriptionAdd.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
                btnConditionDescriptionAddChecker = !btnConditionDescriptionAddChecker;
                gridControlConditionDisplay.Location = new Point(0, panelDiagnosisCondition.Location.Y + panelDiagnosisCondition.Height + 10);


            }
            else if (!btnConditionDescriptionAddChecker)
            {
                panelDiagnosisCondition.Size = new Size(panelDiagnosisCondition.Width, 100);
                btnConditionDescriptionAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnConditionDescriptionAdd.ImageOptions.Image")));
                btnConditionDescriptionAddChecker = !btnConditionDescriptionAddChecker;
                gridControlConditionDisplay.Location = new Point(0, panelDiagnosisCondition.Location.Y + panelDiagnosisCondition.Height + 10);

            }
        }
      
        private void btnConditionAdd_Click(object sender, EventArgs e)
        {
            //validation
            ComboxData data1 = searchLookUpEditCondition.EditValue as ComboxData;
            if (data1 is null || data1.Id <= 1)
            {
                searchLookUpEditCondition.BackColor = Color.LightPink;
                searchLookUpEditCondition.Focus();
                return;
            }
            if (!(checkBoxActive.Checked || checkBoxInActive.Checked || checkBoxHistoryof.Checked))
            {
                checkBoxActive.BackColor = Color.LightPink;
                checkBoxActive.Focus();
                checkBoxInActive.BackColor = Color.LightPink;
                checkBoxInActive.Focus();
                checkBoxHistoryof.BackColor = Color.LightPink;
                checkBoxHistoryof.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dateEditCondition.Text.Trim()))
            {
                dateEditCondition.BackColor = Color.LightPink;
                dateEditCondition.Focus();
            }
            /*
             * creating a modle data to crate the detail grid view
             * and to save to the database
             */

            DignosisConditionDetail conditionDetail = new DignosisConditionDetail();
            conditionDetail.Condition = searchLookUpEditCondition.Properties.GetDisplayText(searchLookUpEditCondition.EditValue);
            conditionDetail.Date = dateEditCondition.DateTime.Date;
            conditionDetail.Note = richTextBoxConditionDescription.Text.Trim();
            if (checkBoxActive.Checked)
            {
                conditionDetail.Action = "Active";

            }
            else if (checkBoxInActive.Checked)
            {
                conditionDetail.Action = "In Active";
            }
            else
            {
                conditionDetail.Action = "History Of";
            }

            conditionDetails.Add(conditionDetail);
            //creating a grid view to show detail about patient conditions

            GridView gridViewConditiondisplay = new GridView(gridControlConditionDisplay);
            gridControlConditionDisplay.MainView = gridViewConditiondisplay;
            gridControlConditionDisplay.Location = new Point(0, panelDiagnosisCondition.Location.Y + panelDiagnosisCondition.Height + 20);
            gridControlConditionDisplay.Size = new Size(panelDiagnosisCondition.Width, 400);
            gridViewConditiondisplay.OptionsBehavior.Editable = false;
            //gridViewConditiondisplay.CustomRowCellEdit += gridViewConditiondisplay_CustomRowCellEdit;
            gridViewConditiondisplay.PopulateColumns();
            gridViewConditiondisplay.OptionsView.ShowGroupPanel = false;
            gridViewConditiondisplay.Appearance.HeaderPanel.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            gridViewConditiondisplay.Appearance.Row.Font = new Font("Times New Roman", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            gridViewConditiondisplay.Appearance.HeaderPanel.BackColor = Color.RoyalBlue;

            gridControlConditionDisplay.DataSource = conditionDetails;
            panelDignosis.Controls.Add(gridControlConditionDisplay);
            //clearing filed values
            searchLookUpEditCondition.EditValue = 1;
            checkBoxActive.Checked = false;
            checkBoxInActive.Checked = false;
            checkBoxHistoryof.Checked = false;
            richTextBoxConditionDescription.Text = string.Empty;
            dateEditCondition.DateTime = DateTime.Today;


        }


        private void panelDiagnosisControlBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelDiagnosisCondition_SizeChanged(object sender, EventArgs e)
        {
            gridControlConditionDisplay.Location = new Point(0, panelDiagnosisCondition.Location.Y + panelDiagnosisCondition.Height + 20);

        }

        private void btnAcceptDignosisDiseases_Click(object sender, EventArgs e)
        {

        }

        private void searchLookUpEditCondition_EditValueChanged(object sender, EventArgs e)
        {
            string displayMember = searchLookUpEditCondition.Properties.GetDisplayText(dropDownDiseases.EditValue);
            searchLookUpEditCondition.BackColor = Color.White;

        }

        private void btnAcceptCondition_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxActive_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxActive.Checked)
            {
                checkBoxInActive.Checked = false;
                checkBoxInActive.BackColor = Color.White;
                checkBoxHistoryof.Checked = false;
                checkBoxHistoryof.BackColor = Color.White;

            }
            checkBoxActive.BackColor = Color.White;
        }

        private void checkBoxInActive_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxInActive.Checked)
            {
                checkBoxActive.Checked = false;
                checkBoxActive.BackColor = Color.White;
                checkBoxHistoryof.Checked = false;
                checkBoxHistoryof.BackColor = Color.White;

            }
            checkBoxInActive.BackColor = Color.White;
        }

        private void checkBoxHistoryof_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHistoryof.Checked)
            {
                checkBoxInActive.Checked = false;
                checkBoxInActive.BackColor = Color.White;
                checkBoxActive.Checked = false;
                checkBoxActive.BackColor = Color.White;

            }
            checkBoxHistoryof.BackColor = Color.White;

        }

        private void dateEditCondition_EditValueChanged(object sender, EventArgs e)
        {
            dateEditCondition.BackColor = Color.White;
        }
        #endregion
        #region Disposition
        /*
         * outer by habtish
         * the method performs the following actions
         * 1-checks the selected action and checks for validation
         * 2- checks the dispositon status of the patient
         * saves the paties dispaosiition
         * refresh the disposition status for that patient
         */

        private void btnSaveDisposition_Click(object sender, EventArgs e)
        {
            if (dropDownSelectAction.SelectedIndex < 1)
            {
                dropDownSelectAction.BackColor = Color.LightPink;
                dropDownSelectAction.Focus();
                return;
            }
            if (String.IsNullOrEmpty(description.Text.Trim()))
            {
                description.BackColor=Color.LightPink;
                description.Focus();
                return;
            }

            DispositionModel model=new DispositionModel();
            var dispositioType=dropDownSelectAction.Text.Trim();    
            var DispositionNote=description.Text.Trim();
      
            if(dispositioType=="Addmit Patient")
            {
                model.Patient_Id = patientID;
                model.Room_Id = 3;
                model.Date = DateTime.Now;
                model.Added_By = "Habtish";
                model.Status = true;
                if (context.InsertDisposition(model))
                {

                    MessageBox.Show("Patient Addmitted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshComboBoxListAfterSaved();
                }

                else
                {
                    MessageBox.Show("Patient Addmission Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
                
                    
            }
            else if(dispositioType== "Discharge Patient")
            {

                model.Patient_Id= patientID; 
                model.Date = DateTime.Now;
                model.Added_By = "Habtish";
                model.Status = false;
                if (context.UpdateDisposition(model))
                {

                    MessageBox.Show("Patient Discharged", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshComboBoxListAfterSaved();
                }

                else
                {
                    MessageBox.Show("Patient Discharging Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
            }

            else
            {
                MessageBox.Show("Nothing You Can Do With This Patient", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
        public void RefreshComboBoxListAfterSaved()
        {


            List<String> dispositionStatus = context.LoadDispositionType(patientID);
            dropDownSelectAction.DataSource = dispositionStatus;
        }
        public void dropDownSelectAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            dropDownSelectAction.BackColor= SystemColors.Window;
        }
        #endregion       
        #region Lab Order
        public void createSamples()
        {
            int sampleLocationX = 27;
            int sampleLocationY = 5;
            int countSamples = 0;
            int numberOfSampleRows = 1;
            //panelSelectedTest.Size = new Size(252, 10);
            var samples =orders.GetSamples();
            foreach (var sample in samples)
            { 
                Button button = new Button();
                button.Text = sample.SampleName;
                button.ForeColor = Color.White;
                button.MouseEnter += Button_MouseEnter;
                button.MouseLeave += Button_MouseLeave;
               
                button.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                button.BackColor = Color.CadetBlue;
                button.Tag = sample;
                button.Click+=SampleButton_Click;
                button.Size = new Size(210,32);
                button.Location = new Point(sampleLocationX,sampleLocationY);
                sampleLocationX += button.Width + 5;
                panelSample.Controls.Add(button);
                countSamples++;
                if (countSamples % 5 == 0)
                {
                    sampleLocationX = 27;   
                    numberOfSampleRows++;   
                    sampleLocationY += button.Height + 5;
                }
            }
            panelSample.Height=numberOfSampleRows*37+10;
            groupBoxPanel.Location= new Point(panelSample.Location.X, panelSample.Bottom + 10);
            //panelSelectedTest.Location= new Point(panelSelectedTest.Location.X, panelSelectedTestTitle.Bottom);


        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button hoverdButton = (Button)sender;
            hoverdButton.BackColor= Color.DarkCyan;
        }
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button hoverdButton = (Button)sender;
            hoverdButton.BackColor = Color.CadetBlue;
        }
        private void SampleButton_Click(object sender, EventArgs e)
        {

            if (sender is Button button)
            {
                            
                if (button.Tag is Sample samples)
                {
                 
                    if (samples.isLoadFromDb)
                    {
                        panels = orders.GetPanels(samples.Id);
                        allTests = orders.getPanelAandTests(samples.Id);
                        allpanelsinAllSamples.AddRange(panels);
                        allTestsinAllSamples.AddRange(allTests);
                    }
                    else
                    {
                        panels=allpanelsinAllSamples.Where(x => x.SampleId == samples.Id).ToList();
                        allTests= allTestsinAllSamples.Where(x=>x.SampleId == samples.Id).ToList(); 
                    }

                    samples.isLoadFromDb = false;
                
                
                    int panelButtonX = 11;
                    int panelButtonY = 35;
                    int panelCount = 0;
                    int countButtons = 0;
                    groupBoxPanel.Controls.Clear();                  
                    foreach (Panels panel in panels) 
                    {
                        SimpleButton panelButton =new SimpleButton();
                        panelButton.Tag = panel;
                        panelButton.Click += PanelButton_Click;
                        panelButton.Text = panel.PanelName;
                        panelButton.Size = new Size(270, 42);
                        panelButton.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

                        if (panel.isClicked)
                        {
                            panelButton.ForeColor = Color.Black;
                            panelButton.BackColor = Color.WhiteSmoke;

                        }
                        else
                        {                           
                            panelButton.ForeColor = Color.Blue;
                            panelButton.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton5.ImageOptions.Image")));
                            panelButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
                            panelButton.BackColor = Color.DarkGray;
                        }
                        panelButton.Location=new Point(panelButtonX,panelButtonY);
                        panelButtonX += 280;
                        groupBoxPanel.Controls.Add(panelButton);    
                        panelCount++;
                        if (panelCount % 4 == 0)
                        {
                            panelButtonX = 11;
                            panelButtonY += 54;
                            countButtons++;
                        }
                       
                   }
                    if (panels.Count % 4 != 0)
                    {
                        countButtons+=1;
                    }
                    groupBoxPanel.Height=countButtons*54+40;
                    groupBoxTests.Location = new Point(groupBoxTests.Location.X, groupBoxPanel.Bottom+10);


                    ////for tests
               
                    int testButtonX = 11;
                    int testButtonY = 35;
                    int testCount = 0;
                    int countButtonsForTests = 0;
                    groupBoxTests.Controls.Clear();
                    foreach (var test in allTests)
                     {
                        SimpleButton testButton = new SimpleButton();
                        testButton.Tag = test;                       
                        testButton.Text = test.TestName;
                        testButton.Size = new Size(270, 42);
                        testButton.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

                        if (test.isClicked)
                        {
                            testButton.ForeColor = Color.Black;
                            testButton.BackColor = Color.WhiteSmoke;


                        }
                        else
                        {
                            testButton.ForeColor = Color.Blue;
                            testButton.BackColor = Color.White;
                            testButton.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton5.ImageOptions.Image")));
                            testButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;


                        }
                        testButton.Location = new Point(testButtonX, testButtonY);
                        testButtonX += 280;
                        groupBoxTests.Controls.Add(testButton);
                        ListofButtons listofButtons = new ListofButtons
                        {
                            TestButton = testButton,
                            Id = test.PanelId
                        };
                        testButtonList.Add(listofButtons);
                        testCount++;
                        if (testCount % 4 == 0)
                        {
                            testButtonX = 11;
                            testButtonY += 54;
                            countButtonsForTests++;
                        }
                    }
                    if (allTests.Count % 4 != 0)
                    {
                        countButtonsForTests += 1;
                    }
                    groupBoxTests.Height = countButtonsForTests * 54 + 40;
                }
            }

        }

        private void PanelButton_Click(object sender, EventArgs e)
        {
           
            if (sender is SimpleButton button)
            {
                if (button.Tag is Panels panel)
                {
                    List<ListofButtons> testbuttons = testButtonList.Where(x=>x.Id==panel.Id).ToList();
                    var panelWithTests = allTests.Where(x => x.PanelId == panel.Id);
                    if (panel.isClicked)
                    {
                        button.ForeColor = Color.Blue;
                        button.Text= panel.PanelName.ToString();
                        button.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton5.ImageOptions.Image")));
                        button.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
                        button.BackColor = Color.DarkGray;
                        
                        panel.isClicked = false;
                        
                        //create order
                        Label lbl = new Label();
                        lbl.Tag = panel.Id;
                        lbl.Text = panel.PanelName.ToString().Trim();
                        lbl.BackColor = Color.WhiteSmoke;
                        lbl.ForeColor=Color.Black;
                        lbl.Font= new Font("Times New Roman", 11F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                         if (OrderedPanelsLabel.Count == 0)
                            {
                                lbl.Location = new Point(30, 55);
                              
                            }
                            else
                            {
                                lbl.Location = new Point(30, OrderedPanelsLabel[OrderedPanelsLabel.Count - 1].Location.Y + OrderedPanelsLabel[OrderedPanelsLabel.Count - 1].Height + 20);
                            }
                        

                        panelSelectedTest.Controls.Add(lbl);
                        OrderedPanelsLabel.Add(lbl);    
                        //add the selected panel to the list
                     
                        foreach(SampleElements test in panelWithTests)
                        {
                            test.isClicked = false;
                          


                        }
                        //var panelandTests = allTests.FirstOrDefault(x => x.PanelId == panel.Id);
                        //sampleElements.Add(panelandTests);
                        foreach (ListofButtons testbutton in testbuttons)
                        {
                            testbutton.TestButton.ForeColor = Color.Black;
                            testbutton.TestButton.BackColor = Color.WhiteSmoke;
                            testbutton.TestButton.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton5.ImageOptions.Image")));
                            testbutton.TestButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
                  

                        }




                    }
                    else if (!panel.isClicked)
                    {
                        button.ForeColor = Color.DarkBlue;
                        button.BackColor = Color.LightGray;
                        button.ImageOptions.Image = null;
                        panel.isClicked = true;
                        //removing the label and the panel from the list
                        Label label = panelSelectedTest.Controls.OfType<Label>()
                         .FirstOrDefault(tag=>tag.Tag!=null&& tag.Tag.ToString()==panel.Id.ToString());
                        panelSelectedTest.Controls.Remove(label);
                        OrderedPanelsLabel.Remove(label);
                        var firstPanel = true;
                        int index = 0;
                        foreach (var item in OrderedPanelsLabel)
                        {

                            if (firstPanel)
                            {
                                firstPanel = false;
                                index++;
                                item.Location = new Point(30, 55);
                                continue;
                            }

                            item.Location = new Point(30, OrderedPanelsLabel[index - 1].Location.Y + OrderedPanelsLabel[index - 1].Height + 20);
                            index++;
                        }
                        foreach (var test in panelWithTests)
                        {
                            test.isClicked = true;


                        }
                        foreach (var testbutton in testbuttons)
                        {
                           testbutton.TestButton.ForeColor = Color.Black;
                           testbutton.TestButton.BackColor = Color.White;
                           testbutton.TestButton.ImageOptions.Image = null;
                       
                        }

                    }
                   
                
            }
            }
        }
       
        
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            panelRadiology.Visible = !panelRadiology.Visible;
            if(!panelRadiology.Visible ) {
                 //simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));

            }
            else
            {
                 //simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));

            }
        }

      

        private void btnOrderSave_Click(object sender, EventArgs e)
        {
            LabOrderExtended orderExtended = new LabOrderExtended();
            orderExtended.PatientId = patientID;
            orderExtended.UserId = 1;
            orderExtended.Date=DateTime.Now;    
            if(orders.saveLabOrders(sampleElements,orderExtended))
            {
                MessageBox.Show("Lab Order Successfully Ordered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lab Order Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnOrderCancel_Click(object sender, EventArgs e)
        {

        }

        #endregion
        #region Appointment
        public int dateComparision(DateTime date)
        {
            DateTime today = DateTime.Today;
            if (date.Date == today)
                return 0;
            else
                return -date.CompareTo(today);
        }
        public void loadAppointmentFields()
        {
            var servicetypes = dbContext.getServiceType();
            comboBoxServices.DataSource = servicetypes;
            comboBoxServices.ValueMember = "id";
            comboBoxServices.DisplayMember = "service_description";

            List<VisitLocation> visitLocation = dbContext.getVisitLocation();
            comboBoxLocation.DataSource = visitLocation;
            comboBoxLocation.DisplayMember = "description";
            comboBoxLocation.ValueMember = "id";


        }
        private void gridViewAppointmentdocument_RowStyle(object sender, RowStyleEventArgs e)
        {

            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.RowHandle >= 0)
            {

                DateTime cellValue = DateTime.Parse(view.GetRowCellValue(e.RowHandle,
                                                        "OrderedDate").ToString());


                if (cellValue > DateTime.Now)
                {
                    e.Appearance.BackColor = Color.LightGray;

                }
                if (cellValue < DateTime.Now)
                {
                    e.Appearance.BackColor = Color.LightPink;
                }

                if (cellValue == DateTime.Now)
                {
                    e.Appearance.BackColor = Color.LightBlue;
                    e.HighPriority = true;
                }

            }
        }

        private void btnSaveAppointment_Click(object sender, EventArgs e)
        {

            VisitLocation selectedVisitLocation = (VisitLocation)comboBoxLocation.SelectedItem;
            var visitLocationId = selectedVisitLocation.id;

            ServiceType selectedServiceType = (ServiceType)comboBoxServices.SelectedItem;
            var serviceTypeId = selectedServiceType.id;

            if (string.IsNullOrEmpty(comboBoxServices.Text.Trim()) || serviceTypeId <= 1)
            {
                comboBoxServices.BackColor = Color.LightPink;
                comboBoxServices.Focus();
                return;
            }
            if (string.IsNullOrEmpty(comboBoxLocation.Text.Trim()) || visitLocationId <= 1)
            {
                comboBoxLocation.BackColor = Color.LightPink;
                comboBoxLocation.Focus();
                return;
            }
            if (string.IsNullOrEmpty(appointmentDate.DateTime.ToString()) || appointmentDate.DateTime <= DateTime.Now)
            {
                appointmentDate.BackColor = Color.LightPink;
                appointmentDate.Focus();
                MessageBox.Show("Appointmnet Date Can not be the Passed Date, Re Enter The Date Again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(appointmentNote.Text.Trim()))
            {
                appointmentNote.BackColor = Color.LightPink;
                appointmentNote.Focus();
                return;
            }

            AppointmmentDto appointmmentDto = new AppointmmentDto();
            appointmmentDto.PatientId = patient.ID.ToString();

            appointmmentDto.AppointmentTypeId = visitLocationId;


            appointmmentDto.VisitLocationId = (int)serviceTypeId;

            appointmmentDto.AppointmentDescription = appointmentNote.Text.Trim();
            appointmmentDto.Status = true;
            appointmmentDto.OrderedBy = "Habtish";
            appointmmentDto.AppointmentDate = appointmentDate.DateTime;
            appointmmentDto.Remark = string.Empty;
            var addAppointment = dbContext.addAppointment(appointmmentDto);
            if (addAppointment)
            {

                MessageBox.Show("Appointment Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //refresh the appoiontment list
                var appointmentList = dbContext.loadAppointmentSummary();
                appointmentList.OrderBy(x => dateComparision(x.OrderedDate)).ToList();
                gridControlAppointmentDocument.DataSource = dbContext.loadAppointmentSummary();
            }
            else
            {
                MessageBox.Show("Appointment Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void appointmentDate_Click(object sender, EventArgs e)
        {

        }

        private void appointmentDate_DateTimeChanged(object sender, EventArgs e)
        {
            appointmentDate.BackColor = SystemColors.Window;
        }

        private void appointmentNote_TextChanged(object sender, EventArgs e)
        {
            appointmentNote.BackColor = SystemColors.Window;
        }
        private void comboBoxServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxServices.BackColor = SystemColors.Window;
        }

        private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxLocation.BackColor = SystemColors.Window;
        }
        #endregion
        #region Medication

        bool drugOrderPanel = true;
        
        private void btnDrugOrder_Click(object sender, EventArgs e)
        {
            int height = 527;
            height= drugOrderPanel? panelDrug.Height:height;
            
            if (drugOrderPanel)
            {
                panelDrug.Height = 0;
                drugOrderPanel=!drugOrderPanel;
            }
            
            else 
            {
                panelDrug.Height = height;
                drugOrderPanel = !drugOrderPanel;
            }
        }



        #endregion

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            appointmentDate.DateTime= DateTime.Now;
        }
    }
}

using Clinical_Managment_System.Data_Access_Layer;
using Clinical_Managment_System.Models;
using Clinical_Managment_System.Properties;
using Clinical_Managment_System.Validation;
using DevExpress.Utils.Extensions;
using DevExpress.Utils.ScrollAnnotations;
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
        List<Panel> panelCollection = new List<Panel>();
        Orders orders = new Orders();
        List<Panels> panels = new List<Panels>();
        List<Tests> tests = new List<Tests>();
        List<SampleElements> sampleElements = new List<SampleElements>();
        List<SampleElements> allTests=new List<SampleElements>();
        List<Label> OrderedPanelsLabel= new List<Label>();
       
     
        public PatientModel patient {  get; set; }
        int patientID;
      
        int dignosisPanelX = 217;
      
        List<ListofButtons> testButtonList = new List<ListofButtons>();
  
        bool checkStatus = true;
        Controls dignosisControl = new Controls();
        List<Controls> listControls = new List<Controls>();
        List<Panel> dignosisPanel=new List<Panel>();    
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClinicalManagmentSystemForm));


        #region

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
           var comboList=context.LoadDiseasesType();
           
            dropDownDiseases.Properties.DataSource=comboList;
            dropDownDiseases.EditValue = 1;
            dropDownDiseases.Properties.DisplayMember = "Description";
            ////dropDownDiseases.Properties.ValueMember = "Id";
            dropDownDiseases.Properties.PopulateViewColumns();

            //Load Combo Box List For Disposition Type
            //pass patient id as a parameter
            //load patint id based on person id from patient table
             patientID = context.loadPatientId(patient.ID.ToString());
            List<String> type = context.LoadDispositionType(patientID);
            dropDownSelectAction.DataSource = type;

            //for Dignosis Desccription
           
            diagnosisDescription.Visible = false; 
            descriptionTitle.Visible = false;
            
            //ende for the above action

            //////////////////////////Lab Order//////////
            createSamples();

        }
        #region

        public void createConsultationControls()
        {
            ControlsModelDbAcess controlsModelDb=new ControlsModelDbAcess();
            var controls=controlsModelDb.getAllControls();


        }

        public void RefreshComboBoxListAfterSaved()
        {
                                      
           
            List<String> type = context.LoadDispositionType(patientID);
            dropDownSelectAction.DataSource = type;
        }

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

        private void btnExit_Click(object sender, EventArgs e)
        {
           Application.Exit();  
        }



        private void description_TextChanged(object sender, EventArgs e)
        {
            description.BackColor=SystemColors.Window;
        }

        private void btnSaveDiagnosis_Click(object sender, EventArgs e)
        {
            ///validation
            if (Convert.ToInt32(dropDownDiseases.EditValue) <= 1)
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
            //    if (String.IsNullOrEmpty(control.DiseasesType.Text.Trim()))
            //    {
            //        control.DiseasesType.BackColor = Color.LightPink;
            //        control.DiseasesType.Focus();

            //        return;
            //    }

                if (Convert.ToInt32(control.DiseasesType.EditValue)<=1)
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
                object slectedItem1 =control.DiseasesType.EditValue;
           

                dignosis1.Patient_Id = patientID;
                dignosis1.DiseasisTypeId = Convert.ToInt32(slectedItem1);
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
            object diagnosisId = dropDownDiseases.EditValue;

            dignosis.Patient_Id = patientID;
            dignosis.DiseasisTypeId = Convert.ToInt32(diagnosisId);
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



        private void checkBoxprimary_CheckedChanged_1(object sender, EventArgs e)
        {
            if(checkBoxprimary.Checked) 
            {
                checkBoxSecondary.Checked = false;
                checkBoxSecondary.BackColor = SystemColors.Window;
            }
            checkBoxprimary.BackColor = SystemColors.Window;
        }

        private void checkBoxSecondary_CheckedChanged_1(object sender, EventArgs e)
        {
            if(checkBoxSecondary.Checked)
            {
                checkBoxprimary.Checked = false;
                checkBoxprimary.BackColor= SystemColors.Window; 
            }
            checkBoxSecondary.BackColor = SystemColors.Window;  
        }

        private void checkBoxConfirmed_CheckedChanged_1(object sender, EventArgs e)
        {
            if(checkBoxConfirmed.Checked) 
            { 
                checkBoxPersumed.Checked = false;
                checkBoxPersumed.BackColor = SystemColors.Window;
            }
            checkBoxConfirmed.BackColor = SystemColors.Window;
        }

        private void checkBoxPersumed_CheckedChanged_1(object sender, EventArgs e)
        {
            if(checkBoxPersumed.Checked) 
            { 
                checkBoxConfirmed.Checked = false;  
                checkBoxConfirmed.BackColor= SystemColors.Window;   
            }
            checkBoxPersumed.BackColor = SystemColors.Window;   
        }

       

        private void btnNewDiagnosis_Click(object sender, EventArgs e)
        {
            dropDownDiseases.Text = string.Empty;
            checkBoxprimary.Checked = false;
            checkBoxSecondary.Checked = false;
            checkBoxPersumed.Checked= false;
            checkBoxConfirmed.Checked= false;   
            checkBoxStatus.Checked= false;
        }

        private void dropDownDiseases_TextChanged(object sender, EventArgs e)
        {
            dropDownDiseases.BackColor = SystemColors.Window;   
        }

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

        private void btnNewDisposition_Click(object sender, EventArgs e)
        {
            dropDownSelectAction.Text = String.Empty;
            description.Text = String.Empty;
        }

        private void btnNewConsultation_Click(object sender, EventArgs e)
        {
            richtxtConsultation.Text = String.Empty;
        }

        private void richtxtConsultation_TextChanged(object sender, EventArgs e)
        {
            richtxtConsultation.BackColor = SystemColors.Window;
        }

       

        private void btnBackHome_Click(object sender, EventArgs e)
        {
            HomeClinicalSystem homeClinicalSystem = new HomeClinicalSystem();
            homeClinicalSystem.Show();
        }
        #endregion

        #region Diagnosis Part
        bool btnDescriptionButton = true;
        private void addDescription_Click(object sender, EventArgs e)
        {
            if (btnDescriptionButton)
            {
                diagnosisDescription.Visible = true;
                descriptionTitle.Visible = true;
                addDescription.Image= ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
                paneldDignosis.Size = new Size(paneldDignosis.Width, 155);
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
                       panel.Location = new Point(0, paneldDignosis.Location.Y + paneldDignosis.Height + 5);


                        firstPanel = false;
                        index++;
                        continue;
                    }

                    panel.Location = new Point(0, dignosisPanel[index - 1].Location.Y + dignosisPanel[index - 1].Height + 5);

                    index++;
                }

                panelDiagnosisCondition.Location = new Point(0, dignosisPanel[dignosisPanel.Count - 1].Location.Y +
                    dignosisPanel[dignosisPanel.Count - 1].Height + 10);


            }
            else
            {
                panelDiagnosisCondition.Location = new Point(0, paneldDignosis.Location.Y + paneldDignosis.Height + 10);

            }

        }
        private void DignosisPanelSizeChanged(object sender, EventArgs e)
        {
            var firstPanel = true;
            int index = 0;
            foreach (var panel in dignosisPanel)
            {
              
                if (firstPanel)
                {
                    firstPanel=false;
                    index++;
                    continue;

                }

                panel.Location = new Point(0, dignosisPanel[index - 1].Location.Y + dignosisPanel[index - 1].Height + 5);

                index++;
            }

            panelDiagnosisCondition.Location = new Point(0, dignosisPanel[dignosisPanel.Count - 1].Location.Y +
                dignosisPanel[dignosisPanel.Count - 1].Height + 10);


        }
        private void addDignosis_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = true;


            Panel newPanel = new Panel();
            newPanel.Size = new Size(1280, 80);
            //newPanel.Location = new Point(0, dignosisPanelX); // Adjust the location as needed
            if (dignosisPanel.Count == 0)
            {
                newPanel.Location = new Point(0, paneldDignosis.Location.Y + paneldDignosis.Height + 5);
            }
            else
            {
                newPanel.Location=new Point(0, dignosisPanel[dignosisPanel.Count - 1].Location.Y + dignosisPanel[dignosisPanel.Count - 1].Height + 5);
            }
            panelDiagnosisCondition.Location= new Point(0, newPanel.Location.Y + newPanel.Height + 10);

            // Adjust the location as needed
            newPanel.BackColor = Color.FromArgb(240, 240, 240);
            newPanel.SizeChanged += DignosisPanelSizeChanged;
            dignosisPanel.Add(newPanel);

            // Add the panel to the collection and the form
            panelCollection.Add(newPanel);
            Button button=new Button();
            //button.Text = "Add";
            button.Image= ((System.Drawing.Image)(resources.GetObject("addDescription.ImageOptions.Image")));
            button.Location=new Point(1210, 9);
            button.Size = new Size(51,46);
            button.BackColor = Color.White;
            button.Click += new EventHandler(ButtonForEachPanel_Click);
            newPanel.Controls.Add(button);

            Button btn=new Button();
            btn.Location = new Point(270, 18);
            btn.Text = "Accept";
            btn.Size = new Size(76, 32);
            btn.BackColor= Color.WhiteSmoke;
            btn.ForeColor= Color.Black;
            btn.Click += new EventHandler(AcceptButtonForEachDiagnosis_Click);    
            btn.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            newPanel.Controls.Add(btn);

            DignosisPanel.Controls.Add(newPanel);

            dignosisControl.DiseasesType = new SearchLookUpEdit();
            dignosisControl.DiseasesType.EditValue = 1;
            dignosisControl.DiseasesType.Properties.DataSource = context.LoadDiseasesType();
            dignosisControl.DiseasesType.Size = new Size(210, 28);
            dignosisControl.DiseasesType.Location = new Point(40, 23);
            dignosisControl.DiseasesType.Font = new Font("Times New Roman", 12.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));      
            dignosisControl.DiseasesType.Properties.DisplayMember = "Description";
            dignosisControl.DiseasesType.Properties.ValueMember = "Id";
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

            dignosisControl.DescriptionTitle=new Label();
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
                clickedButton.Image= ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
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

            if (panelCollection.Count > 0)
            {
                Panel lastPanel = panelCollection[panelCollection.Count - 1];
                panelCollection.Remove(lastPanel);
                DignosisPanel.Controls.Remove(lastPanel);
                dignosisPanel.Remove(lastPanel);    
                dignosisPanelX = dignosisPanelX - 190;
                if (dignosisPanel.Count != 0)
                {
                    panelDiagnosisCondition.Location = new Point(0, dignosisPanel[dignosisPanel.Count - 1].Location.Y + dignosisPanel[dignosisPanel.Count - 1].Height + 20);

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
                control.DiseasesType.BackColor = Color.White;   

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
        #endregion



        ////////////////////////////Lab Order////////////////////////////////////////
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
                button.Click+=Button_Click;
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
        private void Button_Click(object sender, EventArgs e)
        {

            if (sender is Button button)
            {
                            
                if (button.Tag is Sample samples)
                {
                 
                    panels = orders.GetPanels(samples.Id);                    
                     allTests = orders.getPanelAandTests(samples.Id);
                    int panelButtonX = 11;
                    int panelButtonY = 35;
                    int panelCount = 0;
                    int countButtons = 0;
                    groupBoxPanel.Controls.Clear();                  
                    foreach (var panel in panels) 
                    {
                        SimpleButton panelButton =new SimpleButton();
                        panelButton.Tag = panel;
                        panelButton.Click += PanelButton_Click;
                        panelButton.Text = panel.PanelName;
                        panelButton.Size = new Size(270, 42);
                        panelButton.ForeColor = Color.Black;
                        panelButton.BackColor = Color.WhiteSmoke;
                        panelButton.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                        testButton.ForeColor = Color.Black;
                        testButton.BackColor = Color.WhiteSmoke;
                        testButton.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                    var testbuttons = testButtonList.Where(x=>x.Id==panel.Id).ToList();

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
                        SampleElements panelWithTests=allTests.FirstOrDefault(x=>x.PanelId==panel.Id);
                        sampleElements.Add(panelWithTests);
                        foreach (var testbutton in testbuttons)
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
        //bool isTestButtonClicked = true;
        //private void TestButton_Click(object sender, EventArgs e)
        //{
            //if (sender is SimpleButton button)
            //{
            //    if (button.Tag is SampleElements elements)
            //    {
            //       if (elements.isClicked)
            //        {
            //            button.ForeColor = Color.Black;
            //            button.BackColor = Color.WhiteSmoke;
            //            isTestButtonClicked = !isTestButtonClicked;
            //            elements.isClicked = !elements.isClicked;
            //            sampleElements.Add(elements);
                       
            //           //lable for slected order
            //           Panel panelTest =new Panel();
            //            panelTest.Size=new Size(251, 40);
            //            panelTest.Location=new Point(0, selectedTestPanelHeight);
            //            selectedTestPanelHeight += 45;
            //            panelSelectedTest.Controls.Add(panelTest);

            //            Label label = new Label();
            //            label.Text = elements.TestName.Trim();
            //            label.Tag = elements.TestId;
            //            //label.AutoSize = true;  
            //            label.Location = new Point(6, 14);
            //            //labelY += 28;
            //            label.BackColor = Color.WhiteSmoke;
            //            button.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton5.ImageOptions.Image")));
            //            button.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            //            label.Font= new Font("Times New Roman", 11F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            //            panelTest.Controls.Add(label);
            //            panelSelectedTest.Size = new Size(panelSelectedTest.Width, panelSelectedTest.Height + 45);
                        ///for remove button \
                        //Button buttonRemove = new Button();
                        //buttonRemove.Text = "Remove";
                        //button.Tag = elements;
                        //buttonRemove.Location = new Point(160, 10);
                        //buttonRemove.Click += new EventHandler(ButtonForEachLabel_Click);
                        //panelTest.Controls.Add(buttonRemove);
                   // }
            //        else if (!elements.isClicked)
            //        {
                     
            //            button.ForeColor = Color.Black;
            //            button.BackColor = Color.White;
            //            isTestButtonClicked = !isTestButtonClicked;
            //            elements.isClicked = !elements.isClicked;
            //            button.ImageOptions.Image = null;
            //            Control label = panelSelectedTest.Controls.OfType<Label>()
            //                .FirstOrDefault(tag=>tag.Tag!=null&& tag.Tag.ToString()==elements.TestId.ToString());

            //           panelSelectedTest.Controls.Remove(label);
            //           panelSelectedTest.Size = new Size(panelSelectedTest.Width, panelSelectedTest.Height - 45);
            //           sampleElements.Remove(elements);
                  


            //        }
            //    }
            //}
       // }

        private void ButtonForEachLabel_Click(object sender, EventArgs e)
        {
            //Button clickedButton = (Button)sender;
            //Panel parentPanel = (Panel)clickedButton.Parent;
            //SampleElements element= (SampleElements)clickedButton.Tag;
            //panelSelectedTest.Controls.Remove(parentPanel);
            //panelSelectedTest.Size = new Size(panelSelectedTest.Width, panelSelectedTest.Height - 45);
            //sampleElements.Remove(element);


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
     
        
      
        #endregion

        private void dropDownDiseases_EditValueChanged(object sender, EventArgs e)
        {
            string displayMember = dropDownDiseases.Properties.GetDisplayText(dropDownDiseases.EditValue);
            lblTest.Text=displayMember; 
        }

        bool btnConditionDescriptionAddChecker = true;
        private void btnConditionDescriptionAdd_Click(object sender, EventArgs e)
        {
            if (btnConditionDescriptionAddChecker)
            {
                panelDiagnosisCondition.Size = new Size(panelDiagnosisCondition.Width, 185);
                btnConditionDescriptionAdd.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
                btnConditionDescriptionAddChecker = !btnConditionDescriptionAddChecker;   
            }
            else if(!btnConditionDescriptionAddChecker) 
            {
                panelDiagnosisCondition.Size = new Size(panelDiagnosisCondition.Width, 100);
                btnConditionDescriptionAdd.Image= ((System.Drawing.Image)(resources.GetObject("btnConditionDescriptionAdd.ImageOptions.Image")));
                btnConditionDescriptionAddChecker = !btnConditionDescriptionAddChecker;
            }
        }

        private void btnConditionAdd_Click(object sender, EventArgs e)
        {
            DignosisConditionDetail conditionDetail=new DignosisConditionDetail();
            conditionDetail.ConditionDetail = "Condition";
                //searchLookUpEditCondition.Properties.GetDisplayText(searchLookUpEditCondition.EditValue);
            conditionDetail.Status = "Active";
            conditionDetail.CreatedDate= dateEditCondition.DateTime.Date;
            conditionDetail.Note = "note";
            conditionDetail.Action = "Active";
           // Create the GridControl
            GridControl gridControl1 = new GridControl();
            GridView gridView1 = new GridView();

            // Add the GridControl to the form
            DignosisPanel.Controls.Add(gridControl1);

            // Set up the GridControl properties
            gridControl1.Location = new Point(0, panelDiagnosisCondition.Location.Y + panelDiagnosisCondition.Height + 10);
            gridControl1.Size = new Size(panelDiagnosisCondition.Width , 200);
           // gridControl1.ViewCollection.AddRange(new BaseView[] { gridView1 });

            // Set up the GridView properties
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsView.ColumnAutoWidth = true;
            gridControl1.ViewCollection.AddRange(new BaseView[] { gridView1 });

          

            // Assign a data source (example: a simple list of objects)
            gridView1.Columns.Clear(); // Clear columns in case of any default columns

            // Define columns
            gridView1.Columns.AddVisible("ID", "ID");
            gridView1.Columns.AddVisible("Name", "Name");
            gridView1.Columns.AddVisible("Age", "Age");
            gridView1.Columns.AddVisible("Action", "action");

            // Create a sample data source (can be a list of objects)
            var data = new[] {
                new { ID = 1, Name = "John Doe", Age = 30, action="Active" },
                new { ID = 2, Name = "Jane Smith", Age = 25 ,action="History of"}
                // Add more data as needed
            };

            gridControl1.DataSource = data;
            RepositoryItemButtonEdit repositoryItemButton = new RepositoryItemButtonEdit();
            repositoryItemButton.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButton.Buttons[0].Caption = "Active";
            repositoryItemButton.Buttons[0].Kind = ButtonPredefines.Glyph;
            gridView1.Columns["Action"].ColumnEdit = repositoryItemButton;
        }

        private void btnSaveDisposition_Click_1(object sender, EventArgs e)
        {

        }
    }
}
#endregion
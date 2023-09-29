using Clinical_Managment_System.Data_Access_Layer;
using Clinical_Managment_System.Models;
using Clinical_Managment_System.Properties;
using Clinical_Managment_System.Validation;
using DevExpress.Utils.ScrollAnnotations;
using DevExpress.XtraEditors;

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
        public PatientModel patient {  get; set; }
        int patientID;
      
        int x = 217;
        int panelHeight = 85;
      
       
        bool checkStatus = true;
        Controls dignosisControl = new Controls();
        List<Controls> listControls = new List<Controls>();


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
            patientId.Text = patient.Id.ToString();
            patientPhone.Text = patient.PhoneNumber.ToString();
        }

        private void ClinicalManagmentSystemForm_Load(object sender, EventArgs e)
        {
            //Load Combo Box List For Diseases Type
           var comboList=context.LoadDiseasesType();
            dropDownDiseases.DataSource=comboList;
            
            dropDownDiseases.DisplayMember = "Description";
            dropDownDiseases.ValueMember = "Id";

            //Load Combo Box List For Disposition Type
            //pass patient id as a parameter
            //load patint id based on person id from patient table
           //patientID = context.loadPatientId(patient.Id.ToString());
           List<String> type = context.LoadDispositionType(patientID);
            dropDownSelectAction.DataSource = type;

            //for Dignosis Desccription
           
            diagnosisDescription.Visible = false; 
            descriptionTitle.Visible = false;
            paneldDignosis.Size = new Size(1213, 85);
            //ende for the above action

            //////////////////////////Lab Order//////////
            createSamples();

        }
        #region
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
            if (string.IsNullOrEmpty(dropDownDiseases.Text.Trim()))
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
                if (String.IsNullOrEmpty(control.DiseasesType.Text.Trim()))
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
                ComboxData slectedItem1 = (ComboxData)control.DiseasesType.SelectedItem;
           

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
            ComboxData slectedItem = (ComboxData)dropDownDiseases.SelectedItem;

            dignosis.Patient_Id = patientID;
            dignosis.DiseasisTypeId = slectedItem.Id;
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
        private void addDignosis_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = true;


            Panel newPanel = new Panel();
            newPanel.Size = new Size(1213, 180);
            newPanel.Location = new Point(0, x); // Adjust the location as needed
            newPanel.BackColor = Color.FromArgb(240, 240, 240);
           

            // Add the panel to the collection and the form
            panelCollection.Add(newPanel);
            Button button=new Button();
            button.Text = "Add";
            button.Location=new Point(1050, 42);
            button.Click += new EventHandler(ButtonForEachPanel_Click);
            newPanel.Controls.Add(button);
           

            DignosisPanel.Controls.Add(newPanel);

            dignosisControl.DiseasesType = new System.Windows.Forms.ComboBox();
            dignosisControl.DiseasesType.DataSource = context.LoadDiseasesType();
            dignosisControl.DiseasesType.Size = new Size(123, 30);
            dignosisControl.DiseasesType.Location = new Point(72, 16);
            dignosisControl.DiseasesType.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));      
            dignosisControl.DiseasesType.DisplayMember = "Description";
            dignosisControl.DiseasesType.ValueMember = "Id";
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


          
            x = x + 190;
            
            listControls.Add(dignosisControl);  


        }
        bool countButtonClick = true;
        private void ButtonForEachPanel_Click(object sender, EventArgs e)
        {                   
            Button clickedButton = (Button)sender;
            Panel parentPanel = (Panel)clickedButton.Parent;

            if (countButtonClick)
            {
                clickedButton.Text = "Remove";
                countButtonClick = !countButtonClick;
                parentPanel.Size = new Size(1213, 180);
               // parentPanel.Location=new Point()
            }
            else if (!countButtonClick)
            {
                countButtonClick = !countButtonClick;
                clickedButton.Text = "Add";
                parentPanel.Size = new Size(1213, 85);
            }
        }
      

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (panelCollection.Count > 0)
            {
                
                Panel lastPanel = panelCollection[panelCollection.Count - 1];
                panelCollection.Remove(lastPanel);
                DignosisPanel.Controls.Remove(lastPanel);
                x = x - 190;
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
        #region habtish
        private void addDescription_Click(object sender, EventArgs e)
        {
            if (countButtonClick)
            {
                diagnosisDescription.Visible = true;
                descriptionTitle.Visible = true;
               
                paneldDignosis.Size = new Size(1213, 180);
                countButtonClick = !countButtonClick;
                panelHeight = 180;
            }
            else if(!countButtonClick){
                diagnosisDescription.Visible = false;
                descriptionTitle.Visible = false;
               
                paneldDignosis.Size = new Size(1213, 85);
                countButtonClick= !countButtonClick;
                panelHeight = 85;
            }

        }
        #endregion



        ////////////////////////////Lab Order////////////////////////////////////////

        public void createSamples()
        {
            int sampleHeight = 0;
            var samples=orders.GetSamples();
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
                button.Size = new Size(252,32);
                button.Location = new Point(0, sampleHeight);
                sampleHeight += 35;
                panelSample.Controls.Add(button);
            }
            panelSample.Height=samples.Count*35+5;
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
                // Retrieve the associated data from the sender's Tag property
                if (button.Tag is Sample samples)
                {
                    var panels = orders.GetPanels(samples.Id);
                    int panelButtonX = 11;
                    int panelButtonY = 35;
                    int panelCount = 0;
                    int countButtons = 0;
                    groupBoxPanel.Controls.Clear(); 
                    foreach (var panel in panels) 
                    { 
                        Button panelButton=new Button();
                        panelButton.Tag = panel;
                        panelButton.Click += PanelButton_Click;
                        panelButton.Text = panel.PanelName;
                        panelButton.Size = new Size(270, 42);
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
                    //groupBoxTests.Location = new Point(377, groupBoxPanel.Height + 78);
                    groupBoxTests.Location = new Point(groupBoxTests.Location.X, groupBoxPanel.Bottom+10);

                    //MessageBox.Show("Sample Name: " + button.Text + "\nID: " + samples.Id);
                }
            }

        }
        bool isClicked = true;
        private void PanelButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                // Retrieve the associated data from the sender's Tag property
                if (button.Tag is Panels panels)
                {
                    if (isClicked)
                    {
                        button.ForeColor = Color.Black;
                        button.BackColor = Color.WhiteSmoke;
                        isClicked = !isClicked;
                    }
                    else if (!isClicked)
                    {
                        button.ForeColor = Color.DarkBlue;
                        button.BackColor = Color.LightGray;
                        isClicked = !isClicked;
                    }
                    var tests = orders.GetTests(panels.Id);
                    int testButtonX = 11;
                    int testButtonY = 35;
                    int testCount = 0;
                    int countButtons = 0;
                    groupBoxTests.Controls.Clear();
                    foreach (var test in tests)
                    {
                        Button testButton = new Button();
                        testButton.Tag = test;
                        testButton.Click += TestButton_Click;
                        testButton.Text = test.TestName;
                        testButton.Size = new Size(270, 42);
                        testButton.BackColor = Color.WhiteSmoke;
                        testButton.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        testButton.Location = new Point(testButtonX, testButtonY);
                        testButtonX += 280;
                        groupBoxTests.Controls.Add(testButton);
                        testCount++;
                        if (testCount % 4 == 0)
                        {
                            testButtonX = 11;
                            testButtonY += 54;
                            countButtons++; 
                        }
                    }
                    if (tests.Count % 4 != 0)
                    {
                        countButtons += 1;
                    }
                    groupBoxTests.Height = countButtons* 54+40;
                    //MessageBox.Show("Sample Name: " + button.Text + "\nID: " + samples.Id);
                
            }
            }
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                // Retrieve the associated data from the sender's Tag property
                if (button.Tag is Tests test)
                {
                    MessageBox.Show("Test Name: " + button.Text + "\nID: " + test.Id + "\nType: " + test.TestType);
                }
            }
        }
    }
}
#endregion
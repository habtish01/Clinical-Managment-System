namespace Clinical_Managment_System
{
    partial class HomeClinicalSystem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeClinicalSystem));
            this.homePanel = new System.Windows.Forms.Panel();
            this.panelList = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.LoggedUser = new System.Windows.Forms.Label();
            this.LogoName = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxLogedUser = new System.Windows.Forms.PictureBox();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.panelHomePage = new System.Windows.Forms.Panel();
            this.HomeControl = new DevExpress.XtraGrid.GridControl();
            this.layoutViewHomeView = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.homePanel.SuspendLayout();
            this.panelList.SuspendLayout();
            this.panel1.SuspendLayout();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogedUser)).BeginInit();
            this.footerPanel.SuspendLayout();
            this.panelHomePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HomeControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewHomeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            this.SuspendLayout();
            // 
            // homePanel
            // 
            this.homePanel.BackColor = System.Drawing.Color.White;
            this.homePanel.Controls.Add(this.panelList);
            this.homePanel.Controls.Add(this.panel1);
            this.homePanel.Controls.Add(this.footerPanel);
            this.homePanel.Controls.Add(this.panelHomePage);
            this.homePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homePanel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homePanel.Location = new System.Drawing.Point(0, 0);
            this.homePanel.Name = "homePanel";
            this.homePanel.Size = new System.Drawing.Size(1556, 827);
            this.homePanel.TabIndex = 1;
            // 
            // panelList
            // 
            this.panelList.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelList.Controls.Add(this.label1);
            this.panelList.Location = new System.Drawing.Point(1, 110);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(1555, 40);
            this.panelList.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(177, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "List Of Active Patients";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.HeaderPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1556, 110);
            this.panel1.TabIndex = 24;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.Color.SkyBlue;
            this.HeaderPanel.Controls.Add(this.btnExit);
            this.HeaderPanel.Controls.Add(this.LoggedUser);
            this.HeaderPanel.Controls.Add(this.LogoName);
            this.HeaderPanel.Controls.Add(this.pictureBoxLogo);
            this.HeaderPanel.Controls.Add(this.pictureBoxLogedUser);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(1556, 109);
            this.HeaderPanel.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(1492, 2);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnExit.Size = new System.Drawing.Size(38, 32);
            this.btnExit.TabIndex = 9;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // LoggedUser
            // 
            this.LoggedUser.AutoSize = true;
            this.LoggedUser.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoggedUser.Location = new System.Drawing.Point(137, 45);
            this.LoggedUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LoggedUser.Name = "LoggedUser";
            this.LoggedUser.Size = new System.Drawing.Size(153, 21);
            this.LoggedUser.TabIndex = 6;
            this.LoggedUser.Text = "Habtamu Esubalew";
            // 
            // LogoName
            // 
            this.LogoName.AutoSize = true;
            this.LogoName.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogoName.Location = new System.Drawing.Point(501, 68);
            this.LogoName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LogoName.Name = "LogoName";
            this.LogoName.Size = new System.Drawing.Size(341, 24);
            this.LogoName.TabIndex = 7;
            this.LogoName.Text = "Heal Africa Health City EMR System";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(579, 2);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(173, 64);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 8;
            this.pictureBoxLogo.TabStop = false;
            // 
            // pictureBoxLogedUser
            // 
            this.pictureBoxLogedUser.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogedUser.Image")));
            this.pictureBoxLogedUser.Location = new System.Drawing.Point(49, 10);
            this.pictureBoxLogedUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxLogedUser.Name = "pictureBoxLogedUser";
            this.pictureBoxLogedUser.Size = new System.Drawing.Size(84, 83);
            this.pictureBoxLogedUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogedUser.TabIndex = 5;
            this.pictureBoxLogedUser.TabStop = false;
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.LightGray;
            this.footerPanel.Controls.Add(this.label15);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 758);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(1556, 69);
            this.footerPanel.TabIndex = 23;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.LimeGreen;
            this.label15.Location = new System.Drawing.Point(455, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(522, 22);
            this.label15.TabIndex = 15;
            this.label15.Text = "@CopyWrite Heal Aafrica Health City S.C. All Right Reserved";
            // 
            // panelHomePage
            // 
            this.panelHomePage.Controls.Add(this.HomeControl);
            this.panelHomePage.Location = new System.Drawing.Point(49, 156);
            this.panelHomePage.Name = "panelHomePage";
            this.panelHomePage.Size = new System.Drawing.Size(1454, 596);
            this.panelHomePage.TabIndex = 3;
            // 
            // HomeControl
            // 
            this.HomeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HomeControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.HomeControl.Location = new System.Drawing.Point(0, 0);
            this.HomeControl.MainView = this.layoutViewHomeView;
            this.HomeControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.HomeControl.Name = "HomeControl";
            this.HomeControl.Size = new System.Drawing.Size(1454, 596);
            this.HomeControl.TabIndex = 0;
            this.HomeControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutViewHomeView});
            // 
            // layoutViewHomeView
            // 
            this.layoutViewHomeView.Appearance.Card.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.layoutViewHomeView.Appearance.Card.BorderColor = System.Drawing.Color.Blue;
            this.layoutViewHomeView.Appearance.Card.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold);
            this.layoutViewHomeView.Appearance.Card.ForeColor = System.Drawing.Color.White;
            this.layoutViewHomeView.Appearance.Card.Options.UseBackColor = true;
            this.layoutViewHomeView.Appearance.Card.Options.UseBorderColor = true;
            this.layoutViewHomeView.Appearance.Card.Options.UseFont = true;
            this.layoutViewHomeView.Appearance.Card.Options.UseForeColor = true;
            this.layoutViewHomeView.Appearance.FieldCaption.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutViewHomeView.Appearance.FieldCaption.Options.UseFont = true;
            this.layoutViewHomeView.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.layoutViewHomeView.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutViewHomeView.Appearance.FieldValue.Options.UseBackColor = true;
            this.layoutViewHomeView.Appearance.FieldValue.Options.UseFont = true;
            this.layoutViewHomeView.AppearancePrint.Card.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.layoutViewHomeView.AppearancePrint.Card.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.layoutViewHomeView.AppearancePrint.Card.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.layoutViewHomeView.AppearancePrint.Card.ForeColor = System.Drawing.Color.White;
            this.layoutViewHomeView.AppearancePrint.Card.Options.UseBackColor = true;
            this.layoutViewHomeView.AppearancePrint.Card.Options.UseBorderColor = true;
            this.layoutViewHomeView.AppearancePrint.Card.Options.UseFont = true;
            this.layoutViewHomeView.AppearancePrint.Card.Options.UseForeColor = true;
            this.layoutViewHomeView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.layoutViewHomeView.CardMinSize = new System.Drawing.Size(250, 50);
            this.layoutViewHomeView.DetailHeight = 244;
            this.layoutViewHomeView.GridControl = this.HomeControl;
            this.layoutViewHomeView.Name = "layoutViewHomeView";
            this.layoutViewHomeView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.layoutViewHomeView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.layoutViewHomeView.OptionsBehavior.AllowExpandCollapse = false;
            this.layoutViewHomeView.OptionsBehavior.Editable = false;
            this.layoutViewHomeView.OptionsBehavior.ReadOnly = true;
            this.layoutViewHomeView.OptionsCustomization.UseAdvancedRuntimeCustomization = true;
            this.layoutViewHomeView.OptionsHeaderPanel.EnableColumnModeButton = false;
            this.layoutViewHomeView.OptionsLayout.StoreAllOptions = true;
            this.layoutViewHomeView.OptionsView.ShowCardCaption = false;
            this.layoutViewHomeView.OptionsView.ShowCardExpandButton = false;
            this.layoutViewHomeView.OptionsView.ShowCardLines = false;
            this.layoutViewHomeView.OptionsView.ShowFieldHints = false;
            this.layoutViewHomeView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.layoutViewHomeView.OptionsView.ShowHeaderPanel = false;
            this.layoutViewHomeView.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiColumn;
            this.layoutViewHomeView.TemplateCard = this.layoutViewCard1;
            this.layoutViewHomeView.CardClick += new DevExpress.XtraGrid.Views.Layout.Events.CardClickEventHandler(this.layoutView1_CardClick);
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.GroupBordersVisible = false;
            this.layoutViewCard1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Name = "layoutViewCard1";
            // 
            // HomeClinicalSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 827);
            this.Controls.Add(this.homePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HomeClinicalSystem";
            this.Text = "HomeClinicalSystem";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HomeClinicalSystem_Load);
            this.homePanel.ResumeLayout(false);
            this.panelList.ResumeLayout(false);
            this.panelList.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogedUser)).EndInit();
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.panelHomePage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HomeControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewHomeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel homePanel;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelHomePage;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutViewHomeView;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
        private DevExpress.XtraGrid.GridControl HomeControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel HeaderPanel;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private System.Windows.Forms.Label LoggedUser;
        private System.Windows.Forms.Label LogoName;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.PictureBox pictureBoxLogedUser;
    }
}
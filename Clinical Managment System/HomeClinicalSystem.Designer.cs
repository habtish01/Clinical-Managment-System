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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelList = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.LoggedUser = new System.Windows.Forms.Label();
            this.LogoName = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxLogedUser = new System.Windows.Forms.PictureBox();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            this.homePanel.SuspendLayout();
            this.panelList.SuspendLayout();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogedUser)).BeginInit();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // homePanel
            // 
            this.homePanel.Controls.Add(this.txtPageNumber);
            this.homePanel.Controls.Add(this.label2);
            this.homePanel.Controls.Add(this.btnNext);
            this.homePanel.Controls.Add(this.footerPanel);
            this.homePanel.Controls.Add(this.panel1);
            this.homePanel.Controls.Add(this.panelList);
            this.homePanel.Controls.Add(this.HeaderPanel);
            this.homePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homePanel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homePanel.Location = new System.Drawing.Point(0, 0);
            this.homePanel.Name = "homePanel";
            this.homePanel.Size = new System.Drawing.Size(1532, 872);
            this.homePanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(130, 178);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1356, 538);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panelList
            // 
            this.panelList.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelList.Controls.Add(this.label1);
            this.panelList.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelList.Location = new System.Drawing.Point(0, 105);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(1532, 49);
            this.panelList.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(383, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "List Of Active Patients";
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.HeaderPanel.Controls.Add(this.btnExit);
            this.HeaderPanel.Controls.Add(this.LoggedUser);
            this.HeaderPanel.Controls.Add(this.LogoName);
            this.HeaderPanel.Controls.Add(this.pictureBoxLogo);
            this.HeaderPanel.Controls.Add(this.pictureBoxLogedUser);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Margin = new System.Windows.Forms.Padding(2);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(1532, 105);
            this.HeaderPanel.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(1492, 2);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
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
            this.LoggedUser.Location = new System.Drawing.Point(146, 21);
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
            this.LogoName.Location = new System.Drawing.Point(501, 57);
            this.LogoName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LogoName.Name = "LogoName";
            this.LogoName.Size = new System.Drawing.Size(341, 24);
            this.LogoName.TabIndex = 7;
            this.LogoName.Text = "Heal Africa Health City EMR System";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(586, 2);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(127, 52);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 8;
            this.pictureBoxLogo.TabStop = false;
            // 
            // pictureBoxLogedUser
            // 
            this.pictureBoxLogedUser.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogedUser.Image")));
            this.pictureBoxLogedUser.Location = new System.Drawing.Point(49, 10);
            this.pictureBoxLogedUser.Margin = new System.Windows.Forms.Padding(2);
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
            this.footerPanel.Location = new System.Drawing.Point(0, 799);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(1532, 73);
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
            // btnNext
            // 
            this.btnNext.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Appearance.Options.UseFont = true;
            this.btnNext.Location = new System.Drawing.Point(1325, 738);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 26);
            this.btnNext.TabIndex = 24;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1205, 745);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 25;
            this.label2.Text = "Page:";
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(1258, 738);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Size = new System.Drawing.Size(61, 26);
            this.txtPageNumber.TabIndex = 26;
            // 
            // HomeClinicalSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1532, 872);
            this.Controls.Add(this.homePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HomeClinicalSystem";
            this.Text = "HomeClinicalSystem";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.HomeClinicalSystem_Load);
            this.homePanel.ResumeLayout(false);
            this.homePanel.PerformLayout();
            this.panelList.ResumeLayout(false);
            this.panelList.PerformLayout();
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogedUser)).EndInit();
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel homePanel;
        private System.Windows.Forms.Panel HeaderPanel;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private System.Windows.Forms.Label LoggedUser;
        private System.Windows.Forms.Label LogoName;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.PictureBox pictureBoxLogedUser;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnNext;
    }
}
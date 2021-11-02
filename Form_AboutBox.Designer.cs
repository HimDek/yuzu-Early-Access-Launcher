
namespace yuzu_Early_Access_Launcher
{
    partial class Form_AboutBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AboutBox));
            this.pictureBox_yuzuIcon = new System.Windows.Forms.PictureBox();
            this.label_yuzu = new System.Windows.Forms.Label();
            this.label_yuzuDesc1 = new System.Windows.Forms.Label();
            this.label_yuzuDesc2 = new System.Windows.Forms.Label();
            this.link_yuzuSite = new System.Windows.Forms.LinkLabel();
            this.link_yuzuSource = new System.Windows.Forms.LinkLabel();
            this.link_yuzuContrib = new System.Windows.Forms.LinkLabel();
            this.link_yuzuLicense = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox_LauncherLogo = new System.Windows.Forms.PictureBox();
            this.label_Launcher = new System.Windows.Forms.Label();
            this.label_LauncherDesc = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel_FirmwareRepo = new System.Windows.Forms.LinkLabel();
            this.linkLabel_prodRepo = new System.Windows.Forms.LinkLabel();
            this.linkLabel_EARepo = new System.Windows.Forms.LinkLabel();
            this.linkLabel_LauncherSource = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label_LauncherVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_yuzuIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LauncherLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_yuzuIcon
            // 
            this.pictureBox_yuzuIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_yuzuIcon.BackgroundImage")));
            this.pictureBox_yuzuIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_yuzuIcon.Location = new System.Drawing.Point(19, 19);
            this.pictureBox_yuzuIcon.Margin = new System.Windows.Forms.Padding(10);
            this.pictureBox_yuzuIcon.Name = "pictureBox_yuzuIcon";
            this.pictureBox_yuzuIcon.Size = new System.Drawing.Size(236, 183);
            this.pictureBox_yuzuIcon.TabIndex = 0;
            this.pictureBox_yuzuIcon.TabStop = false;
            // 
            // label_yuzu
            // 
            this.label_yuzu.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_yuzu.Location = new System.Drawing.Point(275, 11);
            this.label_yuzu.Margin = new System.Windows.Forms.Padding(10);
            this.label_yuzu.Name = "label_yuzu";
            this.label_yuzu.Size = new System.Drawing.Size(118, 59);
            this.label_yuzu.TabIndex = 1;
            this.label_yuzu.Text = "yuzu";
            // 
            // label_yuzuDesc1
            // 
            this.label_yuzuDesc1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_yuzuDesc1.Location = new System.Drawing.Point(280, 85);
            this.label_yuzuDesc1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.label_yuzuDesc1.Name = "label_yuzuDesc1";
            this.label_yuzuDesc1.Size = new System.Drawing.Size(441, 45);
            this.label_yuzuDesc1.TabIndex = 2;
            this.label_yuzuDesc1.Text = "yuzu is an experimental open-source emulator for the Nintendo Switch Licensed und" +
    "er GPLv2.0.";
            this.label_yuzuDesc1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_yuzuDesc2
            // 
            this.label_yuzuDesc2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_yuzuDesc2.Location = new System.Drawing.Point(280, 140);
            this.label_yuzuDesc2.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.label_yuzuDesc2.Name = "label_yuzuDesc2";
            this.label_yuzuDesc2.Size = new System.Drawing.Size(441, 45);
            this.label_yuzuDesc2.TabIndex = 3;
            this.label_yuzuDesc2.Text = "This Software should not be used to play games you have not legally obtained";
            this.label_yuzuDesc2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // link_yuzuSite
            // 
            this.link_yuzuSite.AutoSize = true;
            this.link_yuzuSite.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.link_yuzuSite.Location = new System.Drawing.Point(281, 220);
            this.link_yuzuSite.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.link_yuzuSite.Name = "link_yuzuSite";
            this.link_yuzuSite.Size = new System.Drawing.Size(46, 13);
            this.link_yuzuSite.TabIndex = 4;
            this.link_yuzuSite.TabStop = true;
            this.link_yuzuSite.Text = "Website";
            this.link_yuzuSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_yuzuSite_LinkClicked);
            // 
            // link_yuzuSource
            // 
            this.link_yuzuSource.AutoSize = true;
            this.link_yuzuSource.Location = new System.Drawing.Point(337, 220);
            this.link_yuzuSource.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.link_yuzuSource.Name = "link_yuzuSource";
            this.link_yuzuSource.Size = new System.Drawing.Size(69, 13);
            this.link_yuzuSource.TabIndex = 6;
            this.link_yuzuSource.TabStop = true;
            this.link_yuzuSource.Text = "Source Code";
            this.link_yuzuSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_yuzuSource_LinkClicked);
            // 
            // link_yuzuContrib
            // 
            this.link_yuzuContrib.AutoSize = true;
            this.link_yuzuContrib.Location = new System.Drawing.Point(416, 220);
            this.link_yuzuContrib.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.link_yuzuContrib.Name = "link_yuzuContrib";
            this.link_yuzuContrib.Size = new System.Drawing.Size(63, 13);
            this.link_yuzuContrib.TabIndex = 8;
            this.link_yuzuContrib.TabStop = true;
            this.link_yuzuContrib.Text = "Contributors";
            this.link_yuzuContrib.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_yuzuContrib_LinkClicked);
            // 
            // link_yuzuLicense
            // 
            this.link_yuzuLicense.AutoSize = true;
            this.link_yuzuLicense.Location = new System.Drawing.Point(489, 220);
            this.link_yuzuLicense.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.link_yuzuLicense.Name = "link_yuzuLicense";
            this.link_yuzuLicense.Size = new System.Drawing.Size(44, 13);
            this.link_yuzuLicense.TabIndex = 10;
            this.link_yuzuLicense.TabStop = true;
            this.link_yuzuLicense.Text = "License";
            this.link_yuzuLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_yuzuLicense_LinkClicked);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(281, 239);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(440, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "\"Nintendo Switch\" is a trademark of Nintendo, yuzu is not affiliated with Nintend" +
    "o in any way.";
            // 
            // pictureBox_LauncherLogo
            // 
            this.pictureBox_LauncherLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_LauncherLogo.BackgroundImage")));
            this.pictureBox_LauncherLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_LauncherLogo.Location = new System.Drawing.Point(12, 289);
            this.pictureBox_LauncherLogo.Name = "pictureBox_LauncherLogo";
            this.pictureBox_LauncherLogo.Size = new System.Drawing.Size(255, 135);
            this.pictureBox_LauncherLogo.TabIndex = 9;
            this.pictureBox_LauncherLogo.TabStop = false;
            // 
            // label_Launcher
            // 
            this.label_Launcher.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Launcher.Location = new System.Drawing.Point(278, 289);
            this.label_Launcher.Margin = new System.Windows.Forms.Padding(10);
            this.label_Launcher.Name = "label_Launcher";
            this.label_Launcher.Size = new System.Drawing.Size(368, 35);
            this.label_Launcher.TabIndex = 12;
            this.label_Launcher.Text = "yuzu Early Acces Launcher";
            // 
            // label_LauncherDesc
            // 
            this.label_LauncherDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_LauncherDesc.Location = new System.Drawing.Point(280, 361);
            this.label_LauncherDesc.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.label_LauncherDesc.Name = "label_LauncherDesc";
            this.label_LauncherDesc.Size = new System.Drawing.Size(441, 45);
            this.label_LauncherDesc.TabIndex = 14;
            this.label_LauncherDesc.Text = "This Launcher can install and keep yuzu Early Access along with prod.key and Firm" +
    "ware updated for free.";
            this.label_LauncherDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(281, 460);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(440, 30);
            this.label2.TabIndex = 22;
            this.label2.Text = "\"yuzu and yuzu Early Access\" is developed by Team yuzu. The maker of this Launche" +
    "r is not affiliated with Nintendo or Team yuzu in any way.";
            // 
            // linkLabel_FirmwareRepo
            // 
            this.linkLabel_FirmwareRepo.AutoSize = true;
            this.linkLabel_FirmwareRepo.Location = new System.Drawing.Point(573, 441);
            this.linkLabel_FirmwareRepo.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.linkLabel_FirmwareRepo.Name = "linkLabel_FirmwareRepo";
            this.linkLabel_FirmwareRepo.Size = new System.Drawing.Size(86, 13);
            this.linkLabel_FirmwareRepo.TabIndex = 21;
            this.linkLabel_FirmwareRepo.TabStop = true;
            this.linkLabel_FirmwareRepo.Text = "Firmware Source";
            this.linkLabel_FirmwareRepo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_FirmwareRepo_LinkClicked);
            // 
            // linkLabel_prodRepo
            // 
            this.linkLabel_prodRepo.AutoSize = true;
            this.linkLabel_prodRepo.Location = new System.Drawing.Point(475, 441);
            this.linkLabel_prodRepo.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.linkLabel_prodRepo.Name = "linkLabel_prodRepo";
            this.linkLabel_prodRepo.Size = new System.Drawing.Size(88, 13);
            this.linkLabel_prodRepo.TabIndex = 19;
            this.linkLabel_prodRepo.TabStop = true;
            this.linkLabel_prodRepo.Text = "prod.keys source";
            this.linkLabel_prodRepo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_prodRepo_LinkClicked);
            // 
            // linkLabel_EARepo
            // 
            this.linkLabel_EARepo.AutoSize = true;
            this.linkLabel_EARepo.Location = new System.Drawing.Point(360, 441);
            this.linkLabel_EARepo.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.linkLabel_EARepo.Name = "linkLabel_EARepo";
            this.linkLabel_EARepo.Size = new System.Drawing.Size(105, 13);
            this.linkLabel_EARepo.TabIndex = 17;
            this.linkLabel_EARepo.TabStop = true;
            this.linkLabel_EARepo.Text = "Early Access Source";
            this.linkLabel_EARepo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_EARepo_LinkClicked);
            // 
            // linkLabel_LauncherSource
            // 
            this.linkLabel_LauncherSource.AutoSize = true;
            this.linkLabel_LauncherSource.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLabel_LauncherSource.Location = new System.Drawing.Point(281, 441);
            this.linkLabel_LauncherSource.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.linkLabel_LauncherSource.Name = "linkLabel_LauncherSource";
            this.linkLabel_LauncherSource.Size = new System.Drawing.Size(69, 13);
            this.linkLabel_LauncherSource.TabIndex = 15;
            this.linkLabel_LauncherSource.TabStop = true;
            this.linkLabel_LauncherSource.Text = "Source Code";
            this.linkLabel_LauncherSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LauncherSource_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(327, 220);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "|";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(406, 220);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "|";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(479, 220);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "|";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(350, 441);
            this.label8.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "|";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(465, 441);
            this.label9.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "|";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(563, 441);
            this.label10.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "|";
            // 
            // label_LauncherVersion
            // 
            this.label_LauncherVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label_LauncherVersion.Location = new System.Drawing.Point(281, 334);
            this.label_LauncherVersion.Name = "label_LauncherVersion";
            this.label_LauncherVersion.Size = new System.Drawing.Size(252, 16);
            this.label_LauncherVersion.TabIndex = 13;
            this.label_LauncherVersion.Text = "Version";
            // 
            // Form_AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(736, 511);
            this.Controls.Add(this.label_LauncherVersion);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.linkLabel_FirmwareRepo);
            this.Controls.Add(this.linkLabel_prodRepo);
            this.Controls.Add(this.linkLabel_EARepo);
            this.Controls.Add(this.linkLabel_LauncherSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_LauncherDesc);
            this.Controls.Add(this.label_Launcher);
            this.Controls.Add(this.pictureBox_LauncherLogo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.link_yuzuLicense);
            this.Controls.Add(this.link_yuzuContrib);
            this.Controls.Add(this.link_yuzuSource);
            this.Controls.Add(this.link_yuzuSite);
            this.Controls.Add(this.label_yuzuDesc2);
            this.Controls.Add(this.label_yuzuDesc1);
            this.Controls.Add(this.label_yuzu);
            this.Controls.Add(this.pictureBox_yuzuIcon);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_AboutBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About yuzu Early Access and Launcher";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_yuzuIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LauncherLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_yuzuIcon;
        private System.Windows.Forms.Label label_yuzu;
        private System.Windows.Forms.Label label_yuzuDesc1;
        private System.Windows.Forms.Label label_yuzuDesc2;
        private System.Windows.Forms.LinkLabel link_yuzuSite;
        private System.Windows.Forms.LinkLabel link_yuzuSource;
        private System.Windows.Forms.LinkLabel link_yuzuContrib;
        private System.Windows.Forms.LinkLabel link_yuzuLicense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox_LauncherLogo;
        private System.Windows.Forms.Label label_Launcher;
        private System.Windows.Forms.Label label_LauncherDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel_FirmwareRepo;
        private System.Windows.Forms.LinkLabel linkLabel_prodRepo;
        private System.Windows.Forms.LinkLabel linkLabel_EARepo;
        private System.Windows.Forms.LinkLabel linkLabel_LauncherSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_LauncherVersion;
    }
}
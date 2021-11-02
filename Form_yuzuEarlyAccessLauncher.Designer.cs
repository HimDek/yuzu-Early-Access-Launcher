
namespace yuzu_Early_Access_Launcher
{
    partial class Form_yuzuEarlyAccessLauncher
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_yuzuEarlyAccessLauncher));
            this.pictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.button_Launch = new System.Windows.Forms.Button();
            this.button_Download = new System.Windows.Forms.Button();
            this.groupBox_Progress = new System.Windows.Forms.GroupBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label_Progress = new System.Windows.Forms.Label();
            this.label_Speed = new System.Windows.Forms.Label();
            this.label_Time = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_Message = new System.Windows.Forms.Label();
            this.groupBox_Firmware = new System.Windows.Forms.GroupBox();
            this.button_Firmware = new System.Windows.Forms.Button();
            this.label_Firmware = new System.Windows.Forms.Label();
            this.groupBox_SystemInfo = new System.Windows.Forms.GroupBox();
            this.label_RAMInfo = new System.Windows.Forms.Label();
            this.label_CPUInfo = new System.Windows.Forms.Label();
            this.label_RAMCapacity = new System.Windows.Forms.Label();
            this.label_CPUName = new System.Windows.Forms.Label();
            this.groupBox_HelpandSupport = new System.Windows.Forms.GroupBox();
            this.button_FAQ = new System.Windows.Forms.Button();
            this.button_About = new System.Windows.Forms.Button();
            this.button_Compatibility = new System.Windows.Forms.Button();
            this.button_Report = new System.Windows.Forms.Button();
            this.button_Support = new System.Windows.Forms.Button();
            this.button_Video = new System.Windows.Forms.Button();
            this.backgroundWorker_Check = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_SystemInfo = new System.ComponentModel.BackgroundWorker();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.label_Info = new System.Windows.Forms.Label();
            this.backgroundWorker_Download = new System.ComponentModel.BackgroundWorker();
            this.timer_ExtractProgress = new System.Windows.Forms.Timer(this.components);
            this.timer_Refresh = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Logo)).BeginInit();
            this.groupBox_Progress.SuspendLayout();
            this.groupBox_Firmware.SuspendLayout();
            this.groupBox_SystemInfo.SuspendLayout();
            this.groupBox_HelpandSupport.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Logo
            // 
            this.pictureBox_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_Logo.BackgroundImage")));
            this.pictureBox_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Logo.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_Logo.Name = "pictureBox_Logo";
            this.pictureBox_Logo.Size = new System.Drawing.Size(706, 207);
            this.pictureBox_Logo.TabIndex = 0;
            this.pictureBox_Logo.TabStop = false;
            // 
            // button_Launch
            // 
            this.button_Launch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Launch.Location = new System.Drawing.Point(12, 225);
            this.button_Launch.Name = "button_Launch";
            this.button_Launch.Size = new System.Drawing.Size(350, 60);
            this.button_Launch.TabIndex = 1;
            this.button_Launch.Text = "Launch";
            this.button_Launch.UseVisualStyleBackColor = true;
            this.button_Launch.Visible = false;
            this.button_Launch.Click += new System.EventHandler(this.Button_Launch_Click);
            // 
            // button_Download
            // 
            this.button_Download.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Download.Location = new System.Drawing.Point(368, 225);
            this.button_Download.Name = "button_Download";
            this.button_Download.Size = new System.Drawing.Size(350, 60);
            this.button_Download.TabIndex = 2;
            this.button_Download.Text = "Download";
            this.button_Download.UseVisualStyleBackColor = true;
            this.button_Download.Visible = false;
            this.button_Download.Click += new System.EventHandler(this.Button_Download_Click);
            // 
            // groupBox_Progress
            // 
            this.groupBox_Progress.Controls.Add(this.button_Cancel);
            this.groupBox_Progress.Controls.Add(this.label_Progress);
            this.groupBox_Progress.Controls.Add(this.label_Speed);
            this.groupBox_Progress.Controls.Add(this.label_Time);
            this.groupBox_Progress.Controls.Add(this.progressBar1);
            this.groupBox_Progress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Progress.Location = new System.Drawing.Point(12, 238);
            this.groupBox_Progress.Name = "groupBox_Progress";
            this.groupBox_Progress.Size = new System.Drawing.Size(706, 118);
            this.groupBox_Progress.TabIndex = 3;
            this.groupBox_Progress.TabStop = false;
            this.groupBox_Progress.Text = "Progress";
            this.groupBox_Progress.Visible = false;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Cancel.Location = new System.Drawing.Point(473, 80);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(227, 30);
            this.button_Cancel.TabIndex = 4;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // label_Progress
            // 
            this.label_Progress.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label_Progress.Location = new System.Drawing.Point(6, 53);
            this.label_Progress.Name = "label_Progress";
            this.label_Progress.Size = new System.Drawing.Size(286, 23);
            this.label_Progress.TabIndex = 1;
            this.label_Progress.Text = "0%";
            this.label_Progress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Speed
            // 
            this.label_Speed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label_Speed.Location = new System.Drawing.Point(298, 54);
            this.label_Speed.Name = "label_Speed";
            this.label_Speed.Size = new System.Drawing.Size(402, 23);
            this.label_Speed.TabIndex = 2;
            this.label_Speed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Time
            // 
            this.label_Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label_Time.Location = new System.Drawing.Point(7, 79);
            this.label_Time.Name = "label_Time";
            this.label_Time.Size = new System.Drawing.Size(459, 30);
            this.label_Time.TabIndex = 3;
            this.label_Time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 27);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(693, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // label_Message
            // 
            this.label_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Message.Location = new System.Drawing.Point(1, 202);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(728, 194);
            this.label_Message.TabIndex = 4;
            this.label_Message.Text = "Processing!";
            this.label_Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox_Firmware
            // 
            this.groupBox_Firmware.Controls.Add(this.button_Firmware);
            this.groupBox_Firmware.Controls.Add(this.label_Firmware);
            this.groupBox_Firmware.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_Firmware.Location = new System.Drawing.Point(12, 291);
            this.groupBox_Firmware.Name = "groupBox_Firmware";
            this.groupBox_Firmware.Size = new System.Drawing.Size(706, 65);
            this.groupBox_Firmware.TabIndex = 4;
            this.groupBox_Firmware.TabStop = false;
            this.groupBox_Firmware.Text = "Firmware:";
            this.groupBox_Firmware.Visible = false;
            // 
            // button_Firmware
            // 
            this.button_Firmware.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Firmware.Location = new System.Drawing.Point(473, 21);
            this.button_Firmware.Name = "button_Firmware";
            this.button_Firmware.Size = new System.Drawing.Size(227, 30);
            this.button_Firmware.TabIndex = 1;
            this.button_Firmware.Text = "Firmware";
            this.button_Firmware.UseVisualStyleBackColor = true;
            this.button_Firmware.Click += new System.EventHandler(this.Button_Firmware_Click);
            // 
            // label_Firmware
            // 
            this.label_Firmware.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Firmware.Location = new System.Drawing.Point(6, 21);
            this.label_Firmware.Name = "label_Firmware";
            this.label_Firmware.Size = new System.Drawing.Size(460, 30);
            this.label_Firmware.TabIndex = 0;
            this.label_Firmware.Text = "Firmware";
            this.label_Firmware.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox_SystemInfo
            // 
            this.groupBox_SystemInfo.Controls.Add(this.label_RAMInfo);
            this.groupBox_SystemInfo.Controls.Add(this.label_CPUInfo);
            this.groupBox_SystemInfo.Controls.Add(this.label_RAMCapacity);
            this.groupBox_SystemInfo.Controls.Add(this.label_CPUName);
            this.groupBox_SystemInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_SystemInfo.Location = new System.Drawing.Point(12, 398);
            this.groupBox_SystemInfo.Name = "groupBox_SystemInfo";
            this.groupBox_SystemInfo.Size = new System.Drawing.Size(706, 77);
            this.groupBox_SystemInfo.TabIndex = 7;
            this.groupBox_SystemInfo.TabStop = false;
            this.groupBox_SystemInfo.Text = "System Info:";
            // 
            // label_RAMInfo
            // 
            this.label_RAMInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RAMInfo.Location = new System.Drawing.Point(239, 40);
            this.label_RAMInfo.Name = "label_RAMInfo";
            this.label_RAMInfo.Size = new System.Drawing.Size(460, 23);
            this.label_RAMInfo.TabIndex = 3;
            this.label_RAMInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_CPUInfo
            // 
            this.label_CPUInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CPUInfo.Location = new System.Drawing.Point(309, 17);
            this.label_CPUInfo.Name = "label_CPUInfo";
            this.label_CPUInfo.Size = new System.Drawing.Size(390, 23);
            this.label_CPUInfo.TabIndex = 1;
            this.label_CPUInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_RAMCapacity
            // 
            this.label_RAMCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RAMCapacity.Location = new System.Drawing.Point(6, 40);
            this.label_RAMCapacity.Name = "label_RAMCapacity";
            this.label_RAMCapacity.Size = new System.Drawing.Size(227, 23);
            this.label_RAMCapacity.TabIndex = 2;
            this.label_RAMCapacity.Text = "RAM:";
            this.label_RAMCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_CPUName
            // 
            this.label_CPUName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CPUName.Location = new System.Drawing.Point(6, 17);
            this.label_CPUName.Name = "label_CPUName";
            this.label_CPUName.Size = new System.Drawing.Size(297, 23);
            this.label_CPUName.TabIndex = 0;
            this.label_CPUName.Text = "CPU:";
            this.label_CPUName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox_HelpandSupport
            // 
            this.groupBox_HelpandSupport.Controls.Add(this.button_FAQ);
            this.groupBox_HelpandSupport.Controls.Add(this.button_About);
            this.groupBox_HelpandSupport.Controls.Add(this.button_Compatibility);
            this.groupBox_HelpandSupport.Controls.Add(this.button_Report);
            this.groupBox_HelpandSupport.Controls.Add(this.button_Support);
            this.groupBox_HelpandSupport.Controls.Add(this.button_Video);
            this.groupBox_HelpandSupport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_HelpandSupport.Location = new System.Drawing.Point(12, 481);
            this.groupBox_HelpandSupport.Name = "groupBox_HelpandSupport";
            this.groupBox_HelpandSupport.Size = new System.Drawing.Size(706, 97);
            this.groupBox_HelpandSupport.TabIndex = 8;
            this.groupBox_HelpandSupport.TabStop = false;
            this.groupBox_HelpandSupport.Text = "Help and Support:";
            // 
            // button_FAQ
            // 
            this.button_FAQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_FAQ.Location = new System.Drawing.Point(239, 55);
            this.button_FAQ.Name = "button_FAQ";
            this.button_FAQ.Size = new System.Drawing.Size(228, 30);
            this.button_FAQ.TabIndex = 5;
            this.button_FAQ.Text = "Ask about a Problem";
            this.button_FAQ.UseVisualStyleBackColor = true;
            this.button_FAQ.Click += new System.EventHandler(this.Button_FAQ_Click);
            // 
            // button_About
            // 
            this.button_About.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_About.Location = new System.Drawing.Point(473, 55);
            this.button_About.Name = "button_About";
            this.button_About.Size = new System.Drawing.Size(227, 30);
            this.button_About.TabIndex = 6;
            this.button_About.Text = "About";
            this.button_About.UseVisualStyleBackColor = true;
            this.button_About.Click += new System.EventHandler(this.Button_About_Click);
            // 
            // button_Compatibility
            // 
            this.button_Compatibility.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Compatibility.Location = new System.Drawing.Point(6, 55);
            this.button_Compatibility.Name = "button_Compatibility";
            this.button_Compatibility.Size = new System.Drawing.Size(227, 30);
            this.button_Compatibility.TabIndex = 4;
            this.button_Compatibility.Text = "Check Compatibility of a Game";
            this.button_Compatibility.UseVisualStyleBackColor = true;
            this.button_Compatibility.Click += new System.EventHandler(this.Button_Compatibility_Click);
            // 
            // button_Report
            // 
            this.button_Report.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Report.Location = new System.Drawing.Point(473, 19);
            this.button_Report.Name = "button_Report";
            this.button_Report.Size = new System.Drawing.Size(227, 30);
            this.button_Report.TabIndex = 3;
            this.button_Report.Text = "Report an Issue";
            this.button_Report.UseVisualStyleBackColor = true;
            this.button_Report.Click += new System.EventHandler(this.Button_Report_Click);
            // 
            // button_Support
            // 
            this.button_Support.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Support.Location = new System.Drawing.Point(239, 19);
            this.button_Support.Name = "button_Support";
            this.button_Support.Size = new System.Drawing.Size(228, 30);
            this.button_Support.TabIndex = 2;
            this.button_Support.Text = "Support Me";
            this.button_Support.UseVisualStyleBackColor = true;
            this.button_Support.Click += new System.EventHandler(this.Button_Support_Click);
            // 
            // button_Video
            // 
            this.button_Video.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Video.Location = new System.Drawing.Point(6, 19);
            this.button_Video.Name = "button_Video";
            this.button_Video.Size = new System.Drawing.Size(227, 30);
            this.button_Video.TabIndex = 1;
            this.button_Video.Text = "View Video Guide";
            this.button_Video.UseVisualStyleBackColor = true;
            this.button_Video.Click += new System.EventHandler(this.Button_Video_Click);
            // 
            // backgroundWorker_Check
            // 
            this.backgroundWorker_Check.WorkerReportsProgress = true;
            this.backgroundWorker_Check.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Check_DoWork);
            this.backgroundWorker_Check.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Check_ProgressChanged);
            this.backgroundWorker_Check.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_Check_RunWorkerCompleted);
            // 
            // backgroundWorker_SystemInfo
            // 
            this.backgroundWorker_SystemInfo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_SystemInfo_DoWork);
            this.backgroundWorker_SystemInfo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_SystemInfo_RunWorkerCompleted);
            // 
            // button_Refresh
            // 
            this.button_Refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Refresh.Location = new System.Drawing.Point(485, 362);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(233, 30);
            this.button_Refresh.TabIndex = 6;
            this.button_Refresh.Text = "Refresh";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Visible = false;
            this.button_Refresh.Click += new System.EventHandler(this.Button_Refresh_Click);
            // 
            // label_Info
            // 
            this.label_Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Info.Location = new System.Drawing.Point(15, 359);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(464, 36);
            this.label_Info.TabIndex = 5;
            this.label_Info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_Info.Visible = false;
            // 
            // backgroundWorker_Download
            // 
            this.backgroundWorker_Download.WorkerReportsProgress = true;
            this.backgroundWorker_Download.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Download_DoWork);
            this.backgroundWorker_Download.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Download_ProgressChanged);
            // 
            // timer_ExtractProgress
            // 
            this.timer_ExtractProgress.Tick += new System.EventHandler(this.Timer_ExtractProgress_Tick);
            // 
            // timer_Refresh
            // 
            this.timer_Refresh.Interval = 1000;
            this.timer_Refresh.Tick += new System.EventHandler(this.Timer_Refresh_Tick);
            // 
            // Form_yuzuEarlyAccessLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 590);
            this.Controls.Add(this.label_Info);
            this.Controls.Add(this.groupBox_HelpandSupport);
            this.Controls.Add(this.groupBox_SystemInfo);
            this.Controls.Add(this.pictureBox_Logo);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.button_Launch);
            this.Controls.Add(this.button_Download);
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.groupBox_Progress);
            this.Controls.Add(this.groupBox_Firmware);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_yuzuEarlyAccessLauncher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "yuzu Early Access Launcher";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Logo)).EndInit();
            this.groupBox_Progress.ResumeLayout(false);
            this.groupBox_Firmware.ResumeLayout(false);
            this.groupBox_SystemInfo.ResumeLayout(false);
            this.groupBox_HelpandSupport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Logo;
        private System.Windows.Forms.Button button_Launch;
        private System.Windows.Forms.Button button_Download;
        private System.Windows.Forms.GroupBox groupBox_Progress;
        private System.Windows.Forms.Label label_Speed;
        private System.Windows.Forms.Label label_Time;
        private System.Windows.Forms.Label label_Progress;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_Message;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.GroupBox groupBox_Firmware;
        private System.Windows.Forms.Button button_Firmware;
        private System.Windows.Forms.Label label_Firmware;
        private System.Windows.Forms.GroupBox groupBox_SystemInfo;
        private System.Windows.Forms.Label label_RAMInfo;
        private System.Windows.Forms.Label label_CPUInfo;
        private System.Windows.Forms.Label label_RAMCapacity;
        private System.Windows.Forms.Label label_CPUName;
        private System.Windows.Forms.GroupBox groupBox_HelpandSupport;
        private System.Windows.Forms.Button button_Report;
        private System.Windows.Forms.Button button_Support;
        private System.Windows.Forms.Button button_Video;
        private System.Windows.Forms.Button button_FAQ;
        private System.Windows.Forms.Button button_About;
        private System.Windows.Forms.Button button_Compatibility;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Check;
        private System.ComponentModel.BackgroundWorker backgroundWorker_SystemInfo;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.Label label_Info;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Download;
        private System.Windows.Forms.Timer timer_ExtractProgress;
        private System.Windows.Forms.Timer timer_Refresh;
    }
}
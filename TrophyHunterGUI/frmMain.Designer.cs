namespace TrophyHunterGUI
{
    partial class frmMain
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
            this.btnReadFriendList = new System.Windows.Forms.Button();
            this.btnStartHunting = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.barStatus = new System.Windows.Forms.StatusStrip();
            this.barProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.grpFriendList = new System.Windows.Forms.GroupBox();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radOnline = new System.Windows.Forms.RadioButton();
            this.bWorkerRead = new System.ComponentModel.BackgroundWorker();
            this.bWorkerHunt = new System.ComponentModel.BackgroundWorker();
            this.btnReopenChrome = new System.Windows.Forms.Button();
            this.numContinueFrom = new System.Windows.Forms.NumericUpDown();
            this.barCaptchaNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.barStatus.SuspendLayout();
            this.grpFriendList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numContinueFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReadFriendList
            // 
            this.btnReadFriendList.Location = new System.Drawing.Point(139, 12);
            this.btnReadFriendList.Name = "btnReadFriendList";
            this.btnReadFriendList.Size = new System.Drawing.Size(73, 40);
            this.btnReadFriendList.TabIndex = 0;
            this.btnReadFriendList.Text = "&1. Read Friend List";
            this.btnReadFriendList.UseVisualStyleBackColor = true;
            this.btnReadFriendList.Click += new System.EventHandler(this.btnReadFriendList_Click);
            // 
            // btnStartHunting
            // 
            this.btnStartHunting.Location = new System.Drawing.Point(218, 12);
            this.btnStartHunting.Name = "btnStartHunting";
            this.btnStartHunting.Size = new System.Drawing.Size(77, 40);
            this.btnStartHunting.TabIndex = 1;
            this.btnStartHunting.Text = "&2. Hunt Trophies";
            this.btnStartHunting.UseVisualStyleBackColor = true;
            this.btnStartHunting.Click += new System.EventHandler(this.btnStartHunting_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(139, 58);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(156, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "&9. Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // barStatus
            // 
            this.barStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barProgress,
            this.lblProgress});
            this.barStatus.Location = new System.Drawing.Point(0, 219);
            this.barStatus.Name = "barStatus";
            this.barStatus.Size = new System.Drawing.Size(434, 22);
            this.barStatus.TabIndex = 3;
            this.barStatus.Text = "statusStrip1";
            // 
            // barProgress
            // 
            this.barProgress.Name = "barProgress";
            this.barProgress.Size = new System.Drawing.Size(100, 16);
            this.barProgress.Step = 1;
            this.barProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lblProgress
            // 
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 17);
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.Location = new System.Drawing.Point(12, 95);
            this.lstLog.Name = "lstLog";
            this.lstLog.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstLog.Size = new System.Drawing.Size(410, 121);
            this.lstLog.TabIndex = 4;
            // 
            // grpFriendList
            // 
            this.grpFriendList.Controls.Add(this.radAll);
            this.grpFriendList.Controls.Add(this.radOnline);
            this.grpFriendList.Location = new System.Drawing.Point(12, 12);
            this.grpFriendList.Name = "grpFriendList";
            this.grpFriendList.Size = new System.Drawing.Size(121, 69);
            this.grpFriendList.TabIndex = 6;
            this.grpFriendList.TabStop = false;
            this.grpFriendList.Text = "Read Friend List";
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Location = new System.Drawing.Point(6, 43);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(70, 17);
            this.radAll.TabIndex = 2;
            this.radAll.Text = "All mutual";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // radOnline
            // 
            this.radOnline.AutoSize = true;
            this.radOnline.Checked = true;
            this.radOnline.Location = new System.Drawing.Point(7, 20);
            this.radOnline.Name = "radOnline";
            this.radOnline.Size = new System.Drawing.Size(89, 17);
            this.radOnline.TabIndex = 1;
            this.radOnline.TabStop = true;
            this.radOnline.Text = "Online mutual";
            this.radOnline.UseVisualStyleBackColor = true;
            this.radOnline.CheckedChanged += new System.EventHandler(this.radOnline_CheckedChanged);
            // 
            // bWorkerRead
            // 
            this.bWorkerRead.WorkerReportsProgress = true;
            this.bWorkerRead.WorkerSupportsCancellation = true;
            this.bWorkerRead.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWorkerRead_DoWork);
            this.bWorkerRead.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bWorkerRead_ProgressChanged);
            this.bWorkerRead.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWorkerRead_RunWorkerCompleted);
            // 
            // bWorkerHunt
            // 
            this.bWorkerHunt.WorkerReportsProgress = true;
            this.bWorkerHunt.WorkerSupportsCancellation = true;
            this.bWorkerHunt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWorkerHunt_DoWork);
            this.bWorkerHunt.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bWorkerHunt_ProgressChanged);
            this.bWorkerHunt.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWorkerHunt_RunWorkerCompleted);
            // 
            // btnReopenChrome
            // 
            this.btnReopenChrome.Enabled = false;
            this.btnReopenChrome.Location = new System.Drawing.Point(302, 58);
            this.btnReopenChrome.Name = "btnReopenChrome";
            this.btnReopenChrome.Size = new System.Drawing.Size(120, 23);
            this.btnReopenChrome.TabIndex = 7;
            this.btnReopenChrome.Text = "ReOpen Chrome";
            this.btnReopenChrome.UseVisualStyleBackColor = true;
            this.btnReopenChrome.Click += new System.EventHandler(this.btnReopenChrome_Click);
            // 
            // numContinueFrom
            // 
            this.numContinueFrom.Location = new System.Drawing.Point(302, 24);
            this.numContinueFrom.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numContinueFrom.Name = "numContinueFrom";
            this.numContinueFrom.Size = new System.Drawing.Size(120, 20);
            this.numContinueFrom.TabIndex = 5;
            // 
            // barCaptchaNotify
            // 
            this.barCaptchaNotify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.barCaptchaNotify.BalloonTipText = "Captcha found! Waiting for input";
            this.barCaptchaNotify.BalloonTipTitle = "Trophy Hunter";
            this.barCaptchaNotify.Text = "Trophy Hunter";
            this.barCaptchaNotify.Visible = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 241);
            this.Controls.Add(this.btnReopenChrome);
            this.Controls.Add(this.grpFriendList);
            this.Controls.Add(this.btnReadFriendList);
            this.Controls.Add(this.numContinueFrom);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.barStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStartHunting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.Tag = "";
            this.Text = "Trophy Hunter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.barStatus.ResumeLayout(false);
            this.barStatus.PerformLayout();
            this.grpFriendList.ResumeLayout(false);
            this.grpFriendList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numContinueFrom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadFriendList;
        private System.Windows.Forms.Button btnStartHunting;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.StatusStrip barStatus;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.ToolStripProgressBar barProgress;
        private System.Windows.Forms.GroupBox grpFriendList;
        private System.Windows.Forms.RadioButton radOnline;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.ComponentModel.BackgroundWorker bWorkerRead;
        private System.ComponentModel.BackgroundWorker bWorkerHunt;
        private System.Windows.Forms.Button btnReopenChrome;
        private System.Windows.Forms.NumericUpDown numContinueFrom;
        private System.Windows.Forms.NotifyIcon barCaptchaNotify;

    }
}


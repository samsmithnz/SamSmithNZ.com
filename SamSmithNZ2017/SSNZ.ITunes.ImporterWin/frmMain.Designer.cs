namespace SSNZ.ITunes.ImporterWin
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
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblStatus = new System.Windows.Forms.Label();
            this.prgStatus = new System.Windows.Forms.ProgressBar();
            this.btnFillInPlayListGaps = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(24, 27);
            this.txtFile.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(988, 31);
            this.txtFile.TabIndex = 85;
            this.txtFile.Text = "C:\\Users\\samsmit\\Documents\\PersonalGoogleDriveSync\\Personal\\Itunes\\";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(1028, 23);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(60, 44);
            this.btnOpenFile.TabIndex = 86;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.ButtonOpenFile_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(24, 127);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(70, 25);
            this.lblStatus.TabIndex = 88;
            this.lblStatus.Text = "label1";
            // 
            // prgStatus
            // 
            this.prgStatus.Location = new System.Drawing.Point(24, 158);
            this.prgStatus.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.prgStatus.Name = "prgStatus";
            this.prgStatus.Size = new System.Drawing.Size(1064, 44);
            this.prgStatus.TabIndex = 89;
            // 
            // btnFillInPlayListGaps
            // 
            this.btnFillInPlayListGaps.Location = new System.Drawing.Point(24, 79);
            this.btnFillInPlayListGaps.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnFillInPlayListGaps.Name = "btnFillInPlayListGaps";
            this.btnFillInPlayListGaps.Size = new System.Drawing.Size(1064, 44);
            this.btnFillInPlayListGaps.TabIndex = 90;
            this.btnFillInPlayListGaps.Text = "Fill in playlist gaps";
            this.btnFillInPlayListGaps.UseVisualStyleBackColor = true;
            this.btnFillInPlayListGaps.Click += new System.EventHandler(this.ButtonFillInPlayListGaps_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 225);
            this.Controls.Add(this.btnFillInPlayListGaps);
            this.Controls.Add(this.prgStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtFile);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.TextBox txtFile;
        internal System.Windows.Forms.Button btnOpenFile;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar prgStatus;
        internal System.Windows.Forms.Button btnFillInPlayListGaps;
    }
}


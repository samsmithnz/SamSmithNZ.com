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
            this.btnImport = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblStatus = new System.Windows.Forms.Label();
            this.prgStatus = new System.Windows.Forms.ProgressBar();
            this.btnFillInPlayListGaps = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Location = new System.Drawing.Point(18, 62);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(142, 35);
            this.btnImport.TabIndex = 84;
            this.btnImport.Text = "Import Playlist";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(18, 22);
            this.txtFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(742, 26);
            this.txtFile.TabIndex = 85;
            this.txtFile.Text = "C:\\Projects\\SSNZDefault\\SamSmithNZ\\SamSmithNZ\\ITunesSourceFiles\\";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(771, 18);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(45, 35);
            this.btnOpenFile.TabIndex = 86;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(18, 102);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(51, 20);
            this.lblStatus.TabIndex = 88;
            this.lblStatus.Text = "label1";
            // 
            // prgStatus
            // 
            this.prgStatus.Location = new System.Drawing.Point(18, 126);
            this.prgStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.prgStatus.Name = "prgStatus";
            this.prgStatus.Size = new System.Drawing.Size(798, 35);
            this.prgStatus.TabIndex = 89;
            // 
            // btnFillInPlayListGaps
            // 
            this.btnFillInPlayListGaps.Location = new System.Drawing.Point(549, 63);
            this.btnFillInPlayListGaps.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnFillInPlayListGaps.Name = "btnFillInPlayListGaps";
            this.btnFillInPlayListGaps.Size = new System.Drawing.Size(267, 35);
            this.btnFillInPlayListGaps.TabIndex = 90;
            this.btnFillInPlayListGaps.Text = "Fill in playlist gaps (USE ME)";
            this.btnFillInPlayListGaps.UseVisualStyleBackColor = true;
            this.btnFillInPlayListGaps.Click += new System.EventHandler(this.btnFillInPlayListGaps_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 180);
            this.Controls.Add(this.btnFillInPlayListGaps);
            this.Controls.Add(this.prgStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtFile);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnImport;
        internal System.Windows.Forms.TextBox txtFile;
        internal System.Windows.Forms.Button btnOpenFile;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar prgStatus;
        internal System.Windows.Forms.Button btnFillInPlayListGaps;
    }
}


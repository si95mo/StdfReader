namespace StdfReader
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.trvRecords = new System.Windows.Forms.TreeView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.btnExpandAll = new System.Windows.Forms.Button();
            this.btnCollapseAll = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblRecordCounter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenFile.Location = new System.Drawing.Point(12, 13);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(92, 47);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open file";
            this.btnOpenFile.UseVisualStyleBackColor = false;
            this.btnOpenFile.Click += new System.EventHandler(this.BtnOpenFile_Click);
            // 
            // trvRecords
            // 
            this.trvRecords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.trvRecords.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvRecords.ForeColor = System.Drawing.SystemColors.InfoText;
            this.trvRecords.Location = new System.Drawing.Point(12, 106);
            this.trvRecords.Name = "trvRecords";
            this.trvRecords.Size = new System.Drawing.Size(639, 803);
            this.trvRecords.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(119, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(533, 47);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "--";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExpandAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpandAll.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpandAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExpandAll.Location = new System.Drawing.Point(12, 68);
            this.btnExpandAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(92, 31);
            this.btnExpandAll.TabIndex = 3;
            this.btnExpandAll.Text = "Expand all";
            this.btnExpandAll.UseVisualStyleBackColor = false;
            this.btnExpandAll.Click += new System.EventHandler(this.BtnExpandAll);
            // 
            // btnCollapseAll
            // 
            this.btnCollapseAll.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCollapseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCollapseAll.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCollapseAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCollapseAll.Location = new System.Drawing.Point(112, 68);
            this.btnCollapseAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnCollapseAll.Name = "btnCollapseAll";
            this.btnCollapseAll.Size = new System.Drawing.Size(91, 31);
            this.btnCollapseAll.TabIndex = 4;
            this.btnCollapseAll.Text = "Collapse all";
            this.btnCollapseAll.UseVisualStyleBackColor = false;
            this.btnCollapseAll.Click += new System.EventHandler(this.BtnCollapseAll_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExport.Location = new System.Drawing.Point(561, 68);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(91, 31);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // lblRecordCounter
            // 
            this.lblRecordCounter.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCounter.Location = new System.Drawing.Point(210, 68);
            this.lblRecordCounter.Name = "lblRecordCounter";
            this.lblRecordCounter.Size = new System.Drawing.Size(344, 31);
            this.lblRecordCounter.TabIndex = 6;
            this.lblRecordCounter.Text = "--";
            this.lblRecordCounter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(664, 921);
            this.Controls.Add(this.lblRecordCounter);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnCollapseAll);
            this.Controls.Add(this.btnExpandAll);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.trvRecords);
            this.Controls.Add(this.btnOpenFile);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 10F);
            this.ForeColor = System.Drawing.SystemColors.Info;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Stdf file reader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TreeView trvRecords;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Button btnExpandAll;
        private System.Windows.Forms.Button btnCollapseAll;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblRecordCounter;
    }
}


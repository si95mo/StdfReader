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
            this.bgReaderWorker = new System.ComponentModel.BackgroundWorker();
            this.btnExpandAll = new System.Windows.Forms.Button();
            this.btnCollapseAll = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblRecordCounter = new System.Windows.Forms.Label();
            this.cbxFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bgExcelWorker = new System.ComponentModel.BackgroundWorker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txbFieldName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFile.ForeColor = System.Drawing.SystemColors.Info;
            this.btnOpenFile.Location = new System.Drawing.Point(13, 13);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(92, 31);
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
            this.trvRecords.Location = new System.Drawing.Point(12, 96);
            this.trvRecords.Name = "trvRecords";
            this.trvRecords.Size = new System.Drawing.Size(917, 813);
            this.trvRecords.TabIndex = 1;
            this.trvRecords.Click += new System.EventHandler(this.TrvRecords_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(110, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(563, 31);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "--";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExpandAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpandAll.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpandAll.ForeColor = System.Drawing.SystemColors.Info;
            this.btnExpandAll.Location = new System.Drawing.Point(12, 54);
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
            this.btnCollapseAll.ForeColor = System.Drawing.SystemColors.Info;
            this.btnCollapseAll.Location = new System.Drawing.Point(112, 54);
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
            this.btnExport.ForeColor = System.Drawing.SystemColors.Info;
            this.btnExport.Location = new System.Drawing.Point(837, 15);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(91, 31);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // lblRecordCounter
            // 
            this.lblRecordCounter.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCounter.Location = new System.Drawing.Point(210, 46);
            this.lblRecordCounter.Name = "lblRecordCounter";
            this.lblRecordCounter.Size = new System.Drawing.Size(463, 47);
            this.lblRecordCounter.TabIndex = 6;
            this.lblRecordCounter.Text = "--";
            this.lblRecordCounter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbxFilter
            // 
            this.cbxFilter.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.cbxFilter.ForeColor = System.Drawing.SystemColors.Info;
            this.cbxFilter.FormattingEnabled = true;
            this.cbxFilter.Location = new System.Drawing.Point(739, 19);
            this.cbxFilter.Name = "cbxFilter";
            this.cbxFilter.Size = new System.Drawing.Size(91, 24);
            this.cbxFilter.TabIndex = 7;
            this.cbxFilter.SelectedIndexChanged += new System.EventHandler(this.CbxFilter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(679, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Expand";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.Info;
            this.btnSearch.Location = new System.Drawing.Point(837, 54);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 31);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txbFieldName
            // 
            this.txbFieldName.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txbFieldName.ForeColor = System.Drawing.SystemColors.Info;
            this.txbFieldName.Location = new System.Drawing.Point(682, 56);
            this.txbFieldName.Name = "txbFieldName";
            this.txbFieldName.Size = new System.Drawing.Size(148, 28);
            this.txbFieldName.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(941, 921);
            this.Controls.Add(this.txbFieldName);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxFilter);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TreeView trvRecords;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker bgReaderWorker;
        private System.Windows.Forms.Button btnExpandAll;
        private System.Windows.Forms.Button btnCollapseAll;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblRecordCounter;
        private System.Windows.Forms.ComboBox cbxFilter;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker bgExcelWorker;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txbFieldName;
    }
}


namespace StudentMangementSystem_GUI
{
    partial class TeacherSubmissionsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvSubmissions = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSubmissions)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelTop (Controls Wrapper)
            // 
            this.panelTop.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelTop.Controls.Add(this.btnBack);
            this.panelTop.Controls.Add(this.lblSubject);
            this.panelTop.Controls.Add(this.cmbSubject);
            this.panelTop.Controls.Add(this.lblSearch);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(900, 80);
            this.panelTop.TabIndex = 0;

            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(82, 13);
            this.lblSearch.Text = "Search Student:";

            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 35);
            this.txtSearch.Size = new System.Drawing.Size(180, 20);

            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(200, 33);
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;

            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(300, 15);
            this.lblSubject.Text = "Filter by Subject:";

            // 
            // cmbSubject
            // 
            this.cmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubject.Location = new System.Drawing.Point(300, 35);
            this.cmbSubject.Size = new System.Drawing.Size(150, 21);

            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(470, 33);
            this.btnRefresh.Size = new System.Drawing.Size(75, 25);
            this.btnRefresh.Text = "Refresh";

            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(800, 25);
            this.btnBack.Size = new System.Drawing.Size(85, 30);
            this.btnBack.Text = "Back";

            // 
            // dgvSubmissions
            // 
            this.dgvSubmissions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSubmissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubmissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSubmissions.Location = new System.Drawing.Point(0, 80);
            this.dgvSubmissions.Name = "dgvSubmissions";
            this.dgvSubmissions.Size = new System.Drawing.Size(900, 420);
            this.dgvSubmissions.TabIndex = 1;

            // 
            // TeacherSubmissionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.dgvSubmissions);
            this.Controls.Add(this.panelTop);
            this.Name = "TeacherSubmissionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Student Submissions Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubmissions)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvSubmissions;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Panel panelTop;
    }
}
namespace StudentMangementSystem_GUI
{
    partial class ViewSubmissionForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.footer = new System.Windows.Forms.Label();
            this.dgvSubmissions = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.btnGrade = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.txtFeedback = new System.Windows.Forms.TextBox();
            this.txtMarks = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubmissions)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 74);
            this.panel1.TabIndex = 117;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(708, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(5);
            this.btnSearch.Size = new System.Drawing.Size(68, 37);
            this.btnSearch.TabIndex = 56;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtSearch.Location = new System.Drawing.Point(497, 14);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(279, 37);
            this.txtSearch.TabIndex = 55;
            this.txtSearch.Text = "  Search Here";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Student Management System";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Navy;
            this.panel2.Controls.Add(this.footer);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(0, 595);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(801, 68);
            this.panel2.TabIndex = 118;
            // 
            // footer
            // 
            this.footer.AutoSize = true;
            this.footer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.footer.Location = new System.Drawing.Point(262, 25);
            this.footer.Name = "footer";
            this.footer.Size = new System.Drawing.Size(311, 15);
            this.footer.TabIndex = 0;
            this.footer.Text = " © 2026 Student Management System | All Right Reserved";
            this.footer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSubmissions
            // 
            this.dgvSubmissions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSubmissions.BackgroundColor = System.Drawing.Color.Navy;
            this.dgvSubmissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubmissions.GridColor = System.Drawing.Color.White;
            this.dgvSubmissions.Location = new System.Drawing.Point(11, 175);
            this.dgvSubmissions.Name = "dgvSubmissions";
            this.dgvSubmissions.Size = new System.Drawing.Size(776, 299);
            this.dgvSubmissions.TabIndex = 122;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(275, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(298, 32);
            this.label8.TabIndex = 121;
            this.label8.Text = "Assignment Submissions";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGrade
            // 
            this.btnGrade.AutoSize = true;
            this.btnGrade.BackColor = System.Drawing.Color.Green;
            this.btnGrade.FlatAppearance.BorderSize = 0;
            this.btnGrade.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnGrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrade.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrade.ForeColor = System.Drawing.Color.White;
            this.btnGrade.Location = new System.Drawing.Point(607, 531);
            this.btnGrade.Name = "btnGrade";
            this.btnGrade.Padding = new System.Windows.Forms.Padding(5);
            this.btnGrade.Size = new System.Drawing.Size(74, 37);
            this.btnGrade.TabIndex = 120;
            this.btnGrade.Text = "Grade";
            this.btnGrade.UseVisualStyleBackColor = false;
            // 
            // back
            // 
            this.back.AutoSize = true;
            this.back.BackColor = System.Drawing.Color.Red;
            this.back.FlatAppearance.BorderSize = 0;
            this.back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.back.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back.ForeColor = System.Drawing.Color.White;
            this.back.Location = new System.Drawing.Point(89, 531);
            this.back.Name = "back";
            this.back.Padding = new System.Windows.Forms.Padding(5);
            this.back.Size = new System.Drawing.Size(70, 37);
            this.back.TabIndex = 119;
            this.back.Text = "Back";
            this.back.UseVisualStyleBackColor = false;
            // 
            // txtFeedback
            // 
            this.txtFeedback.Location = new System.Drawing.Point(463, 505);
            this.txtFeedback.Name = "txtFeedback";
            this.txtFeedback.Size = new System.Drawing.Size(100, 20);
            this.txtFeedback.TabIndex = 123;
            // 
            // txtMarks
            // 
            this.txtMarks.Location = new System.Drawing.Point(223, 505);
            this.txtMarks.Name = "txtMarks";
            this.txtMarks.Size = new System.Drawing.Size(100, 20);
            this.txtMarks.TabIndex = 124;
            // 
            // ViewSubmissionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 661);
            this.Controls.Add(this.txtMarks);
            this.Controls.Add(this.txtFeedback);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvSubmissions);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnGrade);
            this.Controls.Add(this.back);
            this.Name = "ViewSubmissionForm";
            this.Text = "ViewSubmissionForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubmissions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label footer;
        private System.Windows.Forms.DataGridView dgvSubmissions;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnGrade;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.TextBox txtFeedback;
        private System.Windows.Forms.TextBox txtMarks;
    }
}
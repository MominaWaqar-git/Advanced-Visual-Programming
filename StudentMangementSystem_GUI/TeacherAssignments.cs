using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class TeacherAssignments : Form
    {
        int teacherId;

        string connStr = "server=localhost;user=root;password=;database=student_management_system";

        public TeacherAssignments(int id)
        {
            InitializeComponent();
            teacherId = id;

            btnLoad.Click += BtnLoad_Click;
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnSave.Click += BtnSave_Click;
            btnOpen.Click += BtnOpen_Click;
            btnBack.Click += BtnBack_Click;

            cmbSubject.SelectedIndexChanged += CmbSubjects_SelectedIndexChanged;

            LoadSubjects();
            LoadAssignments();
        }

        // ================= LOAD SUBJECTS =================
        private void LoadSubjects()
        {
            cmbSubject.Items.Clear();

            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                string q = @"SELECT DISTINCT subject 
                             FROM teacher_student 
                             WHERE teacher_id=@tid";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@tid", teacherId);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmbSubject.Items.Add(dr["subject"].ToString());
                }
            }
        }

        // ================= LOAD DATA =================
        private void LoadAssignments(string search = "")
        {
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                string q = @"
                SELECT 
                    s.id,
                    a.title,
                    a.subject,
                    s.student_username,
                    s.file_path,
                    s.marks,

                    CASE 
                        WHEN s.is_checked = 0 THEN '⏳ Pending'
                        ELSE '✔ Checked'
                    END AS Status

                FROM submissions s

                INNER JOIN assignments a 
                    ON s.assignment_id = a.id

                WHERE a.teacher_id = @tid";

                // SUBJECT FILTER
                if (!string.IsNullOrEmpty(cmbSubject.Text))
                {
                    q += " AND LOWER(a.subject) = LOWER(@sub)";
                }

                // SEARCH FILTER
                if (!string.IsNullOrEmpty(search))
                {
                    q += @" AND (
                        a.title LIKE @search OR 
                        a.subject LIKE @search OR 
                        s.student_username LIKE @search
                    )";
                }

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@tid", teacherId);

                if (!string.IsNullOrEmpty(cmbSubject.Text))
                    cmd.Parameters.AddWithValue("@sub", cmbSubject.Text);

                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAssignments.DataSource = dt;

                dgvAssignments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // ================= EVENTS =================

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            LoadAssignments();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadAssignments(txtSearch.Text.Trim());
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbSubject.SelectedIndex = -1;
            LoadAssignments();
        }

        private void CmbSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAssignments();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Subject Error: " + ex.Message);
            }
        }

        // ================= OPEN FILE =================
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select assignment first!");
                return;
            }

            string filePath = dgvAssignments.SelectedRows[0]
                .Cells["file_path"].Value.ToString();

            if (File.Exists(filePath))
            {
                Process.Start(filePath);
            }
            else
            {
                MessageBox.Show("File not found!");
            }
        }

        // ================= SAVE MARKS =================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select assignment!");
                return;
            }

            int submissionId = Convert.ToInt32(
                dgvAssignments.SelectedRows[0].Cells["id"].Value
            );

            int marks;
            if (!int.TryParse(txtMarks.Text, out marks))
            {
                MessageBox.Show("Enter valid marks!");
                return;
            }

            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                string q = @"UPDATE submissions 
                             SET marks=@marks, is_checked=1 
                             WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@marks", marks);
                cmd.Parameters.AddWithValue("@id", submissionId);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Marks saved ✔");

                LoadAssignments();
            }
        }

        // ================= BACK =================
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            TeacherDashboard dash = new TeacherDashboard(teacherId);
            dash.Show();
        }
    }
}
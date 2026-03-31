using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class StudentAssignmentForm : Form
    {
        int studentId;
        string studentCourse;
        string connStr = "server=localhost;user=root;password=;database=student_management_system";

        public StudentAssignmentForm(int id)
        {
            InitializeComponent();
            studentId = id;

            // Events
            btnSubmit.Click += BtnSubmit_Click;
            btnBack.Click += BtnBack_Click;
            btnSearch.Click += BtnSearch_Click;

            LoadStudentCourse();
            LoadAssignments();
        }

        private void LoadStudentCourse()
        {
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();
                string q = "SELECT course FROM students WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", studentId);
                studentCourse = cmd.ExecuteScalar()?.ToString();
            }
        }

        private void LoadAssignments(string search = "")
        {
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                string q = @"SELECT id, subject, title, description, uploaded_at, file_path 
                             FROM assignments 
                             WHERE subject=@course";

                if (!string.IsNullOrEmpty(search))
                    q += " AND title LIKE @search";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@course", studentCourse);

                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvAssignments.DataSource = dt;
                dgvAssignments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAssignments.MultiSelect = false;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadAssignments(txtSearch.Text.Trim());
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an assignment first!");
                return;
            }

            int assignmentId = Convert.ToInt32(dgvAssignments.SelectedRows[0].Cells["id"].Value);
            string filePath = txtFilePath.Text.Trim();

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please browse a file to submit.");
                return;
            }

            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                // Check if already submitted
                string checkQuery = "SELECT COUNT(*) FROM submissions WHERE student_id=@sid AND assignment_id=@aid";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@sid", studentId);
                checkCmd.Parameters.AddWithValue("@aid", assignmentId);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("You have already submitted this assignment!");
                    return;
                }

                string q = @"INSERT INTO submissions(student_id, assignment_id, file_path, submitted_at)
                             VALUES(@sid, @aid, @file, NOW())";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@aid", assignmentId);
                cmd.Parameters.AddWithValue("@file", filePath);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Assignment submitted successfully!");
                txtFilePath = null;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            StudentDashboard dash = new StudentDashboard(studentId);
            dash.Show();
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }
    }
}
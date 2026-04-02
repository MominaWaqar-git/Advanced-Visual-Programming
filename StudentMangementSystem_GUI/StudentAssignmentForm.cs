using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class StudentAssignmentForm : Form
    {
        int studentId;
        string connStr = "server=localhost;user=root;password=;database=student_management_system";

        public StudentAssignmentForm(int id)
        {
            InitializeComponent();
            studentId = id;

            btnSubmit.Click += BtnSubmit_Click;
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnBrowse.Click += BtnBrowse_Click;
            btnBack.Click += BtnBack_Click;
            cmbSubject.SelectedIndexChanged += CmbSubject_SelectedIndexChanged;

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
                             WHERE student_id=@sid";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@sid", studentId);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmbSubject.Items.Add(dr["subject"].ToString());
                }
            }
        }

        // ================= SUBJECT CHANGE =================
        private void CmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAssignments();
        }

        // ================= LOAD ASSIGNMENTS (CASE INSENSITIVE FIX) =================
        private void LoadAssignments(string search = "")
        {
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                string q = @"
        SELECT 
            a.id,
            a.subject,
            a.title,
            a.description,
            a.file_path,

            -- ✅ MARKS COLUMN (BASED ON YOUR TABLE)
            IFNULL(s.marks, 0) AS Marks,

            CASE 
                WHEN s.id IS NULL THEN '❌ Pending'
                WHEN s.is_checked = 0 THEN '⏳ Submitted'
                ELSE '✔ Checked'
            END AS Status

        FROM assignments a

        LEFT JOIN submissions s 
            ON s.assignment_id = a.id 
            AND s.student_id = @sid

        WHERE LOWER(a.subject) IN 
        (
            SELECT LOWER(subject) 
            FROM teacher_student 
            WHERE student_id = @sid
        )";

                // ✅ SUBJECT FILTER (CASE INSENSITIVE)
                if (!string.IsNullOrEmpty(cmbSubject.Text))
                {
                    q += " AND LOWER(a.subject) = LOWER(@sub)";
                }

                // ✅ SEARCH FILTER
                if (!string.IsNullOrEmpty(search))
                {
                    q += " AND (a.title LIKE @search OR a.subject LIKE @search)";
                }

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@sid", studentId);

                if (!string.IsNullOrEmpty(cmbSubject.Text))
                    cmd.Parameters.AddWithValue("@sub", cmbSubject.Text);

                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAssignments.DataSource = dt;
            }
        }

        // ================= SEARCH =================
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadAssignments(txtSearch.Text.Trim());
        }

        // ================= REFRESH =================
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbSubject.SelectedIndex = -1;
            LoadAssignments();
        }

        // ================= BROWSE FILE =================
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        // ================= SUBMIT =================
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvAssignments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select assignment!");
                return;
            }

            int assignmentId = Convert.ToInt32(dgvAssignments.SelectedRows[0].Cells["id"].Value);
            string filePath = txtFilePath.Text;

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Select file!");
                return;
            }

            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                // CHECK ALREADY SUBMITTED
                string check = "SELECT COUNT(*) FROM submissions WHERE student_id=@sid AND assignment_id=@aid";
                MySqlCommand checkCmd = new MySqlCommand(check, con);
                checkCmd.Parameters.AddWithValue("@sid", studentId);
                checkCmd.Parameters.AddWithValue("@aid", assignmentId);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Already Submitted ✔");
                    return;
                }

                // GET STUDENT USERNAME ✅
                string studentUsername = "";

                string getName = "SELECT username FROM students WHERE id=@sid";
                MySqlCommand nameCmd = new MySqlCommand(getName, con);
                nameCmd.Parameters.AddWithValue("@sid", studentId);

                object result = nameCmd.ExecuteScalar();
                if (result != null)
                {
                    studentUsername = result.ToString();
                }

                // FILE COPY
                string folder = Path.Combine(Application.StartupPath, "Submissions");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string dest = Path.Combine(folder, Path.GetFileName(filePath));
                File.Copy(filePath, dest, true);

                // ✅ FIXED QUERY
                string q = @"INSERT INTO submissions 
                    (student_id, assignment_id, student_username, file_path, marks, is_checked, submitted_at)
                    VALUES (@sid, @aid, @uname, @file, NULL, 0, NOW())";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@aid", assignmentId);
                cmd.Parameters.AddWithValue("@uname", studentUsername); 
                cmd.Parameters.AddWithValue("@file", dest);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Submitted Successfully ✔");

                txtFilePath.Clear();
                LoadAssignments();
            }
        }

        // ================= BACK =================
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentDashboard dash = new StudentDashboard(studentId);
            dash.Show();
        }
    }
}
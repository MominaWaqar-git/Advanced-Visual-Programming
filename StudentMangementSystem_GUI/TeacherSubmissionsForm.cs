using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic; // InputBox ke liye zaroori hai

namespace StudentMangementSystem_GUI
{
    public partial class TeacherSubmissionsForm : Form
    {
        int teacherId;
        string connStr = "server=localhost;user=root;password=;database=student_management_system";

        public TeacherSubmissionsForm(int id)
        {
            InitializeComponent();
            teacherId = id;

            // Event Handlers attach karna
            this.Load += TeacherSubmissionsForm_Load;
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnBack.Click += btnBack_Click;
            cmbSubject.SelectedIndexChanged += cmbSubject_SelectedIndexChanged;

            // DataGridView Setup
            dgvSubmissions.CellContentClick += dgvSubmissions_CellContentClick;
            dgvSubmissions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSubmissions.ReadOnly = true;
        }

        private void TeacherSubmissionsForm_Load(object sender, EventArgs e)
        {
            LoadTeacherSubjects();
            LoadSubmissions();
            AddActionButtons(); // Grid mein Grade button add karne ke liye
        }

        // Dropdown mein subjects load karna
        private void LoadTeacherSubjects()
        {
            cmbSubject.Items.Clear();
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();
                string q = "SELECT DISTINCT subject FROM teacher_student WHERE teacher_id=@tid";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@tid", teacherId);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) { cmbSubject.Items.Add(dr["subject"].ToString()); }
            }
        }

        // Submissions fetch karna
        private void LoadSubmissions(string search = "")
        {
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();
                string q = @"SELECT s.id, st.name AS Student, a.title AS Assignment, 
                             a.subject, s.file_path, s.marks, 
                             CASE WHEN s.is_checked = 1 THEN 'Checked' ELSE 'Pending' END AS Status
                             FROM submissions s
                             JOIN assignments a ON s.assignment_id = a.id
                             JOIN students st ON s.student_id = st.id
                             JOIN teacher_student ts ON ts.student_id = s.student_id AND ts.subject = a.subject
                             WHERE ts.teacher_id = @tid";

                if (cmbSubject.SelectedIndex != -1) q += " AND a.subject = @sub";
                if (!string.IsNullOrEmpty(search)) q += " AND (st.name LIKE @search OR a.title LIKE @search)";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@tid", teacherId);
                if (cmbSubject.SelectedIndex != -1) cmd.Parameters.AddWithValue("@sub", cmbSubject.Text);
                if (!string.IsNullOrEmpty(search)) cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvSubmissions.DataSource = dt;
            }
        }

        // Grid mein "Grade" button add karna
        private void AddActionButtons()
        {
            if (dgvSubmissions.Columns["btnGrade"] == null)
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "Action";
                btn.Text = "Enter Marks";
                btn.Name = "btnGrade";
                btn.UseColumnTextForButtonValue = true;
                dgvSubmissions.Columns.Add(btn);
            }
        }

        
        private void dgvSubmissions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0 && dgvSubmissions.Columns[e.ColumnIndex].Name == "btnGrade")
            {
                int submissionId = Convert.ToInt32(dgvSubmissions.Rows[e.RowIndex].Cells["id"].Value);
                string currentMarks = dgvSubmissions.Rows[e.RowIndex].Cells["marks"].Value.ToString();
                string studentName = dgvSubmissions.Rows[e.RowIndex].Cells["Student"].Value.ToString();

                // Popup InputBox for Marks
                string input = Interaction.InputBox($"Enter marks for {studentName}:", "Grading System", currentMarks);
                if (!string.IsNullOrEmpty(input))
                {
                    if (int.TryParse(input, out int newMarks))
                    {
                        SaveMarksToDatabase(submissionId, newMarks);
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        // Database mein marks save karne ka function
        private void SaveMarksToDatabase(int subId, int marks)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connStr))
                {
                    con.Open();
                    string query = "UPDATE submissions SET marks = @m, is_checked = 1 WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@m", marks);
                    cmd.Parameters.AddWithValue("@id", subId);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Marks saved successfully!");
                        LoadSubmissions(); // Grid refresh karein
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) => LoadSubmissions(txtSearch.Text.Trim());
        private void cmbSubject_SelectedIndexChanged(object sender, EventArgs e) => LoadSubmissions(txtSearch.Text.Trim());

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbSubject.SelectedIndex = -1;
            LoadSubmissions();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            TeacherDashboard dash = new TeacherDashboard(teacherId);
            dash.Show();
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class StudentQuizForm : Form
    {
        int studentId;
        string connStr = "server=localhost;user=root;password=;database=student_management_system";

        public StudentQuizForm(int id)
        {
            InitializeComponent();
            studentId = id;

            btnBrowse.Click += BtnBrowse_Click;
            btnSubmit.Click += BtnSubmit_Click;
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnBack.Click += BtnBack_Click;
            cmbSubject.SelectedIndexChanged += CmbSubject_SelectedIndexChanged;

            LoadSubjects();
            LoadQuizzes();
        }

        // ================= SUBJECTS =================
        private void LoadSubjects()
        {
            cmbSubject.Items.Clear();

            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();
                string q = "SELECT DISTINCT subject FROM teacher_student WHERE student_id=@sid";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@sid", studentId);

                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmbSubject.Items.Add(dr["subject"].ToString());
                }
            }
        }

        private void CmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadQuizzes();
        }

        // ================= LOAD QUIZZES =================
        private void LoadQuizzes(string search = "")
        {
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                string q = @"
                SELECT q.id, q.subject, q.title, q.description, q.file_path,

                IFNULL(s.marks,0) AS marks,

                CASE 
                    WHEN s.id IS NULL THEN '❌ Not Attempted'
                    WHEN s.is_checked = 0 THEN '⏳ Submitted'
                    ELSE '✔ Checked'
                END AS status

                FROM quizzes q
                LEFT JOIN quiz_submissions s 
                ON q.id = s.quiz_id AND s.student_id = @sid

                WHERE q.subject IN (
                    SELECT subject FROM teacher_student WHERE student_id=@sid
                )";

                if (cmbSubject.SelectedIndex != -1)
                    q += " AND q.subject=@sub";

                if (!string.IsNullOrEmpty(search))
                    q += " AND (q.title LIKE @search OR q.subject LIKE @search)";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@sid", studentId);

                if (cmbSubject.SelectedIndex != -1)
                    cmd.Parameters.AddWithValue("@sub", cmbSubject.Text);

                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvQuizzes.DataSource = dt;
            }
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

        // ================= SUBMIT QUIZ =================
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvQuizzes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select quiz!");
                return;
            }

            int quizId = Convert.ToInt32(dgvQuizzes.SelectedRows[0].Cells["id"].Value);
            string filePath = txtFilePath.Text;

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Select file!");
                return;
            }

            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                // check already submitted
                string check = "SELECT COUNT(*) FROM quiz_submissions WHERE student_id=@sid AND quiz_id=@qid";
                MySqlCommand c = new MySqlCommand(check, con);
                c.Parameters.AddWithValue("@sid", studentId);
                c.Parameters.AddWithValue("@qid", quizId);

                if (Convert.ToInt32(c.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Already submitted!");
                    return;
                }

                // file save
                string folder = Path.Combine(Application.StartupPath, "QuizSubmissions");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string dest = Path.Combine(folder, Path.GetFileName(filePath));
                File.Copy(filePath, dest, true);

                // insert
                string q = @"INSERT INTO quiz_submissions 
                (student_id, quiz_id, file_path, marks, is_checked, submitted_at)
                VALUES (@sid,@qid,@file,NULL,0,NOW())";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@qid", quizId);
                cmd.Parameters.AddWithValue("@file", dest);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Quiz Submitted!");
                LoadQuizzes();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e) => LoadQuizzes(txtSearch.Text.Trim());
        private void BtnRefresh_Click(object sender, EventArgs e) { txtSearch.Clear(); cmbSubject.SelectedIndex = -1; LoadQuizzes(); }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new StudentDashboard(studentId).Show();
        }
    }
}
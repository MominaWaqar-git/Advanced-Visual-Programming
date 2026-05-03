using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;

namespace StudentMangementSystem_GUI
{
    public partial class MarkQuizForm : Form
    {
        int teacherId;
        string connStr = "server=localhost;user=root;password=;database=student_management_system";

        public MarkQuizForm(int id)
        {
            InitializeComponent();
            teacherId = id;

            LoadSubjects();
            LoadSubmissions();

            dgvQuizSub.CellContentClick += DgvQuizSub_CellContentClick;
            cmbSubject.SelectedIndexChanged += (s, e) => LoadSubmissions();
            btnSearch.Click += (s, e) => LoadSubmissions(txtSearch.Text.Trim());
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); cmbSubject.SelectedIndex = -1; LoadSubmissions(); };
        }

        // ================= SUBJECTS =================
        private void LoadSubjects()
        {
            cmbSubject.Items.Clear();

            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();
                string q = "SELECT DISTINCT subject FROM quizzes WHERE teacher_id=@tid";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@tid", teacherId);

                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    cmbSubject.Items.Add(dr["subject"].ToString());
            }
        }

        // ================= LOAD SUBMISSIONS =================
        private void LoadSubmissions(string search = "")
        {
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                string q = @"
SELECT 
    qs.id,
    st.name AS Student,
    q.title AS Quiz,
    q.subject,
    qs.file_path,
    IFNULL(qs.marks,0) AS Marks,

    CASE 
        WHEN qs.id IS NULL THEN '❌ Pending'
        WHEN qs.is_checked = 0 THEN '⏳ Submitted'
        ELSE '✔ Checked'
    END AS Status

FROM quiz_submissions qs

JOIN quizzes q 
    ON qs.quiz_id = q.id

JOIN students st 
    ON qs.student_id = st.id

JOIN teacher_student ts 
    ON ts.student_id = qs.student_id 
    AND ts.subject = q.subject

WHERE ts.teacher_id = @tid
";

                if (cmbSubject.SelectedIndex != -1)
                    q += " AND q.subject = @sub";

                if (!string.IsNullOrEmpty(search))
                    q += " AND (st.name LIKE @search OR q.title LIKE @search)";

                MySqlCommand cmd = new MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@tid", teacherId);

                if (cmbSubject.SelectedIndex != -1)
                    cmd.Parameters.AddWithValue("@sub", cmbSubject.Text);

                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);   

                dgvQuizSub.DataSource = dt;

                if (!dgvQuizSub.Columns.Contains("btnGrade"))
                {
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "btnGrade";
                    btn.HeaderText = "Action";
                    btn.Text = "Enter Marks";
                    btn.UseColumnTextForButtonValue = true;
                    dgvQuizSub.Columns.Add(btn);
                }
            }
        
        }

        // ================= GRADE QUIZ =================
        private void DgvQuizSub_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvQuizSub.Columns[e.ColumnIndex].Name == "btnGrade")
            {
                int id = Convert.ToInt32(dgvQuizSub.Rows[e.RowIndex].Cells["id"].Value);
                string student = dgvQuizSub.Rows[e.RowIndex].Cells["Student"].Value.ToString();
                string current = dgvQuizSub.Rows[e.RowIndex].Cells["marks"].Value.ToString();

                string input = Interaction.InputBox("Marks for " + student, "Grade Quiz", current);

                if (int.TryParse(input, out int marks))
                {
                    using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();
                        string q = "UPDATE quiz_submissions SET marks=@m, is_checked=1 WHERE id=@id";
                        MySqlCommand cmd = new MySqlCommand(q, con);
                        cmd.Parameters.AddWithValue("@m", marks);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Marks saved!");
                    LoadSubmissions();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

            TeacherDashboard teacherDashboard = new TeacherDashboard(teacherId);
            teacherDashboard.Show();
            this.Hide();
        }
    }
}
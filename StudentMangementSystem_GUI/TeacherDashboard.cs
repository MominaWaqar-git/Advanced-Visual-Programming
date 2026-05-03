using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class TeacherDashboard : Form
    {
        int teacherId; // logged-in teacher's ID
        string conn = "server=localhost;user=root;password=;database=student_management_system";

        public TeacherDashboard(int id)
        {
            InitializeComponent();
            teacherId = id;

            // Button events
            btnUploadLecture.Click += btnUploadLecture_Click;
            btnUploadAssignment.Click += btnUploadAssignment_Click;
            btnUploadQuiz.Click += btnUploadQuiz_Click;
            btnViewStudent.Click += btnViewStudent_Click;
            btnAnnouncements.Click += btnAnnouncements_Click;
            btnLogout.Click += btnLogout_Click;

            // Form load
            this.Load += TeacherDashboard_Load;
        }

        private void TeacherDashboard_Load(object sender, EventArgs e)
        {
            LoadCounts();
        }

        private void LoadCounts()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();

                    // ---------------- Lectures count ----------------
                    MySqlCommand cmdLecture = new MySqlCommand(
                        "SELECT COUNT(*) FROM lectures WHERE teacher_id=@tid", con);
                    cmdLecture.Parameters.AddWithValue("@tid", teacherId);
                    lblTotalLecture.Text = cmdLecture.ExecuteScalar().ToString();

                    // ---------------- Assignments count ----------------
                    MySqlCommand cmdAssignment = new MySqlCommand(
                        "SELECT COUNT(*) FROM assignments WHERE teacher_id=@tid", con);
                    cmdAssignment.Parameters.AddWithValue("@tid", teacherId);
                    lblUploadAssignments.Text = cmdAssignment.ExecuteScalar().ToString();

                    // ---------------- Quizzes count ----------------
                    MySqlCommand cmdQuiz = new MySqlCommand(
                        "SELECT COUNT(*) FROM quizzes WHERE teacher_id=@tid", con);
                    cmdQuiz.Parameters.AddWithValue("@tid", teacherId);
                    lblTotalQuizzes.Text = cmdQuiz.ExecuteScalar().ToString();

                    // ---------------- Students count ----------------
                    MySqlCommand cmdStudent = new MySqlCommand(
                        @"SELECT COUNT(DISTINCT s.id)
                          FROM students s
                          INNER JOIN teacher_student ts ON s.id = ts.student_id
                          WHERE ts.teacher_id=@tid", con);
                    cmdStudent.Parameters.AddWithValue("@tid", teacherId);
                    lblTotalTeachers.Text = cmdStudent.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading counts: " + ex.Message);
            }
        }

        private void btnUploadLecture_Click(object sender, EventArgs e)
        {
            UploadLectureForm upload = new UploadLectureForm(teacherId);
            upload.Show();
            this.Hide();
        }

        private void btnUploadAssignment_Click(object sender, EventArgs e)
        {
            UploadAssignmentForm upload = new UploadAssignmentForm(teacherId);
            upload.Show();
            this.Hide();
        }

        private void btnUploadQuiz_Click(object sender, EventArgs e)
        {
            UploadQuizForm upload = new UploadQuizForm(teacherId);
            upload.Show();
            this.Hide();
        }

        private void btnViewStudent_Click(object sender, EventArgs e)
        {
            ViewStudentsForm upload = new ViewStudentsForm(teacherId);
            upload.Show();
            this.Hide();
        }

        private void btnAnnouncements_Click(object sender, EventArgs e)
        {
            TeacherAnnouncementsForm upload = new TeacherAnnouncementsForm(teacherId);
            upload.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                TeacherLogin login = new TeacherLogin();
                login.Show();
                this.Hide();
            }
        }

        private void mark_assignment_Click(object sender, EventArgs e)
        {
            TeacherSubmissionsForm teacherSubmissions = new TeacherSubmissionsForm(teacherId);
            teacherSubmissions.Show();
            this.Hide();
        }

        private void m_quizzes_Click(object sender, EventArgs e)
        {
            MarkQuizForm markQuizForm = new MarkQuizForm(teacherId);
            markQuizForm.Show();
            this.Hide();
        }

        
    }
}
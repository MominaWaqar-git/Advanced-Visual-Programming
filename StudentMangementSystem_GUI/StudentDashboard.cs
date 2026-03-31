using System;
using System.Windows.Forms;
using StudentMangementSystem_GUI;

namespace StudentMangementSystem_GUI
{
    public partial class StudentDashboard : Form
    {
        int studentId;

        public StudentDashboard(int id)
        {
            InitializeComponent();
            studentId = id;

            btnLogout.Click += BtnLogout_Click;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Logout?", "Confirm", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                StudentLoginForm login = new StudentLoginForm();
                login.Show();
                this.Hide();
            }
        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnAssignment_Click(object sender, EventArgs e)
        {
            StudentAssignmentForm stu = new StudentAssignmentForm(studentId);
            stu.Show();
            this.Hide();
        }

        private void btnLecture_Click(object sender, EventArgs e)
        {
            StudentLectureForm stu = new StudentLectureForm();
            stu.Show();
            this.Hide();
        }

        private void btnAnnouncements_Click(object sender, EventArgs e)
        {
            StudentAnnouncementsForm stu = new StudentAnnouncementsForm(studentId);
            stu.Show();
            this.Hide();
        }
    }
}
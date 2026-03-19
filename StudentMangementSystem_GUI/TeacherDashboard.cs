using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentMangementSystem_GUI
{
    public partial class TeacherDashboard : Form
    {
        int teacherId; // logged-in teacher's ID

        public TeacherDashboard(int id)
        {
            InitializeComponent();
            teacherId = id;

            // Button events
            btnUploadLecture.Click += btnUploadLecture_Click;
            btnUploadAssignment.Click += btnUploadAssignment_Click;
            btnUploadQuiz.Click += btnUploadQuiz_Click;
            btnViewStudent.Click += btnViewStudent_Click;
        }
        private void TeacherDashboard_Load(object sender, EventArgs e)
        {

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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
         "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                TeacherLogin admin = new TeacherLogin();
                admin.Show();
                this.Hide();
            }
        }

        private void btnAnnouncements_Click(object sender, EventArgs e)
        {
            ViewStudentsForm upload = new ViewStudentsForm(teacherId);
            upload.Show();
            this.Hide();
        }
    }
}

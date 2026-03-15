using StudentMangementSystem_GUI;
using System;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            // agar load par kuch karna ho to yahan likho
        }
      

        private void btnAdmin_Click_Click(object sender, EventArgs e)
        {
            AdminLoginForm admin = new AdminLoginForm();
            admin.Show();
            this.Hide();
        }

        private void btnTeacher_Click_Click(object sender, EventArgs e)
        {
            TeacherLoginForm teacher = new TeacherLoginForm();
            teacher.Show();
            this.Hide();
        
        }

        private void btnStudent_Click_Click(object sender, EventArgs e)
        {
            StudentLoginForm student = new StudentLoginForm();
            student.Show();
            this.Hide();
        }

        private void btnExit_Click_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
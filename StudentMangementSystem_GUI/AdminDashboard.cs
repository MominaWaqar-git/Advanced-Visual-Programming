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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void total_teachers_Click(object sender, EventArgs e)
        {

        }

        private void add_teachers_Click(object sender, EventArgs e)
        {
            AddTeachers add = new AddTeachers();
            add.Show();
            this.Hide();
        }

        private void view_teachers_Click(object sender, EventArgs e)
        {
            ViewTeachers view = new ViewTeachers();
            view.Show();
            this.Hide();
        }

        private void add_students_Click(object sender, EventArgs e)
        {
            AddStudents add = new AddStudents();
            add.Show();
            this.Hide();
        }

        private void view_students_Click(object sender, EventArgs e)
        {
            ViewStudents view = new ViewStudents();
            view.Show();
            this.Hide();
        }

        private void assigned_teachers_Click(object sender, EventArgs e)
        {
            AssignedTeachers assigned = new AssignedTeachers();
            assigned.Show();
            this.Hide();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
         "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AdminLoginForm admin = new AdminLoginForm();
                admin.Show();
                this.Hide();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}

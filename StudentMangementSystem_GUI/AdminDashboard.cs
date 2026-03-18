using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;   // ✅ ADD
using System.Collections.Generic; // ✅ ADD

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

        // ================= LOAD =================
        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadCounts();  // ✅ IMPORTANT
        }

        // ================= COUNTS FUNCTION =================
        private void LoadCounts()
        {
            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                // ===== TOTAL TEACHERS =====
                MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM teachers", con);
                lblTotalTeachers.Text = cmd1.ExecuteScalar().ToString();

                // ===== TOTAL STUDENTS =====
                MySqlCommand cmd2 = new MySqlCommand("SELECT COUNT(*) FROM students", con);
                lblTotalStudents.Text = cmd2.ExecuteScalar().ToString();

                // ===== TOTAL SUBJECTS (comma split) =====
                MySqlCommand cmd3 = new MySqlCommand("SELECT subject FROM teachers", con);
                MySqlDataReader dr = cmd3.ExecuteReader();

                HashSet<string> subjects = new HashSet<string>();

                while (dr.Read())
                {
                    string subjectData = dr["subject"].ToString();
                    string[] subs = subjectData.Split(',');

                    foreach (string s in subs)
                    {
                        string clean = s.Trim();

                        if (clean != "")
                            subjects.Add(clean);
                    }
                }

                dr.Close();

                lblTotalSubjects.Text = subjects.Count.ToString();
            }
        }

        private void addAnnouncements_Click(object sender, EventArgs e)
        {
            AddAnnouncements add = new AddAnnouncements();
            add.Show();
            this.Hide();
        }
    }
}
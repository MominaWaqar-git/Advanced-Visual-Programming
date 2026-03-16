using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class AddTeachers : Form
    {
        public AddTeachers()
        {
            InitializeComponent();
        }

        private void AddTeachers_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void save_Click(object sender, EventArgs e)
        {
            string connStr = "server=localhost;user=root;password=;database=student_management_system";

            MySqlConnection con = new MySqlConnection(connStr);

            try
            {
                con.Open();

                string query = "INSERT INTO teachers(name,cnic,age,email,phone,subject,username,password) VALUES(@name,@cnic,@age,@email,@phone,@subject,@username,@password)";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@cnic", txtCNIC.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@subject", txtSubject.Text);
                cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Teacher Added Successfully!");

                txtName.Clear();
                txtCNIC.Clear();
                txtAge.Clear();
                txtEmail.Clear();
                txtPhoneNumber.Clear();
                txtSubject.Clear();
                txtUserName.Clear();
                txtPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            con.Close();
        }

        private void back_Click(object sender, EventArgs e)
        {
            AddTeachers add = new AddTeachers();
            add.Show();
            this.Hide();
        }
    }
}
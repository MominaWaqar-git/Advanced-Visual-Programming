using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace StudentMangementSystem_GUI
{
    public partial class AddTeachers : Form
    {
        public AddTeachers()
        {
            InitializeComponent();
        }

        private void AddTeachers_Load(object sender, EventArgs e) { }

        private void save_Click(object sender, EventArgs e)
        {
            // =============== VALIDATION ===============
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtCNIC.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(txtSubject.Text) ||
                string.IsNullOrWhiteSpace(txtUserName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("All fields are required!");
                return;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Invalid Email!");
                return;
            }

            if (!Regex.IsMatch(txtCNIC.Text, @"^\d{5}-\d{7}-\d$"))
            {
                MessageBox.Show("CNIC must be in the format 12345-1234567-1");
                return;
            }

            if (txtPhoneNumber.Text.Length < 10 || txtPhoneNumber.Text.Length > 15)
            {
                MessageBox.Show("Phone number must be 10-15 digits!");
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age < 18)
            {
                MessageBox.Show("Age must be a number and at least 18!");
                return;
            }

            // =============== INSERT INTO DATABASE ===============
            string connStr = "server=localhost;user=root;password=;database=student_management_system";
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                try
                {
                    con.Open();
                    string query = @"INSERT INTO teachers(name, cnic, age, email, phone, subject, username, address) 
                                     VALUES(@name,@cnic,@age,@email,@phone,@subject,@username,@address)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@cnic", txtCNIC.Text);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("@subject", txtSubject.Text);
                    cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Added Successfully!");

                    // Clear all fields
                    txtName.Clear(); txtCNIC.Clear(); txtAge.Clear(); txtEmail.Clear();
                    txtPhoneNumber.Clear(); txtSubject.Clear(); txtUserName.Clear(); txtAddress.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            AdminDashboard add = new AdminDashboard();
            add.Show();
            this.Hide();
        }
    }
}
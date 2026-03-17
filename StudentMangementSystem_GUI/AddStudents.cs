using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class AddStudents : Form
    {
        public AddStudents()
        {
            InitializeComponent();

            // 🔥 Event fix
            save.Click += save_Click;
        }

        private void save_Click(object sender, EventArgs e)
        {
            // ========= VALIDATION =========
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtRegNo.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(txtCourse.Text) ||
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

            if (!Regex.IsMatch(txtRegNo.Text, @"^[A-Za-z0-9\-]+$"))
            {
                MessageBox.Show("RegNo must be alphanumeric!");
                return;
            }

            if (txtPhoneNumber.Text.Length < 10 || txtPhoneNumber.Text.Length > 15)
            {
                MessageBox.Show("Phone must be 10-15 digits!");
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age < 5)
            {
                MessageBox.Show("Age must be >= 5");
                return;
            }

            // ========= DATABASE =========
            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                // Check duplicate RegNo
                MySqlCommand check = new MySqlCommand("SELECT COUNT(*) FROM students WHERE regno=@r", con);
                check.Parameters.AddWithValue("@r", txtRegNo.Text);

                int count = Convert.ToInt32(check.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("RegNo already exists!");
                    return;
                }

                string q = @"INSERT INTO students(name, regno, age, email, phone, course, username, address)
                             VALUES(@name,@regno,@age,@email,@phone,@course,@username,@address)";

                MySqlCommand cmd = new MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@regno", txtRegNo.Text);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@course", txtCourse.Text);
                cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Student Added!");

                // Clear fields
                txtName.Clear();
                txtRegNo.Clear();
                txtAge.Clear();
                txtEmail.Clear();
                txtPhoneNumber.Clear();
                txtCourse.Clear();
                txtUserName.Clear();
                txtAddress.Clear();
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            new AdminDashboard().Show();
            this.Close();
        }
    }
}
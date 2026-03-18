using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class EditStudent : Form
    {
        int id;

        public EditStudent(int sid)
        {
            InitializeComponent();
            id = sid;

            // 🔥 EVENT FIX
            btnUpdate.Click += btnUpdate_Click;
        }

        private void EditStudent_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM students WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtName.Text = dr["name"].ToString();
                    txtRegNo.Text = dr["regno"].ToString();
                    txtAge.Text = dr["age"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    txtPhoneNumber.Text = dr["phone"].ToString();
                    txtCourse.Text = dr["course"].ToString(); // comma separated
                    txtUserName.Text = dr["username"].ToString();
                    txtAddress.Text = dr["address"].ToString();

                    txtUserName.ReadOnly = true;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                MessageBox.Show("All fields required!");
                return;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Invalid Email!");
                return;
            }

            if (!Regex.IsMatch(txtRegNo.Text, @"^[A-Za-z0-9\-]+$"))
            {
                MessageBox.Show("Invalid RegNo!");
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

            // ========= UPDATE =========
            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string q = @"UPDATE students SET name=@name, regno=@regno, age=@age,
                             email=@email, phone=@phone, course=@course,
                             address=@address WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@regno", txtRegNo.Text);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@course", txtCourse.Text); // comma separated
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Student Updated!");
                this.Close();
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            new AdminDashboard().ShowDialog();
            this.Close();
        }
    }
}
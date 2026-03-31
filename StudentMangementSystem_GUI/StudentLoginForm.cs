using MySql.Data.MySqlClient;
using StudentManagementSystem;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace StudentMangementSystem_GUI
{
    public partial class StudentLoginForm : Form
    {
        string conn = "server=localhost;user=root;password=;database=student_management_system";

        public StudentLoginForm()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsernameEmail.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user))
            {
                lblMessage.Text = "Enter username or email.";
                return;
            }

            if (string.IsNullOrEmpty(pass))
            {
                lblMessage.Text = "Enter password.";
                return;
            }

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string query = "SELECT id, password FROM students WHERE username=@user OR email=@user LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", user);

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int id = Convert.ToInt32(dr["id"]);
                    string savedPass = dr["password"].ToString();
                    dr.Close();

                    // ===== FIRST TIME PASSWORD SET =====
                    if (string.IsNullOrEmpty(savedPass))
                    {
                        string hashed = HashPassword(pass);

                        MySqlCommand update = new MySqlCommand(
                            "UPDATE students SET password=@pass WHERE id=@id", con);
                        update.Parameters.AddWithValue("@pass", hashed);
                        update.Parameters.AddWithValue("@id", id);
                        update.ExecuteNonQuery();

                        MessageBox.Show("Password set successfully! Please login again.");
                        txtPassword.Clear();
                        return;
                    }

                    // ===== NORMAL LOGIN =====
                    if (VerifyPassword(pass, savedPass))
                    {
                        MessageBox.Show("Login successful!");

                        StudentDashboard sd = new StudentDashboard(id);
                        sd.Show();
                        this.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Incorrect password.";
                    }
                }
                else
                {
                    lblMessage.Text = "User not found.";
                }
            }
        }

        // ===== HASH PASSWORD =====
        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                string salted = "student123" + password; // simple salt
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(salted));
                return Convert.ToBase64String(bytes);
            }
        }

        // ===== VERIFY PASSWORD =====
        private bool VerifyPassword(string entered, string stored)
        {
            return HashPassword(entered) == stored;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            WelcomeForm welcome = new WelcomeForm();
            welcome.Show();
            this.Hide();
        }
    }
}
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class EditTeacher : Form
    {
        int id;

        public EditTeacher(int tid)
        {
            InitializeComponent();
            id = tid;

            // 🔥 Event fix
            btnUpdate.Click += btnUpdate_Click;
        }

        private void EditTeacher_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string conn = "server=localhost;user id=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM teachers WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtName.Text = dr["name"].ToString();
                    txtCNIC.Text = dr["cnic"].ToString();
                    txtAge.Text = dr["age"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    txtPhoneNumber.Text = dr["phone"].ToString();
                    txtSubject.Text = dr["subject"].ToString();
                    txtAddress.Text = dr["address"].ToString();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // ========= VALIDATION =========
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtCNIC.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(txtSubject.Text) ||
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
                MessageBox.Show("CNIC format: 12345-1234567-1");
                return;
            }

            if (txtPhoneNumber.Text.Length < 10 || txtPhoneNumber.Text.Length > 15)
            {
                MessageBox.Show("Phone must be 10-15 digits!");
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age < 18)
            {
                MessageBox.Show("Age must be >= 18");
                return;
            }

            // ========= UPDATE =========
            string conn = "server=localhost;user id=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string q = @"UPDATE teachers SET name=@name, cnic=@cnic, age=@age,
                             email=@email, phone=@phone, subject=@subject, address=@address
                             WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@cnic", txtCNIC.Text);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@subject", txtSubject.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Teacher Updated!");
                this.Close();
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            ViewTeachers view = new ViewTeachers();
            view.ShowDialog();
            this.Close();
        }
    }
}
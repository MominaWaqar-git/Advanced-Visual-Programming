using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class ViewStudents : Form
    {
        public ViewStudents()
        {
            InitializeComponent();

           
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }

        private void ViewStudents_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string q = @"SELECT id AS ID, name AS Name, regno AS RegNo, age AS Age,
                             email AS Email, phone AS Phone, course AS Course,
                             username AS Username, address AS Address FROM students";

                MySqlDataAdapter da = new MySqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                if (!dataGridView1.Columns.Contains("btnEdit"))
                {
                    DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
                    edit.Name = "btnEdit";
                    edit.Text = "Edit";
                    edit.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(edit);
                }

                if (!dataGridView1.Columns.Contains("btnDelete"))
                {
                    DataGridViewButtonColumn del = new DataGridViewButtonColumn();
                    del.Name = "btnDelete";
                    del.Text = "Delete";
                    del.UseColumnTextForButtonValue = true;
                    dataGridView1.Columns.Add(del);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                EditStudent obj = new EditStudent(id);
                obj.ShowDialog();
                LoadStudents();
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DialogResult r = MessageBox.Show("Delete student?", "Confirm", MessageBoxButtons.YesNo);

                if (r == DialogResult.Yes)
                {
                    DeleteStudent(id);
                    LoadStudents();
                }
            }
        }

        private void DeleteStudent(int id)
        {
            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM students WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Student Deleted!");
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                e.CellStyle.BackColor = Color.Green;
                e.CellStyle.ForeColor = Color.White;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                e.CellStyle.BackColor = Color.Red;
                e.CellStyle.ForeColor = Color.White;
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            new AdminDashboard().Show();
            this.Close();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string q = @"SELECT id AS ID, name AS Name, regno AS RegNo, age AS Age,
                     email AS Email, phone AS Phone, course AS Course,
                     username AS Username, address AS Address 
                     FROM students
                     WHERE name LIKE @key OR regno LIKE @key OR course LIKE @key";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No student found!");
                }
            }
        }
    }
}
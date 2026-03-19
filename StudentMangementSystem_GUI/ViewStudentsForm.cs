using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class ViewStudentsForm : Form
    {
        string conn = "server=localhost;user=root;password=;database=student_management_system";
        int teacherId;

        public ViewStudentsForm(int tid)
        {
            InitializeComponent();
            teacherId = tid;

            this.Load += ViewStudentsForm_Load;
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnBack.Click += BtnBack_Click;

            dgvStudents.CellContentClick += DgvStudents_CellContentClick;
            dgvStudents.CellFormatting += DgvStudents_CellFormatting;
        }

        private void ViewStudentsForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents(string keyword = "")
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();

                    // Query: join students with teacher_student mapping table
                    string query = @"
                        SELECT s.id AS ID, s.name AS Name, s.regno AS RegNo, s.age AS Age,
                               s.email AS Email, s.phone AS Phone, s.course AS Course,
                               s.username AS Username, s.address AS Address
                        FROM students s
                        INNER JOIN teacher_student ts ON s.id = ts.student_id
                        WHERE ts.teacher_id = @tid";

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " AND (s.name LIKE @key OR s.regno LIKE @key OR s.course LIKE @key OR s.email LIKE @key)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@tid", teacherId);

                        if (!string.IsNullOrEmpty(keyword))
                            cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        dgvStudents.DataSource = dt;
                        dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Add Edit/Delete buttons if not already added
                        if (!dgvStudents.Columns.Contains("btnEdit"))
                        {
                            DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
                            edit.Name = "btnEdit";
                            edit.Text = "Edit";
                            edit.UseColumnTextForButtonValue = true;
                            dgvStudents.Columns.Add(edit);
                        }

                        if (!dgvStudents.Columns.Contains("btnDelete"))
                        {
                            DataGridViewButtonColumn del = new DataGridViewButtonColumn();
                            del.Name = "btnDelete";
                            del.Text = "Delete";
                            del.UseColumnTextForButtonValue = true;
                            dgvStudents.Columns.Add(del);
                        }

                        lblMessage.Text = (dt.Rows.Count == 0) ? "No student found." : "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            LoadStudents(keyword);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadStudents();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            TeacherDashboard dashboard = new TeacherDashboard(teacherId);
            dashboard.Show();
            this.Close();
        }

        private void DgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = 0;
            try
            {
                id = Convert.ToInt32(dgvStudents.Rows[e.RowIndex].Cells["ID"].Value);
            }
            catch
            {
                MessageBox.Show("Cannot read student ID.");
                return;
            }

            if (dgvStudents.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                EditStudent obj = new EditStudent(id);
                obj.ShowDialog();
                LoadStudents(txtSearch.Text.Trim());
            }

            if (dgvStudents.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DialogResult r = MessageBox.Show("Delete student?", "Confirm", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    DeleteStudent(id);
                    LoadStudents(txtSearch.Text.Trim());
                }
            }
        }

        private void DeleteStudent(int id)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();

                    // Only delete if the student belongs to this teacher
                    MySqlCommand cmd = new MySqlCommand(
                        @"DELETE s 
                          FROM students s
                          INNER JOIN teacher_student ts ON s.id = ts.student_id
                          WHERE s.id = @id AND ts.teacher_id = @tid", con);

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@tid", teacherId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Student Deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting student: " + ex.Message);
            }
        }

        private void DgvStudents_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvStudents.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                e.CellStyle.BackColor = Color.Green;
                e.CellStyle.ForeColor = Color.White;
            }

            if (dgvStudents.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                e.CellStyle.BackColor = Color.Red;
                e.CellStyle.ForeColor = Color.White;
            }
        }
    }
}
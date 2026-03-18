using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class AssignedTeachers : Form
    {
        public AssignedTeachers()
        {
            InitializeComponent();

            // EVENTS
            this.Load += AssignedTeachers_Load;
            cmbSubject.SelectedIndexChanged += cmbSubject_SelectedIndexChanged;
            btnAssign.Click += btnAssign_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnSearch.Click += btnSearch_Click;
            dgvAssigned.CellContentClick += dgvAssigned_CellContentClick;
            back.Click += back_Click;
        }

        // ================= LOAD =================
        private void AssignedTeachers_Load(object sender, EventArgs e)
        {
            LoadSubjects();
            LoadStudents();
            LoadAssignedData();
        }

        // ================= LOAD SUBJECT =================
        private void LoadSubjects()
        {
            cmbSubject.Items.Clear();

            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT subject FROM teachers", con);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmbSubject.Items.Add(dr["subject"].ToString());
                }
            }
        }

        // ================= SUBJECT CHANGE =================
        private void cmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTeacher.Items.Clear();

            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string q = "SELECT id, name FROM teachers WHERE subject=@sub";
                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@sub", cmbSubject.Text);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmbTeacher.Items.Add(new ComboItem(dr["name"].ToString(), dr["id"].ToString()));
                }
            }
        }

        // ================= LOAD STUDENTS =================
        private void LoadStudents()
        {
            cmbStudent.Items.Clear();

            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT id, name FROM students", con);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmbStudent.Items.Add(new ComboItem(dr["name"].ToString(), dr["id"].ToString()));
                }
            }
        }

        // ================= ASSIGN =================
        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (cmbSubject.Text == "" || cmbTeacher.SelectedItem == null || cmbStudent.SelectedItem == null)
            {
                MessageBox.Show("Select all fields!");
                return;
            }

            ComboItem teacher = (ComboItem)cmbTeacher.SelectedItem;
            ComboItem student = (ComboItem)cmbStudent.SelectedItem;

            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string check = @"SELECT COUNT(*) FROM teacher_student 
                                 WHERE teacher_id=@t AND student_id=@s AND subject=@sub";

                MySqlCommand checkCmd = new MySqlCommand(check, con);
                checkCmd.Parameters.AddWithValue("@t", teacher.Value);
                checkCmd.Parameters.AddWithValue("@s", student.Value);
                checkCmd.Parameters.AddWithValue("@sub", cmbSubject.Text);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Already Assigned!");
                    return;
                }

                string q = @"INSERT INTO teacher_student (teacher_id, student_id, subject)
                             VALUES (@t,@s,@sub)";

                MySqlCommand cmd = new MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@t", teacher.Value);
                cmd.Parameters.AddWithValue("@s", student.Value);
                cmd.Parameters.AddWithValue("@sub", cmbSubject.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Assigned Successfully!");
                LoadAssignedData();
            }
        }

        // ================= LOAD GRID =================
        private void LoadAssignedData()
        {
            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string q = @"SELECT ts.id,
                             t.name AS Teacher,
                             s.name AS Student,
                             ts.subject AS Subject
                             FROM teacher_student ts
                             JOIN teachers t ON ts.teacher_id = t.id
                             JOIN students s ON ts.student_id = s.id";

                MySqlDataAdapter da = new MySqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAssigned.DataSource = dt;

                if (!dgvAssigned.Columns.Contains("btnDelete"))
                {
                    DataGridViewButtonColumn del = new DataGridViewButtonColumn();
                    del.Name = "btnDelete";
                    del.Text = "Delete";
                    del.UseColumnTextForButtonValue = true;
                    dgvAssigned.Columns.Add(del);
                }
            }
        }

        // ================= SEARCH =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            string conn = "server=localhost;user=root;password=;database=student_management_system";

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string q = @"SELECT ts.id,
                             t.name AS Teacher,
                             s.name AS Student,
                             ts.subject AS Subject
                             FROM teacher_student ts
                             JOIN teachers t ON ts.teacher_id = t.id
                             JOIN students s ON ts.student_id = s.id
                             WHERE t.name LIKE @key 
                             OR s.name LIKE @key 
                             OR ts.subject LIKE @key";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAssigned.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No record found!");
                }
            }
        }

        // ================= DELETE =================
        private void dgvAssigned_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvAssigned.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                int id = Convert.ToInt32(dgvAssigned.Rows[e.RowIndex].Cells["id"].Value);

                DialogResult r = MessageBox.Show("Delete assignment?", "Confirm", MessageBoxButtons.YesNo);

                if (r == DialogResult.Yes)
                {
                    string conn = "server=localhost;user=root;password=;database=student_management_system";

                    using (MySqlConnection con = new MySqlConnection(conn))
                    {
                        con.Open();

                        MySqlCommand cmd = new MySqlCommand("DELETE FROM teacher_student WHERE id=@id", con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Deleted!");
                        LoadAssignedData();
                    }
                }
            }
        }

        // ================= REFRESH =================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAssignedData();
        }

        // ================= BACK =================
        private void back_Click(object sender, EventArgs e)
        {
            new AdminDashboard().Show();
            this.Close();
        }
    }

    // ================= COMBO CLASS =================
    public class ComboItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public ComboItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
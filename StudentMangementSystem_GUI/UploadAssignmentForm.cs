using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace StudentMangementSystem_GUI
{
    public partial class UploadAssignmentForm : Form
    {
        int teacherId;
        string conn = "server=localhost;user=root;password=;database=student_management_system";

        public UploadAssignmentForm(int teacherId)
        {
            InitializeComponent();
            this.teacherId = teacherId;

            this.Load += UploadAssignmentForm_Load;
            btnBrowse.Click += BtnBrowse_Click;
            btnUpload.Click += BtnUpload_Click;
            dgvAssignments.CellContentClick += DgvAssignments_CellContentClick;
        }

        private void UploadAssignmentForm_Load(object sender, EventArgs e)
        {
            LoadSubjects();
            LoadAssignments();
        }

        // Load subjects assigned to teacher
        private void LoadSubjects()
        {
            cmbSubject.Items.Clear();

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                string query = "SELECT subject FROM teachers WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", teacherId);

                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string[] subjects = dr["subject"].ToString().Split(',');
                    foreach (string s in subjects)
                    {
                        string clean = s.Trim();
                        if (!string.IsNullOrEmpty(clean))
                            cmbSubject.Items.Add(clean);
                    }
                }
            }
        }

        // Browse assignment file
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF, Word, PPT|*.pdf;*.doc;*.docx;*.ppt;*.pptx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        // Upload assignment
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            string subject = cmbSubject.Text.Trim();
            string title = txtTitle.Text.Trim();
            string desc = txtDescription.Text.Trim();
            string filePath = txtFilePath.Text.Trim();

            if (string.IsNullOrEmpty(subject))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Select a subject.";
                return;
            }
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Select a valid file.";
                return;
            }

            try
            {
                string destDir = Path.Combine(Application.StartupPath, "Assignments");
                if (!Directory.Exists(destDir))
                    Directory.CreateDirectory(destDir);

                string destFile = Path.Combine(destDir, Path.GetFileName(filePath));
                File.Copy(filePath, destFile, true);

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    string query = @"INSERT INTO assignments (teacher_id, subject, title, description, file_path)
                                     VALUES (@tid, @sub, @title, @desc, @path)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@tid", teacherId);
                    cmd.Parameters.AddWithValue("@sub", subject);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Parameters.AddWithValue("@path", destFile);
                    cmd.ExecuteNonQuery();
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Assignment uploaded successfully!";
                txtTitle.Clear();
                txtDescription.Clear();
                txtFilePath.Clear();

                LoadAssignments();
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        // Load assignments in DataGridView
        private void LoadAssignments()
        {
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                string query = "SELECT id, subject, title, description, file_path, uploaded_at FROM assignments WHERE teacher_id=@tid ORDER BY uploaded_at DESC";
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@tid", teacherId);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAssignments.DataSource = dt;

                if (dgvAssignments.Columns.Contains("file_path"))
                    dgvAssignments.Columns["file_path"].Visible = false;

                if (!dgvAssignments.Columns.Contains("btnDelete"))
                {
                    DataGridViewButtonColumn del = new DataGridViewButtonColumn();
                    del.Name = "btnDelete";
                    del.HeaderText = "Delete";
                    del.Text = "Delete";
                    del.UseColumnTextForButtonValue = true;
                    dgvAssignments.Columns.Add(del);
                }

                dgvAssignments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // Handle Delete button
        private void DgvAssignments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvAssignments.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                int id = Convert.ToInt32(dgvAssignments.Rows[e.RowIndex].Cells["id"].Value);
                string path = dgvAssignments.Rows[e.RowIndex].Cells["file_path"].Value.ToString();

                if (MessageBox.Show("Delete this assignment?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        if (File.Exists(path))
                            File.Delete(path);

                        using (MySqlConnection con = new MySqlConnection(conn))
                        {
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand("DELETE FROM assignments WHERE id=@id", con);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Assignment deleted!");
                        LoadAssignments();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting assignment: " + ex.Message);
                    }
                }
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            TeacherDashboard upload = new TeacherDashboard(teacherId);
            upload.Show();
            this.Hide();
        }
    }
}
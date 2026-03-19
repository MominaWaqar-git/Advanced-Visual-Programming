using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace StudentMangementSystem_GUI
{
    public partial class UploadLectureForm : Form
    {
        int teacherId; // Pass from login
        string conn = "server=localhost;user=root;password=;database=student_management_system";

        public UploadLectureForm(int teacherId)
        {
            InitializeComponent();
            this.teacherId = teacherId;

            this.Load += UploadLectureForm_Load;
            btnBrowse.Click += BtnBrowse_Click;
            btnUpload.Click += BtnUpload_Click;
            dgvLectures.CellContentClick += DgvLectures_CellContentClick;
        }

        private void UploadLectureForm_Load(object sender, EventArgs e)
        {
            LoadSubjects();
            LoadLectures();
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

        // Browse file
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF, PPT, Word, Video|*.pdf;*.ppt;*.pptx;*.doc;*.docx;*.mp4;*.avi";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        // Upload lecture
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            string subject = cmbSubject.Text.Trim();
            string title = txtLectureTitle.Text.Trim();
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
                lblMessage.Text = "Select a valid file to upload.";
                return;
            }

            try
            {
                string destDir = Path.Combine(Application.StartupPath, "Lectures");
                if (!Directory.Exists(destDir))
                    Directory.CreateDirectory(destDir);

                string destFile = Path.Combine(destDir, Path.GetFileName(filePath));
                File.Copy(filePath, destFile, true);

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    string query = @"INSERT INTO lectures (teacher_id, subject, title, description, file_path) 
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
                lblMessage.Text = "Lecture uploaded successfully!";
                txtLectureTitle.Clear();
                txtDescription.Clear();
                txtFilePath.Clear();

                LoadLectures();
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        // Load lectures in DataGridView
        private void LoadLectures()
        {
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                string query = "SELECT id, subject, title, description, file_path, uploaded_at FROM lectures WHERE teacher_id=@tid ORDER BY uploaded_at DESC";
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@tid", teacherId);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvLectures.DataSource = dt;

                // Hide file path column (optional)
                if (dgvLectures.Columns.Contains("file_path"))
                    dgvLectures.Columns["file_path"].Visible = false;

                // Add Delete button column if not exist
                if (!dgvLectures.Columns.Contains("btnDelete"))
                {
                    DataGridViewButtonColumn del = new DataGridViewButtonColumn();
                    del.Name = "btnDelete";
                    del.HeaderText = "Delete";
                    del.Text = "Delete";
                    del.UseColumnTextForButtonValue = true;
                    dgvLectures.Columns.Add(del);
                }

                // Optional: Auto-size columns
                dgvLectures.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // Handle delete button click
        private void DgvLectures_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvLectures.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                int id = Convert.ToInt32(dgvLectures.Rows[e.RowIndex].Cells["id"].Value);
                string path = dgvLectures.Rows[e.RowIndex].Cells["file_path"].Value.ToString();

                if (MessageBox.Show("Delete this lecture?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        // Delete file
                        if (File.Exists(path))
                            File.Delete(path);

                        // Delete from DB
                        using (MySqlConnection con = new MySqlConnection(conn))
                        {
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand("DELETE FROM lectures WHERE id=@id", con);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Lecture deleted!");
                        LoadLectures();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting lecture: " + ex.Message);
                    }
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            TeacherDashboard upload = new TeacherDashboard(teacherId);
            upload.Show();
            this.Hide();
        }
    }
}
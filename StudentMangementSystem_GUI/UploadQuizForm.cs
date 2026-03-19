using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class UploadQuizForm : Form
    {
        int teacherId;
        string conn = "server=localhost;user=root;password=;database=student_management_system";

        public UploadQuizForm(int teacherId)
        {
            InitializeComponent();
            this.teacherId = teacherId;

            this.Load += UploadQuizForm_Load;
            btnBrowse.Click += BtnBrowse_Click;
            btnUpload.Click += BtnUpload_Click;
            dgvQuizzes.CellContentClick += DgvQuizzes_CellContentClick;
        }

        private void UploadQuizForm_Load(object sender, EventArgs e)
        {
            LoadSubjects();
            LoadQuizzes();
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

        // Browse optional quiz file
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF, Word, PPT|*.pdf;*.doc;*.docx;*.ppt;*.pptx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        // Upload quiz
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            string subject = cmbSubject.Text.Trim();
            string title = txtTitle.Text.Trim();
            string desc = txtDescription.Text.Trim();
            string filePath = txtFilePath.Text.Trim();
            string destFile = null;

            if (string.IsNullOrEmpty(subject))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Select a subject.";
                return;
            }

            try
            {
                // Save file if selected
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    string destDir = Path.Combine(Application.StartupPath, "Quizzes");
                    if (!Directory.Exists(destDir))
                        Directory.CreateDirectory(destDir);

                    destFile = Path.Combine(destDir, Path.GetFileName(filePath));
                    File.Copy(filePath, destFile, true);
                }

                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    string query = @"INSERT INTO quizzes (teacher_id, subject, title, description, file_path)
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
                lblMessage.Text = "Quiz uploaded successfully!";
                txtTitle.Clear();
                txtDescription.Clear();
                txtFilePath.Clear();

                LoadQuizzes();
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        // Load quizzes in DataGridView
        private void LoadQuizzes()
        {
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                string query = "SELECT id, subject, title, description, file_path, uploaded_at FROM quizzes WHERE teacher_id=@tid ORDER BY uploaded_at DESC";
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@tid", teacherId);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvQuizzes.DataSource = dt;

                if (dgvQuizzes.Columns.Contains("file_path"))
                    dgvQuizzes.Columns["file_path"].Visible = false;

                if (!dgvQuizzes.Columns.Contains("btnDelete"))
                {
                    DataGridViewButtonColumn del = new DataGridViewButtonColumn();
                    del.Name = "btnDelete";
                    del.HeaderText = "Delete";
                    del.Text = "Delete";
                    del.UseColumnTextForButtonValue = true;
                    dgvQuizzes.Columns.Add(del);
                }

                dgvQuizzes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // Handle Delete button
        private void DgvQuizzes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvQuizzes.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                int id = Convert.ToInt32(dgvQuizzes.Rows[e.RowIndex].Cells["id"].Value);
                string path = dgvQuizzes.Rows[e.RowIndex].Cells["file_path"].Value.ToString();

                if (MessageBox.Show("Delete this quiz?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                            File.Delete(path);

                        using (MySqlConnection con = new MySqlConnection(conn))
                        {
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand("DELETE FROM quizzes WHERE id=@id", con);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Quiz deleted!");
                        LoadQuizzes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting quiz: " + ex.Message);
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
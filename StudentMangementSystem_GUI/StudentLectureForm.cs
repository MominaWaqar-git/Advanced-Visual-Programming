using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class StudentLectureForm : Form
    {
        string connStr = "server=localhost;user=root;password=;database=student_management_system";

        public StudentLectureForm()
        {
            InitializeComponent();

            dgvLectures.CellClick += dgvLectures_CellClick;
            cmbSubject.SelectedIndexChanged += cmbSubject_SelectedIndexChanged;

            LoadSubjects();
            LoadLectures();
        }

        // 🔹 LOAD SUBJECTS INTO COMBOBOX
        private void LoadSubjects()
        {
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                string query = "SELECT DISTINCT subject FROM lectures";

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader dr = cmd.ExecuteReader();

                cmbSubject.Items.Clear();
                cmbSubject.Items.Add("All");

                while (dr.Read())
                {
                    cmbSubject.Items.Add(dr["subject"].ToString());
                }

                cmbSubject.SelectedIndex = 0;
            }
        }

        // 🔹 LOAD LECTURES
        private void LoadLectures(string subject = "", string search = "")
        {
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();

                string query = @"
                SELECT id, title, subject, description, file_path
                FROM lectures
                WHERE 1=1";

                if (!string.IsNullOrEmpty(subject) && subject != "All")
                {
                    query += " AND LOWER(subject) = LOWER(@subject)";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += @" AND (LOWER(title) LIKE LOWER(@search) 
                 OR LOWER(subject) LIKE LOWER(@search))";
                }

                MySqlCommand cmd = new MySqlCommand(query, con);

                if (!string.IsNullOrEmpty(subject) && subject != "All")
                    cmd.Parameters.AddWithValue("@subject", subject);

                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvLectures.DataSource = dt;

                AddDownloadButton();
            }
        }

        // 🔹 DOWNLOAD BUTTON COLUMN
        private void AddDownloadButton()
        {
            if (!dgvLectures.Columns.Contains("Download"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "Download";
                btn.HeaderText = "Action";
                btn.Text = "Download";
                btn.UseColumnTextForButtonValue = true;
                dgvLectures.Columns.Add(btn);
            }
        }

        // 🔍 SEARCH
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string subject = cmbSubject.SelectedItem?.ToString();
            string search = txtSearch.Text.Trim();

            LoadLectures(subject, search);
        }

        // 🔄 REFRESH
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbSubject.SelectedIndex = 0;
            LoadLectures();
        }

        // 🔙 BACK
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // 🔽 SUBJECT FILTER
        private void cmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            string subject = cmbSubject.SelectedItem.ToString();
            LoadLectures(subject);
        }

        // 📥 DOWNLOAD
        private void dgvLectures_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvLectures.Columns[e.ColumnIndex].Name == "Download")
            {
                try
                {
                    var value = dgvLectures.Rows[e.RowIndex].Cells["file_path"].Value;

                    if (value == null)
                    {
                        MessageBox.Show("No file path found!");
                        return;
                    }

                    string filePath = value.ToString();

                    if (!File.Exists(filePath))
                    {
                        MessageBox.Show("File not found!");
                        return;
                    }

                    SaveFileDialog save = new SaveFileDialog();
                    save.FileName = Path.GetFileName(filePath);

                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(filePath, save.FileName, true);
                        MessageBox.Show("Download Successful!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
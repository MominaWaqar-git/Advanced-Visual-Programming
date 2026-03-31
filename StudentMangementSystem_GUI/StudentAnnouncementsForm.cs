using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class StudentAnnouncementsForm : Form
    {
        int studentId;
        string conn = "server=localhost;user=root;password=;database=student_management_system";

        public StudentAnnouncementsForm(int id)
        {
            InitializeComponent();
            studentId = id;

            // EVENTS
            this.Load += StudentAnnouncementsForm_Load;
            btnSearch.Click += btnSearch_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
            btnRefresh.Click += btnRefresh_Click;
            btnBack.Click += btnBack_Click;
        }

        // ================= LOAD =================
        private void StudentAnnouncementsForm_Load(object sender, EventArgs e)
        {
            // ComboBox filter
            if (cmbFilter.Items.Count == 0)
            {
                cmbFilter.Items.Add("All");
                cmbFilter.Items.Add("Admin");
                cmbFilter.Items.Add("Teacher");
            }
            cmbFilter.SelectedIndex = 0;

            LoadAnnouncements();
        }

        // ================= LOAD ANNOUNCEMENTS =================
        private void LoadAnnouncements(string filter = "All", string keyword = "")
        {
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string query = @"
                SELECT 
                    a.message,
                    a.audience,
                    a.created_at,
                    IF(a.created_by='admin','Admin', t.name) AS PostedBy
                FROM announcements a
                LEFT JOIN teachers t ON a.teacher_id = t.id
                WHERE (a.audience='student' OR a.audience='both')";

                // Filter by Admin / Teacher
                if (filter == "Admin")
                    query += " AND a.created_by='admin'";
                else if (filter == "Teacher")
                    query += " AND a.created_by='teacher'";

                // Search filter
                if (!string.IsNullOrEmpty(keyword))
                    query += " AND (LOWER(a.message) LIKE @key OR LOWER(audience) LIKE @key OR LOWER(t.name) LIKE @key)";

                query += " ORDER BY a.id DESC";

                MySqlCommand cmd = new MySqlCommand(query, con);
                if (!string.IsNullOrEmpty(keyword))
                    cmd.Parameters.AddWithValue("@key", "%" + keyword.ToLower() + "%");

                DataTable dt = new DataTable();
                new MySqlDataAdapter(cmd).Fill(dt);

                dgvAnnouncements.DataSource = dt;

                if (dt.Rows.Count == 0 && !string.IsNullOrEmpty(keyword))
                    MessageBox.Show("No announcement found!");
            }
        }

        // ================= SEARCH BUTTON =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filter = cmbFilter.SelectedItem?.ToString() ?? "All";
            string keyword = txtSearch.Text.Trim();
            LoadAnnouncements(filter, keyword);
        }

        // ================= LIVE SEARCH =================
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        // ================= REFRESH =================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbFilter.SelectedIndex = 0;
            LoadAnnouncements();
        }

        // ================= BACK =================
        private void btnBack_Click(object sender, EventArgs e)
        {
            StudentDashboard sd = new StudentDashboard(studentId);
            sd.Show();
            this.Close();
        }
    }
}
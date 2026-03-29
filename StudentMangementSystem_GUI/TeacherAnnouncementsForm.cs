using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class TeacherAnnouncementsForm : Form
    {
        int teacherId;
        string conn = "server=localhost;user=root;password=;database=student_management_system";

        public TeacherAnnouncementsForm(int id)
        {
            InitializeComponent();
            teacherId = id;

            // EVENTS
            this.Load += TeacherAnnouncementsForm_Load;
            btnPost.Click += btnPost_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnSearch.Click += btnSearch_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
            btnBack.Click += btnBack_Click;
        }

        // ================= LOAD =================
        private void TeacherAnnouncementsForm_Load(object sender, EventArgs e)
        {
            LoadAnnouncements();
        }

        // ================= LOAD ADMIN ANNOUNCEMENTS =================
        // ================= LOAD ANNOUNCEMENTS =================
        private void LoadAnnouncements()
        {
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string q = @"
        SELECT 
            a.message,
            a.audience,
            a.created_at,
            IF(a.created_by='admin','Admin', t.name) AS PostedBy
        FROM announcements a
        LEFT JOIN teachers t ON a.teacher_id = t.id
        WHERE 
            a.audience='teacher' OR 
            a.audience='both' OR 
            (a.audience='student' AND a.teacher_id=@tid) -- teacher ki student announcements bhi
        ORDER BY a.id DESC";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@tid", teacherId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAnnouncements.DataSource = dt;
            }
        }
        
        // ================= SEARCH =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string q = @"
                SELECT 
                    a.message,
                    a.audience,
                    a.created_at,
                    IF(a.created_by='admin','Admin', t.name) AS PostedBy
                FROM announcements a
                LEFT JOIN teachers t ON a.teacher_id = t.id
                WHERE (a.audience='teacher' OR a.audience='both')
                AND (
                    a.message LIKE @key OR 
                    a.created_by LIKE @key OR
                    t.name LIKE @key
                )
                ORDER BY a.id DESC";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAnnouncements.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No announcement found!");
                }
            }
        }

        // ================= LIVE SEARCH =================
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        // ================= POST (TEACHER → STUDENT) =================
        private void btnPost_Click(object sender, EventArgs e)
        {
            string msg = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(msg))
            {
                MessageBox.Show("Enter message!");
                return;
            }

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();

                string q = @"INSERT INTO announcements 
                             (message, audience, created_by, teacher_id) 
                             VALUES (@msg, 'student', 'teacher', @tid)";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@msg", msg);
                cmd.Parameters.AddWithValue("@tid", teacherId);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Announcement sent to students!");
            txtMessage.Clear();
        }

        // ================= REFRESH =================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadAnnouncements();
        }

        // ================= BACK =================
        private void btnBack_Click(object sender, EventArgs e)
        {
            TeacherDashboard td = new TeacherDashboard(teacherId);
            td.Show();
            this.Close();
        }
    }
}
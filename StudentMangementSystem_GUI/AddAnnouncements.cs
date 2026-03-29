using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMangementSystem_GUI
{
    public partial class AddAnnouncements : Form
    {
        public AddAnnouncements()
        {
            InitializeComponent();

            // EVENTS
            this.Load += AddAnnouncements_Load;
            btnPost.Click += btnPost_Click;
           
            btnRefresh.Click += btnRefresh_Click;
            btnSearch.Click += btnSearch_Click;
            btnBack.Click += btnBack_Click;
            dgvAnnouncements.CellContentClick += dgvAnnouncements_CellContentClick;
        }

        string conn = "server=localhost;user=root;password=;database=student_management_system";

        // ================= LOAD =================
        private void AddAnnouncements_Load(object sender, EventArgs e)
        {
            // ✅ Add audience only once
            if (cmbAudience.Items.Count == 0)
            {
                cmbAudience.Items.Add("teacher");
                cmbAudience.Items.Add("student");
                cmbAudience.Items.Add("both");
            }

            LoadAnnouncements();
        }

        // ================= POST =================
        private void btnPost_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text) || string.IsNullOrWhiteSpace(cmbAudience.Text))
            {
                MessageBox.Show("Fill all fields!");
                return;
            }

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                string q = "INSERT INTO announcements (message, audience, created_by) VALUES (@msg, @aud, 'admin')";
                MySqlCommand cmd = new MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@msg", txtMessage.Text.Trim());
                cmd.Parameters.AddWithValue("@aud", cmbAudience.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Announcement Posted!");
            txtMessage.Clear();
            cmbAudience.SelectedIndex = -1;

            LoadAnnouncements();
        }

        // ================= LOAD GRID =================
        private void LoadAnnouncements()
        {
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                string q = "SELECT id, message, audience, created_at FROM announcements ORDER BY id DESC";

                MySqlDataAdapter da = new MySqlDataAdapter(q, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAnnouncements.DataSource = dt;

                // ✅ Add Delete button column only once
                if (!dgvAnnouncements.Columns.Contains("btnDelete"))
                {
                    DataGridViewButtonColumn del = new DataGridViewButtonColumn();
                    del.Name = "btnDelete";
                    del.Text = "Delete";
                    del.UseColumnTextForButtonValue = true;
                    dgvAnnouncements.Columns.Add(del);
                }
            }
        }

        // ================= VIEW =================
        private void btnView_Click(object sender, EventArgs e)
        {
            LoadAnnouncements();
        }

        // ================= REFRESH =================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtMessage.Clear();
            txtSearch.Clear();
            cmbAudience.SelectedIndex = -1;

            LoadAnnouncements();
        }

        // ================= SEARCH =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                string q = @"SELECT id, message, audience, created_at 
                             FROM announcements
                             WHERE message LIKE @key OR audience LIKE @key
                             ORDER BY id DESC";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                DataTable dt = new DataTable();
                new MySqlDataAdapter(cmd).Fill(dt);

                dgvAnnouncements.DataSource = dt;

                if (dt.Rows.Count == 0)
                    MessageBox.Show("No announcement found!");
            }
        }

        // ================= DELETE =================
        private void dgvAnnouncements_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvAnnouncements.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                int id = Convert.ToInt32(dgvAnnouncements.Rows[e.RowIndex].Cells["id"].Value);

                if (MessageBox.Show("Delete this announcement?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (MySqlConnection con = new MySqlConnection(conn))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM announcements WHERE id=@id", con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Deleted!");
                    LoadAnnouncements();
                }
            }
        }

        // ================= BACK =================
        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminDashboard ad = new AdminDashboard();
            ad.Show();
            this.Close();
        }
    }
}
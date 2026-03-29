using Sentiment_Analysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using SentimentApp;



namespace Sentiment_Analysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(
               panel1.ClientRectangle,
               Color.HotPink,
               Color.Purple,
               45F
           );

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(
               panel2.ClientRectangle,
               Color.HotPink,
               Color.Purple,
               45F
           );

            e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}

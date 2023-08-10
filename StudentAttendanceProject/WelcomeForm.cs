using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentAttendanceProject
{
    public partial class WelcomeForm : Form
    {
        public static string USERNAME = "";

        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.ShowDialog();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            clsGlobal.cn.ConnectionString = "Data Source=LAPTOP-KDGV02MO\\SQLEXPRESS;Initial Catalog=AttendanceStudent;Integrated Security=True";
            try
            {
                clsGlobal.cn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection fail!", "Connection");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewTodayAttendance form = new ViewTodayAttendance();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

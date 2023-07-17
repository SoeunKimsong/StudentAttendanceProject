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
    public partial class Dashboard : Form
    {
        public static string USERNAME = "";
        public Dashboard()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.ShowDialog();
            labelName.Text = USERNAME;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            RegisterForm form = new RegisterForm();
            form.ShowDialog();
            labelName.Text = USERNAME;
        }

        private void Dashboard_Load(object sender, EventArgs e)
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
    }
}

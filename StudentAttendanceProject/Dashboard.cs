using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week12;

namespace StudentAttendanceProject
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            RegisterForm form = new RegisterForm();
            form.ShowDialog();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            labelName.Text = "Welcome User: " + WelcomeForm.USERNAME;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertStudent form = new InsertStudent();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertRoom form = new InsertRoom();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertTeacher form = new InsertTeacher();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewStudent form = new ViewStudent();
            form.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ViewTeacher form = new ViewTeacher();
            form.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ViewRoom form = new ViewRoom();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewAttendance form = new ViewAttendance();
            form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InsertAttendance form = new InsertAttendance();
            form.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Report form = new Report();
            form.ShowDialog();
        }
    }
}

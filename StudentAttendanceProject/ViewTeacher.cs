using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentAttendanceProject
{
    public partial class ViewTeacher : Form
    {
        public ViewTeacher()
        {
            InitializeComponent();
        }

        SqlConnection cn = clsGlobal.cn;
        private void ViewTeacher_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblTeacher", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string name = textBoxSearch.Text;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblTeacher" +
                    " where dbo.tblTeacher.FirstName LIKE '%" + name + "' OR dbo.tblTeacher.LastName LIKE '%" + name + "'" +
                    " OR dbo.tblTeacher.FirstName LIKE '%" + name + "%' OR dbo.tblTeacher.LastName LIKE '%" + name + "%'" +
                    " OR dbo.tblTeacher.FirstName LIKE '" + name + "%' OR dbo.tblTeacher.LastName LIKE '" + name + "%'", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxSearch.Text = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblTeacher", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }
        }
    }
}

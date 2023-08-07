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
    public partial class ViewRoom : Form
    {
        public ViewRoom()
        {
            InitializeComponent();
        }

        SqlConnection cn = clsGlobal.cn;
        private void ViewRoom_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblRoom", cn);
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


        private void buttonSearch_Click_1(object sender, EventArgs e)
        {

            string name = textBoxSearch.Text;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblRoom" +
                    " where dbo.tblRoom.RoomName LIKE '%" + name + "'" +
                    " OR dbo.tblRoom.RoomName LIKE '%" + name + "%'" +
                    " OR dbo.tblRoom.RoomName LIKE '" + name + "%'", cn);
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

        private void buttonClear_Click_1(object sender, EventArgs e)
        {
            textBoxSearch.Text = "";
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblRoom", cn);
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

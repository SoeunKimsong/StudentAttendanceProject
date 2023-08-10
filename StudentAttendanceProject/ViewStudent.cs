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
    public partial class ViewStudent : Form
    {
        public ViewStudent()
        {
            InitializeComponent();
        }
        SqlConnection cn = clsGlobal.cn;
        private void ViewStudent_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT dbo.tblStudent.StudentID, dbo.tblStudent.FirstName," +
                    " dbo.tblStudent.LastName, dbo.tblStudent.Sex, dbo.tblStudent.DOB, dbo.tblStudent.POB," +
                    " dbo.tblStudent.Address, dbo.tblStudent.Phone, dbo.tblStudent.Email, dbo.tblStudent.Photo," +
                    " dbo.tblRoom.RoomName FROM dbo.tblRoom INNER JOIN dbo.tblStudent ON dbo.tblRoom.RoomID = dbo.tblStudent.RoomID", cn);
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
                SqlCommand cmd = new SqlCommand("SELECT dbo.tblStudent.StudentID, dbo.tblStudent.FirstName," +
                    " dbo.tblStudent.LastName, dbo.tblStudent.Sex, dbo.tblStudent.DOB, dbo.tblStudent.POB," +
                    " dbo.tblStudent.Address, dbo.tblStudent.Phone, dbo.tblStudent.Email, dbo.tblStudent.Photo," +
                    " dbo.tblRoom.RoomName FROM dbo.tblRoom INNER JOIN dbo.tblStudent ON dbo.tblRoom.RoomID = dbo.tblStudent.RoomID" +
                    " where dbo.tblStudent.FirstName LIKE '%"+name+ "' OR dbo.tblStudent.LastName LIKE '%"+name+ "'" +
                    " OR dbo.tblStudent.FirstName LIKE '%"+name+ "%' OR dbo.tblStudent.LastName LIKE '%"+name+ "%'" +
                    " OR dbo.tblStudent.FirstName LIKE '"+name+ "%' OR dbo.tblStudent.LastName LIKE '"+name+ "%'", cn);
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
                SqlCommand cmd = new SqlCommand("SELECT dbo.tblStudent.StudentID, dbo.tblStudent.FirstName," +
                    " dbo.tblStudent.LastName, dbo.tblStudent.Sex, dbo.tblStudent.DOB, dbo.tblStudent.POB," +
                    " dbo.tblStudent.Address, dbo.tblStudent.Phone, dbo.tblStudent.Email, dbo.tblStudent.Photo," +
                    " dbo.tblRoom.RoomName FROM dbo.tblRoom INNER JOIN dbo.tblStudent ON dbo.tblRoom.RoomID = dbo.tblStudent.RoomID", cn);
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

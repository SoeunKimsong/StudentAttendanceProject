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
    public partial class ViewStudentInRoom : Form
    {
        public static string RoomID;
        public static string RoomName;
        public ViewStudentInRoom()
        {
            InitializeComponent();
        }

        SqlConnection cn = clsGlobal.cn;
        private void ViewStudentInRoom_Load(object sender, EventArgs e)
        {
            labelRoom.Text = RoomName;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT StudentID, (Firstname+' '+LastName) StudentName, Sex FROM tblStudent WHERE RoomID = "+RoomID, cn);
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class ViewTodayAttendance : Form
    {
        public ViewTodayAttendance()
        {
            InitializeComponent();
        }
        SqlConnection cn = clsGlobal.cn;
        private void ViewTodayAttendance_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT dbo.tblStudentAttendance.StudentAttendanceID, dbo.tblStudentAttendance.StudentAttendanceDate," +
                    " (dbo.tblTeacher.FirstName + ' ' + dbo.tblTeacher.LastName) TeacherName, dbo.tblRoom.RoomName FROM  dbo.tblRoom " +
                    "INNER JOIN  dbo.tblStudentAttendance ON dbo.tblRoom.RoomID = dbo.tblStudentAttendance.RoomID INNER JOIN dbo.tblTeacher " +
                    "ON dbo.tblStudentAttendance.TeacherID = dbo.tblTeacher.TeacherID Order by dbo.tblStudentAttendance.StudentAttendanceID DESC", cn);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ScanFingerprint.AttendanceID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            ScanFingerprint form = new ScanFingerprint();
            form.ShowDialog();
        }
    }
}

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
        private bool isFirstLaunch;
        public ViewTodayAttendance()
        {
            InitializeComponent();
            isFirstLaunch = true;
        }
        SqlConnection cn = clsGlobal.cn;
        private void ViewTodayAttendance_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT dbo.tblStudentAttendance.StudentAttendanceID, dbo.tblStudentAttendance.StudentAttendanceDate," +
                    " (dbo.tblTeacher.FirstName + ' ' + dbo.tblTeacher.LastName) TeacherName, dbo.tblRoom.RoomName FROM  dbo.tblRoom " +
                    "INNER JOIN  dbo.tblStudentAttendance ON dbo.tblRoom.RoomID = dbo.tblStudentAttendance.RoomID INNER JOIN dbo.tblTeacher " +
                    "ON dbo.tblStudentAttendance.TeacherID = dbo.tblTeacher.TeacherID", cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }
            isFirstLaunch = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!isFirstLaunch)
            {
                ScanFingerprint.AttendanceID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                ScanFingerprint form = new ScanFingerprint();
                form.ShowDialog();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

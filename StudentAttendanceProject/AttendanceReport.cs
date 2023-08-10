using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentAttendanceProject
{
    public partial class AttendanceReport : Form
    {
        public static string RoomID;
        public static string RoomName;
        SqlConnection cn = clsGlobal.cn;
        public AttendanceReport()
        {
            InitializeComponent();
        }

        private void AttendanceReport_Load(object sender, EventArgs e)
        {
            labelRoom.Text = RoomName;

            SqlCommand teachercmd = new SqlCommand("Select tblStudentAttendance.TeacherID, (tblTeacher.FirstName+'_'+tblTeacher.LastName) TeacherName from tblStudentAttendance" +
                " INNER JOIN tblTeacher ON tblStudentAttendance.TeacherID = tblTeacher.TeacherID" +
                " WHERE tblStudentAttendance.RoomID = " + RoomID +
                " Group by tblStudentAttendance.TeacherID, tblTeacher.FirstName, tblTeacher.LastName", cn);
            SqlDataAdapter teacheradapter = new SqlDataAdapter(teachercmd);
            DataTable teacherdt = new DataTable();
            teacheradapter.Fill(teacherdt);

            string queryteacher = "";
            foreach (DataRow row in teacherdt.Rows)
            {
                object teacherID = row["TeacherID"];
                object teacherName = row["TeacherName"];
                queryteacher += ",Count(CASE WHEN dbo.tblStudentAttendanceDetail.AttendanceTypeID=1 AND dbo.tblTeacher.TeacherID = "+teacherID+" THEN 1 ELSE NULL END) "+teacherName;
            }


            string query = "SELECT dbo.tblStudent.StudentID, (dbo.tblStudent.FirstName+' '+dbo.tblStudent.LastName) StudentName, dbo.tblStudent.Sex " +
                queryteacher +
                " FROM dbo.tblStudent INNER JOIN  dbo.tblStudentAttendanceDetail ON dbo.tblStudent.StudentID = dbo.tblStudentAttendanceDetail.StudentID AND dbo.tblStudent.StudentID = dbo.tblStudentAttendanceDetail.StudentID " +
                " INNER JOIN dbo.tblStudentAttendance ON dbo.tblStudentAttendanceDetail.StudentAttendanceID = dbo.tblStudentAttendance.StudentAttendanceID " +
                " INNER JOIN dbo.tblTeacher ON dbo.tblStudentAttendance.TeacherID = dbo.tblTeacher.TeacherID\r\n Group by dbo.tblStudent.StudentID, dbo.tblStudent.FirstName, dbo.tblStudent.LastName, dbo.tblStudent.Sex";
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

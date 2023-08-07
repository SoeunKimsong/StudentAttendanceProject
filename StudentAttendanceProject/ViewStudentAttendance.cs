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
    public partial class ViewStudentAttendance : Form
    {
        public static string AttendanceID;
        SqlConnection cn = clsGlobal.cn;
        public ViewStudentAttendance()
        {
            InitializeComponent();
        }

        private void ViewStudentAttendance_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand selectStatement = new SqlCommand("SELECT dbo.tblStudentAttendance.StudentAttendanceID, dbo.tblStudentAttendance.StudentAttendanceDate," +
                    " (dbo.tblTeacher.FirstName+' '+ dbo.tblTeacher.LastName) TeacherName, dbo.tblRoom.RoomName FROM dbo.tblRoom " +
                    "INNER JOIN dbo.tblStudentAttendance ON dbo.tblRoom.RoomID = dbo.tblStudentAttendance.RoomID " +
                    "INNER JOIN dbo.tblTeacher ON dbo.tblStudentAttendance.TeacherID = dbo.tblTeacher.TeacherID " +
                    "WHERE StudentAttendanceID = " + AttendanceID, cn);
                SqlDataReader selectreader = selectStatement.ExecuteReader();
                selectreader.Read();
                labelRoom.Text = selectreader["RoomName"].ToString();
                labelTeacher.Text = selectreader["TeacherName"].ToString();
                labelDate.Text = ((DateTime)selectreader["StudentAttendanceDate"]).ToString("MM/dd/yyyy");
                selectreader.Close();


                SqlCommand cmd = new SqlCommand("SELECT (dbo.tblStudent.FirstName + ' ' + dbo.tblStudent.LastName) StudentName, dbo.tblAttendanceType.TypeName " +
                    "FROM dbo.tblAttendanceType INNER JOIN dbo.tblStudentAttendanceDetail ON dbo.tblAttendanceType.AttendanceTypeID = dbo.tblStudentAttendanceDetail.AttendanceTypeID" +
                    " INNER JOIN dbo.tblStudent ON dbo.tblStudentAttendanceDetail.StudentID = dbo.tblStudent.StudentID WHERE StudentAttendanceID = " + AttendanceID, cn);
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
    }
}

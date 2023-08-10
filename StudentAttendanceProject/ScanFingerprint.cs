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
    public partial class ScanFingerprint : Form
    {
        public static string AttendanceID;
        public ScanFingerprint()
        {
            InitializeComponent();
        }
        SqlConnection cn = clsGlobal.cn;
        string teacherID;
        string roomID;
        private void ScanFingerprint_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand selectStatement = new SqlCommand("SELECT dbo.tblStudentAttendance.StudentAttendanceID, dbo.tblStudentAttendance.StudentAttendanceDate," +
                    " (dbo.tblTeacher.FirstName+' '+ dbo.tblTeacher.LastName) TeacherName, dbo.tblTeacher.TeacherID, dbo.tblRoom.RoomName, dbo.tblRoom.RoomID FROM dbo.tblRoom " +
                    "INNER JOIN dbo.tblStudentAttendance ON dbo.tblRoom.RoomID = dbo.tblStudentAttendance.RoomID " +
                    "INNER JOIN dbo.tblTeacher ON dbo.tblStudentAttendance.TeacherID = dbo.tblTeacher.TeacherID " +
                    "WHERE StudentAttendanceID = " + AttendanceID, cn);
                SqlDataReader selectreader = selectStatement.ExecuteReader();
                selectreader.Read();
                labelRoom.Text = selectreader["RoomName"].ToString();
                labelTeacher.Text = selectreader["TeacherName"].ToString();
                teacherID = selectreader["TeacherID"].ToString();
                roomID = selectreader["RoomID"].ToString();
                labelDate.Text = ((DateTime)selectreader["StudentAttendanceDate"]).ToString("MM/dd/yyyy");

                if ((DateTime)selectreader["StudentAttendanceDate"] != DateTime.Today)
                {
                    MessageBox.Show("This Attendance List is expired !");
                    buttonScan.Enabled = false;
                }
                selectreader.Close();


                SqlCommand cmd = new SqlCommand("SELECT (dbo.tblStudent.FirstName + ' ' + dbo.tblStudent.LastName) StudentName, dbo.tblAttendanceType.TypeName " +
                    "FROM dbo.tblAttendanceType INNER JOIN dbo.tblStudentAttendanceDetail ON dbo.tblAttendanceType.AttendanceTypeID = dbo.tblStudentAttendanceDetail.AttendanceTypeID" +
                    " INNER JOIN dbo.tblStudent ON dbo.tblStudentAttendanceDetail.StudentID = dbo.tblStudent.StudentID WHERE StudentAttendanceID = " + AttendanceID, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                SqlCommand studentcmd = new SqlCommand("SELECT dbo.tblStudent.StudentID, (dbo.tblStudent.FirstName + ' ' + dbo.tblStudent.LastName) StudentName FROM tblStudent WHERE RoomID = "+roomID, cn);
                SqlDataAdapter studentadapter = new SqlDataAdapter(studentcmd);
                DataTable studentdt = new DataTable();
                studentadapter.Fill(studentdt);
                comboBoxStudent.DataSource = studentdt;
                comboBoxStudent.DisplayMember = "StudentName";
                comboBoxStudent.ValueMember = "StudentID";
            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }

        }


        private void buttonScan_Click(object sender, EventArgs e)
        {
            Scanning form = new Scanning();
            form.ShowDialog();

            string studentId = comboBoxStudent.SelectedValue.ToString();
            string query = "Update tblStudentAttendanceDetail Set tblStudentAttendanceDetail.AttendanceTypeID = " +2 +
                    " Where StudentAttendanceID = " + AttendanceID + " AND StudentID = " + studentId;
            SqlCommand cmd = new SqlCommand(query, cn);
            try
            {
                cmd.ExecuteNonQuery();
                string name = ((DataRowView)comboBoxStudent.SelectedItem).Row["StudentName"].ToString();
                MessageBox.Show("Welcome to class, "+name+". Your attendance has been recorded.", "Scan Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ScanFingerprint_Load(null, null);
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

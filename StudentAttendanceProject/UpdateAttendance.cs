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
    public partial class UpdateAttendance : Form
    {
        public static string AttendanceID;
        List<KeyValuePair<string, string>> attendanceChange = new List<KeyValuePair<string, string>>();
        public UpdateAttendance()
        {
            InitializeComponent();

            SqlCommand teachercmd = new SqlCommand("SELECT *, (FirstName +' '+ LastName) TeacherName  FROM tblTeacher", cn);
            SqlDataAdapter teacheradapter = new SqlDataAdapter(teachercmd);
            teacheradapter.Fill(teacherdt);
            comboBoxTeacher.DataSource = teacherdt;
            comboBoxTeacher.DisplayMember = "TeacherName";
            comboBoxTeacher.ValueMember = "TeacherID";

            SqlCommand roomcmd = new SqlCommand("SELECT * FROM tblRoom", cn);
            SqlDataAdapter roomadapter = new SqlDataAdapter(roomcmd);
            roomadapter.Fill(roomdt);
            comboBoxRoom.DataSource = roomdt;
            comboBoxRoom.DisplayMember = "RoomName";
            comboBoxRoom.ValueMember = "RoomID";
        }

        DataTable roomdt = new DataTable();
        DataTable teacherdt = new DataTable();
        SqlConnection cn = clsGlobal.cn;
        private void UpdateAttendance_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand selectStatement = new SqlCommand("SELECT * FROM tblStudentAttendance WHERE StudentAttendanceID = " + AttendanceID, cn);
                SqlDataReader selectreader = selectStatement.ExecuteReader();
                selectreader.Read();
                comboBoxRoom.SelectedValue = selectreader["RoomID"].ToString();
                comboBoxTeacher.SelectedValue = selectreader["TeacherID"].ToString();
                datePicker.Value = (DateTime)selectreader["StudentAttendanceDate"];
                selectreader.Close();

                SqlCommand cmd = new SqlCommand("SELECT (dbo.tblStudent.FirstName + ' ' + dbo.tblStudent.LastName) StudentName, dbo.tblAttendanceType.TypeName " +
                    "FROM dbo.tblAttendanceType INNER JOIN dbo.tblStudentAttendanceDetail ON dbo.tblAttendanceType.AttendanceTypeID = dbo.tblStudentAttendanceDetail.AttendanceTypeID" +
                    " INNER JOIN dbo.tblStudent ON dbo.tblStudentAttendanceDetail.StudentID = dbo.tblStudent.StudentID WHERE StudentAttendanceID = " + AttendanceID, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch (SqlException err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridView1.CurrentCell;
            if(cell.ColumnIndex == 1)
            {
                if(cell.Value.ToString() == "Absence")
                {
                    cell.Value = "Present";
                    attendanceChange.Add(new KeyValuePair<string, string>(dataGridView1.CurrentRow.Cells[0].Value.ToString(), "2"));
                }
                else
                {
                    cell.Value = "Absence";
                    attendanceChange.Add(new KeyValuePair<string, string>(dataGridView1.CurrentRow.Cells[0].Value.ToString(), "1"));
                }
            }
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {

            foreach(var att in attendanceChange)
            {
                string query = "Update tblStudentAttendanceDetail Set tblStudentAttendanceDetail.AttendanceTypeID = " + att.Value +
                    " FROM tblStudentAttendanceDetail INNER JOIN tblStudent ON tblStudentAttendanceDetail.StudentID = tblStudent.StudentID" +
                    " Where StudentAttendanceID = " + AttendanceID + " AND (tblStudent.FirstName + ' ' + tblStudent.LastName) = '"+att.Key+"'";
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException err)
                {
                    MessageBox.Show(err.ToString());
                }
            }


            DateTime date = datePicker.Value;
            string room = comboBoxRoom.SelectedValue.ToString();
            string teacher = comboBoxTeacher.SelectedValue.ToString();

            try
            {
                string query = "Update tblStudentAttendance SEt StudentAttendanceDate = '" + date + "',TeacherID = '" + teacher + "',RoomID = '" + room + "' " +
                    " Where StudentAttendanceID = " + AttendanceID;
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record update has successfuly");
                    this.Close();
                }
                catch (SqlException err)
                {
                    MessageBox.Show(err.ToString());
                }
            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Delete from tblStudentAttendance where StudentAttendanceID = " + AttendanceID;

                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted has successfuly");
                    this.Close();
                }
                catch (SqlException err)
                {
                    MessageBox.Show(err.ToString());
                }
            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }
        }
    }
}

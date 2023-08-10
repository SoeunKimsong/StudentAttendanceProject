using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StudentAttendanceProject
{
    public partial class CreateAttendance : Form
    {
        public CreateAttendance()
        {
            InitializeComponent();
        }
        DataTable roomdt = new DataTable();
        DataTable teacherdt = new DataTable();
        SqlConnection cn = clsGlobal.cn;
        private void CreateAttendance_Load(object sender, EventArgs e)
        {
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

        private void Createbutton_Click(object sender, EventArgs e)
        {
            string teacher = comboBoxTeacher.SelectedValue.ToString();
            string room = comboBoxRoom.SelectedValue.ToString();
            DateTime today = DateTime.Now;

            try
            {
                string query = "Insert into tblStudentAttendance(StudentAttendanceDate, TeacherID, RoomID)" +
                    "values('" + today + "','" + teacher + "','" + room + "')";
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();

                    SqlCommand selectStatement = new SqlCommand("SELECT TOP 1 * FROM tblStudentAttendance ORDER BY StudentAttendanceID DESC", cn);
                    SqlDataReader selectreader = selectStatement.ExecuteReader();
                    selectreader.Read();
                    var attID = selectreader["StudentAttendanceID"];
                    selectreader.Close();


                    SqlCommand selectstudentcmd = new SqlCommand("SELECT StudentID FROM tblStudent WHERE RoomID = "+room, cn);
                    SqlDataAdapter selectadapter = new SqlDataAdapter(selectstudentcmd);
                    DataTable selectdt = new DataTable();
                    selectadapter.Fill(selectdt);
                    foreach (DataRow row in selectdt.Rows)
                    {
                        object studentID = row["StudentID"];
                        string insertquery = "Insert Into tblStudentAttendanceDetail(StudentAttendanceID, StudentID, AttendanceTypeID)" +
                            "Values('" + attID + "','" + studentID + "','" + 1 + "')";
                        SqlCommand insertcmd = new SqlCommand(insertquery, cn);
                        insertcmd.ExecuteNonQuery();
                    }


                    MessageBox.Show("Record insert has successfuly");
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

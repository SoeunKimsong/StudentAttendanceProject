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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StudentAttendanceProject
{
    public partial class InsertAttendance : Form
    {
        public InsertAttendance()
        {
            InitializeComponent();
        }

        SqlConnection cn = clsGlobal.cn;
        private void InsertAttendance_Load(object sender, EventArgs e)
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
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            }
            catch (SqlException err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateAttendance.AttendanceID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            UpdateAttendance form = new UpdateAttendance();
            form.ShowDialog();
            InsertAttendance_Load(null, null);
        }


    }
}

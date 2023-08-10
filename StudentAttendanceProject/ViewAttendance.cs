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
    public partial class ViewAttendance : Form
    {
        public ViewAttendance()
        {
            InitializeComponent();
        }
        SqlConnection cn = clsGlobal.cn;
        DataTable roomdt = new DataTable();
        private void ViewAttendance_Load(object sender, EventArgs e)
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


                SqlCommand roomcmd = new SqlCommand("SELECT * FROM tblRoom", cn);
                SqlDataAdapter roomadapter = new SqlDataAdapter(roomcmd);
                roomadapter.Fill(roomdt);
                comboBoxRoom.DataSource = roomdt;
                comboBoxRoom.DisplayMember = "RoomName";
                comboBoxRoom.ValueMember = "RoomID";
            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            CreateAttendance form = new CreateAttendance();
            form.ShowDialog();
            ViewAttendance_Load(null, null);
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime date = datePicker.Value;
            string room = comboBoxRoom.SelectedValue.ToString();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT dbo.tblStudentAttendance.StudentAttendanceID, dbo.tblStudentAttendance.StudentAttendanceDate," +
                    " (dbo.tblTeacher.FirstName + ' ' + dbo.tblTeacher.LastName) TeacherName, dbo.tblRoom.RoomName FROM  dbo.tblRoom " +
                    "INNER JOIN  dbo.tblStudentAttendance ON dbo.tblRoom.RoomID = dbo.tblStudentAttendance.RoomID INNER JOIN dbo.tblTeacher " +
                    "ON dbo.tblStudentAttendance.TeacherID = dbo.tblTeacher.TeacherID " +
                    "WHERE dbo.tblStudentAttendance.StudentAttendanceDate = '"+ date + "' AND dbo.tblRoom.RoomID = "+ room +
                    " Order by dbo.tblStudentAttendance.StudentAttendanceID DESC", cn);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewStudentAttendance.AttendanceID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            ViewStudentAttendance form = new ViewStudentAttendance();
            form.ShowDialog();
        }
    }
}

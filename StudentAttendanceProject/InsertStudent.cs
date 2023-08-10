using StudentAttendanceProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week12
{
    public partial class InsertStudent : Form
    {
        DataTable roomdt = new DataTable();
        public InsertStudent()
        {
            InitializeComponent();

            SqlCommand roomcmd = new SqlCommand("SELECT * FROM tblRoom", cn);
            SqlDataAdapter roomadapter = new SqlDataAdapter(roomcmd);
            roomadapter.Fill(roomdt);
            comboBoxRoom.DataSource = null;
            comboBoxRoom.Items.Clear();
            comboBoxRoom.DataSource = roomdt;
            comboBoxRoom.DisplayMember = "RoomName";
            comboBoxRoom.ValueMember = "RoomID";
        }
        SqlConnection cn = clsGlobal.cn;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT dbo.tblStudent.StudentID, dbo.tblStudent.FirstName," +
                    " dbo.tblStudent.LastName, dbo.tblStudent.Sex, dbo.tblStudent.DOB, dbo.tblStudent.POB," +
                    " dbo.tblStudent.Address, dbo.tblStudent.Phone, dbo.tblStudent.Email, dbo.tblStudent.Photo," +
                    " dbo.tblRoom.RoomName FROM dbo.tblStudent INNER JOIN dbo.tblRoom ON dbo.tblRoom.RoomID = dbo.tblStudent.RoomID" +
                    " Order by dbo.tblStudent.StudentID Desc", cn);
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

        private void button1_Click(object sender, EventArgs e)
        {

            string Email = this.textBoxEmail.Text;
            string Firstname = this.textboxFirstname.Text;
            string Lastname = this.textboxLastname.Text;
            string Sex = comboBoxSex.SelectedItem.ToString();
            string DOB = this.DateTextbox.Text;
            string POB = this.PlaceTextBox.Text;
            string Phone = this.PhoneTextBox.Text;
            string Address = this.textBoxAddress.Text;
            string RoomID = this.comboBoxRoom.SelectedValue.ToString();

            try
            {
                string query = "Insert into tblStudent(Email,Firstname,Lastname,Sex,DOB,POB,Phone,Address,RoomID)" +
                    "values('" + Email + "','" + Firstname + "','" + Lastname + "','" + Sex + "','" + DOB + "','" + POB + "','" + Phone + "','" + Address + "','" + RoomID + "')";
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record insert has successfuly");
                    Form1_Load(null, null);
                    emptyTextbox();
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void emptyTextbox()
        {
            StudentID_textBox.Text = "";
            textBoxEmail.Text = "";
            textboxFirstname.Text = "";
            textboxLastname.Text = "";
            DateTextbox.Text = "";
            PlaceTextBox.Text = "";
            PhoneTextBox.Text = "";
            textBoxAddress.Text = "";
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Delete from tblStudent where StudentID = " + Int16.Parse(this.StudentID_textBox.Text);

                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted has successfuly");
                    Form1_Load(null, null);
                    emptyTextbox();
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

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            int StudentID = Int16.Parse(this.StudentID_textBox.Text);
            string Email = this.textBoxEmail.Text;
            string Firstname = this.textboxFirstname.Text;
            string Lastname = this.textboxLastname.Text;
            string Sex = comboBoxSex.SelectedItem.ToString();
            string DOB = this.DateTextbox.Text;
            string POB = this.PlaceTextBox.Text;
            string Phone = this.PhoneTextBox.Text;
            string Address = this.textBoxAddress.Text;
            string RoomID = this.comboBoxRoom.SelectedValue.ToString();
            try
            {
                string query = "Update tblStudent set Email='" + Email + "',Firstname='" + Firstname + "',Lastname='" + Lastname + "',Sex='" + Sex + "',DOB='" + DOB + "',POB='" + POB + "',Phone='" + Phone + "',Address='" + Address + "',RoomID='" + RoomID + "' where StudentID=" + StudentID + "";
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Update has successfuly");
                    Form1_Load(null, null);
                    emptyTextbox();
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


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID_textBox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textboxFirstname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textboxLastname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBoxSex.SelectedItem = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            DateTextbox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            PlaceTextBox.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBoxAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            PhoneTextBox.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBoxEmail.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();

            string roomname = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            for (int i = 0; i < roomdt.Rows.Count; i++)
            {
                if (roomdt.Rows[i].Field<string>("RoomName") == roomname)
                {
                    comboBoxRoom.SelectedValue = roomdt.Rows[i].Field<int>("RoomID");
                }
            }
        }
    }
}


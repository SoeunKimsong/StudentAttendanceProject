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
    public partial class InsertTeacher : Form
    {
        public InsertTeacher()
        {
            InitializeComponent();
        }

        SqlConnection cn = clsGlobal.cn;

        private bool isFirstLoad = true;
        private void InsertTeacher_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * From tblTeacher", cn);
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
            isFirstLoad = false;
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            string Email = this.textBoxEmail.Text;
            string Firstname = this.textboxFirstname.Text;
            string Lastname = this.textboxLastname.Text;
            string Sex = comboBoxSex.SelectedItem.ToString();
            string DOB = this.DateTextbox.Text;
            string POB = this.PlaceTextBox.Text;
            string Phone = this.PhoneTextBox.Text;
            string Address = this.textBoxAddress.Text;

            try
            {
                string query = "Insert into tblTeacher(Email,Firstname,Lastname,Sex,DOB,POB,Phone,Address)" +
                    "values('" + Email + "','" + Firstname + "','" + Lastname + "','" + Sex + "','" + DOB + "','" + POB + "','" + Phone + "','" + Address + "')";
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record insert has successfuly");
                    InsertTeacher_Load(null, null);
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
            if (!isFirstLoad)
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
            }
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
                string query = "Delete from tblTeacher where TeacherID = " + Int16.Parse(this.StudentID_textBox.Text);

                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted has successfuly");
                    InsertTeacher_Load(null, null);
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
            int TeacherID = Int16.Parse(this.StudentID_textBox.Text);
            string Email = this.textBoxEmail.Text;
            string Firstname = this.textboxFirstname.Text;
            string Lastname = this.textboxLastname.Text;
            string Sex = comboBoxSex.SelectedItem.ToString();
            string DOB = this.DateTextbox.Text;
            string POB = this.PlaceTextBox.Text;
            string Phone = this.PhoneTextBox.Text;
            string Address = this.textBoxAddress.Text;

            try
            {
                string query = "Update tblTeacher set Email='" + Email + "',Firstname='" + Firstname + "',Lastname='" + Lastname + "',Sex='" + Sex + "',DOB='" + DOB + "',POB='" + POB + "',Phone='" + Phone + "',Address='" + Address + "' where TeacherID=" + TeacherID + "";
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Update has successfuly");
                    InsertTeacher_Load(null, null);
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

        private void button2_Click(object sender, EventArgs e)
        {
            emptyTextbox();
        }
    }
}

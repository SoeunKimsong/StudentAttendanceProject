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

namespace week12
{
    public partial class InsertStudent : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-LG44BGTQ\\SQLEXPRESS;Initial Catalog=BOB;Integrated Security=True");
        public InsertStudent()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string student_code = this.StudentCodeTextBox.Text;
            string surname = this.UsernameTextBox.Text;
            string given_name = this.GivennameTextBox.Text;
            int sex = Int16.Parse(this.SexTextBox.Text);
            string date_of_birth = this.DateTextbox.Text;
            string place_of_birth = this.PlaceTextBox.Text;
            string phone_number = this.PhoneTextBox.Text;
            try
            {
                conn.Open();
                string query = "Insert into Table_student_list(student_code,surname,given_name,sex,date_of_birth,place_of_birth,phone_number)" +
                    "values('"+student_code+"','"+surname+"','"+ given_name+"',"+sex+",'"+date_of_birth+"','"+ place_of_birth+ "','"+phone_number+"')";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record insert has successfuly");
                    conn.Close();
                    Form1_Load(null, null);
                }
                catch (SqlException err)
                {
                    MessageBox.Show(err.ToString());
                }
            }
            catch(SqlException er)
            {
                MessageBox.Show(er.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Table_student_list order by student_id asc", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (SqlException er)
            {
                MessageBox.Show(er.ToString());
            }
            finally
            {
                conn.Close();
            }
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            StudentID_textBox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            StudentCodeTextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            UsernameTextBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            GivennameTextBox.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            SexTextBox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            DateTextbox.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            PlaceTextBox.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            PhoneTextBox.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "Delete from Table_student_list where student_id = "+ Int16.Parse(this.StudentID_textBox.Text);

                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deteled has successfuly");
                    conn.Close();
                    Form1_Load(null, null);
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
            finally
            {
                conn.Close();
            }
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            int student_id = Int16.Parse(this.StudentID_textBox.Text);
            string student_code = this.StudentCodeTextBox.Text;
            string surname = this.UsernameTextBox.Text;
            string given_name = this.GivennameTextBox.Text;
            int sex = Int16.Parse(this.SexTextBox.Text);
            string date_of_birth = this.DateTextbox.Text;
            string place_of_birth = this.PlaceTextBox.Text;
            string phone_number = this.PhoneTextBox.Text;
            try
            {
                conn.Open();
                string query = "Update Table_student_list set student_code='"+student_code+"',surname='"+surname+"',given_name='"+given_name+"',sex='"+sex+"',date_of_birth='"+date_of_birth+"',place_of_birth='"+place_of_birth+"',phone_number='"+phone_number+"' where student_id="+student_id+"";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Update has successfuly");
                    conn.Close();
                    Form1_Load(null, null);
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
            finally
            {
                conn.Close();
            }
        }
    }
}


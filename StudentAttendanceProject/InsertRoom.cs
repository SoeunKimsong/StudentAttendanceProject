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
    public partial class InsertRoom : Form
    {
        public InsertRoom()
        {
            InitializeComponent();
        }
        SqlConnection cn = clsGlobal.cn;

        private bool isFirstLoad = true;
        private void InsertRoom_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * From tblRoom", cn);
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
        private void emptyTextbox()
        {
            textboxID.Text = "";
            textboxRoomname.Text = "";
        }
        private void Savebutton_Click(object sender, EventArgs e)
        {
            string Roomname = this.textboxRoomname.Text;

            try
            {
                string query = "Insert into tblRoom(Roomname)" +
                    "values('" + Roomname + "')";
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record insert has successfuly");
                    InsertRoom_Load(null, null);
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

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Delete from tblRoom where RoomID = " + Int16.Parse(this.textboxID.Text);

                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted has successfuly");
                    InsertRoom_Load(null, null);
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
            int RoomID = Int16.Parse(this.textboxID.Text);
            string Roomname = this.textboxRoomname.Text;
            try
            {
                string query = "Update tblRoom set Roomname='" + Roomname + "' where RoomID=" + RoomID + "";
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Update has successfuly");
                    InsertRoom_Load(null, null);
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!isFirstLoad)
            {
                textboxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textboxRoomname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentAttendanceProject
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(textBoxUsername.Text=="" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Please input all of the fields.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter(@"Select * From tblAdmin
                                                    Where [username] = @userName 
	                                              and [password] = @password", clsGlobal.cn);
                DataTable dt = new DataTable();
                da.SelectCommand.Parameters.AddWithValue("@username", textBoxUsername.Text);
                da.SelectCommand.Parameters.AddWithValue("@password", textBoxPassword.Text);
                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count >= 1)
                    {
                        this.Close();
                        MessageBox.Show("Login success!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        WelcomeForm.USERNAME = textBoxUsername.Text;
                        Dashboard form = new Dashboard();
                        form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Login fail!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Network error!", "Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

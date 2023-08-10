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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if(
                textBoxUsername.Text == "" ||
                textBoxPhone.Text == "" ||
                textBoxPassword.Text == "" ||
                textBoxConfPassword.Text == ""
                )
            {
                MessageBox.Show("Please input all of the fields.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if( textBoxPassword.Text != textBoxConfPassword.Text )
            {
                MessageBox.Show("The Confirm Password doesn't match.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO tblAdmin
                                                       ([username]
                                                       ,[phonenumber]
                                                       ,[password])
                                                 VALUES
                                                       (@username
                                                       ,@phonenumber
                                                       ,@password)", clsGlobal.cn);
                cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                cmd.Parameters.AddWithValue("@phonenumber", textBoxPhone.Text);
                cmd.Parameters.AddWithValue("@password", textBoxPassword.Text);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Register New Account!", "Register", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    WelcomeForm.USERNAME = textBoxUsername.Text;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Register fail!", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

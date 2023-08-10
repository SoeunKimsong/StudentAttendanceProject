using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentAttendanceProject
{
    public partial class Scanning : Form
    {
        public Scanning()
        {
            InitializeComponent();
        }

        private void Scanning_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Start();

            timer2.Interval = 5000;
            timer2.Start();
        }
        int move = -1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            labelScan.Top = labelScan.Top + move;
            if(labelScan.Top == pictureBox1.Top) {
                move = 1;
            }
            if(labelScan.Top == pictureBox1.Bottom)
            {
                move = -1;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }
    }
}

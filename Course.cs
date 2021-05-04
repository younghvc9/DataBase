using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Course : Form
    {
        public Course()
        {
            InitializeComponent();
        }
        public Course(String Cname)
        {
            InitializeComponent();
            label1.Text = "课程:"+Cname;
        }

        private void Course_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseDesign.Notice nc= new DatabaseDesign.Notice();
            nc.Show();
        }
    }
}

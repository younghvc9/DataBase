using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatabaseDesign
{
    public partial class CreatCourse : Form
    {
        String CourseName = "";
        String CourseTeacher = "";
        String CourseExplain = "";
        public CreatCourse()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CourseName = textBox1.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //Get CourseTeacher;
            textBox3.Text = CourseTeacher ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CourseName != "")
            {
                //update courseinfo;3strings
                WinFormsApp1.Course css = new WinFormsApp1.Course(CourseName);
                css.Show();
                Close();
            }
            else
            {
                MessageBox.Show("课程名为空");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

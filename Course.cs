using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WinFormsApp1
{
    public partial class Course : Form
    {
        String coursename;
        public Course()
        {
            InitializeComponent();
            
        }
        public Course(String Cname)
        {
            InitializeComponent();
            label1.Text = "课程:"+Cname;
            coursename = Cname;
        }

        private void Course_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            string sql = "select COURSE_ID from COURSES where COURSES_NAME = '" + coursename + "'";
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            int a = (int)com.ExecuteScalar();
            DatabaseDesign.Work wk = new DatabaseDesign.Work(label1.Text, a,0);
            wk.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseDesign.Notice nc= new DatabaseDesign.Notice();
            nc.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            string sql = "select COURSE_ID from COURSES where COURSES_NAME = '" + coursename + "'";
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            int a = (int)com.ExecuteScalar();
            DatabaseDesign.Work wk = new DatabaseDesign.Work(label1.Text, a, 1);
            wk.Show();
        }
    }
}

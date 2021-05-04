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
using System.Data.SqlClient;
namespace WinFormsApp1
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            //string connectString = "Data Source=121.196.159.49;Initial Catalog=Student;Integrated Security=True";
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private bool CheckInfo(String user, String pword)
        {
            //datebase comparing
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            /*
            string sql = "select count(*) from USER_ where Identity_='老师'";
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            int a = (int)com.ExecuteScalar();
            if (a != 0)
            {
                label1.Text = "success"+a;
            }
            else
            {
                label1.Text = "no";
            }*/
            String suser = textBox1.Text;
            String spassword = textBox2.Text;
            string sql = "select count(*) from USER_ where Identity_='老师'";
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            int a = (int)com.ExecuteScalar();
            if (CheckInfo(suser, spassword))
            {
                MessageBox.Show("登录成功");
                StuUI stu1 = new StuUI();
                stu1.Show();
            }
            else
            {
                MessageBox.Show("账号或者密码不正确");
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            register reg = new register();
            reg.Show();
        }

        private void login_Load(object sender, EventArgs e)
        {
            
        }
    }
}
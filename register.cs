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
    public partial class register : Form
    {
        int F1=0,F2=0,F3=0,F4=0;
        String regName = "";
        String regPassword = "";
        String uidentity = "";
        public register()
        {
            InitializeComponent();
        }

        private void register_Load(object sender, EventArgs e)
        {
           //从数据库中读取生成身份编号
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            //查询用户个数
            string sql = "select COUNT(*) from USER_ ";
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            int usercnt = (int)com.ExecuteScalar();
            //查询看看是否用户名重复
            bool flag = false;
            for (int i = 1; i <= usercnt; i++)
            {
                string s1 = "select Username from USER_ where ID=" + i.ToString();
                try
                {
                    SqlCommand s2 = new SqlCommand(s1, sqlCnt);
                    String uname = (String)s2.ExecuteScalar();
                    if(String.Equals(uname,regName))
                    {
                        flag = true;
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("发生错误");
                }
            }
            if (flag)
            {
                MessageBox.Show("该用户名已被注册");
            }
            else
            if (F3 == -1)
            {
                MessageBox.Show("两次密码输入不一致");
            }
            else
            {
                if (F1 == 1 && F2 == 1 && F3 == 1 && F4 == 1)
                {
                    //上传数据库
                    //找到主键ID
                    try
                    {
                        string s1 = "select TOP 1 id from user_ order by id desc";
                        SqlCommand s2 = new SqlCommand(s1, sqlCnt);
                        int uID = (int)s2.ExecuteScalar();
                        uID = uID + 1;
                        //找到生成的自动编号
                        string s5 = "select TOp 1 ID_card from USER_ where Identity_='" + uidentity + "' order by ID_card desc";
                        SqlCommand s6 = new SqlCommand(s5, sqlCnt);
                        int idcard = (int)s6.ExecuteScalar();
                        idcard = idcard + 1;
                        label8.Text = idcard.ToString();
                        //上传
                        string s3 = "insert into User_(ID,Username,Password_,Identity_,ID_card) values(" + uID + ",'" + regName + "'," + regPassword + ",'" + uidentity + "'," + idcard + ")";
                        SqlCommand s4 = new SqlCommand(s3, sqlCnt);
                        s4.ExecuteScalar();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("发生错误");
                    }
                    finally
                    {
                        MessageBox.Show("注册成功");
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("存在未填写字段");
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            F4 = 1;
            if (radioButton1.Checked)
            {
                label6.Text = "教师号";
                uidentity = "老师";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            F4 = 1;
            if (radioButton2.Checked)
            {
                label6.Text = "学号";
                uidentity = "学生";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            regPassword = textBox2.Text;
            F2 = 1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            regName = textBox1.Text;
            F1 = 1;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (String.Equals(regPassword, textBox3.Text))
            {
                F3 = 1;
            }
            else
            {
                F3 = -1;
            }
        }
    }
}

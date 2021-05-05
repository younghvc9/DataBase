using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace DatabaseDesign
{
    public partial class Working : Form
    {
        int examid=0;
        int qidnow=0;
        int qtotal = 0;
        int stl = 1;
        public Working()
        {
            InitializeComponent();
        }
        public Working(int eid,string Cname,string Ename,int is_test)
        {
            InitializeComponent();
            stl = 1;
            examid = eid;
            label1.Text = Cname;
            if(is_test==0)
                label2.Text = "作业名称："+Ename;
            else
                label2.Text = "考试名称："+Ename;
            if(is_test==1)
            {
                button4.Enabled = false;
                button4.Visible = false;
            }

            //链接数据库
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            //读取总共多少题目 
            string sql = "select count(*) from QS where EXAM_ID=" +examid;
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            qtotal = (int)com.ExecuteScalar();
            if (qtotal == 0)
            {
                MessageBox.Show("本试题库为空!");
            }
            else
            {
                //读取第一道题目stl=1
                try
                {
                    string s8 = "select TOP 1 QID from QS where QID in(select TOP " + stl + " QID from QS where EXAM_ID=" + examid + " order by QID) order by QID desc";
                    SqlCommand co1 = new SqlCommand(s8, sqlCnt);
                    qidnow = (int)co1.ExecuteScalar();
                    string sql1 = "select Q from QS where QID=" + qidnow;
                    SqlCommand com1 = new SqlCommand(sql1, sqlCnt);
                    String ss = (String)com1.ExecuteScalar();
                    label3.Text = ss;
                    label5.Text = "第" + stl + "题：";
                    label6.Text = "题型：";
                    string sql2 = "select A from QS where QID=" + qidnow;
                    SqlCommand com2 = new SqlCommand(sql2, sqlCnt);
                    int a = (int)com2.ExecuteScalar();
                    if (a != 0)
                        label6.Text += "单项选择题";
                    else
                        label6.Text += "简答题";
                }
                catch (Exception)
                {
                    MessageBox.Show("发生错误");
                }
            }
        }
        private void Working_Load(object sender, EventArgs e)
        {
            if (qtotal == 0)
            {
                Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            string sql2 = "select A from QS where QID=" + qidnow;
            SqlCommand com2 = new SqlCommand(sql2, sqlCnt);
            int ans = (int)com2.ExecuteScalar();
            switch (ans)
            {
                case 0:
                    MessageBox.Show("本题为简答题，不提供答案");
                    break;
                case 1:
                    MessageBox.Show("本题选A");
                    break;
                case 2:
                    MessageBox.Show("本题选B");
                    break;
                case 3:
                    MessageBox.Show("本题选C");
                    break;
                case 4:
                    MessageBox.Show("本题选D");
                    break;
                default:
                    MessageBox.Show("本题为简答题，不提供答案");
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //上传学生答题结果
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (stl <= 1) MessageBox.Show("没有上一条了");
            else
            {
                stl--;
                string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
                SqlConnection sqlCnt = new SqlConnection(ConStr);
                sqlCnt.Open();
                string s8 = "select TOP 1 QID from QS where QID in(select TOP " + stl + " QID from QS where EXAM_ID=" + examid + " order by QID) order by QID desc";
                SqlCommand co1 = new SqlCommand(s8, sqlCnt);
                qidnow = (int)co1.ExecuteScalar();
                string sql1 = "select Q from QS where QID=" + qidnow;
                SqlCommand com1 = new SqlCommand(sql1, sqlCnt);
                String ss = (String)com1.ExecuteScalar();
                label3.Text = ss;
                label5.Text = "第" + stl + "题：";
                label6.Text = "题型：";
                string sql2 = "select A from QS where QID=" + qidnow;
                SqlCommand com2 = new SqlCommand(sql2, sqlCnt);
                int a = (int)com2.ExecuteScalar();
                if (a != 0)
                    label6.Text += "单项选择题";
                else
                    label6.Text += "简答题";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            //查询
            if (stl >= qtotal) MessageBox.Show("没有下一条了,请选择结束答题以交卷");
            else
            {
                stl++;
                string s8 = "select TOP 1 QID from QS where QID in(select TOP " + stl + " QID from QS where EXAM_ID=" + examid + " order by QID) order by QID desc";
                SqlCommand co1 = new SqlCommand(s8, sqlCnt);
                qidnow = (int)co1.ExecuteScalar();
                string sql1 = "select Q from QS where QID=" + qidnow;
                SqlCommand com1 = new SqlCommand(sql1, sqlCnt);
                String ss = (String)com1.ExecuteScalar();
                label3.Text = ss;
                label5.Text = "第" + stl + "题：";
                label6.Text = "题型：";
                string sql2 = "select A from QS where QID=" + qidnow;
                SqlCommand com2 = new SqlCommand(sql2, sqlCnt);
                int a = (int)com2.ExecuteScalar();
                if (a != 0)
                    label6.Text += "单项选择题";
                else
                    label6.Text += "简答题";
            }
        }
    }
}

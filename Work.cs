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
    public partial class Work : Form
    {
        private string InputBox(string Caption, string Hint, string Default)
        {
            Form InputForm = new Form();
            InputForm.MinimizeBox = false;
            InputForm.MaximizeBox = false;
            InputForm.StartPosition = FormStartPosition.CenterScreen;
            InputForm.Width = 250;
            InputForm.Height = 190;
            //InputForm.Font.Name = "宋体";
            //InputForm.Font.Size = 10;
            InputForm.Text = Caption;
            Label lbl = new Label();
            lbl.Text = Hint;
            lbl.Left = 10;
            lbl.Top = 20;
            lbl.Parent = InputForm;
            lbl.AutoSize = true;
            TextBox tb = new TextBox();
            tb.Left = 30;
            tb.Top = 45;
            tb.Width = 160;
            tb.Parent = InputForm;
            tb.Text = Default;
            tb.SelectAll();
            Button btnok = new Button();
            btnok.Left = 30;
            btnok.Top = 80;
            btnok.Parent = InputForm;
            btnok.Text = "确定";
            InputForm.AcceptButton = btnok;//回车响应

            btnok.DialogResult = DialogResult.OK;
            Button btncancal = new Button();
            btncancal.Left = 120;
            btncancal.Top = 80;
            btncancal.Parent = InputForm;
            btncancal.Text = "取消";
            btncancal.DialogResult = DialogResult.Cancel;
            try
            {
                if (InputForm.ShowDialog() == DialogResult.OK)
                {
                    return tb.Text;
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                InputForm.Dispose();
            }
        }
        int is_test = 0;
        int courseID;
        int slt = 1;    //选第一个考试/作业
        public Work()
        {
            InitializeComponent();
            slt = 1;
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            //查询
            string sql = "select TOP 1 EXAM_NAME from EXAM where is_test=" + is_test + " order by EXAM_ID";
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            String s7 = (String)com.ExecuteScalar();
            label2.Text = s7;
        }
        public Work(String s,int cID,int x)
        {
            InitializeComponent();
            is_test = x;    //0 work ,1 exam
            bool iden = WinFormsApp1.login.identity;
            label1.Text = s;
            courseID = cID;
            if (iden == false)
            {
                button1.Enabled = false;
                button1.Visible = false;
            }
            if(is_test==1)
            {
                button1.Text = "创建考试";
                button2.Text = "上一个考试";
                button3.Text = "下一个考试";
            }
            if(is_test == 0)
            {
                button1.Text = "创建作业";
                button2.Text = "上一个作业";
                button3.Text = "下一个作业";
            }
            slt = 1;
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            //查询
            string sql = "select TOP 1 EXAM_NAME from EXAM where is_test="+is_test+" order by EXAM_ID";
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            String s7 = (String)com.ExecuteScalar();
            label2.Text = s7;
        }
        private void Work_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string s1="提示", s2="请输入要创建作业的名称：", s3="";
            string s = InputBox(s1, s2, s3);
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            //找下一个examID
            string sql = "select TOP 1 EXAM_ID from EXAM order by EXAM_ID desc";
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            int examid = (int)com.ExecuteScalar();
            examid = examid + 1;
            //插入
            string sss = "insert into EXAM(EXAM_ID,EXAM_NAME,COURSE_ID,IS_TEST,QNUM) values("+examid+",'"+s+"',"+courseID+","+is_test+",";
            //输入题目
            int cnt = 1;
            while (true)
            {
                //输入题目
                s2 = "输入第" + cnt.ToString() + "道题的题目(单选题请一起输入选项)";
                s1 = "(输入q以结束输入)";
                string qs= InputBox(s1, s2, s3);
                if (String.Equals(qs, "q")) break;
                //输入答案
                s2 = "输入第" + cnt.ToString() + "道题的答案(输入非ABCD即视作简答)";
                s1 = "(输入q以结束输入)";
                string ans = InputBox(s1, s2, s3);
                if (String.Equals(ans, "q")) break;
                //获取QID
                string s6 = "select TOP 1 QID from QS order by QID desc";
                SqlCommand com2 = new SqlCommand(s6, sqlCnt);
                int qid = (int)com2.ExecuteScalar();
                qid = qid + 1;
                //插入题目
                string s5 = "insert into QS(QID,EXAM_ID,Q,A) values(" +qid+","+ examid + ",'" + qs + "',";
                //插入答案
                switch (ans)
                {
                    case "A":
                        s5 = s5 + "1)";
                        break;
                    case "B":
                        s5 = s5 + "2)";
                        break;
                    case "C":
                        s5 = s5 + "3)";
                        break;
                    case "D":
                        s5 = s5 + "4)";
                        break;
                    default:
                        s5 = s5 + "0)";
                        break;
                }
                SqlCommand com1 = new SqlCommand(s5, sqlCnt);
                com1.ExecuteScalar();
                cnt++;
            }
            cnt--;
            sss = sss + cnt + ")";
            SqlCommand co = new SqlCommand(sss, sqlCnt);
            co.ExecuteScalar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (slt <= 1) MessageBox.Show("没有上一条了");
            else
            {
                slt--;
                string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
                SqlConnection sqlCnt = new SqlConnection(ConStr);
                sqlCnt.Open();
                //查询
                string sql = "select TOP 1 EXAM_NAME from EXAM where EXAM_NAME in(select TOP "+slt+ " EXAM_NAME from EXAM where is_test="+is_test+" order by EXAM_ID) order by EXAM_ID desc";
                SqlCommand com = new SqlCommand(sql, sqlCnt);
                String s7 = (String)com.ExecuteScalar();
                label2.Text = s7;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            //查询
            string sql = "select COUNT(*) from EXAM where is_test="+is_test;
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            int coun = (int)com.ExecuteScalar();
            if (slt >=coun) MessageBox.Show("没有下一条了");
            else
            {
                slt++;
                string s8 = "select TOP 1 EXAM_NAME from EXAM where EXAM_NAME in(select TOP " + slt + " EXAM_NAME from EXAM where is_test=" + is_test + " order by EXAM_ID) order by EXAM_ID desc";
                SqlCommand co1 = new SqlCommand(s8, sqlCnt);
                String s9 = (String)co1.ExecuteScalar();
                label2.Text = s9;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            string ConStr = "server=121.196.159.49;database=Course;uid=sa;pwd=02200059Yl";
            SqlConnection sqlCnt = new SqlConnection(ConStr);
            sqlCnt.Open();
            string sql = "select EXAM_ID from EXAM where EXAM_NAME = '"+label2.Text+"'";
            SqlCommand com = new SqlCommand(sql, sqlCnt);
            int eid= (int)com.ExecuteScalar();
            DatabaseDesign.Working wking = new DatabaseDesign.Working(eid,label1.Text,label2.Text,is_test);
            wking.Show();
        }
    }

}


public class FilesSelectDialog
{
    public OpenFileDialog fileDialog;
    //默认打开路径
    public string DirPath = "C:\\";
    public string FilePath;
    public string Title;
    public FilesSelectDialog(string title)
    {
        Title = title;
        fileDialog = new OpenFileDialog();//打开文件对话框 
    }
    public bool Show()
    {
        fileDialog.InitialDirectory = DirPath;//初始化路径
        fileDialog.FilterIndex = 0;//当前使用第二个过滤字符串
        fileDialog.RestoreDirectory = true;//对话框关闭时恢复原目录
        fileDialog.Title = Title;
        if (fileDialog.ShowDialog() == DialogResult.OK)
        {

            FilePath = fileDialog.FileName;
            try
            {
                DirPath = System.IO.Path.GetDirectoryName(FilePath);//更改默认路径为最近打开路径
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
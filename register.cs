using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class register : Form
    {
        int F1=0,F2=0,F3=0,F4=0;
        String regName = "";
        String regPassword = "";
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
            if (F3 == -1)
            {
                MessageBox.Show("两次密码输入不一致");
            }
            else
            {
                if (F1 == 1 && F2 == 1 && F3 == 1 && F4 == 1)
                {
                    MessageBox.Show("注册成功");
                    //上传数据库
                    Close();
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
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            F4 = 1;
            if (radioButton2.Checked)
            {
                label6.Text = "学号";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            regPassword = textBox2.Text;
            F2 = 1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //检测用户名是否重复
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

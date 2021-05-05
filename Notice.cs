using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatabaseDesign
{
    public partial class Notice : Form
    {
        public Notice()
        {
            InitializeComponent();
        }

        private void Notice_Load(object sender, EventArgs e)
        {
            listBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            listBox1.Text = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Services;
using KA4_client.localhost;

namespace KA4_client
{
    public partial class Form3 : Form
    {
        Form1 f = null;
        public Form3( Form1 basis )
        {
            InitializeComponent();
            f = basis;
        }

        public string name;
        public string pass;
        public string stat;

        private void Form3_Load(object sender, EventArgs e)
        {
            nick.Text = name;
            if (stat == "user" || stat == "admin")
            {
                radioButton2.Checked = true;
            }
            else radioButton1.Checked = true;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            f.timer2.Start();
            this.Close();   
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            f.usstat = radioButton2.Text;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            f.usstat = radioButton1.Text;
        }
    }
}

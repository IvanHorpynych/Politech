using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form3 : Form
    {
        int sz;
        public string tmp = "";
        Form1 basic = null;

        public Form3(Form1 _form, string[] buf, int sz_buf)
        {
            InitializeComponent();
            basic = _form;
            int i=0;
            sz = sz_buf;
            for ( i = 0; i < sz_buf; i++)
            {
                RadioButton radio = new RadioButton();
                radio.AutoSize = true;
                radio.Location = new System.Drawing.Point(3, 3 + 25 * i);
                radio.Size = new System.Drawing.Size(80, 17);
                radio.TabIndex = 0;
                radio.UseVisualStyleBackColor = true;
                this.Controls.Add(radio);
                radio.Text = buf[i];
                radio.Name = "radio" + Convert.ToString(i);
            }
           
            but.Location = new System.Drawing.Point(3,3+25*i);
            butext.Location = new System.Drawing.Point(90, 3 + 25 * i);
            but.Text = "submit";
            butext.Text = "cancel";
            this.Controls.Add(butext);
            this.Size = new System.Drawing.Size(200, 28*i+70);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void but_Click(object sender, EventArgs e)
        {
            foreach (Control con in this.Controls)
            {
                RadioButton box = con as RadioButton;
                if (box != null && box.Checked)
                {
                    basic.temp = box.Text;
                    basic.timer3.Enabled = true;
                    this.Close();
                    
                }
            }
        }

        private void butext_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class Form2 : Form
    {
        RichTextBox mainform = new Form1().notes;
        int CurrPos = 0;

        public Form2( RichTextBox _form )
        {
            InitializeComponent();
            mainform = _form;
        }

       
        

       

        private void button2_Click(object sender, EventArgs e)
        {
            CurrPos = 0;
            while ( CurrPos < mainform.Text.Length )
                button1_Click(sender, e);
            CurrPos = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int j = 0;
            int i = CurrPos;
            bool pres = false;
                              
                    while (i < mainform.Text.Length)
                    {

                        while ((i + j) < mainform.Text.Length && j < keyword.Text.Length)
                        {
                            
                            if ((checkBox2.Checked && mainform.Text[i + j].ToString().ToUpper() == keyword.Text[j].ToString().ToUpper()) || mainform.Text[i + j] == keyword.Text[j] || mainform.Text[i + j] == '\n')
                                j++;
                            else
                                break;
                        }
                        if (j == keyword.Text.Length)
                        {
                            mainform.SelectionStart = i;
                            mainform.SelectionLength = j;
                            mainform.SelectionBackColor = Color.Gray;
                            mainform.SelectionColor = Color.White;
                            mainform.SelectionLength = 0;
                            pres = false;
                            CurrPos = i + j;
                            break;
                        }
                                               
                        i += 1;
                        j = 0;
                        CurrPos = i;
                    }
                
            

            if (CurrPos == mainform.Text.Length)
            {
                MessageBox.Show("End of file reached", "!"); 
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                checkBox2.Checked = false;
            else
                checkBox2.Checked = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                checkBox1.Checked = false;
            else
                checkBox1.Checked = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CurrPos = 0;
            mainform.SelectionStart = 0;
            mainform.SelectionLength = mainform.Text.Length;
            mainform.SelectionBackColor = Color.White;
            mainform.SelectionColor = Color.Black;
            mainform.SelectionLength = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CurrPos = 0;
            button1_Click(sender, e);
        }
    }
}

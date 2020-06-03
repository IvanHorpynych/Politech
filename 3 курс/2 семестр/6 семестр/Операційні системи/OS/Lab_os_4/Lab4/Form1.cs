using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OS_Lab4
{
    public partial class Form1 : Form
    {
        BalancedTree bt;

        public Form1()
        {
            InitializeComponent();
            bt = new BalancedTree(this.panel1);

            panel1.AutoScroll = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
                        
            if (textBox1.Text.Length > 1 || (!"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".Contains(textBox1.Text)))
            { 
                MessageBox.Show("Enter please 1 letter", "Error!"); 
                return; 
            }
            richTextBox1.AppendText("Adding ID '" + textBox1.Text + "'\n");
            if (bt.Search(textBox1.Text) == null)
            {
                bt.Add(textBox1.Text);
                richTextBox1.AppendText("-----------------------------\n");
                richTextBox1.ScrollToCaret();
                bt.Print();
            }
            else
            {
                richTextBox1.AppendText("The Tree allready exists ID '" + textBox1.Text + "' \n");
                richTextBox1.ScrollToCaret();
            }
                textBox1.Clear();
                textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bt.RightRotate();
            richTextBox1.AppendText("-----------------------------\n");
            bt.Print();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bt.LeftRotate();
            richTextBox1.AppendText("-----------------------------\n");
            bt.Print();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText("-----------------------------\n");
            richTextBox1.AppendText("Removing ID '" + textBox1.Text + "'\n");
            if (bt.Search(textBox1.Text) != null)
            {
                bt.Delete(textBox1.Text);
                richTextBox1.AppendText("-----------------------------\n");
                bt.Print();
            }
            else
            {
                richTextBox1.AppendText("The Tree doesn't exist ID '" + textBox1.Text + "' \n");
                richTextBox1.AppendText("-----------------------------\n");
            }
            richTextBox1.ScrollToCaret();
            textBox1.Clear();
            textBox1.Focus();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            String text = bt.GetSearchPath(textBox1.Text);
            if (text != null)            
                richTextBox1.AppendText("ID has been found.\nLink:" + text + "\n");            
            else
                richTextBox1.AppendText("ID has not been found. \n");
            richTextBox1.AppendText("-----------------------------\n");
            richTextBox1.ScrollToCaret();
        }
    }
}

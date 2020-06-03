using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZI_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            BinaryWriter inp;
            try
            {
                inp = new BinaryWriter(new FileStream(path.Text, FileMode.Open));
                inp.Seek(40, SeekOrigin.Begin);
                inp.Write(code.Text.ToCharArray());
                inp.Close();
            }
            catch (IOException)
            {
                error1.Visible = true;
                error1.Text = "File doesn't exist!";
            }
            catch
            {
                error1.Visible = true;
                error1.Text = "Error in Writing data";
            }
           
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream outp;
            string s = "";
            char ch = ' ';
            int i;
            try
            {
                outp = new FileStream(path2.Text, FileMode.Open);
                outp.Seek(40, SeekOrigin.Begin);
                do
                {
                    i = outp.ReadByte();
                    ch = (char)i;
                    if ((i >= 33 && i <= 122) || ch == ' ')
                        s += ch.ToString();
                    else break;
                } while ( i != -1 );
                if (s != "")
                    decode.Text = s;
                else decode.Text = "There's no secret text";
                error2.Visible = false;
                outp.Close();
            }

            catch (IOException)
            {
                error2.Visible = true;
                error2.Text = "File doesn't exist!";
                decode.Text = "";
            }

            catch
            {
                error2.Visible = true;
                error2.Text = "Error in reading data";
                decode.Text = "";
            }
        }

        private void path_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s = "";
             
            int len = Convert.ToInt16 (Len.Text);
            for (int i = 0; i < len; i++)
                s += "a";
            BinaryWriter inp;
            try
            {
                inp = new BinaryWriter(new FileStream(test.Text, FileMode.Open));
                inp.Seek(40 , SeekOrigin.Begin);
                inp.Write(s.ToCharArray() );
                inp.Close();
            }
            catch (IOException)
            {
                error1.Visible = true;
                error1.Text = "File doesn't exist!";
            }
            catch
            {
                error1.Visible = true;
                error1.Text = "Error in Writing data";
            }
           // Len.Text = Convert.ToString(len + 12);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void test_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

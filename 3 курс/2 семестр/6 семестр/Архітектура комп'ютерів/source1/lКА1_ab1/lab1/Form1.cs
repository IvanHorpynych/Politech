using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        string[,] alph;
        public Form1()
        {
            InitializeComponent();
            alph = new string[2, 30];
            string text = System.IO.File.ReadAllText("alph.txt", System.Text.Encoding.GetEncoding(1251));
            
            
            try
            {
                int count = 0;
                int i = 0;
                while ( i < 2 ) 
                {
                    int j = 0;

                    while (text[count] != '.')
                    {
                        string buf = text[count].ToString();
                        count++;
                        while (text[count] != '_')
                        {
                            buf += text[count].ToString();
                            count++;
                        }
                        count++;
                        alph[i, j] = buf;
                        buf = "";
                        j++;
                    }
                    count += 3;
                   // MessageBox.Show(text[count].ToString());
                    
                    i++;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //for (int i = 0; i < 30; i++)
              //  notes.Text += alph[0, i] + "_:_" + alph[1, i] + "\n";
        }

        bool modified = false;
        string filename = "";
        public string temp = "";
        DateTime time = DateTime.Now;
        string[] lev_buf;
        int buf_size = 0;
        

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (modified)
            {
                switch (MessageBox.Show("Save?", Text, MessageBoxButtons.YesNoCancel))
                {
                    case (DialogResult.Yes):
                        saveToolStripMenuItem_Click(sender, e);
                        filename = "";
                        break;
                    case (DialogResult.No):
                        filename = "";
                        break;
                    case (DialogResult.Cancel):
                        break;
                }
                
            }
            if ( modified == false )
            {
                Text = "";
                notes.Text = "";
                modified = false;
            }
        }

        private void notes_TextChanged(object sender, EventArgs e)
        {
            modified = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (modified)
                switch (MessageBox.Show("Current file was changed. Save changes?", Text, MessageBoxButtons.YesNoCancel))
                {
                    case (DialogResult.Yes):
                        saveToolStripMenuItem_Click(sender, e);
                        filename = "";
                        modified = false;
                        break;
                    case (DialogResult.No):
                        filename = "";
                        modified = false;
                        break;
                    case (DialogResult.Cancel):
                        break;
                }

           if ( modified == false )
            if (openf.ShowDialog() == DialogResult.OK)
            {
                filename = openf.FileName;
                notes.LoadFile(filename);
                Text = openf.FileName;
                modified = false;

            }
            
        }

        private void savaAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveas.ShowDialog() == DialogResult.OK)
            {
                filename = saveas.FileName;
                notes.SaveFile(filename, RichTextBoxStreamType.RichText);
                modified = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == "")
                savaAsToolStripMenuItem_Click(sender, e);
            else
            if (modified)
            {
                notes.SaveFile(filename, RichTextBoxStreamType.RichText);
                modified = false;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temp = notes.SelectedText; 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = DateTime.Now;
            currtime.Text = time.ToString();
            
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temp = notes.SelectedText;
            notes.SelectedText = "";
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string buf = "";

            for (int i = 0; i < notes.SelectionStart; i++)
                buf += notes.Text[i];
            buf += temp;
            for (int i = notes.SelectionStart; i < notes.Text.Length; i++)
                buf += notes.Text[i];
            notes.Text = buf;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (modified)
                switch (MessageBox.Show("Current file was changed. Save changes?", Text, MessageBoxButtons.YesNoCancel))
                {
                    case (DialogResult.Yes):
                        saveToolStripMenuItem_Click(sender, e);
                        //filename = "";
                        modified = false;
                        break;
                    case (DialogResult.No):
                        filename = "";
                        modified = false;
                        break;
                    case (DialogResult.Cancel):
                        break;
                }

            if ( !modified )
                this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Point p = new Point();
            p = notes.PointToClient(p);
            int _x = p.X + Form1.MousePosition.X, _y = p.Y + Form1.MousePosition.Y;
            if (_x < 0)
                _x = 0;
            if (_y < 0)
                _y = 0;

            curs.Text = _x.ToString() + " : " + _y.ToString();
        }

        public void IncReg()
        {
            // for ( int i = 0; i < mainform.Text.
        }


        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 subform = new Form2 (notes);
            subform.Show();
           /*
                notes.SelectionStart = 0;
                notes.SelectionLength = 2;
                notes.SelectionBackColor = Color.Blue;
                notes.SelectionColor = Color.White;
                notes.SelectionLength = 0;
            */
        }

        private string SearchLet ( string buf )
        {
            for (int i = 0; i < 30; i++)
                if (buf == alph[0, i].ToLower())
                    return alph[1, i];
            return "";
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string buf = notes.SelectedText;
            string cyr_buf = "";
            int beg = notes.SelectionStart;
            int ed = beg + notes.SelectionLength;
            int i = 0;
            notes.Text += "\n";
            bool fl = false;
            while (i < buf.Length)
            {
                string tmp = "";
                for (int j = 2; j > 0; j--)
                {
                    tmp = "";
                    for ( int z = 0; (i+z) < buf.Length && z < j; z++)
                        tmp += buf[i + z];
                    
                    if ( SearchLet(tmp) != "" )
                    {
                        cyr_buf += SearchLet(tmp);
                        i += j;
                        fl = true;
                        break;
                    }
                }
                if ( !fl )
                {
                    cyr_buf += buf[i];
                    i++;
                    
                }
                fl = false;
            }
            buf = "";
            for (i = 0; i < beg; i++)
                buf += notes.Text[i];
            buf += cyr_buf;

            for (i = ed; i < notes.Text.Length; i++)
                buf += notes.Text[i];
            notes.Text = buf;
            notes.SelectionStart = beg;
        }

        private void countNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            int count = 0;

            while (i < notes.Text.Length)
            {
                
                    if (notes.Text[i] >= '0' && notes.Text[i] <= '9')
                    {
                        while ( i < notes.Text.Length && ((notes.Text[i] >= '0' && notes.Text[i] <= '9') || notes.Text[i] == '.'))
                            i++;
                        count++;
                    }
                    else
                        i++;
                
            }

            MessageBox.Show("Numbers' amount is " + count.ToString());
        }

        private void copyToBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < buf_size; i++)
                if (lev_buf[i] == notes.SelectedText)
                    return;
            
            string[] tmp = lev_buf;
            
            lev_buf = null;
            buf_size++;
            
            lev_buf = new string[buf_size];
            for ( int i = 0; i<buf_size -1; i++ )
                lev_buf[i] = tmp[i];
            lev_buf[buf_size - 1] = notes.SelectedText;

        }

        private void pasteFromBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (buf_size == 0)
            {
                MessageBox.Show("Buffer is empty","warning");
                return;
            }
            Form3 panel = new Form3(this, lev_buf, buf_size);
            panel.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void clearBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < buf_size; i++)
                lev_buf[i] = "";
            buf_size = 0;
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon1.Visible = true;
                Hide();
            }
        }

        private void menuMaximize_Click(object sender, System.EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void menuMinimize_Click(object sender, System.EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Activate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Enabled = false;
            pasteToolStripMenuItem_Click(sender, e);
        }
    }
}

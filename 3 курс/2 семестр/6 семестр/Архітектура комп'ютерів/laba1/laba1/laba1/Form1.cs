using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int k = 0;
        DateTime time = DateTime.Now;
        string t;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified)
                switch (MessageBox.Show("Текстовий файл було змінено. Зберегти?", Text, MessageBoxButtons.YesNo))
                {
                    case (DialogResult.Yes):
                        зберегтиФайлЯкToolStripMenuItem_Click(sender, e);
                        break;
                    case (DialogResult.No):
                        break;
                }
            this.Close();
        }

        private void новийФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            if (richTextBox1.Modified)
                switch (MessageBox.Show("А що робити з файлом, з яким ви вже працюєте. Зберегти?", Text, MessageBoxButtons.YesNo))
                {
                    case (DialogResult.Yes):
                        зберегтиФайлЯкToolStripMenuItem_Click(sender, e);
                        break;
                    case (DialogResult.No):
                        break;
                }
            richTextBox1.Text = "";
        }

        private void відкритиФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            if (richTextBox1.Modified)
                switch (MessageBox.Show("А що робити з файлом, з яким ви вже працюєте. Зберегти?", Text, MessageBoxButtons.YesNo))
                {
                    case (DialogResult.Yes):
                        зберегтиФайлЯкToolStripMenuItem_Click(sender, e);
                        break;
                    case (DialogResult.No):
                        break;
                }
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "Текстові файли (*.txt)|*.txt|Всі файли (*.*)|*.*";
            OpenFileDialog1.FilterIndex = 2;
            OpenFileDialog1.RestoreDirectory = true;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(OpenFileDialog1.FileName);
            }
        }

        private void зберегтиФайлЯкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Текстові файли (*.txt)|*.txt|Всі файли (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int j = 0;
            int i = k;
            bool pres = false;
            while (i < richTextBox1.Text.Length)
            {
                while ((i + j) < richTextBox1.Text.Length && j < textBox1.Text.Length)
                {
                    if ((radioButton2.Checked && richTextBox1.Text[i + j].ToString().ToUpper() == textBox1.Text[j].ToString().ToUpper()) || richTextBox1.Text[i + j] == textBox1.Text[j] || richTextBox1.Text[i + j] == '\n')
                        j++;
                    else
                        break;
                }
                if (j == textBox1.Text.Length)
                {
                    richTextBox1.SelectionStart = i;
                    richTextBox1.SelectionLength = j;
                    richTextBox1.SelectionBackColor = Color.Gray;
                    richTextBox1.SelectionColor = Color.White;
                    richTextBox1.SelectionLength = 0;
                    pres = false;
                    k = i + j;
                    break;
                }
                i += 1;
                j = 0;
                k = i;
            }
            if (k == richTextBox1.Text.Length)
            {
                MessageBox.Show("А ми то вже дісталися кінця файлу...", "!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            k = 0;
            while (k < richTextBox1.Text.Length)
                button1_Click(sender, e);
            k = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            k = 0;
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = richTextBox1.Text.Length;
            richTextBox1.SelectionBackColor = Color.White;
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionLength = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = DateTime.Now;
            toolStripStatusLabel1.Text = time.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Point p = new Point();
            p = richTextBox1.PointToClient(p);
            int _x = p.X + Form1.MousePosition.X, _y = p.Y + Form1.MousePosition.Y;
            if (_x < 0)
                _x = 0;
            if (_y < 0)
                _y = 0;
            toolStripStatusLabel2.Text = _x.ToString() + " : " + _y.ToString();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon1.Visible = true;
                Hide();
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void транслюванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s,k="";
 
            s = richTextBox1.SelectedText;
            for (int i = 0; i < s.Length; i++)
                switch (s[i])
                {
                    case ('A'): k = k+"А";break;
                    case ('B'): k = k + "Б";break;
                    case ('C'): k = k + "Ц";break;
                    case ('D'): k = k + "Д";break;
                    case ('E'): k = k + "Е";break;
                    case ('F'): k = k + "Ф";break;
                    case ('G'): k = k + "Г";break;
                    case ('H'): k = k + "Х";break;
                    case ('I'): k = k + "І";break;
                    case ('J'): k = k + "Й";break;
                    case ('K'): k = k + "К";break;
                    case ('L'): k = k + "Л";break;
                    case ('M'): k = k + "М";break;
                    case ('N'): k = k + "Н";break;
                    case ('O'): k = k + "О";break;
                    case ('P'): k = k + "П";break;
                    case ('Q'): k = k + "КВ";break;
                    case ('R'): k = k + "Р";break;
                    case ('S'): k = k + "С";break;
                    case ('T'): k = k + "Т";break;
                    case ('U'): k = k + "У";break;
                    case ('V'): k = k + "В";break;
                    case ('W'): k = k + "В";break;
                    case ('X'): k = k + "ІКС";break;
                    case ('Y'): k = k + "І";break;
                    case ('Z'): k = k + "З";break;
                    case ('a'): k = k + "а";break;
                    case ('b'): k = k + "б";break;
                    case ('c'): k = k + "ц";break;
                    case ('d'): k = k + "д";break;
                    case ('e'): k = k + "е";break;
                    case ('f'): k = k + "ф";break;
                    case ('g'): k = k + "г";break;
                    case ('h'): k = k + "х";break;
                    case ('i'): k = k + "і";break;
                    case ('j'): k = k + "й";break;
                    case ('k'): k = k + "к";break;
                    case ('l'): k = k + "л";break;
                    case ('m'): k = k + "м";break;
                    case ('n'): k = k + "н";break;
                    case ('o'): k = k + "о";break;
                    case ('p'): k = k + "п";break;
                    case ('q'): k = k + "кв";break;
                    case ('r'): k = k + "р";break;
                    case ('s'): k = k + "с";break;
                    case ('t'): k = k + "т";break;
                    case ('u'): k = k + "у";break;
                    case ('v'): k = k + "в";break;
                    case ('w'): k = k + "в";break;
                    case ('x'): k = k + "ікс";break;
                    case ('y'): k = k + "і";break;
                    case ('z'): k = k + "з";break;
                    default:    k = k + s[i];break;
                }
            richTextBox1.SelectedText = k;
        }

        private void вирізатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t = richTextBox1.SelectedText;
            richTextBox1.SelectedText = "";
        }

        private void копіюватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t = richTextBox1.SelectedText;
        }

        private void вставитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = t;
        }

        private void вирізатиДоБагаторівневогоБуфераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t = richTextBox1.SelectedText;
            richTextBox1.SelectedText = "";
            listBox1.Items.Add(t);
        }

        private void копіюватиДоБагаторівневогоБуфераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t = richTextBox1.SelectedText;
            listBox1.Items.Add(t);
        }

        private void очиститиБагаторівневийБуферToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = Convert.ToString(listBox1.SelectedItem);
        }

        private void порахуватиКількістьЦифрУТекстіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool x = false ;
            int k1 = 0;
            for (int i = 0; i < richTextBox1.Text.Length; i++ )
            {
                if (x)  {if ((richTextBox1.Text[i] != '0') &
                             (richTextBox1.Text[i] != '1') &
                             (richTextBox1.Text[i] != '2') &
                             (richTextBox1.Text[i] != '3') &
                             (richTextBox1.Text[i] != '4') &
                             (richTextBox1.Text[i] != '5') &
                             (richTextBox1.Text[i] != '6') &
                             (richTextBox1.Text[i] != '7') &
                             (richTextBox1.Text[i] != '8') &
                             (richTextBox1.Text[i] != '9')) { x=false; } }
                else  {if ((richTextBox1.Text[i] == '0') |
                           (richTextBox1.Text[i] == '1') |
                           (richTextBox1.Text[i] == '2') |
                           (richTextBox1.Text[i] == '3') |
                           (richTextBox1.Text[i] == '4') |
                           (richTextBox1.Text[i] == '5') |
                           (richTextBox1.Text[i] == '6') |
                           (richTextBox1.Text[i] == '7') |
                           (richTextBox1.Text[i] == '8') |
                           (richTextBox1.Text[i] == '9')) { k1++; x = true; }
                }
            }
                MessageBox.Show("У тексті "+k1+" чисел", "!");
        }
    }
}
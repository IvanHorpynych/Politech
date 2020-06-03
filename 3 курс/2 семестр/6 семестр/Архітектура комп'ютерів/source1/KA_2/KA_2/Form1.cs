using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace KA_2
{
    public partial class Form1 : Form
    {
        Hashtable ht;
        Mutex mt, mt2, mt1, mt3, mt4;
        bool ext = true, butclick = false;
        int len = 0, cnt, cnt_bit;
        DateTime dt1;
        TimeSpan ts1, ts2;
        string[,] search;
        Thread th_main, th1, th2, th3;
        ListBox lst1, lst2, lst3, lst4;

        public Form1()
        {
            InitializeComponent();
            
            mt = new Mutex();  //for HashTasble
            mt1 = new Mutex(); //for lst1, lst2
            mt2 = new Mutex(); //for ListBox1
            mt3 = new Mutex(); //for lst3, lst4
            mt4 = new Mutex();
            ts1 = new TimeSpan();
            ts2 = new TimeSpan();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ht = new Hashtable();
            lst1 = new ListBox();
            lst2 = new ListBox();
            lst1.Visible = false;
            lst2.Visible = false;
            lst3 = new ListBox();
            lst4 = new ListBox();
            lst3.Visible = false;
            lst4.Visible = false;

            int count = 0;
            foreach (string str in listBox1.Items)
                fileNames.Text += count++.ToString() + "\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {            
            try
            {

                if (opf.ShowDialog() == DialogResult.OK)
                {
                    FPath.Text = opf.FileName;
                    mt2.WaitOne();
                    if (!listBox1.Items.Contains(FPath.Text))
                    {
                        fileNames.Text += listBox1.Items.Count.ToString() + "\n";
                        listBox1.Items.Add(FPath.Text);
                        
                    }
                    mt2.ReleaseMutex();
                    if (th1 == null)
                    {
                        FPath.Text = "";
                        return;
                    }
                    butclick = true;
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.ToString();
            }
        }

        private int SearchWords( string fname )
        {
           
            try
            {

                for (int i = 0; i < len; i++)
                    if (ht[fname] != null && ((string[])ht[fname]).ToList().Contains(search[0, i]))
                    {
                        
                        search[1, i] = "1";
                        
                    }
            }
            catch (Exception ex)
            {
                richTextBox1.Text += ex.ToString();
            }
           
            int mul = 1;
           
            for (int j = 0; j < len; j++)
            {
                mul *= Convert.ToInt16(search[1, j]);
                search[1, j] = "0";
            }
            
            return mul;
        }

        private void AddToHash(string fname)
        {
           
            string s = System.IO.File.ReadAllText(fname, Encoding.Default);
            s = s.ToUpper();
            string[] WordsArr = s.Split(',', '.', '!', '?', '-', ';', '\r', '\n', ':', '—', ' ', ')', '(', '\"', '/');
            ht.Add(ht.Count.ToString(), WordsArr);
        }

        private void PrepareThread()
        {  

            foreach ( string str in listBox1.Items )
            {
                richTextBox1.Text += "started adding\n";
                mt.WaitOne();
                AddToHash(str);
                DateTime dt = DateTime.Now;
                lst2.Items.Add(dt);
                mt.ReleaseMutex();
            }

            while ( ext )
            {
                if ( butclick )
                {
                    mt.WaitOne();
                    AddToHash(FPath.Text);
                    DateTime dt = DateTime.Now;
                    lst2.Items.Add(dt);
                    mt.ReleaseMutex();
                    FName.Text = "";
                    FPath.Text = "";
                    butclick = false;
                    Thread.Sleep(100);
                }
                Thread.Sleep(100);
            }

            richTextBox1.Text += "prep fin\n";
            but_ser.Enabled = true;
        }

        private void SearchThread()
        {
            richTextBox1.Text += "search\n";
            cnt = 0;
            string _fname = "";
            
           while ( ext )
           {
               mt.WaitOne();
               if (cnt >= ht.Count)
               {
                   mt.ReleaseMutex();
                   Thread.Sleep(500);
                   continue;
               }
               mt.ReleaseMutex();

               DateTime dt = DateTime.Now;

               mt1.WaitOne();
                bool res1 = ( SearchWords(cnt.ToString()) > 0 );
                lst1.Items.Add(res1);
                ts1 = DateTime.Now - (DateTime)lst2.Items[cnt];
                lst2.Items[cnt] = ts1;
               mt1.ReleaseMutex();
                Thread.Sleep(500);

              
                cnt++;
                              
                //richTextBox1.Text += _fname + "\n";
                Thread.Sleep(100);
            }
            richTextBox1.Text += "search fin\n";
            but_ser.Enabled = true;
        }

        private void BitSearch()
        {
            cnt_bit = 0;
            while (ext)
            {
                mt2.WaitOne();
                if (cnt_bit >= listBox1.Items.Count)
                {
                    mt2.ReleaseMutex();
                    Thread.Sleep(500);
                    continue;
                }
                //MessageBox.Show(cnt_bit.ToString(), "list count " + listBox1.Items.Count.ToString());
                
                string fname = listBox1.Items[cnt_bit].ToString();
                mt2.ReleaseMutex();
                DateTime dt = DateTime.Now;
                string s = System.IO.File.ReadAllText(fname, Encoding.Default);
                s = s.ToUpper();
                string[] WordsArr = s.Split(',', '.', '!', '?', '-', ';', '\r', '\n', ':', '—', ' ', ')', '(', '\"', '/');
                int j = 0;
                bool[] resArr = new bool[len];
                BitArray BA = new BitArray(resArr);
                string[] unrep = new string[WordsArr.Length];

                
                for (j = 0; j < len; j++)
                {
                    BA[j] = (BA[j] || WordsArr.ToList().Contains(search[0, j]));
                    
                   // MessageBox.Show(WordsArr.ToList().Contains(search[0, j]).ToString(), search[0, j]);
                }
                    
                bool add_buf = true;

                for (j = 0; j < len; j++)  
                    add_buf = (BA[j] && add_buf);
                mt3.WaitOne();
                lst3.Items.Add(add_buf);
                
                ts2 = DateTime.Now - dt;
                lst4.Items.Add(ts2);
                mt3.ReleaseMutex();

                cnt_bit++;
            }
        }

        private void ResThread()
        {
            int i = 0, j = 0;
            while (ext)
            {
                mt1.WaitOne();
                try
                {
                    if (i < lst1.Items.Count)
                    {
                        result_box.Text += lst1.Items[i].ToString() + "\n";
                        time_box.Text += lst2.Items[i].ToString() + "\n";
                        // time_box.Text += i.ToString() + "\n";
                        i++;
                    }
                    mt1.ReleaseMutex();
                    mt3.WaitOne();
                    if (j < lst3.Items.Count)
                    {
                        bit_res.Text += lst3.Items[j].ToString() + "\n";
                        bit_time.Text += lst4.Items[j].ToString() + "\n";
                        // bit_time.Text += j.ToString() + "\n";
                        j++;
                    }
                }
                catch (Exception ex)
                {
                    richTextBox1.Text += ex.ToString();
                }
                mt3.ReleaseMutex();
                Thread.Sleep(500);
                    
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            ext = true;

                th1 = new Thread(new ThreadStart(PrepareThread));
                th2 = new Thread(new ThreadStart(SearchThread));
                th3 = new Thread(new ThreadStart(BitSearch));
                th_main = new Thread(new ThreadStart(ResThread));
                try
                {
                    th1.Start();
                    th2.Start();
                    th3.Start();
                    th_main.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ext = false;
            button2.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            len = 0;
            
            srcline.Text += " ";
            
            string[] WordsArr = srcline.Text.Split(' ', ',','\0','\n');
            string[] unrep = new string[WordsArr.Length];
            
           // len = WordsArr.Length;
            

            for (int i = 0; i < WordsArr.Length-1; i++)
                WordsArr[i] = WordsArr[i].ToUpper();

            for (int i = 0; i < WordsArr.Length-1; i++)
                if (!unrep.ToList().Contains(WordsArr[i]))
                {
                    unrep[len] = WordsArr[i];
                    len++;
                }
            
            search = new string[2, len];

            for (int i = 0; i < len; i++)
            {
                search[0, i] = unrep[i];
                search[1, i] = "0";
            }

            mt.WaitOne();
            cnt = 0;
            mt.ReleaseMutex();

            mt2.WaitOne();
            cnt_bit = 0;
            mt2.ReleaseMutex();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(listBox1.Items.listBox1.SelectedIndex));
        }

    }
}

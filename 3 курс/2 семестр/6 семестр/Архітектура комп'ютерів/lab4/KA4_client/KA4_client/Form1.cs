using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Services;
using System.Windows.Forms;
using KA4_client.localhost;

namespace KA4_client
{
   
    public partial class Form1 : Form
    {
        localhost.WebService1 service;
        string login, passwd;
        Form2 frm;
        Form3 usinfo;
        string status;
        public string uslogin;
        public string usstat;

        public Form1()
        {
            InitializeComponent();
            frm = new Form2();
            usinfo = new Form3(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            service = new localhost.WebService1();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            
            string str = service.Autorization(LogName.Text, LogPass.Text);
            login = LogName.Text;

            string[] buf = str.Split(' ',',','!');
            if (buf[0] == "hello")
            {
                button3.Enabled = true;
                curr_msg.Enabled = true;
                history.Text += DateTime.Now.ToString() + " : " + LogName.Text + " logged in\n";
                service.WriteDownToJourn(DateTime.Now.ToString() + " : " + LogName.Text + " logged in\n");

                status = service.GetStatus(login);
                if (status == "admin")
                    button5.Visible = true;
                button1.Enabled = false;
                button2.Enabled = false;
                ext.Enabled = true;
            } else
                MessageBox.Show(str);
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            passwd = LogPass.Text;
            string str = service.Register(LogName.Text + ";" + LogPass.Text + ";");
            //login = LogName.Text;
            MessageBox.Show(str);
            //button1.Enabled = false;
            //button2.Enabled = false;
            //ext.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msg = curr_msg.Text;
            string buf = login + "    " + DateTime.Now.ToString() + "\n" + msg + "\n" + "-----------------------------------------------" + "\n";
            all_messages.Text += buf;
            service.AddMsg(buf);
            curr_msg.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            string str = service.Refresh(@"D:\session.txt");
            all_messages.Text = str;
           
            str = "";
            str = service.refreshUsers();
            online.Text = str;

            str = service.Refresh(@"D:\journal.txt");
            history.Text = str;
        }

        private void ext_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;
            ext.Enabled = false;
            listBox1.Visible = false;
            button5.Visible = false;
            LogName.Text = "";
            LogPass.Text = "";

            if (login == "")
                return;
            string s = service.UserLogOff(login);
            history.Text += DateTime.Now.ToString() + " : " + login + " logged off\n";
            service.WriteDownToJourn(DateTime.Now.ToString() + " : " + login + " logged off\n");
            online.Text = s;
            login = "";
            passwd = "";
            status = "";
            listBox1.Visible = false;
            button5.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (frm.Created)
                frm.Focus();
            else
            {
                frm = new Form2();
                frm.Show();
            }
            frm.richTextBox1.Text = service.GetHistory();
        }

        private void Form_Close(object sender, FormClosingEventArgs e)
        {
            ext_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            listBox1.Visible = !listBox1.Visible;
            if (listBox1.Visible == true)
            {
                string str = service.GetRegUsers();
                string[] users = str.Split(';');
                listBox1.Items.Clear();
                for (int i = 0; i < users.Length; i++)
                    listBox1.Items.Add(users[i]);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            uslogin = listBox1.SelectedItem.ToString();
            usstat = service.GetStatus(uslogin);
           
            
          
            if (usinfo.Created)
                usinfo.Focus();
            else
            {
                usinfo = new Form3(this);
                usinfo.name = uslogin;
                usinfo.stat = usstat;
                usinfo.Show();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            service.ChangeStatus(uslogin, usstat);
        }
         
    }
}

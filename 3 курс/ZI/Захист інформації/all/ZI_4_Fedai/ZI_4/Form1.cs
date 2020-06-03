using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace ZI_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection();
        private BindingSource bindingSorce = new BindingSource();

        private void button1_Click(object sender, EventArgs e)
        {
            string strProvider = "Server=localhost;Port=3306;Uid=" + Login.Text + ";password=" + Pass.Text + ";";
            connection.ConnectionString = strProvider;

           try
            {
                connection.Open();
                Cons.Text += "Current user " + Login.Text + "\n";
                MessageBox.Show("OK", "Connection to MySQL");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error!");
            }
            /*
            
            MySqlDataReader mysqlReader = mysqlCmd.ExecuteReader();
            while (mysqlReader.Read())
            {
                //for (int i = 0; i < mysqlReader.FieldCount; i++)
                //res.Text += mysqlReader["text"].ToString() + " , ";
            }*/
           
        }

        private void res_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Close();
            Login.Text = "";
            Pass.Text = "";
           // UsName.Text = "";
           // UsPWD.Text = "";
            //UsPriv.Text = "";
            //DBname.Text = "";
            //Table.Text = "";
            dataGridView1.DataSource = null;
            Cons.Text += "BYE\n"; 
        }

        private void Cons_TextChanged(object sender, EventArgs e)
        {

        }

        private void butgrant_Click(object sender, EventArgs e)
        {
            string CmdStr = "grant " + UsPriv.Text + " on " + DBname.Text + "." + Table.Text +" to " + UsName.Text + "@localhost identified by '" + UsPWD.Text + "';";
            Cons.Text +=  CmdStr + "\n";
            try
            {
                MySqlCommand mysqlCmd = new MySqlCommand(CmdStr, connection);
               
                MySqlDataReader mysqlReader = mysqlCmd.ExecuteReader();

                while (mysqlReader.Read())
                 {
                    for (int i = 0; i < mysqlReader.FieldCount; i++)
                        Cons.Text += mysqlReader["text"].ToString() + " , ";
                 }
                MessageBox.Show("OK", "Grant");
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error!");
            }
           
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (CMDline.TextLength > 0)
                OK.Enabled = true;
            else OK.Enabled = false;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                // command = conn.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = CMDline.Text;
                Cons.Text += CMDline.Text + "\n";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                adapter.Fill(myDataSet);
                
                bindingSorce.DataSource = myDataSet.Tables[0];
                dataGridView1.DataSource = bindingSorce;
                MessageBox.Show("OK!", "OK!");              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string CmdStr = "revoke " + UsPriv.Text + " on " + DBname.Text + "." + Table.Text +" from " + UsName.Text + "@localhost;";
            Cons.Text += CmdStr + "\n";
           try
            {
                MySqlCommand mysqlCmd = new MySqlCommand(CmdStr, connection);

                MySqlDataReader mysqlReader = mysqlCmd.ExecuteReader();

                MessageBox.Show("OK", "Revoke");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error!");
            }
        }
    }
}

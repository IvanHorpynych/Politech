using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OS_2
{
    public partial class Form1 : Form
    {
        Thread th1, th2;
        static int[,] money;    //primal amount of all money in the bank
        static bool fl1, fl2;
        static int turn, value, val;
        static int[] nom = { 100, 50, 25, 10, 5, 2, 1 };    //nominals
        static int[,] res = new int[2, 7];  //resultant money for user
        bool ext = true, butclick = true;

        public Form1()
        {
            InitializeComponent();
            Random rand = new Random();
            money = new int[2, 7];

            for (int i = 0; i < nom.Length; i++)
            {
                money[0, i] = nom[i];
                res[0, i] = nom[i];
                money[1, i] = rand.Next(1, 30);
            }
            fl1 = false;
            fl2 = false;
            for (int i = 0; i < nom.Length; i++)
                richTextBox1.Text += money[0, i].ToString() + " : " + money[1, i].ToString() + "\n";
            turn = 0;
            value = -1;

            for (int i = 0; i < 24; i++)
                label4.Text += "|\n";
        }

        private void ExchangeMoney()
        {
            int i = 1;
            int rest = 0;
            int buf = value;

            foreach (Control con in Controls)
            {
                CheckBox box = con as CheckBox;
                
                if ( box != null )
                {
                    if (box.Checked)
                    {
                        int amount = 0;
                        int j = -1;
                        foreach (Control rtb in Controls)
                        {
                            RichTextBox rcb = rtb as RichTextBox;
                            if (rcb != null)
                            {
                                if (i == j)
                                {
                                    try
                                    {
                                        amount = Convert.ToInt16(rcb.Text);
                                    }
                                    catch
                                    {
                                        amount = 0;
                                    }
                                }
                                j++;
                            }
                        }
                        try
                        {
                            if (money[1, i] >= amount )
                            {
                                j = 0;
                                while (buf > 0 && j < amount)
                                {
                                    money[1, i]--;
                                    res[1, i]++;
                                    buf -= money[0, i];
                                    j++;
                                }
                               
                                if (buf < 0)
                                    buf += money[0, i];
                                
                            }
                        }
                        catch( Exception ex )
                        {
                           
                        }
                        
                    }
                    i++;
                }
                
            }

            i = 1;
            resultBox.Text = "";
            resultBox.Text += "Ваша здача " + buf.ToString() + "коп.\n";
            while ( i < nom.Length )
            {
                rest = buf - nom[i];
                if (rest < 0 || money[1, i] == 0)
                {
                    i++;
                    continue;
                }

                buf = rest;
                res[1, i]++;
                money[1, i]--;
            }

            for (int z = 0; z < nom.Length; z++)
                if (value == nom[z])
                    money[1, z]++;
        }

        private void PrintData()
        {
            resultBox.Text += "Видача грошей\n";
            resultBox.Text += "Номінал  |   Кількість\n";
            int buf = 0;
            for (int i = 0; i < nom.Length; i++)
            {
               // resultBox.Text += res[0, i] + " : " + res[1, i] + "\n";
                buf += res[0, i] * res[1, i];
            }

           // resultBox.Text += buf.ToString() + "\n";
            
            if (buf != value)
            {
                resultBox.Text += "Exchange is impossible\nDon't have enough money for exchange\n";
                int z = 0;
                for (int i = 0; i < nom.Length; i++)
                {
                    if (money[0,i] == value)
                        z = i;
                    money[1, i] += res[1, i];
                    res[1, i] = 0;
                }
                money[1, z]--;
            }
            else
             
                for (int i = 0; i < nom.Length; i++)
                {
                    if (res[1, i] > 0)
                        resultBox.Text += res[0, i] + "      |   " + res[1, i] + "\n";
                    res[1, i] = 0;
                }
                     
            richTextBox1.Text = "";
            for (int i = 0; i < nom.Length; i++)
                richTextBox1.Text += money[0, i].ToString() + " : " + money[1, i].ToString() + "\n";
        }

        private void InputMoney()
        {
            while (ext)
            {
                fl1 = true;
                while (fl2 == true)
                {
                    if (turn == 1)
                    {
                        fl1 = false;
                        do
                        {
                            Thread.Sleep(100);
                        }while ( turn == 1 );
                       
                        fl1 = true;
                    }
                }

                //critical zone for proc1
                while ( butclick )
                {}
                
                ////////////////////////
                fl1 = false;
                turn = 1;
                Thread.Sleep(100);
            }
            resultBox.Text += "input thread is finished\n";
        }

        private void OutMoney()
        {
            
            while (ext)
            {
                fl2 = true;
                while (fl1 == true)
                {
                    
                    if (turn == 0)
                    {
                        fl2 = false;
                        do
                        {
                            Thread.Sleep(100);
                        } while (turn == 0);
                        
                        fl2 = true;
                    }
                }

                //critical zone for proc1
               // while (butclick)
               // { }

                butclick = true;

                try
                {
                    ExchangeMoney();
                    PrintData();
                }
                catch (Exception ex)
                {
                    resultBox.Text += ex.ToString() + "\n";
                    ext = false;
                    fl2 = false;
                    turn = 0;
                    return;
                }
                ////////////////////////
                fl2 = false;
                turn = 0;
                resultBox.Text += "expecting new value...\n";
                Thread.Sleep(100);
            }
            resultBox.Text += "output thread is finished\n";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < nom.Length; i++)
            {
                CheckBox box = new CheckBox();
                RichTextBox rcb = new RichTextBox();
                rcb.Location = new System.Drawing.Point(60, 50 + 25 * i);
                rcb.Height = 20;
                rcb.Width = 50;
                rcb.TabIndex = 0;
                rcb.Name = "rcb" + Convert.ToString(i);
                this.Controls.Add(rcb);

                box.AutoSize = true;
                box.Location = new System.Drawing.Point(15, 50 + 25 * i);
                box.Size = new System.Drawing.Size(80, 17);
                box.TabIndex = 0;
                box.UseVisualStyleBackColor = true;
                this.Controls.Add(box);
                box.Text = nom[i].ToString();
                box.Name = "CheckBox" + Convert.ToString(i);
            }

            th1 = new Thread(new ThreadStart(InputMoney));
            th2 = new Thread(new ThreadStart(OutMoney));
            th1.Start();
            th2.Start();
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ext = false;
            butclick = false;
            //th1.Join();
            //th2.Join();
            //this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            butclick = false;
            try
            {
                value = Convert.ToInt16(textBox1.Text);
            }
            catch (Exception ex)
            {
                resultBox.Text += ex.ToString() + "\n";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            value = nom[rnd.Next(0, nom.Length)]; 
            textBox1.Text = value.ToString();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            this.Close();
            
        }



    }
}

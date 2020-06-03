using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PP7
{
    public partial class Form1 : Form
    {
        int fl = -2;
        int d = 0;
        int d2 = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            BlLeft.Visible = false;
            GrRight.Visible = true;
            CLK.Visible = true;
            res.Text += "Ініціалізація PPI I8255\nІніціалізація PT\n Запис константи в СТ0\nІніціалізація СТ1\nЗапис константи в лічильник СТ1\nІніціалізація СТ2\nЗапис константи в СТ2\n";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Out0.Visible = true;
            turned.Visible = false;
            res.Text += "Формування сигналу Gate0\n";

            res.Text += "Перевірка D1\n";
          
           if (D10.Checked == true )
            {
                timer3.Start();
            }

            else
            {
                res.Text += "Перевірка D2 та D3\n";
              if (D20.Checked == true && D30.Checked == true)
             // if ( d>0 && d2>0 ) 
               // if ( d > 0 )
                {
                    res.Text += "Виділення коду UD1, UD2, UD3\n";
                    timer3.Start();
                }
                else
                    timer2.Start();
            }
            /*
            if (D1.Checked == true)
            {
                timer3.Start();
            }
            else if (D2.Checked == true && D3.Checked == true)
                timer4.Start();
            else timer2.Start();
            */
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            res.Text += "Декремент СТ1\nперевірка сигналу Out СТ1\n";
            
            //Clk1.Visible = true;
            //Gate0.Visible = false;
            
            if (Clk1.Visible == false)
            {
                Clk1.Visible = true;
                Clk2.Visible = false;
                Out0.Visible = false;
                Gate0.Visible = false;

            }
            if (Convert.ToInt16(Clock1.Text) == 0)
            {
                turnoff_no.Visible = true;
                Clk1.Visible = false;
                fin.Start();
                return;
            }
            else
                Clock1.Text = Convert.ToString(Convert.ToInt16(Clock1.Text) - 1);
            
            res.Text += "Перевірка D1\n";
            if (D10.Checked )
                timer3.Start();
            else
            {
                res.Text += "Перевірка D2 і D3\n";
                if (D20.Checked && D30.Checked)
                {
                    
                    timer3.Start();
                }else
                timer2.Start();
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            res.Text += "Кінець програми\n";
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            timer4.Stop();
            BlLeft.Visible = true;
            GrRight.Visible = false;
            turned.Visible = true;
            Gate0.Visible = false;
            Out0.Visible = false;
            Clk1.Visible = false;
            Clk2.Visible = false;
            CLK.Visible = false;
            Control.Visible = false;
            Yes.Visible = false;
        }

        private void Clock1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Clock2_TextChanged(object sender, EventArgs e)
        {

        }

        private void fin_Tick(object sender, EventArgs e)
        {
            fin.Stop();
            button2_Click(sender, e);

            if (turnoff_no.Visible == true)
            {
                Out1.Visible = true;
                turnoff_no.Visible = false;
            }
            else
                if (turnedoff_D1.Visible == true)
                {
                    Out2.Visible = true;
                    turnedoff_D1.Visible = false;
                }
            Out0.Visible = false;
            Clk1.Visible = false;
            Clk2.Visible = false;

            trash_gather.Start();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Stop();
            res.Text += "Декремент СТ2\nперевірка сигналу Out СТ2\n";
            //Clk2.Visible = false;

            if (Clk2.Visible == false)
            {
                Clk2.Visible = true;
                Clk1.Visible = false;
                Gate0.Visible = false;
                Out0.Visible = false;

            }
            if (Convert.ToInt16(Clock2.Text) == 0)
            {
                turnedoff_D1.Visible = true;
                Clk2.Visible = false;
                fin.Start();
                return;
            }
            else
                Clock2.Text = Convert.ToString(Convert.ToInt16(Clock2.Text) - 1);
            /*
            if (D1.Checked == true)
                timer3.Start();
            else if (D2.Checked == true && D3.Checked == true)
                timer4.Start();
            else timer2.Start();
            */
            res.Text += "Перевірка D1\n";
            if (d > 1)
                timer4.Start();
            else timer3.Start();
            /*
            if (D1.Checked == true && D10.Checked && ( ( d >= 0 && d2 == 0) || ( d == 0 && d2>=0) ) )
           // if (d == 0 && D1.Checked == true)
            {
                timer3.Start();
            }

            else
            {
                res.Text += "Перевірка D2 та D3\n";
               // if (D2.Checked == true && D3.Checked == true)
                if ( d>0 && d2>0)
                {
                    res.Text += "Виділення коду UD1, UD2, UD3\n";
                    timer4.Start();
                }
                else
                    timer2.Start();

            }
            */
        }

        private void trash_gather_Tick(object sender, EventArgs e)
        {
            trash_gather.Stop();
            turned.Visible = true;
            Out1.Visible = false;
            Out2.Visible = false;
        }

        private void Out1_Click(object sender, EventArgs e)
        {

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Stop();
            if (Gate0.Visible == false)
            {
                Gate0.Visible = !Gate0.Visible;
                Out0.Visible = !Gate0.Visible;
            }
            else
            {
                Out0.Visible = Gate0.Visible;
                Gate0.Visible = !Gate0.Visible;
            }
            //Gate0.Visible = !Gate0.Visible;
          //  Out0.Visible = !Gate0.Visible;
            Clk1.Visible = false;
            Clk2.Visible = false;
            if (fl == 0 && Out0.Visible == true)
            {
                fl = 1;
            }
            else
                if (Out0.Visible == false)
                {
                    Control.Visible = !Control.Visible;
                }
            if (Control.Visible == true)
            {
                res.Text += "Включення лінії Control\nПерезапуск СТ0\nОчікування Out Ст0\n";
            }
            else
            {
                res.Text += "Вимкнення лінії Control\nПерезапуск СТ0\nОчікування Out Ст0\n";
            }

            Yes.Visible = Control.Visible;

            /*
            if (D1.Checked == true)
                timer3.Start();
            else if (D2.Checked == true && D3.Checked == true)
                timer4.Start();
            else timer2.Start();
             * */
            res.Text += "Перевірка D1\n";
            if (d > 1)
                timer4.Start();
            else timer3.Start();
            
        }

        private void D2_CheckedChanged(object sender, EventArgs e)
        {
            if (D2.Checked == true)
            {
                fl++;
                d++;
            }
            else
            {
                d--;
                Control.Visible = false;
                Yes.Visible = false;
            }
            UD2.Visible = D2.Checked;
           // UD22.Visible = D2.Checked;
          //  D20.Checked = D2.Checked;
            if (d <= 0)
                Control.Visible = false;
        }

        private void D3_CheckedChanged(object sender, EventArgs e)
        {
            if (D3.Checked == true)
            {
                fl++;
                d++;
            }
            else
            {
                Control.Visible = false;
                d--;
            }
            UD3.Visible = D3.Checked;
           // UD33.Visible = D3.Checked;
            //D30.Checked = D3.Checked;
            if (d <= 0)
                Control.Visible = false;
        }

        private void D1_CheckedChanged(object sender, EventArgs e)
        {
            UD1.Visible = D1.Checked;
          //  UD11.Visible = D1.Checked;
           // D10.Checked = D1.Checked;
            if (D1.Checked)
                d++;
            else d--;
            if (d <= 0)
                Control.Visible = false;
        }

        private void Light_Click(object sender, EventArgs e)
        {

        }

        private void timer5_Tick(object sender, EventArgs e)
        {

        }

        private void D10_CheckedChanged(object sender, EventArgs e)
        {
          //  UD1.Visible = D10.Checked;
            UD11.Visible = D10.Checked;
           // D1.Checked = D10.Checked;
            if (D10.Checked)
                d2++;
            else d2--;
            if (d <= 0)
                Control.Visible = false;
        }

        private void D20_CheckedChanged(object sender, EventArgs e)
        {
            if (D20.Checked == true)
            {
                fl++;
                d2++;
            }
            else
            {
                d2--;
                Control.Visible = false;
                Yes.Visible = false;
            }
           // UD2.Visible = D20.Checked;
            UD22.Visible = D20.Checked;
          //  D2.Checked = D20.Checked;
            if (d <= 0)
                Control.Visible = false;
        }

        private void D30_CheckedChanged(object sender, EventArgs e)
        {
            if (D30.Checked == true)
            {
                fl++;
                d2++;
            }
            else
            {
                Control.Visible = false;
                d2--;
            }
           // UD3.Visible = D30.Checked;
            UD33.Visible = D30.Checked;
          //  D3.Checked = D30.Checked;
            if (d <= 0)
                Control.Visible = false;
        }
    }
}



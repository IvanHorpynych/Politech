using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;

namespace ZI_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        BigInteger p, q, d, e, n;

        BigInteger GenSimple(int lim)
        {
            
            BigInteger y = 1, x = y + 1;
            while (((x * x * x - y * y * y) / (x - y)) < lim)
            {
                y++;
                x++;  
            }

            return ((x * x * x - y * y * y) / (x - y));            
        }

        BigInteger GenSimpleDif(BigInteger lim)
        {
            BigInteger y = lim, x = y + 1, num,cmpexpr = ( p - 1 )*( q - 1 );
            do
            {
                num = (x * x * x - y * y * y) / (x - y);
                y++;
                x++;
            } while ( SimplePair(num,cmpexpr ) == false );

            return ((x * x * x - y * y * y) / (x - y));     
        }

        void GenParam()
        {
            p = GenSimple(300);
            q = GenSimple(200);
            n = p * q;
            
            d = GenSimpleDif( 3 );
           
            BigInteger buf = (p - 1) * (q - 1) ;
            int i = 1;
            while ((( buf * i + 1) % d) != 0 )
            {
                i++;  
            }
            e = (buf * i + 1) / d;
        }

        bool SimplePair (BigInteger simple_num1, BigInteger simple_num2)
        {
            if (simple_num1 % simple_num2 == 0 || simple_num2 % simple_num1 == 0)
                return false;
            return true;
        }

        void Encoding()
        {
            string s = signs.Text;
            for (int i = 0; i < s.Length; i++)
            {
                BigInteger code = Convert.ToInt32(s[i]);
               Coding.Text += BigInteger.Pow(code, Convert.ToInt32( e.ToString() )) % n + " ";
            }
            
        }

        void Decoding()
        {
            string s = Coding.Text;
            int count=1;
            for (int i = 0; i < s.Length; i++)
                if (s[i] == ' ')
                    count++;
            
            BigInteger[] Arr = new BigInteger [count + 1];

            string buf = "";
            count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                    buf += s[i].ToString();
                else
                {
                    Arr[count] = Convert.ToInt64(buf);
                    count++;
                    buf = "";
                }
            }

            
            for (int i = 0; i < count; i++)
            {
                BigInteger code = Arr[i] ;
                Arr[i] = BigInteger.Pow(Arr[i], Convert.ToInt32(d.ToString())) % n;
                decoding.Text += ((char) Arr[i]).ToString();
            }
          
        }

        private void Coding_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Encoding();
            //decoding.Text = "";
            }

        private void button2_Click(object sender, EventArgs e)
        {
            Decoding();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenParam();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;

namespace ZI_lab2
{
    public partial class Form1 : Form
    {
        char[] ChArr = new char[36] ;
      
       string[] MasStr4 = new string[(long)Math.Pow(36, 5)];

        string WordName;

        string ExcelName;

        int MaxLen;
        
        bool FlagMaxlen;

        int Counter;

        string Password;

        bool Flag;

        static Microsoft.Office.Interop.Excel._Application docExcel;

        static Microsoft.Office.Interop.Word._Application docWord;

        public Form1()
        {
            InitializeComponent();
            WordName = "4.docx";
            ExcelName = "4.xlsx";
            textBoxExcel.Text = ExcelName;
            textBoxWord.Text = WordName;
            MaxLen = 4;
            textBoxLength.Text = MaxLen.ToString();
            Flag = false;
            Counter = 0;
            int i = 0;
            for (i = 0; i < 26; i++)
            {
                ChArr[i] = (char)(i + 97);
            }
            for (i = 26; i < 36; i++)
            {
                ChArr[i] = (char)(i + 22);
            }
        }

        //------------------------------------------------------------------

        private void buttonExcelDigits_Click(object sender, EventArgs e)
        {
            Flag = false;
            textBoxTime.Clear();
            int MaxPass = (int)Math.Pow(10, MaxLen);
            docExcel = new Microsoft.Office.Interop.Excel.Application();
            object ob = Type.Missing;
            DateTime start = DateTime.Now;
            int i=0;
            while ((i < MaxPass) && (!TryPassExcel(i.ToString())))
            {
                i++;
            }
            docExcel.Quit();
            TimeSpan ts = (DateTime.Now - start);
            textBoxTime.Clear();
            textBoxPassword.Clear();
            textBoxTime.AppendText("" + ts.TotalSeconds);
            textBoxPassword.AppendText(i.ToString());
        }

        private void buttonWordDigits_Click(object sender, EventArgs e)
        {
            Flag = false;
            textBoxTime.Clear();
            int MaxPass = (int)Math.Pow(10, MaxLen);
            docWord = new Microsoft.Office.Interop.Word.Application();
            object ob = Type.Missing;
            DateTime start = DateTime.Now;
            int i = 0;
            while ((i < MaxPass) && (!TryPassWord(i.ToString())))
            {
                i++;
            }
            docWord.Quit();
            TimeSpan ts = (DateTime.Now - start);
            textBoxTime.Clear();
            textBoxPassword.Clear();
            textBoxTime.AppendText("" + ts.TotalSeconds);
            textBoxPassword.AppendText(i.ToString());
        }

        //-----------------------------------------------------------------

       
        private void Masiv4()
        {
           
            long i=0 ,i1 =0 , i2= 0 , i3=0 ,i4 =0;
           long MaxPass = (long)Math.Pow(36, 4);
           
            for (i = 0; i < MaxPass; i++)
           {
               MasStr4[i] = Convert.ToString(ChArr[i1]) + Convert.ToString(ChArr[i2]) + Convert.ToString(ChArr[i3]) + Convert.ToString(ChArr[i4]);
               i4++;
                if (i4 == 36) 
                {
                i3 ++;
                    i4 = 0;
                }
                if ( i3 == 36)
                {
                 i2 ++;
                    i3 =0;
                }
                if (i2 == 36)
                {
                    i1++;
                    i2 = 0 ;
                }
                
            }

        }
        private void Masiv3()
        {

            long i = 0, i2 = 0, i3 = 0, i4 = 0;
            long MaxPass = (long)Math.Pow(36, 3);

            for (i = 0; i < MaxPass; i++)
            {
                MasStr4[i] =   Convert.ToString(ChArr[i2]) + Convert.ToString(ChArr[i3]) + Convert.ToString(ChArr[i4]);
                i4++;
                if (i4 == 36)
                {
                    i3++;
                    i4 = 0;
                }
                if (i3 == 36)
                {
                    i2++;
                    i3 = 0;
                }
               

            }

        }
        private void Masiv2()
        {

            long i = 0,  i3 = 0, i4 = 0;
            long MaxPass = (long)Math.Pow(36, 2);

            for (i = 0; i < MaxPass; i++)
            {
                MasStr4[i] =  Convert.ToString(ChArr[i3]) + Convert.ToString(ChArr[i4]);
                i4++;
                if (i4 == 36)
                {
                    i3++;
                    i4 = 0;
                }
             
               
            }

        }
        private void Masiv1()
        {

            long i = 0, i4 = 0;
            long MaxPass = (long)Math.Pow(36, 1);

            for (i = 0; i < MaxPass; i++)
            {
                MasStr4[i] = Convert.ToString(ChArr[i4]);
                i4++;
               
              
                }

            }

        

        
        private bool TryPassExcel(string pass)
        {
            object ob = Type.Missing;
            try
            {
                textBoxPassword.Clear();
                textBoxPassword.AppendText(pass);
                docExcel.Workbooks.Open(Environment.CurrentDirectory + "\\" + ExcelName, ob, ob, ob, pass);
                docExcel.Workbooks.Close();
                Flag = true;
                Password = pass;
                return true;
            }
            catch
            {
            }
            return false;
        }

        private bool TryPassWord(string pass)
        {
            object ob = Type.Missing;
            try
            {
                textBoxPassword.Clear();
                textBoxPassword.AppendText(pass);
                docWord.Documents.Open(Environment.CurrentDirectory + "\\" + WordName, ob, ob, ob, pass);
                docWord.Documents.Close();
                Flag = true;
                Password = pass;
                return true;
            }
            catch
            {
            }
            return false;
        }

        //------------------------------------------------------------------

        private void textBoxExcel_TextChanged(object sender, EventArgs e)
        {
            ExcelName = textBoxExcel.Text;
        }

        private void textBoxWord_TextChanged(object sender, EventArgs e)
        {
            WordName = textBoxWord.Text;
        }

        private void textBoxLength_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLength.Text.Length != 0)
                MaxLen = Convert.ToInt32(textBoxLength.Text);
        }

        //------------------------------------------------------------------

     /*  void podbor()
        {
            long i = 0, ii5 = 0, i1 = 0, i2 = 0, i3 = 0, i4 = 0, j = 0;
             void Masiv45()
        {

            
           long MaxPass = (long)Math.Pow(36, 4);
           
            
                while ((i) <((i)+ MaxPass))
           {
               MasStr4[i] =Convert.ToString(ChArr[ii5])+ Convert.ToString(ChArr[i1]) + Convert.ToString(ChArr[i2]) + Convert.ToString(ChArr[i3]) + Convert.ToString(ChArr[i4]);
               i4++;
                if (i4 == 36) 
                {
                i3 ++;
                    i4 = 0;
                }
                if ( i3 == 36)
                {
                 i2 ++;
                    i3 =0;
                }
                if (i2 == 36)
                {
                    i1++;
                    i2 = 0 ;
                }
                if (i1== 36)
                {
                    ii5++;
                    i1 = 0;
                }
                    i++;
            }

        }
            for (j = 1; j < ((long)Math.Pow(26, MaxLen) / (long)Math.Pow(26, 4)); j++)
            {


            }
        }*/
      
       
        //------------------------------------------------------------------



        private void buttonWordDigitsLetters_Click(object sender, EventArgs e)
        {

            if (FlagMaxlen == false)
            {
                if (MaxLen == 1)
                {
                    Masiv1();

                }
                if (MaxLen == 2)
                {
                    Masiv2();

                }
                if (MaxLen == 3)
                {
                    Masiv3();
                }
                if (MaxLen == 4)
                {
                    Masiv4();
                }
                /* if (MaxLen == 5)
                 {
                     podbor5();
            
                 }*/

                long i = 0;

                Flag = false;

                textBoxTime.Clear();
                long MaxPass = (long)Math.Pow(36, MaxLen);
                docWord = new Microsoft.Office.Interop.Word.Application();
                object ob = Type.Missing;
                DateTime start = DateTime.Now;
                //-------------------------------------------------------

                while ((TryPassWord(MasStr4[i]) == false) && (i < MaxPass))
                {
                    //textBoxTime.Clear();
                    //textBoxTime.AppendText(Convert.ToString (MaxPass));
                    i++;
                }
                //-------------------------------------------------------
                docWord.Quit();
                TimeSpan ts = (DateTime.Now - start);
                textBoxTime.Clear();
                textBoxPassword.Clear();
                textBoxTime.AppendText("" + ts.TotalSeconds);
                textBoxPassword.AppendText(Password);
            }
//----------------------------------------------------------
   //-------------------------------------------------------        
            
            if (FlagMaxlen == true)
            {
                
                
                    Masiv1();
                    long i = 0;

                    Flag = false;

                textBoxTime.Clear();
                long MaxPass = (long)Math.Pow(36, 1);
                docWord = new Microsoft.Office.Interop.Word.Application();
                object ob = Type.Missing;
                DateTime start = DateTime.Now;
                //-------------------------------------------------------

                while ((TryPassWord(MasStr4[i]) == false) && (i < MaxPass))
                {
                    //textBoxTime.Clear();
                    //textBoxTime.AppendText(Convert.ToString (MaxPass));
                    i++;
                }
                //-------------------------------------------------------

                if (Flag == false)
                {
                    Masiv2();

                    i = 0;
                    MaxPass = (long)Math.Pow(36, 2);
                    docWord = new Microsoft.Office.Interop.Word.Application();


                    //-------------------------------------------------------

                    while ((TryPassWord(MasStr4[i]) == false) && (i < MaxPass) && (Flag == false))
                    {
                        //textBoxTime.Clear();
                        //textBoxTime.AppendText(Convert.ToString (MaxPass));
                        i++;
                    }
                } //-------------------------------------------------------


                if (Flag == false)
                {

                    Masiv3();
                    i = 0;
                    MaxPass = (long)Math.Pow(36, 3);
                    docWord = new Microsoft.Office.Interop.Word.Application();


                    //-------------------------------------------------------

                    while ((TryPassWord(MasStr4[i]) == false) && (i < MaxPass) && (Flag == false))
                    {
                        //textBoxTime.Clear();
                        //textBoxTime.AppendText(Convert.ToString (MaxPass));
                        i++;
                    }
                } //-------------------------------------------------------

                if (Flag == false)
                {

                    Masiv4();
                    i = 0;
                    MaxPass = (long)Math.Pow(36, 4);
                    docWord = new Microsoft.Office.Interop.Word.Application();


                    //-------------------------------------------------------

                    while ((TryPassWord(MasStr4[i]) == false) && (i < MaxPass) && (Flag == false))
                    {
                        //textBoxTime.Clear();
                        //textBoxTime.AppendText(Convert.ToString (MaxPass));
                        i++;
                    }
                }  //-------------------------------------------------------
                   
                /* if (MaxLen == 5)
                 {
                     podbor5();
            
                 }*/

               
                //-------------------------------------------------------
                docWord.Quit();
                TimeSpan ts = (DateTime.Now - start);
                textBoxTime.Clear();
                textBoxPassword.Clear();
                textBoxTime.AppendText("" + ts.TotalSeconds);
                textBoxPassword.AppendText(Password);
            }

        }

       

      
       private void textBoxExcel_TextChanged_1(object sender, EventArgs e)
       {
           ExcelName = textBoxExcel.Text;
       }

       private void textBoxWord_TextChanged_1(object sender, EventArgs e)
       {
           WordName = textBoxWord.Text;
       }

       private void textBoxLength_TextChanged_1(object sender, EventArgs e)
       {
           if (textBoxLength.Text.Length != 0)
               MaxLen = Convert.ToInt32(textBoxLength.Text);
       }

       private void buttonExcelDigitsLetters_Click_1(object sender, EventArgs e)
       {
           if (FlagMaxlen == false)
           {


               if (MaxLen == 1)
               {
                   Masiv1();

               }
               if (MaxLen == 2)
               {
                   Masiv2();

               }
               if (MaxLen == 3)
               {
                   Masiv3();
               }
               if (MaxLen == 4)
               {
                   Masiv4();
               }
               /* if (MaxLen == 5)
                {
                    podbor5();
            
                }*/

               long i = 0;

               Flag = false;

               textBoxTime.Clear();
               long MaxPass = (long)Math.Pow(36, MaxLen);
               docExcel = new Microsoft.Office.Interop.Excel.Application();
               object ob = Type.Missing;
               DateTime start = DateTime.Now;
               //-------------------------------------------------------

               while ((TryPassExcel(MasStr4[i]) == false) && (i < MaxPass))
               {
                   //textBoxTime.Clear();
                   //textBoxTime.AppendText(Convert.ToString (MaxPass));
                   i++;
               }
               //-------------------------------------------------------
               docExcel.Quit();
               TimeSpan ts = (DateTime.Now - start);
               textBoxTime.Clear();
               textBoxPassword.Clear();
               textBoxTime.AppendText("" + ts.TotalSeconds);
               textBoxPassword.AppendText(Password);


           }
           //---------------------------------------

           if (FlagMaxlen == true)
           {


               Masiv1();
               long i = 0;

               Flag = false;

               textBoxTime.Clear();
               long MaxPass = (long)Math.Pow(36, 1);
               docExcel = new Microsoft.Office.Interop.Excel.Application();
               object ob = Type.Missing;
               DateTime start = DateTime.Now;
               //-------------------------------------------------------

               while ((TryPassExcel(MasStr4[i]) == false) && (i < MaxPass))
               {
                   //textBoxTime.Clear();
                   //textBoxTime.AppendText(Convert.ToString (MaxPass));
                   i++;
               }
               //-------------------------------------------------------


               if (Flag == false)
               {
                   Masiv2();

                   i = 0;
                   MaxPass = (long)Math.Pow(36, 2);
                   docExcel = new Microsoft.Office.Interop.Excel.Application();


                   //-------------------------------------------------------

                   while ((Flag == false) && (TryPassExcel(MasStr4[i]) == false) && (i < MaxPass))
                   {
                       //textBoxTime.Clear();
                       //textBoxTime.AppendText(Convert.ToString (MaxPass));
                       i++;
                   }
               }//-------------------------------------------------------


               if (Flag == false)
               {

                   Masiv3();
                   i = 0;
                   MaxPass = (long)Math.Pow(36, 3);
                   docExcel = new Microsoft.Office.Interop.Excel.Application();


                   //-------------------------------------------------------

                   while ((Flag == false) && (TryPassExcel(MasStr4[i]) == false) && (i < MaxPass))
                   {
                       //textBoxTime.Clear();
                       //textBoxTime.AppendText(Convert.ToString (MaxPass));
                       i++;
                   }
               } //-------------------------------------------------------

               if (Flag == false)
               {
                   Masiv4();
                   i = 0;
                   MaxPass = (long)Math.Pow(36, 4);
                   docExcel = new Microsoft.Office.Interop.Excel.Application();


                   //-------------------------------------------------------

                   while ((Flag == false) && (TryPassExcel(MasStr4[i]) == false) && (i < MaxPass))
                   {
                       //textBoxTime.Clear();
                       //textBoxTime.AppendText(Convert.ToString (MaxPass));
                       i++;
                   }
               }  //-------------------------------------------------------

               /* if (MaxLen == 5)
                {
                    podbor5();
            
                }*/


               //-------------------------------------------------------
               docExcel.Quit();
               TimeSpan ts = (DateTime.Now - start);
               textBoxTime.Clear();
               textBoxPassword.Clear();
               textBoxTime.AppendText("" + ts.TotalSeconds);
               textBoxPassword.AppendText(Password);
           }
       }

       private void radioButton1_CheckedChanged(object sender, EventArgs e)
       {
           FlagMaxlen = true;
           
       }

       private void radioButton2_CheckedChanged(object sender, EventArgs e)
       {
           FlagMaxlen = false;
       }

     
        //------------------------------------------------------------------

        

   
        

    }
}

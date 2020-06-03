using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace Lab02_Word_Project
{
    class Program
    {
        //static Microsoft.Office.Interop.Word._Application doc;
        static Microsoft.Office.Interop.Excel._Application doc;
        
        static void Main(string[] args)
        {
            int MaxLen = 4;
            int MaxPass = (int)Math.Pow(10, MaxLen);
            //doc = new Microsoft.Office.Interop.Word.Application();
            doc = new Microsoft.Office.Interop.Excel.Application();
            object ob = Type.Missing;
            DateTime start = DateTime.Now;
            for (int i = 0; i < MaxPass; i++)
            {
                if (TryPass(i.ToString()))
                {
                    doc.Quit();
                    TimeSpan ts = (DateTime.Now-start);
                    Console.Write(i);
                   // Console.Write(""+ts.TotalSeconds);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            doc.Quit();
            Console.Write("FAIL Pass not found!!" + "\nTime: " + (DateTime.Now - start));
            Console.ReadKey();
        }

        static bool TryPass(object pass)
        {
            object ob = Type.Missing;
            try
            {
                //doc.Documents.Open(Environment.CurrentDirectory + "\\1.doc", ob, ob, ob, pass);
                //doc.Documents.Close();
                doc.Workbooks.Open(Environment.CurrentDirectory + "\\1.xls", ob, ob, ob, pass);
                doc.Workbooks.Close();
                return true;
            }
            catch
            {
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mail_pars
{
    class Program
    {
        static void Main(string[] args)
        {
            parser pars = new parser();
            pars.line = "hello@narod.ru";
            try
            {
                StreamReader rd = new StreamReader(new FileStream("email.txt", FileMode.Open));
                string curr_em = "";
                while ( null != ( curr_em = rd.ReadLine()))
                {
                    Console.WriteLine(curr_em);
                    pars.line = curr_em;
                    pars.MainTitle();
                }
                Console.ReadKey();
                rd.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            
            


        }
    }
}

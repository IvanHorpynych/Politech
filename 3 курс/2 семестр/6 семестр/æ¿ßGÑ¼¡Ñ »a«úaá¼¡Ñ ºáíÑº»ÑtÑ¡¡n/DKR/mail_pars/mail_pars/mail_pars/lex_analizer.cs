using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mail_pars
{
    class lex_analizer
    {
        public string[] lexems;
        public int len;

        public lex_analizer()
        {
            len = 0;
            lexems = new string[50];
        }

        public void Automat(string line)
        {
            string str = line;
            string buf = "";
            int strlen = 0, code;
            lexem lex = new lexem();
            while (strlen < line.Length)
            {
                code = lex.RetCode(str[strlen].ToString());
              
                switch (code)
                {
                    case 0:
                        Console.WriteLine("error: name starts with number");
                        while (strlen < line.Length)
                        {
                            code = lex.RetCode(str[strlen].ToString());                          
                            if (code != 0)
                                break;
                            strlen++;
                        }
                        break;
                    case 1:
                        buf = "";
                        while (strlen < line.Length)
                        {
                            code = lex.RetCode(str[strlen].ToString());
                            if (code != 0 && code != 1)
                                break;
                            buf += str[strlen];
                            strlen++;
                        }
                        
                        lexems[len] = buf;
                        len++;
                        break;
                    case 2:
                        lexems[len] = str[strlen].ToString();
                        len++;
                        strlen++;
                        break;
                    case 3:
                        Console.WriteLine("error: used forbidden symbol {0}", str[strlen]);
                        strlen++;
                        break;
                }
            }
          
        }

    }
}

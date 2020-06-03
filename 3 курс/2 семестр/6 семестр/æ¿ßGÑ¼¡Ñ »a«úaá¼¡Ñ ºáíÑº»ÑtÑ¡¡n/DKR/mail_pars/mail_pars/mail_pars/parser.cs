using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mail_pars
{
    class parser
    {
        lex_analizer lex;
        public string line;
        int num_lex;
        int max_len;
        public parser()
        {
            line = "";
            lex = new lex_analizer();
            
        }

        void NickName()
        {
            if (lex.lexems[num_lex] == "@" || lex.lexems[num_lex] == ".")
            {
                Console.WriteLine("error: expected nick name");
                return;
            }

            if (lex.lexems[num_lex].Contains("-"))
                Console.WriteLine("error: name contains symbol - ");
            num_lex++;
        }

        void DomenString()
        {
            if (num_lex < lex.len && lex.lexems[num_lex].Contains("_"))
                Console.WriteLine("error: domen contains symbol _ ");
            lexem check = new lexem();
            int code = check.RetCode(lex.lexems[num_lex].Last().ToString());
            if (num_lex < lex.len && (code > 1 || lex.lexems[num_lex][lex.lexems[num_lex].Length - 1] == '-' || lex.lexems[num_lex][lex.lexems[num_lex].Length - 1] == '_'))
                Console.WriteLine("error: domen name ends with forbidden symbol");
            num_lex++;
        }

        void DomenName()
        {

            if (num_lex >= lex.len)
            {
                Console.WriteLine("error1: expected domen name");
            }else
            DomenString();
            if (num_lex >= lex.len)
            {
                Console.WriteLine("error2: expected domen name");
                return;
            }else
            SubDomenName();
           
            SubDomenName();
            if (num_lex < lex.len)
            {
                Console.WriteLine("error4: unexpected end of e-mail");
            }
        }

        void SubDomenName()
        {
            if (num_lex >= lex.len)
                return;
            

            if ( lex.lexems[num_lex] == ".")
                num_lex++;
            else
            {
                Console.WriteLine("error: expected '.')");
            }
            DomenString();

           // if (num_lex < max_len && lex.lexems[num_lex] == ".")
                
        }

        
        public void MainTitle()
        {
            lex.Automat(line);
            max_len = lex.lexems.Length;
            NickName();
            if (lex.lexems[num_lex] == "@")
            {
                num_lex++;
            }
            else
                Console.WriteLine("error: expected @");
            DomenName();
            Console.WriteLine("--------------------------------------------");
        }

    }
}

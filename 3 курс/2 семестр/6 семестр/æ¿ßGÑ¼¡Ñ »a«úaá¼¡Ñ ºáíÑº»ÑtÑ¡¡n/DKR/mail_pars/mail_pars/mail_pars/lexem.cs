using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mail_pars
{
    class lexem
    {
        string[] letters;
        string[] digits;
        string[] symb;

        public lexem()
        {
            
            digits = new string[11];
            for (int i = 0; i < 10; i++)
               digits[i] = i.ToString();

            letters = new string[54];
            for (char i = 'a'; i <= 'z'; i++)
            {
                letters[i-'a'] = i.ToString();
                letters[i + 26 - 'a'] = i.ToString().ToUpper();
            }
            letters[52] = "_";
            letters[53] = "-";
            symb = new string[2];
            symb[0] = "@";
            symb[1] = ".";
        }

        public int RetCode(string it)
        {
            if (digits.ToList().Contains(it))
                return 0;
            if (letters.ToList().Contains(it))
                return 1;
            if (symb.ToList().Contains(it))
                return 2;
            return 3;
        }

        public void PrintLex()
        {
            for (int i = 0; i < letters.Length; i++)
                Console.WriteLine("{0}", letters[i]);
        }
    }
}

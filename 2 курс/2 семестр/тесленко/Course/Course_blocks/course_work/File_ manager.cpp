#include "Lexical_Analys.h"


void create_lex_analys()
{
string buf_line;
char buf_ch;
lexem* buf_struct;
ifstream file_test;
ofstream file_lexem;
int count = 1;

file_test.open("D:\\Documents\\2 êóðñ\\2 ñåìåñòð\\ÑÏ\\Course\\Course_blocks\\test.asm");
file_lexem.open("D:\\Documents\\2 êóðñ\\2 ñåìåñòð\\ÑÏ\\Course\\Course_blocks\\Lexical_Analys.txt");

if (file_test.is_open())
{
	while (!file_test.eof())
	{
	getline (file_test,buf_line);
	int a = buf_line.size();
	for (int i = 0; i < a; i++)
	{
		buf_ch = buf_line[i];
		buf_line[i] = toupper(buf_ch);
	}
	buf_struct = del_sentence(buf_line);
	file_lexem << "ÐßÄÎÊ ¹" << count << endl;
	file_lexem << "---------------------------------------------------------------------------------------" << endl;
	file_lexem << "|  ¹  |       ËÅÊÑÅÌÀ       |       ÄÎÂÆÈÍÀ       |            ÒÈÏ ËÅÊÑÅÌÈ            |" << endl;
	file_lexem << "---------------------------------------------------------------------------------------" << endl;
	while(buf_struct != NULL)
    {
        file_lexem << buf_struct->lexem_str;
        file_lexem << buf_struct->lexem_length << endl;
        buf_struct = buf_struct->next;
    }
	count++;
	}
	file_test.close();
}
else cout << "Unable to open file";
return;
}

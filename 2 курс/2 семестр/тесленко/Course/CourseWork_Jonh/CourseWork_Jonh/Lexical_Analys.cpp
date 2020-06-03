#include "Analys_header.h"

lexem* del_sentence(string buf_line)
{
	lexem *buf;
	lexem *Parts_sent;
	Parts_sent = (lexem*)malloc(sizeof(lexem));
	buf = Parts_sent;
	buf->next = NULL;
	int a = buf_line.size();
	char in_string[255];
	int g = 0, flag = 0;
	strncpy(in_string, buf_line.c_str(), a);
	in_string[a] = '\0';
	char ch = in_string[0];
	if (ch == '\0')
	{
		char* bufch = (char*)malloc(sizeof(char));
		bufch[0] = '\0';
		buf->lexem_str = bufch;
		buf->lexem_length = 0;
		buf->TypeUser = eof;

	}
	char* out_string = (char*)malloc(sizeof(char));
	out_string[0] = '\0';
	int length, i;
	length = strlen(in_string);



	for (i = 0; i <= length - 1; i)
	{
		if (singlesymb(ch) == true)
		{
			if (ch == ';')
			{
				i = length;
			}

			else if (ch == ' ')
			{
				i++;
				ch = in_string[i];
			}
			else
			{
				char* bufch = (char*)malloc(sizeof(char));
				bufch[0] = ch;
				bufch[1] = '\0';
				buf->lexem_str = bufch;
				buf->lexem_length = 1;
				buf->TypeUser = single_symb;
				i++;
				ch = in_string[i];
				if (in_string[i] != '\0' && in_string[i] != ';')
				{
					buf->next = (lexem*)malloc(sizeof(lexem));
					buf = buf->next;
					buf->next = NULL;
				}
			}
		}
		else
		{
			if (ch == '\'')
			{
				i++;
				ch = in_string[i];
				g = 0;
				out_string[g] = ch;
				g++;
				i++;
				ch = in_string[i];
				while (ch != '\'' && ch != '\0')
				{
					out_string[g] = ch;
					g++;
					i++;
					ch = in_string[i];
				}

				if (ch == '\'')
				{
					buf->TypeUser = const_string;
				}
				else
				{
					buf->TypeUser = user_identifier;
				}
				out_string[g] = '\0';
				buf->lexem_str = out_string;
				buf->lexem_length = g;
				i++;
				ch = in_string[i];
			}
			else
			{
				g = 0;
				while (singlesymb(ch) == false)
				{
					out_string[g] = ch;
					g++;
					i++;
					ch = in_string[i];
				}
				out_string[g] = '\0';
				buf->lexem_str = out_string;
				buf->lexem_length = g;
				if (isalpha(out_string[0]) != 0)
				{
					buf->TypeUser = analys_word(out_string);
				}
				else if (isdigit(out_string[0]) != 0)
				{
					buf->TypeUser = analys_const(out_string);
				}
			}

			if (in_string[i] != '\0' && in_string[i] != ';')
			{
				out_string = (char*)malloc(sizeof(char));
				buf->next = (lexem*)malloc(sizeof(lexem));
				buf = buf->next;
				buf->next = NULL;
			}
		}
	}

	return Parts_sent;
}


bool singlesymb(char ch)
{
	if (ch == ';' || ch == ' ' || ch == ',' || ch == '+' || ch == '-' || ch == ':' || ch == '[' || ch == ']' || ch == '*' || ch == '(' || ch == ')' || ch == '=' || ch == '\0')
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool reg32(char* out_string)
{
	if (strcmp(out_string, "EAX") == 0 || strcmp(out_string, "ECX") == 0 || strcmp(out_string, "EDX") == 0 || strcmp(out_string, "EBX") == 0 || strcmp(out_string, "EBP") == 0 || strcmp(out_string, "ESI") == 0 || strcmp(out_string, "EDI") == 0 || strcmp(out_string, "ESP") == 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool reg16(char* out_string)
{
	if (strcmp(out_string, "AX") == 0 || strcmp(out_string, "CX") == 0 || strcmp(out_string, "DX") == 0 || strcmp(out_string, "BX") == 0 || strcmp(out_string, "SP") == 0 || strcmp(out_string, "BP") == 0 || strcmp(out_string, "SI") == 0 || strcmp(out_string, "DI") == 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool reg8(char* out_string)
{
	if (strcmp(out_string, "AL") == 0 || strcmp(out_string, "AH") == 0 || strcmp(out_string, "BL") == 0 || strcmp(out_string, "BH") == 0 || strcmp(out_string, "CL") == 0 || strcmp(out_string, "CH") == 0 || strcmp(out_string, "DL") == 0 || strcmp(out_string, "DH") == 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool machcommand(char* out_string)
{
	if (strcmp(out_string, "STI") == 0 || strcmp(out_string, "MOV") == 0 || strcmp(out_string, "PUSH") == 0 || strcmp(out_string, "DIV") == 0 || strcmp(out_string, "ADC") == 0 || strcmp(out_string, "SUB") == 0 || strcmp(out_string, "XOR") == 0 || strcmp(out_string, "JZ") == 0 || strcmp(out_string, "JMP") == 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool segreg(char* out_string)
{
	if (strcmp(out_string, "DS") == 0 || strcmp(out_string, "SS") == 0 || strcmp(out_string, "ES") == 0 || strcmp(out_string, "CS") == 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool direct(char* out_string)
{
	if (strcmp(out_string, "SEGMENT") == 0 || strcmp(out_string, "ENDS") == 0 || strcmp(out_string, "END") == 0 || strcmp(out_string, "ASSUME") == 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool dirtype(char* out_string)
{
	if (strcmp(out_string, "DB") == 0 || strcmp(out_string, "DW") == 0 || strcmp(out_string, "EQU") == 0 || strcmp(out_string, "DD") == 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool deftype(char* out_string)
{
	if (strcmp(out_string, "PTR") == 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool fourtype(char* out_string)
{
	if (strcmp(out_string, "BYTE") == 0 || strcmp(out_string, "WORD") == 0 || strcmp(out_string, "DWORD") == 0 || strcmp(out_string, "QWORD") == 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool const8(char* out_string)
{
	if (out_string[strlen(out_string) - 1] == 'O' || out_string[strlen(out_string) - 1] == 'Q')
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool const16(char* out_string)
{
	if (out_string[strlen(out_string) - 1] == 'H')
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool const2(char* out_string)
{
	if (out_string[strlen(out_string) - 1] == 'B')
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool const10(char* out_string)
{
	if (out_string[strlen(out_string) - 1] == 'D' || isdigit(out_string[strlen(out_string) - 1]) != 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool conststr(char* out_string)
{
	if (out_string[strlen(out_string) - strlen(out_string)] == '\'' && out_string[strlen(out_string) - 1] == '\'' || out_string[strlen(out_string) - strlen(out_string)] == '\"'  && out_string[strlen(out_string) - 1] == '\"')
	{
		return true;
	}
	else
	{
		return false;
	}
}

UserType analys_word(char* out_string)
{
	if (direct(out_string) == true)
	{
		return directive;
	}

	if (reg32(out_string) == true)
	{
		return reg_32;
	}

	if (reg16(out_string) == true)
	{
		return reg_16;
	}

	if (reg8(out_string) == true)
	{
		return reg_8;
	}

	if (reg32(out_string) == true)
	{
		return reg_32;
	}

	if (machcommand(out_string) == true)
	{
		return machine_command;
	}

	if (segreg(out_string) == true)
	{
		return segment_reg;
	}

	if (dirtype(out_string) == true)
	{
		return dir_type;
	}

	if (deftype(out_string) == true)
	{
		return defin_type;
	}

	if (fourtype(out_string) == true)
	{
		return type_4;
	}
	return user_identifier;
}

UserType analys_const(char* out_string)
{
	if (const2(out_string) == true)
	{
		return const_binary;
	}

	if (const8(out_string) == true)
	{
		return const_8;
	}

	if (const10(out_string) == true)
	{
		return const_10;
	}

	if (const16(out_string) == true)
	{
		return const_16;
	}

	if (conststr(out_string) == true)
	{
		return const_string;
	}

	return UNDEF;
}

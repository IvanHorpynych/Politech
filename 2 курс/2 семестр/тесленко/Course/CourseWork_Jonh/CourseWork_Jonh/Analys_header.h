#define _CRT_SECURE_NO_WARNINGS
#include <string.h>
#include <stdio.h>
#include <ctype.h>
#include <iomanip>
#include <iostream>
#include <fstream>
#include <string>
using namespace std;


typedef enum
{
	user_identifier,
	reg_32, //{ "EAX", "ECX", "EDX", "EBX", "EBP", "ESI", "EDI", "ESP" }
	reg_16, //{ "AX", "CX", "DX", "BX", "SP", "BP", "SI", "DI" }
	reg_8, //{ "AL", "AH", "BL", "BH", "CL", "CH", "DL", "DH" }
	machine_command,
	segment_reg, //{ "DS", "SS", "ES", "CS" }
	single_symb, //{",;+-:[]*() "}
	directive, //{ "SEGMENT", "ENDS" }
	dir_type, //{ "DB", "DW", "DD" }
	type_4, //{ "BYTE", "WORD", "DWORD", "QWORD"}
	defin_type,  //{"PTR"}
	const_16,
	const_10,
	const_8,
	const_binary,
	const_string,
	eof,
	UNDEF
}UserType;

struct lexem
{
	UserType TypeUser;
	char* lexem_str;
	int lexem_length;
	struct lexem* next;
};

lexem* del_sentence(string buf_line);
bool singlesymb(char ch);
bool reg32(char* out_string);
bool reg16(char* out_string);
bool reg8(char* out_string);
bool machcommand(char* out_string);
bool segreg(char* out_string);
bool direct(char* out_string);
bool dirtype(char* out_string);
bool deftype(char* out_string);
bool fourtype(char* out_string);
bool const8(char* out_string);
bool const16(char* out_string);
bool const2(char* out_string);
bool const10(char* out_string);
bool conststr(char* out_string);
UserType analys_word(char* out_string);
UserType analys_const(char* out_string);
void create_lex_analys();

string string_correction(string buf_line, int a);
int* sentence_struct(lexem* Parts_sent, int flag);
int count_operand(lexem* buf_struct);
void create_sintax_analys();
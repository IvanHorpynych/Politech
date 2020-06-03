#pragma once
# include <iostream>
# include <fstream>
# include <string>
# include <stdlib.h>
# include <stdio.h>
# include <conio.h>
# include <ctype.h>
#include <iomanip>
using namespace std;

typedef enum
{
	IDENTIFIER,data1	
	REG_32,
	REG_8,
	COMMAND,mov
	SEG_REG,cs ds 
	ONESYMB,,]
	DIRECTIVE, segment
	DIR_TYPE, db dw
	
	TYPE_4, dword
	DEF_TYPE, ptr
	CONST_16,
	CONST_10,
	CONST_8,
	CONST_BIN,
	CONST_STR,
	UNDEF,//undefined type of token
	EOT pusta sroka
}TokenType;

struct lexem
{
	TokenType tokenType;
	char* lexem_view;
	int lexem_length;
	struct lexem *next;
};

bool isStartChar(char ch);
bool isOperators(char ch);
bool isDelimeters(char ch);
bool isSymbol(char ch);
void print_list(lexem* Head, int i_str);
TokenType analyze_word(char* mToken);
TokenType analyze_number(char* mToken);
bool isDirective(char* mToken);
bool isDir_Type(char* mToken);
bool isReg_32(char* mToken);
bool isReg_8(char* mToken);
bool isSegmentReg(char* mToken);
bool isCommand(char* mToken);
bool isFar_Near(char* mToken);
bool isBinary_Const(char* mToken);
bool is8_Const(char* mToken);
bool is10_Const(char* mToken);
bool is16_Const(char* mToken);
bool isSTR_Const(char *mToken);
bool isType4(char* mToken);
bool isDef_type(char* mToken);
lexem* lexical_analys(string bufstr);
string string_correction(string bufstr);

is alpha is digit strcmp 

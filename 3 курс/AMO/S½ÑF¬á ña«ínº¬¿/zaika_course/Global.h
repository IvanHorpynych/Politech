#ifndef H_GLOBAL
#define H_GLOBAL

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <unistd.h>
#include <ctype.h>
#include <math.h>

#define CODE_STRING 1000
#define MAX_STRING_LENGTH 255
#define IDENT_NAME_LENGTH 6

#define string_length 1000
#define long_string_length 10000

#define DATAtypeDEC 	"D"
#define DATAtypeHEX 	"H"
#define	DATAtypeBYTE 	"B"
#define DATAtypeSTR		"S"

#define DATAsizeDB		1
#define DATAsizeDW		2
#define DATAsizeDD		4

#define unsigned_byte_max    255
#define unsigned_word_max    65535
#define unsigned_dword_max   4294967295

#define neg_signed_byte_max     -128
#define neg_signed_word_max     -32768
#define neg_signed_dword_max    -2147483647

FILE *finput;
FILE *ftemp;
FILE *TEST;

typedef struct labelNode
{
	char name[IDENT_NAME_LENGTH + 1];
	unsigned long offset;
	int segment_num;
	struct labelNode *next;
} label_type;

typedef struct equNode
{
	char name[IDENT_NAME_LENGTH + 1];
	size_t size;
	unsigned long offset;
	char original_value[32];
	double value;
	struct equNode *next;
} equ_type;

typedef struct segmentNode
{
	char name[IDENT_NAME_LENGTH + 1];
	char segment_reg[2];
	unsigned long offset;
	struct segmentNode *next;
} segment_type;

typedef struct dataNode
{
	char name[IDENT_NAME_LENGTH + 1];
	unsigned int size;
	char original_value[MAX_STRING_LENGTH];
	char *value;
	char data_type;
	int offset;
	struct dataNode *next;
} data_type;

typedef struct assumeNode
{
	char reg_name[3];
	char identifier[8];
	struct assumeNode *next;
} assume_type;

void PreProcessing(FILE *processing_file);
int ProcessingText(FILE *processing_file);
void UpLetter(char *string, size_t length);
void CheckString(char *string, size_t length, size_t line);
void ConvertingStringToLexemes(char *string, size_t length, size_t line);
void ProcessingLexemes();
void ProcessingFile();

void pushLABEL(label_type **label_head, char array[MAX_STRING_LENGTH], int segment_num);
void deleteLABEL(label_type **label_head);
void printLABEL(label_type *label_head);
bool searchLABEL(label_type *label_head, char *label_name);
bool segmentLABEL(label_type *label_head, char *name, int snum);
int offsetLABEL(label_type *label_head, char *label_name);

void pushDATA(data_type **data_head, char *var_name, char *var_value, size_t data_size, char type, int offset);
bool findDATA(data_type *data_head, char *var_name);
int findsizeDATA(data_type *data_head, char *data_name);
int findoffsetDATA(data_type *data_head, char *data_name);
void deleteDATA(data_type **data_head);
void printDATA(data_type *data_head);

void pushSEGMENT(segment_type **segment_head, char array[IDENT_NAME_LENGTH]);
void rewriteSEGMENT(segment_type *segment_head, char array[IDENT_NAME_LENGTH], unsigned int g_offset);
void deleteSEGMENT(segment_type **segment_head);
void printSEGMENT(segment_type *segment_head);

void pushEQU(equ_type **equ_head, int val, char *equ_name);
bool findEQU(equ_type *equ_head, char *equ_name);
int findimmEQU(equ_type *equ_head, char *equ_name);
void deleteEQU(equ_type **equ_head);

void initializingASSUME();
void pushInitASSUME(assume_type **assume_head, char *reg, char *ident);
void rewriteASSUME(assume_type *assume_head, char *reg, char *ident);
void deleteASSUME(assume_type **assume_head);
void printASSUME(assume_type *assume_head, int one);

int CheckSize(double num);
double HexToDecimal(char *string);
double BinToDecimal(char *string);
char *DecimalToHex(char *string, char *result);

void UnknownCharError(char *string, size_t line_number, size_t char_number, char ch);
void IdentNameError(char *string, size_t line_number);
void NoSegmentError(char *string, size_t line_number);

#endif
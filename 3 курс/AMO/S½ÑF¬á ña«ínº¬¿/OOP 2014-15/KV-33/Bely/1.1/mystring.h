/************************************************************************
*file: mystring.h
*synopsis: function prototypes
*author: Turchaninov Gennady
*written: 04/10/2013
*last modified: 09/10/2013
************************************************************************/

int substr(const char *string1, const char *string2);

int subseq(const char *str1, const char *str2);

char ispal(const char *string);

char* makepal(const char *string);

double* txt2double(const char *string, int *size);

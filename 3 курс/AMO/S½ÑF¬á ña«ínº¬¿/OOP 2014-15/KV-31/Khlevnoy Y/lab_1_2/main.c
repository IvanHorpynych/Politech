/************************************************************************
*file: main.c
*purpose: declarations for argz functions, types, constants
*author: Khlevnoy Y
*written: 19/10/2014
*last modified: 19/10/2014
*************************************************************************/
#include "argz.h"


int main()
{
    error_t func;
    char *argz;
    size_t argz_len;
    char *string = "SHELL=/bin/bash:usr=monty:PWD=/bin/monety:LANG=en_US.UTF-8";



    func = argz_create_sep(string, ':', &argz, &argz_len);
    if(func == OK)
        argz_print(argz, argz_len);
    else
        printf("ERROR\n");


    printf("Number of lexemes = %d\n\n\n", argz_count(argz, argz_len));


    printf("Delete:\n");
    argz_delete(&argz, &argz_len, "SHELL=/bin/bash");
    argz_delete(&argz, &argz_len, "PWD=/bin/monety");
    argz_delete(&argz, &argz_len, "LANG=en_US.UTF-8");
    argz_print ( argz,  argz_len);


    printf("Add:\n");
    func = argz_add(&argz, &argz_len, "a new lexeme");
    if(func == OK)
        argz_print(argz, argz_len);
    else
        printf("ERROR\n");

    printf("Add:\n");
    func = argz_add(&argz, &argz_len, "a new lexeme (2)");
    if(func == OK)
        argz_print(argz, argz_len);
    else
        printf("ERROR\n");


    printf("Insert:\n");
    func = argz_insert (&argz, &argz_len, "a new lexeme (2)", "XYZ");
    if(func == OK)
        argz_print(argz, argz_len);
    else
        printf("ERROR\n");


    printf("Next:\n");
    if(argz_next(argz, argz_len, "a new lexeme"))
        printf("%s\n\n", argz_next(argz, argz_len, "a new lexeme"));
    else
        printf("0\n\n");


    printf("Replace:\n");
    func = argz_replace(&argz, &argz_len, "a new lexeme", "ABC");
    if(func == OK)
        argz_print(argz, argz_len);
    else
        printf("ERROR\n");

    return 0;
}

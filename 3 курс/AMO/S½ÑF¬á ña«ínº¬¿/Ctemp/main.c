
#include "l12_argz.h"



int main(){
    char *string = "SHELL=/bin/bash:usr=monty:PWD=/bin/monty:LANG=en_US.UTF-8";
    char *argz = 0;

    size_t argz_len = 0;
    argz_create_sep(string,58,&argz,&argz_len);
    printf("String = %s\n",string);
    printf("agz_size = %i\n",argz_len);
    printf("countval = %i\n",argz_count(argz,argz_len));
/*
    printf("\n START STATE:\n");
    argz_print(argz,argz_len);


    printf("\n ARGZ_ADD\n");
    argz_add(&argz,&argz_len,"ARGZ_ADD ENTRY");
    argz_print(argz,argz_len);


    printf("\n ARGZ_DELETE\n");
    argz_delete(&argz,&argz_len,"LANG=en_US.UTF-8");
    argz_print(argz,argz_len);	


    printf("\n ARGZ_INSERT\n");
    argz_insert(&argz,&argz_len,"PWD=/bin/monty","ARGZ_INSERT");
    argz_print(argz,argz_len);


    printf("\n ARGZ_REPLACE \n");
    argz_replace(&argz,&argz_len,"usr=monty","usr=newname");
    argz_print(argz,argz_len);  
*/
    system("PAUSE");
    return 0;
}

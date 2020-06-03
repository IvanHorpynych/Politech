/************************************************************************
*file: test.c
*author: Ivanov Alexander
*written: 07/10/2014
*last modified: 21/10/2014
************************************************************************/
#include "mystring.h"
#include "argz.h"

#include<stdio.h>
#include<math.h>
#include<conio.h> 
#include<windows.h>


int main()
{  char *const string = "SHELL=/bin/bash:usr=monty:PWD=/bin/monty:LANG=en_US.UTF-8";
   char *argz;
   size_t argz_len;

	const char *string1, *string2; int *size; int i; double *arr;
	

	
	printf("1 - part 1 \n");
	printf("2 - part 2 \n");
    switch (getchar())
	 {	
	    case '1':
	      {
             printf("substr:\n");
             string1 = "substring";
             string2 = "string";
	         printf("i=%d\n",substr(string1,string2));
        
             printf("subseq:\n");
             string1 = "docent";
             string2 = "student";
	         printf("max=%d\n",subseq(string1,string2));
        
             printf("ispal:\n");
             string1 = "malayalam";
             string2 = "student";
			 printf(" %s \n",string1);
	         if (ispal(string1)==1)
               printf("palidrom\n");
	         else
		       printf("not palindrom\n");
			 printf(" %s \n",string2);
	         if (ispal(string2)==1)
               printf("palindrom\n");
	         else
		       printf("not palindrom\n");
        
             printf("makepal:\n");
             string1 = "korrok";
             printf("%s : %s\n\n", string1, makepal(string1));
        
             printf("txt2double:\n");
             string1 = "11.54; 45.1; 5; 0.124";
        
             printf(" %s :\n", string1);
			  size=(int *)malloc(sizeof(int));
			  
             arr = txt2double(string1, size);
             for ( i = 0; i < *size; i++)
               printf(" %.3f\n", arr[i]);
             printf("\n");
             
				
		  } break;
		case '2':{
		    string1 = "SHELL=/bin/bash:usr=monty:PWD=/bin/monty:LANG=en_US.UTF-8";
			printf(" \"%s\"\n\n", string1);
            printf("argz_create_sep:\n");
            argz_create_sep(string1, ':', &argz, &argz_len);
            argz_print(argz, argz_len);

		    printf("argz_count:\n");
		    printf("amount : %li\n", argz_count(argz, argz_len));

		    printf("argz_add:\n");
		    argz_add(&argz, &argz_len, "string");
            argz_print(argz, argz_len);
				 
		    printf("argz_delete:\n");
		    argz_delete(&argz, &argz_len, "SHELL=/bin/bash");
            argz_print(argz, argz_len);

		
		    printf("argz_insert :\n");
            argz_insert(&argz, &argz_len, argz_next(argz, argz_len, "SHELL=/bin/bash"), "argz_insert");
            argz_print(argz, argz_len);

	   	    printf("argz_replace :\n");
		    argz_replace(&argz, &argz_len, "PWD=/bin/monty", "rwd/wdr/ivanov/alex");
            argz_print(argz, argz_len);
				 
				 
				 } break;


	}



	_getch();
	return 0;
}


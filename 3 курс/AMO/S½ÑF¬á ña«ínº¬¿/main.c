#include "HF.h"
#define  str "SHELL=/bin/bash:usr=monty:PWD=/bin/monty:LANG=en_US.UTF-8"
 int main() {
 char* argz;
 size_t argz_len;
 if (argz_create_sep(str, 58, &argz, &argz_len) == OK) {
     printf("Array: \n");
     argz_print(argz, argz_len);
     }
 else {
      printf("Error!\n");
      exit(1);
      }
 printf ( "\n Argz array consist of %i elements\n\n", argz_count (argz, argz_len));  
 if (argz_add (&argz, &argz_len, "Add_El") == OK) {
     printf ( "\n Argz array after adding\n");        
     argz_print(argz, argz_len);
     }
 else {
      printf("Error!\n");
      exit(1);      
      }  
 argz_delete (&argz, &argz_len, "Add_El");
 printf ( "\n Argz array after deleting('Add_El')\n");        
 argz_print(argz, argz_len);
 if (OK == argz_insert (&argz, &argz_len, "SHELL=/bin/bash", "Ins_el")) {
       printf ( "\n Argz array after insert\n"); 
       argz_print(argz, argz_len);
       }   
 else {
      printf("Error!\n");
      exit(1);      
      } 
 printf("\nNext element after %s is %s\n","Ins_el",argz_next (argz, argz_len, "Ins_el"));  
 if (OK == argz_replace (&argz, &argz_len, "Ins_el", "Rep_el")) {
       printf ( "\n Argz array after replacing\n"); 
       argz_print(argz, argz_len);
       }   
 else {
      printf("Error!\n");
      exit(1);      
      }            
 getchar();
 return 0;
 }

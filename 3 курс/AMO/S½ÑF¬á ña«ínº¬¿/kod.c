#include "HF.h"
int help_argz_cr_sep (const char *str, int sep, char **argz) {
int i, j, len;    
len = strlen(str);
j = len;
for (i = 0; i <= len; i++) {
  if (str[i] == sep) {
       if (!i) 
             j--;    
       else
          if (argz)
             (*argz)[i] =  '\0';    
       while (str[i+1] == sep) {
             i++;
             j--;
             }                     
   } else {
       if(argz)
         (*argz)[i] = str[i];
   }   
 }    
return (j+1);    
}    


error_t argz_create_sep (const char *str, int sep, char **argz, size_t *argz_len) {
int j;
if (NULL  == str) {
    *argz = NULL;
    return ENOMEM;
    }
else {
   j = help_argz_cr_sep(str, sep, 0);
   (*argz_len) = j; 
   (*argz) = (char*)malloc(j);
   help_argz_cr_sep(str, sep, argz);
   }  
return OK;      
}


size_t argz_count (const char *argz, size_t argz_len) {
int i, num_of_elem;
if (NULL == argz) 
      return 0;
else {    
   num_of_elem = 0;
   for (i = 0; i <= argz_len; i++) {
       if (argz[i] == '\0')
         num_of_elem++;
       } 
  }
return num_of_elem;
}

error_t argz_add (char **argz, size_t *argz_len, const char *str) {
char *buf;
int i,j,len;
if (NULL == str) 
    return ENOMEM;
else {
      len = strlen(str);
      buf = (char*)malloc((*argz_len)+len+1);
      for (i = 0; i < (*argz_len); i++)
        buf[i] = (*argz) [i];
      for (j = 0; j <= len; j++)
           buf[i++] = str[j];
      (*argz_len)+= len+1;
      free(*argz);
      (*argz) = buf;   
     } 
return OK;           
}

int argz_find(char *argz, size_t argz_len, const char *str, int flag){
int i, j, len;
j = 0;
len = strlen(str);
for (i = 0; i < argz_len; i++) {
    while ((argz[i] == str[j]) && j < len) {
           i++;
           j++;
          }
    if (j == len)      
        break;
    j = 0;    
    }
    if (j == 0)
      return -1;
    if (flag == 0)
        return (i-len);
    else  
        return (i); 
}

void argz_delete (char **argz, size_t *argz_len, const char *entry) {
    char *buf;
    int fl, len, i, j;
    if (NULL == entry) 
        printf("Error! NULL - string.\n");
    else {
         fl = argz_find(*argz, *argz_len, entry, 0);
         if (-1 == fl)
             printf("There is no such element in the string\n");
         else {
              len = strlen(entry);
              buf = (char*)malloc((*argz_len)- len -1);
              j = 0;
              for (i = 0; i < (*argz_len); i++) {
                   if (fl == i)
                       i+= len+1;      
                   buf[j++] = (*argz)[i];  
                   }
              (*argz_len)-= (len+1); 
              free(*argz);
              (*argz) = buf;                                  
              printf("The element is removed\n");  
              }
         }       
  }    
error_t argz_insert (char **argz, size_t *argz_len, char *before, const char *entry) {
int i, j, k, len, ind;
char *buf;
if ((before == NULL) || (entry == NULL))
   return ENOMEM;
else {
     ind = argz_find(*argz, *argz_len, before, 1);
     if (-1 == ind) 
            printf("String before not found\n");
     else {
      len = strlen(entry);
      buf = (char*)malloc((*argz_len)+len+1);
      j = 0; k = 0;
      for (i = 0; i < (*argz_len); i++) {
           buf[j] = (*argz)[i];
           j++; 
           if (i == ind) {
              while(k <= len) { 
               buf[j++] = entry[k++];    
               }              
              }            
         }
      (*argz_len) += len+1;
      free(*argz);
      (*argz) = buf;                                  
       printf("The element is inserted \n"); 
       return OK;   
       }
     }   
}
 
int argz__count(char *argz, const char *entry, int begin) {
int i;
i = begin;
while (argz[i] != '\0')   
       i++;
return (i - begin);
}

char * argz_next (char *argz, size_t argz_len, const char *entry) {
char *buf;
int i,j,ind;
if (NULL == entry) {
         j = argz__count(argz, entry,0);
         buf = (char*)malloc(j+1);
         for (i = 0; i <= j; i++) 
            buf[i] = argz[i];
         }
else {
     ind = argz_find(argz, argz_len, entry, 1);
     if (-1 == ind)
         return NULL;
     else
        j = argz__count(argz, entry,ind+1);
        buf = (char*)malloc(j+1);
        for (i = 0; i <= j; i++) 
            buf[i] = argz[i+ind+1];   
     }  
return buf;                     
} 

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with){
int ind, len1, len2, i, j, k;
char *buf;
if (NULL == str) 
         return ENOMEM;
else {
     ind = argz_find((*argz), (*argz_len), str, 0);
     if (-1 == ind)
        return ENOMEM;
     else {   
        if (NULL == with) { 
             argz_delete (argz, argz_len, str);
             return OK;
             }
         else {
              len1 = strlen(str);
              len2 = strlen(with);
              buf = (char*)malloc((*argz_len)-len1+len2);
              j = 0; k = 0;
              for (i = 0; i < (*argz_len); i++) {
                   if (i == ind) {
                         while(k <= len2) {
                           buf[j] = with[k]; 
                           j++;
                           k++;
                           }
                       i += (len1+1);    
                      }
                   buf[j] = (*argz)[i];
                   j++;   
                   }
              (*argz_len) += (len2-len1);
              (*argz) = NULL;
              (*argz) = buf;                                  
               printf("The element is replaced\n");     
              return OK;
              }    
         }
     }                      
}
      
void argz_print(const char *argz, size_t argz_len) {
    int i; 
    for(i = 0; i< argz_len; i++)
        printf("%c", argz[i]);
        printf("\n");
     }

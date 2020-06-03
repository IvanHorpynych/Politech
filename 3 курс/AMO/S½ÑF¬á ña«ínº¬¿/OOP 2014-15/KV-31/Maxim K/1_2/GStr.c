/*
File: GStr.c
Synopsis: declaration of functions described in Hstr.h
Author: Maxim K.E.
Group: KV-31, the faculty of applied math (FPM)
Created: 15.10.2014
*/


#include "Hstr.h"
error_t argz_create_sep(const char *string, int sep, char **argz, int *argz_len){
	int i=0;	
	char *dstr;
	if (strlen(string) != 0){
		*argz_len = strlen(string) ;		
		dstr = (char *)malloc(*argz_len);
		for ( i = 0; i < *argz_len; i++)
		{
		if (string[i] != sep) dstr[i] = string[i];
		else dstr[i] = '\0';
		}
		dstr[*argz_len] = '\0';
		*argz = dstr;
		return OK;
	}
	else{
		printf("String is empty! \n");
		return ENOMEM;
	}
}

size_t argz_count (const char*argz, size_t arg_len)
{ int i=0,count=1;
if (strlen(argz)!=0) 
{
	for (i=0;i<arg_len;i++)
	{ if (argz[i]=='\0') count++ ;}
	return count;
     }
else {	printf("String is empty!\n");
return 0;}
}

error_t argz_add(char **argz,size_t *argz_len,const char * str)
 { int count=0,i=0,j,k=0;
char*dopstr;
int size_add;
	if (strlen(argz) != 0 && strlen(str) != 0)
{     size_add = strlen(str) + *argz_len + 1;
	 dopstr=(char *)malloc(size_add* sizeof(char)); 
	
		for(i=0;i<*argz_len;i++)
		 dopstr[i] = argz[0][i];
		 dopstr[*argz_len] ='\0';
		for(k=0;k<strlen(str)+1;k++){
			i++;
			dopstr[i] = str[k];		
		}
		dopstr[size_add]='\0';
	*argz_len = size_add;		
		* argz=dopstr;		 	
 return OK;
} else return ENOMEM;
 }
void argz_delete(char **argz, int *argz_len, char *entry){
	int i=0,k=0,j=0;
		int flag=0;
	char *dstr;	
	if (strlen(*argz) != 0){		
		dstr = (char *)malloc(*argz_len);		
		for (i=0;i<=*argz_len;i++){
			dstr[i]=argz[0][i];
		}
		i=0;
		while ( i < *argz_len)		
		{	
	        if (dstr[i]==entry[0]) 
			{
				for (j=0;j<=strlen(entry);j++){
					if (dstr[i+j]==entry[j]) flag=1; 			
	else {flag=0; break;}
				}		
			 if (flag==1) {
	for(k=i;k<=(*argz_len);k++){
		dstr[k]=dstr[strlen(entry)+k+1];
		if (k>=(*argz_len-strlen(entry))) dstr[k]='\0';		
	} 
			i--;	
				*argz_len = *argz_len - strlen(entry) - 1;
	} 
		 else	 i++;
			}
			else  i++;	
		}			
		dstr[*argz_len] = '\0';
		*argz=dstr;
			}
	else{
		printf("String is empty! \n");		
	}
}
error_t argz_insert(char **argz,size_t *argz_len,char *before, char *entry)
{int i=0,k=0,dcon=0,j=0;
		int flag=0;
	char *dstr;
		if (strlen(*argz) != 0){		
		
		 dstr = (char *)malloc(*argz_len+strlen(entry));
		for (i=0;i<=*argz_len;i++){
			dstr[i]=argz[0][i];
		}
		i=0;
		while ( i < *argz_len)		
		{	
	        if (dstr[i]==before[0]) 
			{
				for (j=0;j<=strlen(before);j++){
					if (dstr[i+j]==before[j]) flag=1; 			
	else {flag=0; break;}
				}		
			 if (flag==1) {
			
				 for (dcon=0; dcon<=strlen(entry);dcon++){
	for(k=(*argz_len+strlen(entry));k>=(i+1);k--)
	{
		dstr[k]=dstr[k-1];			
	}
				 }
 for (dcon=0; dcon<=strlen(entry);dcon++) dstr[i+dcon]=entry[dcon];				 
			i=i+strlen(before)+strlen(entry);	
				*argz_len = *argz_len + strlen(entry) + 1;
	} 
		 else	 i++;
			}
			else  i++;	
		}			
		dstr[*argz_len] = '\0';
		*argz=dstr;
		return OK;
			}
	else{
		printf("String is empty! \n");	
		return ENOMEM;
	}
} 
char *argz_next(char *argz,size_t argz_len,const char *entry)
{
int i=0,j=0,flag=0;
	char*pointer=0;		
	pointer= (char *)malloc(argz_len);
		if (entry == 0){			
			pointer = argz;
		return pointer;
		}
	else{
		for (i = 0; i < argz_len; i++){
			if (argz[i] == entry[0])
			
			{
				for (j=0;j<=strlen(entry);j++){
					if (argz[i+j]==entry[j]) { flag=1; 	
					if (j >= strlen(entry)) break;}
	else {flag=0; break;}
				}		
			 if (flag==1)   	
				pointer = &argz[i+strlen(entry)+1] ;
	
			else pointer = NULL;
		return pointer;
			}
		}
	}
	}
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with)
{
int i=0,k=0,j=0,dcon=0;
		int flag=0;
	char *dstr;	
	if (strlen(*argz) != 0){		
		dstr = (char *)malloc(*argz_len);		
		for (i=0;i<=*argz_len;i++){
			dstr[i]=argz[0][i];
		}
		i=0;
		while ( i < *argz_len)		
		{	
	        if (dstr[i]==str[0]) 
			{
				for (j=0;j<=strlen(str);j++){
					if (dstr[i+j]==str[j]) flag=1; 			
	else {flag=0; break;}
				}		
			if (flag==1) {        
			if (strlen(str)<strlen(with)){
				 for (dcon=0; dcon<=(strlen(with)-strlen(str));dcon++)
				 {	for(k=(*argz_len+strlen(with));k>=(i+1);k--)
		dstr[k]=dstr[k-1];
						 }
 for (dcon=0; dcon<=strlen(with);dcon++) dstr[i+dcon]=with[dcon];
				 
			i=i+strlen(with);
			
				*argz_len = *argz_len + strlen(with)-strlen(str) + 1;				
	} else  {
		for (dcon=0; dcon<=strlen(str);dcon++){
				if (dcon>strlen(with) ){
					for (j= i+ strlen(with);j<=*argz_len+strlen(with);j++) dstr[j]=dstr[j+1];
				}
				else {
		dstr[i+dcon]=with[dcon];
				}
		}	
			i=i+strlen(with);
				*argz_len = *argz_len + strlen(with)-strlen(str) + 1;
						}
			}
		 else	 i++;
			}
			else  i++;	
		}			
		dstr[*argz_len] = '\0';
		*argz=dstr;
		return OK;
			}
	else{
		printf("String is empty! \n");	
		return ENOMEM;
	}
} 

void argz_print(const char *argz, size_t argz_len)
{int i=0;
	if (strlen(argz) != 0)
	{
		for ( i = 0; i < argz_len; i++)
			printf("%c", argz[i]);
		
		printf("\n");
		
	}
	else
		printf("String is empty! \n");
}



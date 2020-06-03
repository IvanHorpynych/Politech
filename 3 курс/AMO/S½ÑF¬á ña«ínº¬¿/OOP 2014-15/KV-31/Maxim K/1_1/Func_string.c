/*
File: Func_String.c
Synopsis: declaration of functions described in Head_String.h
Author: Maxim K.E.
Group: KV-31, the faculty of applied math (FPM)
Created: 30.09.2014
*/


#include "Head_String.h"
int substr(const char*string1,const char*string2)
{
	char *str=strstr(string1,string2);
		if (str==NULL)
	{
		return 0;
	}
	else
		return str-string1;
}
int subseq(const char*string1, const char*string2)
{
 unsigned	int i,j,c,b ;
 int maxlen=0 ,length=0;
 if(strlen(string1)>=strlen(string2)) 
 {
	 for (i=0;i<=strlen(string1); i++)
	 {
		 for (j=0;j<=strlen(string2);j++)
		 {
			 if(string1[i]==string2[j])
			 { 
				 length=0;
				 for (c=i, b=j;(( string1[c]==string2[b])&& (string1[c]!='\0'));c++ ,b++)
									
				 length++ ;
				 if (length>maxlen )
						 maxlen=length;
			 }
		 }
	 }
 }
 else 
 {
	 for (i=0;i<=strlen(string2); i++)
	 {
		 for (j=0;j<=strlen(string1);j++)
		 {
			 if(string1[i]==string2[j])
			 { 
				 length=0;
				 for (c=i, b=j;(( string1[c]==string2[b])&& (string2[b]!='\0'));c++,b++)
				
					 length++;
				
					 if (length>maxlen )
						 maxlen=length;
			 }
		 }
	 }
 }
 return maxlen;
}
 char ispal(const char *string)
 {
	 int i;
	 int flag=0;
	
	 if ((strlen(string))%2==0) 
	 { 
		 for (i=0;( i<=((strlen(string)/2)-1));i++)
		   { 
			 if (string[i]==string[strlen(string)-i-1]) 
		      flag=1;
		    else 
		    {
			 flag=0;
			 break;
		     }
		 }
		 	 }
	 else 
			 { 
		 for (i=0;( i<=((strlen(string)/2)));i++)
		 { 
			 if (string[i]==string[strlen(string)-i-1]) 
		 flag=1;
		 else 			 
			 { 
				 flag=0;
				 break;
			 }
		 }
		 
	 }
	  if( flag==1 ) 
	  return 1;
  else return 0 ;
 }	  
 

 


char * makepal(const char * string)
{ int i;int counter = 0;
	
	if (ispal(string) == 1)
		return string;
	else{
	    char *temp;
		int max = strlen(string); 		
	temp =(char*) calloc(2*max, sizeof(char));
	
		for ( i = 0; (i < max); i++){		
			temp[i] = string[i];
		temp[max] = '\0';
		}
		i=0;		
			while (ispal(temp) == 0)
			{ for(i = 0; i<=counter; i++){
             temp[max+i] = string[counter-i]; }
               counter++;
                 }	
				return temp;
	}

}

double* txt2double(const char *str, int *size)
{
	double *dstring, numb;
	int i=0, j=0,toc=0,zap=0, count;

	if (strlen(str)==0) {
		*size = 0;
		return NULL ;
    } else {
		while (str[i] != '\0') 
		{
			if (str[i] == ';' || str[i] == '\0') {
				j++;
			}
			i++;
		}
	}	
	for (i = 0; i < strlen(str); i++)
	{
			if (str[i] == ';'){
				zap++;
				if( toc>1 ) return NULL;
				toc = 0;
			}
			if (str[i] == '.') 
				toc++;			
		}
		if( toc > 1)
		{			
			return NULL;
		}
	dstring = (double*)malloc(sizeof(double)*(j+1));
	*size = j+1;
	i = 0;
	j = 0;
	count = 0;
	while (str[i] != '\0') 
	{		numb = 0;
		while (str[i] != '\0' && str[i] != ';') 
		{
			if (str[i] <= '9' && str[i] >= '0')
			{
				numb = numb*10+ (str[i] - '0');
				if (count != 0) count *= 10;
			} else {
				if (str[i] == '.')
				{
					count = 1;
				} else {
					*size = 0;
					return NULL;
				}
			}
			i++;
		}
		
		if (count != 0) numb = numb/count;
		dstring[j] = numb;
		i++;
		count = 0;
		j++;
	}
	return dstring ;
}



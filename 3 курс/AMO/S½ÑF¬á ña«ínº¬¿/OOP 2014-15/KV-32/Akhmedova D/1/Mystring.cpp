#include "Mystring.h"
 
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>

int substr(const char *str1, const char *str2)
{   int A=strlen(str1);
    int B=strlen(str2);
    int p = -1,k,l;
       for (int i = 0; i<A; i++)
    {
        if (str1[i] == str2[0])
        { k=i;
	   for(l=1;l<B;l++)
	   {
		   k++;
		    if(str1[k]!=str2[l])
				break;
	   }
	     if(k-i+1==B && l!=B-1)
		 {p=i;
		 printf("Stroka 2 podstroka1 stroki 1\n");
		 break;}
	}
 }
	 if(p==-1)
		  printf("Stroka 2 NE podstroka1 stroki 1\n");
 return p;
}
int subseq(const char *str1, const char *str2)
	{ 
	int A=strlen(str1);
    int B=strlen(str2);
	int max = 0;
	int i,j,buf,k,p;
	for(i=0;i<A;i++)
	{
	   for(j=0;j<B;j++)
	   {     buf=0;
             k = i;
             p = j;
  while ((str1[k] == str2[p]) && (k < A) && (p < B))
            {
                ++buf;
                ++k;
                ++p;
            }
            if (buf > max)
                max = buf;
        }
    }
     
    return max;
}
char ispal(const char*s)
{int A=strlen(s);
int i;
int f=1;
	for(i=0;i<A/2;i++)
	if(s[i]!=s[A-i-1])
		{f=0;
	break;}
	return f;
}
char *makepal(const char *s)
{ int A=strlen(s);
  int f=1;
  int i=0,j=A-1,l=0,p=0,B,C,buf,n;  char s1[100];
    char *s2;
	if (ispal(s)==1)
	{	strcpy(s1,s);
	return s1;}
   for(i=0;i<j;i++)
    if(s[i]!=s[j])
	  { 		
		for(l=0;l<=i;l++)
		  { s1[l]=s[l];
            s1[l+1]='\0';
            j=j-p;
		    p=0;
          }
     }
	else
   {    j--;
		p++;
   }
	B=strlen(s1);
   s2 = (char *)malloc((A+B+1)*sizeof(char));
   for (i=0; i<A; i++)
	s2[i]=s[i];
    s2[i]='\0';
		   for (i=0; i<B; i++)
	   {  buf=s1[i];
		 s1[i]=s1[B-i-1];
		 s1[B-i-1]=buf;
		 i++;}
		   strcat(s2,s1);	
	return s2;
	} 
double* txt2double(const char *s1 , int *size)
{ double *m; int i=0,j=0,l=0,A=strlen(s1); char s[10];
  *size=0;
  while (i<=A)
   {
     while (i<A && s1[i]!=';')
	  { 
		if ((s1[i]<'0') && (s1[i]>'9') && s1[i]!='.')
	     {    
			l=-1;
			*size=0;
		    break; 
		 }
	  i++; 
	}
     if (l==-1)
		 break;
	i++;
	(*size)++;
    }
if (*size)
{i=0;
m=(double *)malloc((*size)*sizeof(double));  
while (i<=A)
  {
	j=0; s[j]='\0';
    while (i<=A && s1[i]!=';')
    { s[j]=s1[i];
	  i++; 
	  j++; 
	}
	i++;
	m[l]=atof(s);
	l++;}
}
 return m;
}

/************************************************************************
*file: mystring.c
*author: Ivanov Alexander
*written: 07/10/2014
*last modified: 07/10/2014
************************************************************************/
#include "mystring.h"



//функция возвращает индекс элемента в строке string1, с которого начинается подстрока, равная string2. 
int substr(const char *string1, const char *string2)
{ int i=0,j=0,p=1;
   while (i<strlen(string1))
	 {  while (string2[0]!=string1[i])
	   i++;
   if (i<strlen(string1))
   {p=i;
    j=0;
   
   while (j<strlen(string2) && p!=-1)
	{   if(string2[j]!=string1[i])
		   p=-1;
    j++; i++;
   
   }}
   else
	   p=-1;
   }
   return p;
	 	
}

int subseq(const char *string1, const char *string2)
{ int i=0,i1,j1,j,kol=0,max=0;
   while(i<strlen(string1))
    {j=0;
		while(j<strlen(string2))
	     { if(string1[i]==string2[j])
	        { kol=0;
		      i1=i;
		      j1=j;
		      while(i1<strlen(string1) && j1<strlen(string2) && string1[i1]==string2[j1] )
               { kol++;
			     i1++; 
				 j1++; 
               }
              if(max<kol)
		        max=kol;
	        }
          j++;
        }
    i++;
   }
return max;
}
char ispal(const char *string)
{int i=0,j,p=1, len=strlen(string);

	   j=len-1;
    while(i<len/2 && j>len/2 && p!=0)
		{if(string[i]!=string[j])
			p=0;
	 i++;
	 j--;
	}
   return p;
}
 char *makepal(const char *s1)
 { int i,j,l=0,p=0, len1=strlen(s1), len;  char s[100];

    char *s2;
	if (!(ispal(s1)))
	{
	i=0;
   j=len1-1;
   for(i=0;i<j;i++)
    if(s1[i]!=s1[j])
	  { 		
		for(l=0;l<=i;l++)
		  { s[l]=s1[l];
            s[l+1]='\0';
            j=j-p;

		    p=0;
          }
     }
	else
   { j--;
		p++;
   }
   len=strlen(s);
   s2 = (char *)malloc((len1+len+1)*sizeof(char));
   for (i=0; i<len1; i++)
	   s2[i]=s1[i];
    s2[i]='\0';
   for(i=len-1;i>=0;i--)
	{   s2[p=strlen(s2)]=s[i];
 
   s2[p+1]='\0';}
	
   return s2;}
	else
		return s1;
 }
double* txt2double(const char *s1 , int *size)
{ double *arr; int i=0,j=0,l=0,len=strlen(s1); char s[10];
  
  *size=0;
  while (i<=len)
   {
     while (i<len && s1[i]!=';')
	  { 
		if ((s1[i]<'0') && (s1[i]>'9') && s1[i]!='.' && s1[i]!=' ')
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
{
i=0;
arr=(double *)malloc((*size)*sizeof(double));  
while (i<=len)
  {
	j=0; s[j]='\0';
    while (i<=len && s1[i]!=';')
    { s[j]=s1[i];
	  i++; 
	  j++; 
	  s[j]='\0';
	}
     
	i++;
    
	arr[l]=atof(s);
	l++;}
}

 return arr;
}


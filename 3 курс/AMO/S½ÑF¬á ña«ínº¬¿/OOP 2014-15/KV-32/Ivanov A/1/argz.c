/************************************************************************
*file: argz.c
*author: Ivanov Alexander
*written: 07/10/2014
*last modified: 07/10/2014
************************************************************************/

#include "argz.h"

error_t argz_create_sep(const char *string, char sep, char **argz, size_t *argz_len)
{ int i,j=0, len=strlen(string);
    if ((*string)==NULL || sep==NULL)
		return ENOMEM;
	else
    {*argz=(char *)malloc((strlen(string)+1)*sizeof(char));
    for(i=0;i<len;i++)
     { if (string[i]==sep)
         (*argz)[i]='\0';
	   else
         (*argz)[i]=string[i];
     }
    (*argz)[len] ='\0';
    *argz_len = len + 1;

    return OK;
	}
	
}
size_t argz_count (const char *argz, size_t argz_len)
{ int i; size_t n=0;
  for (i=0;i<argz_len;i++)
    {if(argz[i]=='\0')
       n++;
    }
    
  return n;
}
error_t argz_add (char **argz, size_t *argz_len, const char *str)
{ int i, len=strlen(str);
 if (**argz==NULL || *argz_len==NULL || *str==NULL)
	 return ENOMEM;
 else
 {
  *argz=(char *)realloc((void *)*argz,*argz_len+len+1);
  for (i=0;i<=len;i++)
    (*argz)[i+*argz_len]=str[i];
  (*argz_len)=(*argz_len)+len+1;
    
 return OK;
 }
}
void argz_delete(char **argz, size_t *argz_len, char *entry)
{  int j,i=0,p, len=strlen(entry);
  while(i<*argz_len)
   {j=0;
	p=0;
	while ((*argz)[i]!='\0')
	 {if (entry[j]==(*argz)[i])
	    p++;
	  i++;
	  j++;
	 }
	
	if (p==len)
	 {while(i<*argz_len)
       {(*argz)[i-len-1] = (*argz)[i];
        i++;
       }
	  break;
	 }
	else
	 i++;
   }
  *argz=(char *)realloc((void *)*argz,*argz_len-len);
  (*argz_len)=(*argz_len)-len-1;
    
}
error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry)
{ int j,i=0,p, len=strlen(entry);
if (**argz==NULL || *argz_len==NULL || *before==NULL || *entry==NULL)
	return ENOMEM;
else
{
 *argz = (char *)realloc((void *)*argz, *argz_len + len + 1);
 while(i<*argz_len)
  {
   j=0;
   p=0;
   while ((*argz)[i] != '\0')
    {if (before[j]==(*argz)[i])
	  p++;
	 i++;
     j++;
	}
   if (p==strlen(before))
	{for (j=*argz_len;j>i;j--)
	   (*argz)[j+len+1]=(*argz)[j];
	 break;
    }
	 else
	   {i++;
	   
	   }
  }
 for (j = 0; j <=len; j++)
  (*argz)[j+i+1] = entry[j];
 (*argz_len) += len+1 ;
    
 return OK;
}
}
char *argz_next(char *argz, size_t argz_len, const char *entry)
{char *s; int j,i=0,p;
 if (entry!=0)
 {while(i<argz_len)
  {
   j=0;
   p=0;
   while (argz[i] != '\0')
    { if (entry[j]==argz[i])
	    p++;
      i++;
      j++;
    
	}
   if (p==strlen(entry))
    {i++;
      break;
   }
   else
	   i++;
  }
   
 return argz +i;
 }
 else
  return argz;
}
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with)
{ int k,j,i=0,p, len_s=strlen(str), len_w=strlen(with);
if (**argz==NULL || *argz_len==NULL || *str==NULL || *with==NULL)
	return ENOMEM;
else
{
 while(i<*argz_len)
  {
   j=0;
   p=0;
   while((*argz)[i]!='\0')
    {if (str[j]==(*argz)[i]);
       p++;
	 i++;
	 j++;
	}
   if(p==len_s)
	{i=i-len_s;
	 k=len_s-len_w;
	 if(len_s>len_w)
	  {j=i+len_s;
	   while(j<*argz_len)
	    {(*argz)[j-k]=(*argz)[j];
		 j++;
		}
       *argz=(char *)realloc((void *)*argz,*argz_len-k);
	  } 
     else
	  {j=*argz_len;
	   *argz=(char *)realloc((void *)*argz,*argz_len-k);
	   while(j>i+len_s)
	    {(*argz)[j-k]=(*argz)[j];
		 j--;
		}
	  } 
        break;
	 }
	else
     i++;
	 
   }
  for(j=0;j<=len_w;j++)
   (*argz)[j+i] = with[j];
  *argz=(char *)realloc((void *)*argz,*argz_len-k);
  (*argz_len)=(*argz_len)-k;
    
 return OK;
}
}
void argz_print(const char *argz, size_t argz_len)
{int i;
 for (i=0;i<argz_len;i++)
  {if(argz[i] == '\0')
     printf("'\\0'");
   else
     printf("%c", argz[i]);
  }
 printf("\n");
}
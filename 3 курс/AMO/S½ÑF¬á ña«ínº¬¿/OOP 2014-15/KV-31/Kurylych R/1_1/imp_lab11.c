#include "lab11.h"

int substr(const char *str1, const char *str2){
	int len1=strlen(str1), len2=strlen(str2);
	int i,j;
	if (len1<len2) return -1;
	for(i = 0; i<len1;i++){
		if(str1[i] == str2[0]){
			for(j = 1;j<len2;j++){
				if(str1[i+j] != str2[j]) break;
				if(j == len2-1) return i;
			}
		}
	}
	return -1;
}


int subseq(const char *str1, const char *str2){
	int i,j,ind, res, max_res = 0;  
	int len1 = strlen(str1), len2 = strlen(str2);
	if (len1== 0 || len2 == 0) return 0;
	if(len1>=len2){     
	for(i = 0; i<len1;i++){
		ind = 0;  
		res = 0;
		for(j = 0;j<len2;j++){
			if(str1[i+ind]==str2[j]){
				res++;
				ind++;
			} else ind = 0;
		}
		if(res>max_res) max_res = res; 
	 }
	}else{
		for(i = 0; i<len2;i++){
		ind = 0;  
		res = 0;
		for(j = 0;j<len1;j++){
			if(str1[i+ind]==str2[j]){
				res++;
				ind++;
			} else ind = 0;
		}
		if(res>max_res) max_res = res;
	}
   }
	return max_res;
}


char ispal( const char *str){
	int i;
	int len=strlen(str);
	for(i=0; i<(len+1)/2; i++){
		if(str[i] != str[len-i-1]) return 0;
	} 
    return 1;
}

char* makepal( const char *str){
	int i,ind;
	int len=strlen(str);
	char *str1;
	str1=(char*)calloc(2*len,sizeof(char));
	for(i=0; i<len; i++){
		str1[i]=str[i];
	}
    
	ind=0;
	while (ispal(str1) == 0){
	   for(i=0; i<=ind; i++){
		   str1[len+i]=str[ind-i];
	   }
	   ind++;
	} 
	return str1;
}

double* txt2double(const char *str , int *size){
	int i, b=0;
	int len=strlen(str);
	int len_goga = 0;
	int ind_mas=0;
	char *goga;
	double *mas;
	if (len == 0) {
		*size = 0;
		return mas;
	}

	for(i = 0; i<len; i++){
		if((str[i]<'0' || str[i]>'9')&& str[i]!=';' && str[i]!='.'){
			*size = -1;
			printf("Error: String have letter \n");
			return mas;
		}
	}

	for (i=0; i<=len; i++){
		if (str[i] == ';'|| str[i] == '\0'){
			b++;
		}
	}
	*size=b;
	mas=(double*)calloc(b,sizeof(double));

	for (i=0; i<=len; i++) {
		if (str[i] == ';'|| str[i] == '\0') {
			goga=(char*)calloc((len_goga+1),sizeof(char));
			for(b = 0; b<len_goga; b++){
				goga[b]=str[i-len_goga+b];
			}
			goga[len_goga] = '\0';
			mas[ind_mas] = atof(goga);
			ind_mas++;
			len_goga = 0;
			free(goga);
		}
		else len_goga++;
	}
	return mas;
}

/*******************************************************************
*Laboratory work #1
*File: lab11_Implementation.c
*Description: This file describes functions which works with strings
*These functions are declared in the file "lab11_Header.h"
*Author: Anastasiev D. V.
*written 18/09/2014
*last modified: 18/09/2014
*******************************************************************/


#include "Heder_lab1.h"


int substr(const char *str1, const char *str2){  
	int i,j,ind = -1;
	int len1 = strlen(str1), len2 = strlen(str2);
	if(len1<len2) return -1;
	for (i = 0; i<len1 - len2 + 1; i++){            //Убираем лишние итерации цыкла(len1-len2+1)
		if (str1[i] == str2[0]){                  //Проверка на равность первого символа подстроки с символов основной строки
			ind = i;                             
			for(j = 0;j<len2;j++){            //проверка остальных символов подстроки 
				if(j == len1 || str1[i+j]!=str2[j] ){ 
					ind = -1;
					break;
				}
			}
			if (ind != -1) return ind; // подстрока найдена
		}
	}
	if(ind == -1) return ind;
}

//*****************************************************************************************************
int subseq(const char *str1, const char *str2){
	int i,j,ind, res, max_res = 0;  //res  -  поточная длинна подпоследовательности, max_res - максимальная длинна подпоследовательности
	int len1 = strlen(str1), len2 = strlen(str2);
	if (len1 == 0 || len2 == 0) return 0;
	if(len1>=len2){     //Для избежания лишних итераций цыкла, первый цыкл должен пробегать по строке которая длиннее 
	for(i = 0; i<len1;i++){
		ind = 0;  //Здвиг символов первой строки при совпадени поточного 
		res = 0;
		for(j = 0;j<len2;j++){
			if(str1[i+ind]==str2[j]){
				res++;
				ind++;
			} else ind = 0;
		}
		if(res>max_res) max_res = res; //Сравнения поточной длинны подпоследовательности с максимальной 
	 }
	}else{
		for(i = 0; i<len2;i++){
		ind = 0;  //Здвиг символов первой строки при совпадени поточного 
		res = 0;
		for(j = 0;j<len1;j++){
			if(str2[i+ind]==str1[j]){
				res++;
				ind++;
			} else ind = 0;
		}
		if(res>max_res) max_res = res;
	}
   }
	return max_res;
}


//******************************************************************************************************
char ispal( const char *str){
	int len = strlen(str) , i;
	for (i = 0;i<len/2;i++){
		if (str[i] != str[len - i-1]) return 0;
	}
	return 1;
}

//******************************************************************************************************
char* makepal(const char *str){
	char *res_str;
	int len = strlen(str);
	int i, k = 0;

	if(len == 0) return "Empty string";    //Возврат сообщения в случае пустой строки 

	res_str = (char*)calloc(2*len,sizeof(char)); //Выдиление памяти для в строку которая возвращает результат		
	for(i = 0; i<len; i++){
		res_str[i] = str[i];   //Переписываем заданую строку в новую 
	}
	while (ispal(res_str) != 1){
			for(i = 0; i<=k; i++){
				res_str[len+i] = str[k-i];
				res_str[len+i+1] = '\0';                                 //Добавляем в новую строку символы которых не хватает до создания палиндрома 
			}
			k++;

	}


	return res_str;
}

//*****************************************************************************************************
double* txt2double(const char *str , int *size){
	char *st;
	double *v = NULL;
	int len = strlen(str), i, num_len = 0, k = 0, ind_vect = 0;
	if(len == 0) {
		*size = 0;
		printf("Error: String is empty \n");
		return v;
	}
	for(i = 0; i<len; i++){
		if((str[i]<'0' || str[i]>'9')&& str[i]!=';' && str[i]!='.'){
			*size = -1;
			printf("Error: String have letter \n");
			return v;
		}
	}
	for(i = 0; i<=len; i++){
		if((str[i] == ';') || (str[i] == '\0')) k++;
	}
	*size = k;
	v = (double*)malloc(*size * sizeof(double));
	
	for(i = 0; i<=len; i++){
		if(str[i]==';' || str[i] == '\0'){
			st = (char*)calloc(num_len+1,sizeof(char));
			k = 0;
			for(k = 0 ; k<num_len; k++) st[k] = str[i-num_len+k];
			v[ind_vect] = atof(st);
			ind_vect++;
			free(st);
			num_len = 0;
		} else num_len++;
		
	}
	return v;
}
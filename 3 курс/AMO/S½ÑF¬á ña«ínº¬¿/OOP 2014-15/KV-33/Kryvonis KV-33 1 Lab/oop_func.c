/******************************************************************
*Laboratory work 1
*File: oop_func.c
*Description: This file describes functions, which do some actions with strings
*These functions are declared in the include file "oop_first_lab.h".
*Author: Artem Kryvonis
*written: 08/09/2014
*last modified 13/09/2014 256 day =) 
******************************************************************/

#include "MyHeadFile.h"
#include <stdbool.h>

/*Функція що повертає індекс елемента в рядку string1, з якого починається підрядок, рівний string2. */

int substr(const char *s1, const char *s2){
	bool flag = false;							//Прапорець для встановлення наявності підрядка
	int tmp = -1;								//Для запамятовування позиції підрядка
	int len1 = strlen(s1);
	int len2 = strlen(s2);
	if (len1 < len2 || len1 == 0 || len2 == 0){			//Перевірка на пустоту і чи підрядок меньший за сам рядок
		return -1;
	}
	/*Шукаємо перше співпадіння flag = true , а далі рухаємось по підрядку перевіряючи чи співпадають букви,
	якщо помічяємо різницю то flag = false знову йдемо по першому рядку шукаючи співпадіння з підрядком, до поки підрядок не співпаде
	*/
	for (int i = 0; i < len1; i++){
		if (s1[i] == s2[0]){
			for (int j = 0; j < len2; j++){
				flag = true;
				tmp = i;
				if (s1[i + j] != s2[j]){
					flag = false;
					break;
				}

			}

		}
	}
	if (flag)
		return tmp;
	else{
		return -1;
	}
}
/*Функція що повертає найбільшу довжину  спільної підпослідовності символів рядків string1 й string2*/
int subseq(const char *s1, const char *s2){
	int i, j, k;
	int	result;									//для підрахунку кількості співпадінь 
	int minlen;									//мінімальна довжина рядка 
	int sumlen;									//сума довжин
	int max = 0;
	int len1 = strlen(s1), len2 = strlen(s2);
	if (len1 == 0 || len2 == 0) return 0;			//Перевірка на пустоту

	sumlen = len1 + len2;
	/*Обчислення мінімальної довжини для подальшого алгоритму*/
	
	/*рухаємось в першому циклі по рядку в якого найбільша довжина,
	  беремо першу букву в найдовшому рядку і шукаємо співпадіння в другому рядку
	  якщо найшли починаємо рухатись далі використовуючи змінну К по першому і другому рядку
	  збільшуючи змінну в яку записується результат співпадінь.
	  Перевіряєм чи ця послідовність є найбільшою "result>maх".
	  Після цього алгоритму вертаєм довжину максимальної послідовності.
	  */
	if (len1 >= len2){
		minlen = len2;
		for (i = 0; i < len1; i++){
			k = 0;
			result = 0;

			for (j = 0; j<len2; j++){
				if (s1[i + k] == s2[j]){
					result++;
					k++;
				}
				else k = 0;
			}
			if (result>max) max = result;

		}
	}
	else{
		for (i = 0; i < len2; i++){
			k = 0;
			result = 0;

			for (j = 0; j<len1; j++){
				if (s2[i + k] == s1[j]){
					result++;
					k++;
				}
				else k = 0;
			}
			if (result>max) max = result;

		}
	}
	return max;
}
/*Функція що повертає 1, якщо string є паліндромом й 0 – у противному випадку*/
char ispal(const char *s){
	// якщо і-тий символ неспівпадає з len-i-1 то тоді слово не являється поліндромом
	int len = strlen(s);
	for (int i = 0; i < len / 2; i++){
		if (s[i] != s[len - i - 1]) return 0;
	}
	return 1;
}
/*Функція що одержує як параметр вказівник на рядок символів,
перетворює його на паліндром додаючи до нього найменше число символів у кінець рядка
й повертає покажчик на рядок з паліндромом*/
char* makepal(const char *s){
	char *res;										//результуючий рядок куди записуєм поліндром
	int len = strlen(s);
	bool flag=true;
	if (len == 0) {
		res = "Exeption:INPUT STRING IS EMPTY\n";
		return res;
	}
	if (ispal(s)) return s;

	res = (char*)calloc(2 * len, sizeof(char));		//виділяємо память для рядка в 2 рази більшу за заданий рядок оскільки потрібно дописати ще символ "\0"
	int tmp,j=0;

	strcpy(res, s);
	for (int i = 0; i < len - 1; i++){
		while (j <= i){
			res[len + j] = s[i - j];
			j++;
		}
		if (ispal(res)) return res;
		j = 0;

	}
	return res;
}
/*Функція що розміщає числа в динамічному масиві й повертає покажчик на нього*/
double* txt2double(const char *s, int *size){
	int len = strlen(s);
	double *resvect = NULL;												//результуючий масив чисел
	int k = 0, num_len = 0 , vect_ind = 0;								//k-кількість ";"  num_len - довжина цифри
	char *str;
	/* 
	Перевірка на те чи є в заданому рядку непотрібні за умовою символи
	*/

	if (len == 0) return 0;

	for (int i = 0; i < len; i++){
		if ((s[i] < '0' || s[i] > '9') && s[i] != '.' && s[i] != ';'){
			*size = 0;
			return resvect;
		}
	}
	/*
	Підрахунок кількості цифр які потрібно записати
	*/
	for (int i = 0; i <= len; i++){
		if (s[i] == ';' || s[i] == '\0') k++;
	}
	*size = k;
	// виділяємо память для ншого результуючого масиву
	resvect = calloc(*size, sizeof(double));

	/*
	Рухаємось посимвольно, 
	Якщо зустрічаємо ";" виділяємо память для рядка в який записуємо данне число яке щойно считали
	заповнюємо рядок в циклі зі змінною К. записуємо в комірку результуючого масиву тільки що знайдене число
	обнуляємо довжину цифри і звільняємо виділенну память під рядок
	Інакше збільшуємо довжину цифри.
	*/
	for (int i = 0; i <= len; i++){
		if (s[i] == ';' || s[i] == '\0'){
			str = (char*)malloc((num_len + 1)*sizeof(char));
			for (k = 0; k < num_len; k++) str[k] = s[i - num_len + k];
			resvect[vect_ind++] = atof(str);
			num_len = 0;
			free(str);
			str = NULL;
		}
		else num_len++;
	}
	return resvect;
}
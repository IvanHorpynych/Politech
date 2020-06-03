#include "argz.h"
#include <locale.h>

void main(){

	error_t func;
	char *argz;
	size_t argz_len;

	
	char *string = "SHELL=/bin/bash:usr=monty:PWD=/bin/monety:LANG=en_US.UTF-8";

	setlocale(LC_ALL, "Russian");

	func = argz_create_sep(string, ':',&argz, &argz_len);
	if(func == OK)argz_print(argz, argz_len);
	else printf("Помилка \n");
	printf("Кiлькiсть слiв = %d\n\n\n", argz_count(argz,argz_len));
	
	printf("Додавання: \n");
    func = argz_add(&argz, &argz_len, "goga");
	if(func == OK)argz_print(argz, argz_len);
	else printf("ERROR \n");

	
	printf("Видалення: \n");
	argz_delete(&argz,&argz_len,"LANG=en_US.UTF-8");
	argz_print(argz,argz_len); 


	printf("Вставка: \n");
	func = argz_insert (&argz, &argz_len, "usr=monty", "aaa");
	if(func == OK)argz_print(argz, argz_len);
	else printf("ERROR \n");


	printf("Перехiд на наступний : \n");
	printf("%s\n\n\n",argz_next(argz,argz_len,"usr=monty"));
	
		
	printf("Перестановка : \n");
	func = argz_replace(&argz, &argz_len, "SHELL=/bin/bash", "Поставте А");
	if(func == OK)argz_print(argz, argz_len);
	else printf("ERROR \n");
}

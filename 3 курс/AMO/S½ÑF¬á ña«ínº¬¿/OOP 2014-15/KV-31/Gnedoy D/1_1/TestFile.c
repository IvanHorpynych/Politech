/***********************************************************************
*file: TestFile.c
*author: Gnedoj D. O.
*group: KV-31, FPM
*written: 10/09/2014
*last modified: 10/09/2014
************************************************************************/

#include "string_func.h"

int main()
{
	char *task_1_1 = "HelleH";
	char *task_1_2 = "eHl";
	printf("Task 1:\n\tstring_1 = %s\n\tstring_2 = %s\nAnswer = %d;\n\n", task_1_1, task_1_2, substr(task_1_1, task_1_2));
	
	char *task_2_1 = "HelleHas";
	char *task_2_2 = "eHadfel";
	printf("Task 2:\n\tstring_1 = %s\n\tstring_2 = %s\nAnswer = %d;\n\n", task_2_1, task_2_2, subseq(task_2_1, task_2_2));
	
	char *task_3 = "Mamma";
	printf("Task 3:\n\tstring_1 = %s\nAnswer = %d;\n\n", task_3, ispal(task_3));

	char *task_4 = "kristsirk";
	printf("Task 4:\n\tstring_1 = %s\nAnswer = %s;\n\n", task_4, makepal(task_4));

	char *task_5 = "23;23;23.4";
	int size_of_massiv = 0;
	printf("Task 5:\n\tstring_1 = %s\nAnswer:\n", task_5);
	double *massiv = txt2double(task_5, &size_of_massiv);
	
	if (size_of_massiv == 0) printf("String is fail!\n");
	else
		for (int i = 0; i < size_of_massiv; i++)
			printf("\t%8.2f \n", *(massiv+i));

return 0;
} 
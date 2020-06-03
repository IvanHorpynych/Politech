#include "Head_String.h"

int main()
{
	//int *msize = 0;
	char *task_1_1 = "FreshApple";
	char *task_1_2 = "Apple";
	char *task_2_1 = "IlikePotato";
	char *task_2_2 = "Potato";
	char *task_3 = "AbandonnodnabA";
	char *task_4 = "12343";



	int dcount = 0, i;
	double *vec;
	char *ch5 = "144.5;5.j7;159.8";



	printf("Task 1:\n\tstring_1 = %s\n\tstring_2 = %s\nAnswer = %d;\n\n", task_1_1, task_1_2, substr(task_1_1, task_1_2));

	printf("Task 2:\n\tstring_1 = %s\n\tstring_2 = %s\nAnswer = %d;\n\n", task_2_1, task_2_2, subseq(task_2_1, task_2_2));
	printf("Task 3:\n\tstring_1 = %s\nAnswer = %d;\n\nLength=%d;\n", task_3, ispal(task_3), strlen(task_3));
	printf("Task 4:\n\tstring_1 = %s\nAnswer = %s;\n\n", task_4, makepal(task_4));


	printf("Task 5\nstring=%s\n", ch5);
	vec = txt2double(ch5, &dcount);
	printf("Size of vector = %d\n", dcount);
	if (dcount != 0)
	{
		for (i = 0; i<dcount; i++)
			printf("%.1f\n", vec[i]);
	}

	getchar();
	return 0;
}
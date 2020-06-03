#include "string_l.h"

int main()
{
	int *msize = 0;
	char *task_1_1 = "HelleH";
	char *task_1_2 = "elleHt";
	char *task_2_1 = "HellhhehtyHas";
	char *task_2_2 = "HelleHafds";
	char *task_3 = "MbaxanaxabM";
	char *task_4 = "lera";

	printf("Task 1:\n\tstring_1 = %s\n\tstring_2 = %s\nAnswer = %d;\n\n", task_1_1, task_1_2, substr(task_1_1, task_1_2));
	printf("Task 2:\n\tstring_1 = %s\n\tstring_2 = %s\nAnswer = %d;\n\n", task_2_1, task_2_2, subseq(task_2_1, task_2_2));
	printf("Task 3:\n\tstring_1 = %s\nAnswer = %d;\n\nLength=%d;\n", task_3, ispal(task_3), strlen(task_3));
	printf("Task 4:\n\tstring_1 = %s\nAnswer = %s;\n\n", task_4, makepal(task_4));
	printf("Task 5:\n");
	int size3 = -1;
	txt2double("3.12;4.00;5.89", &size3);  //без & это переменная, а с адрес
	printf("Size= %d", size3);
	getchar();
	return 0;
}
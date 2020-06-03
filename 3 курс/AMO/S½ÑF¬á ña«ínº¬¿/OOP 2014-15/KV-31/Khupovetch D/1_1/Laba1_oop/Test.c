#include "header.h"

void main()
{ 
	int size;
	printf("pos = %ld\n", substr("1234567", "34"));
	printf("max lenght = %ld\n", subseq("1234567", "12567"));
	printf("%ld\n", ispal("12321"));
	printf("%s\n",makepal("abcddc"));
	printf("Vector\n");
	double *vec;
	vec = txt2double("123;49.6;54", &size);
	printf("size = %i\n", size);
	if (size > 0)
	{
		for (int i = 0; i < size; i++)
		{
			printf(" %6.5lf", vec[i]);
		}
	}
	printf("\n");
	_getch();
}
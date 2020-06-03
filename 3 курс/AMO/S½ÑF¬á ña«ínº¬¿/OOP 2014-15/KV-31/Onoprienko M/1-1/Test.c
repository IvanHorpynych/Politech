#include "header.h"

void main()
{
	int size;
	printf("pos = %ld\n", substr("siglbnj", "bn"));
	printf("max lenght = %ld\n", subseq("eirhotno", "irhob"));
	printf("%ld\n", ispal("kolgglok"));
	printf("%s\n", makepal("erhgllgh"));
	printf("Vector\n");
	double *vec;
	vec = txt2double(" ;87.9;36", &size);
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

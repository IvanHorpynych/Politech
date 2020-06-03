#include "MemAlloc.h"
#include <math.h>

int main ()
{
    int i;
    char leter = 'a';  
    char * A = mem_alloc(sizeof(char)*32);
	int * B = mem_alloc(sizeof(int)*32);
	double * C = mem_alloc(sizeof(double)*32);
	int ** matrix1 = mem_alloc(sizeof(matrix1) * 10);
    int ** matrix2 = mem_alloc(sizeof(matrix2) * 10);
	char * D = mem_alloc(64);
	int dz = 0;
	int j;
	mem_dump();
    //Ініціалізація блоку А
    for (i = 0; i < 32; i++)
        A[i] = leter + i;
    
    //Ініціалізація блоку Б
    for (i = 0; i < 32; i++)
        B[i] = 64*i + 1 + i;
    
    //ініціалізація блоку С
    for (i = 0; i < 32; i++)		
        C[i] = i*3.14 + 2.71*(i + 1);

    mem_free(B);
    B = mem_alloc(sizeof(int)*64);
    for (i = 0; i < 32; i++)
        B[i] = 64*i + 1 + i;
    
    for (i = 0; i < 32; i++)
        D[i] = leter + i;

    mem_free(A);
    A = mem_alloc(16);
    for (i = 0; i < 16; i++)
        A[i] = 'A';

    A = mem_realloc(A, 512);
    for (i = 0; i < 512; i++)
        A[i] = 'A';
	mem_dump();

    mem_free(B);
    mem_free(C);
    mem_free(D);
    mem_free(A); 

	mem_dump();
    
    for (i = 0; i < 10; i++)
        matrix1[i] = mem_alloc(sizeof(int) * 10);
mem_dump();
    for (i = 0; i < 10; i++)
        matrix2[i] = mem_alloc(sizeof(int) * 10);
mem_dump();
    for (i = 0; i < 10; i++)
        for (j = 0; j < 10; j++)
            matrix1[i][j] = i*j;

    for (i = 0; i < 10; i++)
        for (j = 0; j < 10; j++)
            matrix2[i][j] = matrix1[i][j];

    
    for (i = 0; i < 10; i++)
        for (j = 0; j < 10; j++)
            dz = matrix2[i][j];

    for (i = 0; i < 10; i++)
    {
        mem_free(matrix1[i]);
        mem_free(matrix2[i]);
    }
mem_dump();
    mem_free(matrix1);
    mem_free(matrix2);
mem_dump();
    return 0;
}

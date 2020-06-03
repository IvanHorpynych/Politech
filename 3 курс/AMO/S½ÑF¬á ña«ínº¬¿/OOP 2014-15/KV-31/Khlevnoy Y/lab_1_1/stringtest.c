/************************************************************************
*file: stringtest.c
*synopsis: test-drive file of UDF-functions declared in "mystring.h".
*author: Khlevnoy Y.A.
*written: 20/09/2014
*last modified: 12/10/2014
************************************************************************/


#include <stdio.h>
#include <stdlib.h>
#include "mystring.h"

int stringtest();

int stringtest()
{

    double *vector;
	int s ,i;

//1
    printf("1. substr function:\n\"Hello, world!\" and \"world\"\n");
    printf("returned %d\n", substr("Hello, world!", "world"));

    printf("\"Hello, world!\" and \"word\"\n");
    printf("returned %d\n\n", substr("Hello, world!", "word"));

//2
    printf("2. subseq function:\n\"Welcome to my world.\" and \"Hello, world!\"\n");
    printf("returned %d\n", subseq("Welcome to my world.", "Hello, world!"));

    printf("\"Yes\" and \"No\"\n");
    printf("returned %d\n\n", subseq("Yes", "No"));

//3
    printf("3. ispal function:\n\"HAL 9000\"\n");
    printf("returned %d\n", ispal("HAL 9000"));

    printf("\"HAL 9000 0009 LAH\"\n");
    printf("returned %d\n\n", ispal("HAL"));

//4
    printf("4. makepal function:\n\"abcb\"\n");
    printf("returned %s\n", makepal("abcb"));

    printf("\"abcba\"\n");
    printf("returned %s\n\n", makepal("abcba"));

//5
    printf("5. txt2double:\n");
    vector = txt2double("12.2;256;14", &s);
    printf("INPUT: \"12.2;256;14\"\nvector's address is %p\n", vector);
    if(s != 1){
		for( i = 0; i < s ; i++ ) {
			printf("vector[%d]=%.2f\n", i, vector[i]);
		}
		printf("\n");
	}
	free(vector);

	vector = txt2double("12.2;256;14x", &s);
	printf("INPUT: \"12.2;256;14x\"\nvector's address is %p\n", vector);
    if(s != 1 ){
		for( i = 0; i < s; i++ ) {
			printf("vector[%d]=%.2f\n", i, vector[i]);
		}
		printf("\n\n");
	}
	free(vector);

    return 0;
}

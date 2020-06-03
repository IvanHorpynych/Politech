#include "MyString.h"

int main () {
	double *vect; int i, size = 0; 
	printf("~~~ Test: SUBSTR Function ~~~\n\n");
	printf("STRING1 = 'ABCDEDEF'; STRING2 = 'DE'; SUBSTR = %d\n\n", substr("ABCDEDEF", "DE"));
	printf("~~~ Test: SUBSEQ Function ~~~\n\n");
	printf("STRING1 = 'ABCDEF'; STRING2 = 'ABCD'; SUBSEQ = %d\n\n", subseq("ABCDEF", "ABCD"));
	printf("~~~ Test: ISPAL Function ~~~\n\n");
	printf("STRING = 'ABCDCBA'; ISPAL = %d\n\n", ispal("ABCDCBA"));
	printf("~~~ Test: MAKEPAL Function ~~~\n\n");
	printf("STRING = 'ABCD'; MAKEPAL = %s\n\n", makepal("¿¡—"));
	printf("~~~ Test: TXT2DOUBLE Function ~~~\n\n");
	printf("STRING = '-3.14;0.0;9.86'; TXT2DOUBLE = ");
	vect = txt2double("-3.14;0.0;9.86", &size);
	for (i = 0; i < size; i++) printf(" %f;", vect[i]);
	printf(" SIZE = %d\n\n", size);
	

}
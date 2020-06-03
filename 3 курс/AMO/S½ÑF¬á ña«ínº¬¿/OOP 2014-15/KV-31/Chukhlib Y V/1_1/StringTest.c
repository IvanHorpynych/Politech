#include "MyString.h"


int main(){

	double size;
	//txt2double("23;34.6;234;3.6;9", &size);
	//for (int i = 0; i < size; i++)
	//{
	//	printf("%d", &size[i]);

	//}



	int i;
	double * vec = txt2double("4c4;34.4;34545;0", &size);
	if (size > 0){

		for (i = 0; i < size; i++)
			printf("  %.3f\n", vec[i]);
	
	}
	


	//printf("%d  \n", txt2double("23;34.6;234;3.6;9", &size));
	return 0;
}

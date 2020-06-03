#include "lab11_head.h"

void main(){

	double *mas;
	int size;
	int i;

	printf("1. Substr  %d \n", substr("Hellooooooooo","loooo"));
    printf("2. Subsec %d \n", subseq("gogagoga","goga" ));
	printf("3. Ispal  %d \n", ispal("hoe"));
	printf("4. Makepol %s \n", makepal("goga"));
    printf("5. Txt2double \n\n");
	    
		mas = txt2double("3.14;2.177;7",&size);
		if(size!=0){
		for(i=0;i<size;i++){
			printf("vect[%d]=%.2f\n",i,mas[i]);
			}
		}
	}
#include "lab11.h"

void main(){

	double *mas;
	int size;
	int i;

	printf("1. Substr  %d \n", substr("Sonyvaio","yva"));
    printf("2. Subseq %d \n", subseq("romario","roma" ));
	printf("3. Ispal  %d \n", ispal("mama"));
	printf("4. Makepol %s \n", makepal("sony"));
    printf("5. Txt2double \n\n");
	    
		mas = txt2double("3.14;2.1;7",&size);
		if(size!=0){
		for(i=0;i<size;i++){
			printf("vect[%d]=%.2f\n",i,mas[i]);
			}
		}
		free(mas);
	}


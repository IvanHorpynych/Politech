/************************************************************************************
*file: OOP_lab3_test.c																*
*Synopsis: this is a test file with main function.									*
*in the "OOP_lab3.c".																*
*related files: OOP_lab3.c															*
*author: Chernysh Andrey															*
*written: 20/11/2014																*
*last modified: 26/11/2014															*
************************************************************************************/

#pragma warning (disable : 4996)
#include <stdio.h>
#include <stdlib.h>
#include "OOP_lab3.h"

int main(){
	char *dba = (char*)malloc(sizeof(char));
	printf("Make file?(y/n)\n");
	if (_getch() == 'y') {
		printf("\nEnter the name of your .dba file : ");
		scanf("%s", dba);
		is_open(dba);
	}
	else{
		printf("\nEnter the name of your .dba file : ");
		scanf("%s", dba);
	}

	printf("\nTask 1 : ");
	// We will write this struct into the .dba file;
	SCAN_INFO *example = (SCAN_INFO*)malloc(sizeof(SCAN_INFO));
	strcpy(example->manufacturer, "Canon75");
	strcpy(example->model, "LaserJet");
	example->optr = 100;
	example->price = 123.120003;
	example->x_size = 10;
	example->y_size = 15;
	example->year = 2015;
	if (write2dba(dba, example) != 0){
		_getch();
		return 0;
	}
	printf("\nIn your %s file you can see this scanners : \n", dba);
	read_from_dba(dba);
	printf("\n-----------------------------------------------");
	char *csv = (char*)malloc(sizeof(char));
	printf("\nEnter name of .csv file :");
	scanf("%s", csv);
	printf("\n\nTask 2 : \nNow you can see new %s file, which has scanners from %s file!", dba, csv);
	csv2dba(dba, csv);
	read_from_dba(dba);

	printf("\n-----------------------------------------------");
	int type = 0;
	printf("\n\nTask 3 : \n \Making index file : \n\t\t");
	printf("\nDo you want to see indexes in your file ?(y/n)");
	if (_getch() == 'y'){
		type = 1;
		printf("\n\tAnswer : ");
	}
	make_index(dba, "manufacturer", type);

	printf("\n------------------------------------------------");
	printf("\n\nTask 4 : \n To demonstrate this function, the programme will write in the console year of first scanner : ");
	printf("%d", get_recs_by_index(dba, "manufacturer.idx")->recs->year);

	printf("\n------------------------------------------------");
	printf("\n\nTask 5 : \n Now you can see files in your folder! ");
	printf("\nDo you want to see indexes in your files ?(y/n)");
	if (_getch() == 'y'){
		type = 1;
		printf("\n\tAnswer : ");
	}
	else type = 0;
	reindex(dba, type);

	printf("\n------------------------------------------------");
	printf("\n\nTask 6 : \n Deleting : ");
	del_scan(dba, 1);
	read_from_dba(dba);

	printf("\n------------------------------------------------");
	printf("\n\nTask 7 : \n Making text file : ");
	if (make_txt(dba, 20000) == 0)
		printf(".txt file has been successfully made");
	else printf(".txt file has not been made");
	printf("\n\t\t\t\tfinishing...");

	_getch();
	return 0;
}
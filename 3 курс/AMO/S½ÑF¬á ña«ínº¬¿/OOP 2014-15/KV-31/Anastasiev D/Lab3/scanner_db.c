/************************************************************************
*file: scanner_db.c
*purpose: file
*author: Anastasiev D.
*written: 24/10/2014
*last modified: 21/11/2014
*************************************************************************/
#include "Scanner.h"


void main(){
	FILE *f;
	SCAN_INFO test_scan = {"HP",2007,"JetScan7",959.99,210,297,300};
	SCAN_INFO test_scan1 = {"ASUS",2009,"AsScan",299.99,210,297,100};
	SCAN_INFO scan_temp;
	RECORD_SET *rec;
	int count, i;
	//////////////////////////////////////////////////
	first_input();
	/////////////////////////////////////////////////
	inputscanner(test_scan);
	inputscanner(test_scan1);
	//inputscanner(test_scan1);
	f = fopen("Scanners.dba", "rb");
	count = fgetc(f);
	printf("%d\n",count);
	for(i = 0;i<count;i++){
		fread(&scan_temp,sizeof(scan_temp),1,f);

		printf( "%s\n", scan_temp.manufacturer);
		printf( "%d\n", scan_temp.year);
		printf("%s\n", scan_temp.model);
		printf("%f\n", scan_temp.price);
		printf("%d\n", scan_temp.x_size);
		printf("%d\n", scan_temp.y_size);
		printf("%d\n", scan_temp.optr);
		printf("\n");
	}

	fclose(f);
///////////////////////////////////////////////////////////////////////
	printf("Make index\n");
	make_index("Scanners.dba","price");
	f = fopen("manufacturer.idx","rb");
	while(!feof(f)){
		i = fgetc(f);
		if(i != EOF)printf("%d ",i);
	}
	fclose(f);
////////////////////////////////////////////////////////////////////////
	reindex("Scanners.dba");
///////////////////////////////////////////////////////////////////////
	puts("\nDelete:\n");
	if(del_scan("Scanners.dba",0)){
		f = fopen("manufacturer.idx","rb");
		while(!feof(f)){
			i = fgetc(f);
			if(i != EOF)printf("%d ",i);
		}
	}
	fclose(f);
/////////////////////////////////////////////////////////////////////
	read_price_txt("Scanners.dba");
///////////////////////////////////////////////////////////////////
}


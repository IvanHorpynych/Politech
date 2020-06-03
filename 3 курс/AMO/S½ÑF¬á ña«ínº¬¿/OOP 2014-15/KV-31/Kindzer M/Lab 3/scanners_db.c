#include "scanners.h"

void main(){
	SCAN_INFO scan_temp, test = {"HP", 2014, "Envy", 998.5, 300, 300, 400},
		test1 = {"Alienware", 2013, "SomeModel", 1200, 400, 400, 400};
	int i;
	FILE *f;
	input_dba();
	printf("First input of Scanners.dba file:\n");
	print_dba();
	printf("|---------------------------------|\n");
	scanner_input(test);
	scanner_input(test1);
	printf("Scanners.dba after scanners_input:\n");
	print_dba();
	printf("|---------------------------------|\n");
	reindex("Scanners.dba");
	///////////////////////////////////////////////////////////////////////
	printf("Make index\n");
	make_index("Scanners.dba","manufacturer");
	f = fopen("manufacturer.idx","rb");
	while(!feof(f)){
		i = fgetc(f);
		if(i != EOF)printf("%d ",i);
	}
	fclose(f);
	printf("\n");
	/////////////////////////////////////////////////////////
	input_txt_by_value(1000);
	del_scan("Scanners.dba", 2);
	printf("Scanners.dba after del_scan 2:\n");
	print_dba();
}
/************************************************************************
*file: svanner_db.c
*synopsis:file for testing functions from scanner.h
*author: Kyrylo Svintsov
*written: 19.11.2014
*last modified: 26.11.2014
************************************************************************/
#include"Scanner.h"

FILE *fPTR;
RECORD_SET *ptr;

#define dba_name "Scaners.dba"

int main(){
	scan_file();
	scanner_r();
	reindex(dba_name);
	printf("\nCheck an idx files before reindexation.Press any button to continue");
	getchar();
	del_scan(dba_name,2);
	comf_price(dba_name, 200.00);
	ptr = get_recs_by_index(dba_name, "year");
	printf("\nAll operations is done");
	getchar();
	return 0;
}
/************************************************************************
*file: Testscanner.ñ
*author: Maxim K.E.
*group: KV-31, FPM
*written: 20/11/2014
*last modified: 9/12/2014
************************************************************************/
#include "scanner.h"
void main(){
	RECORD_SET *p;
	create_file("Scaners.csv", "Scaner.dba");
	make_index("Scaner.dba", "model");
p = get_recs_by_index("Scaners.dba", "price.idx");
//	printf("%s\n", p->recs[0].model);
	reindex("Scaners.dba");
	/*del_scan("Scaners.dba", 2);

	dba_to_txt("Scaners.dba", "Scaner.txt", 600);*/
	getchar();
}
/************************************************************************
*file: Lab3_Test.с
*author: Gnedoj D. O.
*group: KV-31, FPM
*written: 10/10/2014
*last modified: 15/10/2014
************************************************************************/

#include "Lab3.h"

void main(){
	create_file("Scaners.csv", "Scaner.dba");
 	reindex("Scaners.dba");

	RECORD_SET *p = get_recs_by_index( "Scaners.dba", "price.idx" );
//	printf("%s", p->recs[0].model);
	
	del_scan("Scaners.dba", 10); 
    from_dba_to_txt("Scaners.dba", "Scaners.txt", 1000.0);
	
}
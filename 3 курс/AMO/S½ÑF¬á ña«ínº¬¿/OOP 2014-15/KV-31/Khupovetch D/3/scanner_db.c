#include "scanner.h"

int main()
{
	char *name_file_dba = "Scaners.dba";
	//scan_file();
	//scanner_r();
	//reindex(name_file_dba);
	//RECORD_SET *help;
	//help = get_recs_by_index(name_file_dba, "year.idx");
	//printf("%d\n", help->rec_nmb);
	del_scan(name_file_dba, 2);
	//comf_price(name_file_dba, 1000.1);
	return 0;
}
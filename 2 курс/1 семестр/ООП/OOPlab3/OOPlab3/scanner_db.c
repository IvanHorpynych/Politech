#include "scanner.h"

int main(){
	RECORD_SET *rec;
	int del = 3, price = 999.00;
	// 1, 2
 	printf("1, 2 functions\n");
	print_dba("Scaners.csv");
	init_file();
	print_bin("Scaners.dat");

	// 3, 4, 5
	printf("3, 4, 5 functions\n");
	reindex("Scanner.dba");
	print_dba("Scanner.dba");
	rec = get_recs_by_index("Scaners.dba", "manufacturer");
	print_rec_set(rec);
	rec = get_recs_by_index("Scaners.dba", "model");
	print_rec_set(rec);
	rec = get_recs_by_index("Scaners.dba", "year");
	print_rec_set(rec);
	rec = get_recs_by_index("Scaners.dba", "price");
	print_rec_set(rec);
	rec = get_recs_by_index("Scaners.dba", "x_size");
	print_rec_set(rec);
	rec = get_recs_by_index("Scaners.dba", "y_size");
	print_rec_set(rec);
	rec = get_recs_by_index("Scaners.dba", "optr");
	print_rec_set(rec);
	printf("\n");

	//6
	printf("6 functions\n");
	printf("del %d element\n", del);
	del_scan("Scanner.dba", del);
	print_dba("Scanner.dba");

	//7
	printf("7 functions\n");
	printf("Elem. price <= %d\n", price);
	price_scan("Scanner.dba", "Scanner.txt", price);
	print_dba("Scanner.txt");

	return 0;
}

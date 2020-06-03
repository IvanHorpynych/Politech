#include "File_func.h"
#pragma warning(disable : 4996)
int main()
{
	FILE *f1 = NULL, *f2 = NULL;
	SCAN_INFO *sc = (SCAN_INFO *)malloc(sizeof(SCAN_INFO));
	SCAN_INFO *sci = (SCAN_INFO *)malloc(sizeof(SCAN_INFO));
	csvtodba(f1, f2);
	char *path = "D:/Projects/OOP_Lab3/Scaners.dba";
	f1 = fopen("D:/Projects/OOP_Lab3/Scaners.dba", "r+b");
	fseek(f1, sizeof(int) + 1*sizeof(SCAN_INFO), SEEK_SET);
	fread(sc, sizeof(SCAN_INFO), 1, f1);
	printf("%.2f\n", sc->price);
	reindex(path);
	//del_scan(path, 1);
	f1 = fopen("D:/Projects/OOP_Lab3/Scaners.dba", "r+b");
	fseek(f1, sizeof(int)+1 * sizeof(SCAN_INFO), SEEK_SET);
	fread(sc, sizeof(SCAN_INFO), 1, f1);
	printf("%.2f\n", sc->price);
	showbyprice(path, 600);
	return 0;
}
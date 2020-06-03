/************************************************************************
>file: TestFile.c
>author: Hlibov A.R
>group: KV-31, FPM
>written: 6/12/2014
>last modified: 9/12/2014
************************************************************************/

#include "Laba3.h"

void main(){
	write_from_csv("scanners.csv", "scanners.dba");
	del_from_dba("scanners.dba", 1);
	write_to_txt("scanners.txt", 350);
}
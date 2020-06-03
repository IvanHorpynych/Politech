#include "header.h"


int main (int argc, char* argv[]) {
	BisSuccMeth equat;

	//1st root
	equat.RunIteration(4.7, 6.3, 4.28, 7.55);
	equat.RunBisection(4.7, 6.3);

	//2nd root
	equat.RunIteration(8.1, 8.9, 7.98, 8.59);
	equat.RunBisection(8.1, 8.9);

	// 3d root
	equat.RunIteration(9.9, 10.1, 9.22, 10.14);
	equat.RunBisection(9.9, 10.1);

   return 0;
}
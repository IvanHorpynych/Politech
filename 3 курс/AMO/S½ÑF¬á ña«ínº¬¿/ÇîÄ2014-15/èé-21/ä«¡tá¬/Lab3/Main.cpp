#include <stdio.h>
#include <iostream>
#include "Metod_jedynogo_podilu.h"
#include "Metod_prostoji_iteratsiji.h"

int main(){
	
	double Massiv[4][5] = { { 27.0, 2.0, -13.0, 0.0, 10.0 },
							{ 15.0, 42.0, 17.0, 9.0, 275.0 },
							{ 1.0, 16.0, 22.0, 4.0, 158.0 },
							{ 1.0, 6.0, 1.0, 21.0, 49.0 } };
	double**M = new double*[4];
	for (int i = 0; i < 4; i++){
		M[i] = new double[5];
	}
	for (int i = 0; i < 4; i++){
		for (int j = 0; j < 5; j++){
			M[i][j] = Massiv[i][j];
		}
	}
	double Massiv1[4][5] = { { 6.0, 12.0, 20.0, 15.0, 155.0 },
							{ 15.0, 42.0, 17.0, 9.0, 275.0 },
							{ 1.0, 16.0, 22.0, 4.0, 158.0 },
							{ 10.0, 2.0, 17.0, 7.0, 103.0 } };
	double**M1 = new double*[4];
	for (int i = 0; i < 4; i++){
		M1[i] = new double[5];
	}
	for (int i = 0; i < 4; i++){
		for (int j = 0; j < 5; j++){
			M1[i][j] = Massiv1[i][j];
		}
	}
	//ObnulenniaStovptsia(M, 0, 4, 5);
	//ObnulenniaNyzhniojiChastyny(M, 4, 5);
	printf("Systema, prydatna do iteratsiji.\n");
	printf("(2)+2*(4)-2*(3)-(1)\n(2)\n(3)\n2*(1)-(3)-(4)\n");
	for (int i = 0; i < 4; i++){
		for (int j = 0; j < 5; j++){
			printf("%f ", M[i][j]);
		}
		printf("\n");
	}
	printf("\n\n");
	/*
	double* k = ZvorotnijHid(M, 4, 5);
	//ObnulenniaNyzhniojiChastyny(M, 4, 5);
	printf("\n\n");
	for (int i = 0; i < 4; i++){
		printf("X%d = %f\n", i + 1, k[i]);
	}
	*/

	double eps = 0.000000000000000000001;
	double *k = RozvjazanniaMetodomIteratsiji(M, 4, eps);
	printf("\n\n");
	printf("Rozvjazok metodom iteratsij.\n");
	for (int i = 0; i < 4; i++){
		printf("X%d = %f\n", i + 1, k[i]);
	}
	ObnulenniaNyzhniojiChastyny(M, 4, 5);
	double* k2 = ZvorotnijHid(M, 4, 5);
	printf("\n\n");
	printf("Rozvjazok metodom prostogo podilu.\n");
	for (int i = 0; i < 4; i++){
		printf("X%d = %f\n", i + 1, k[i]);
	}

	//*****************************************************************
	printf("Pochatkova systema.\n");
	for (int i = 0; i < 4; i++){
		for (int j = 0; j < 5; j++){
			printf("%f ", M1[i][j]);
		}
		printf("\n");
	}
	printf("\n\n");
	//ObnulenniaStovptsia(M, 0, 4, 5);
	ObnulenniaNyzhniojiChastyny(M1, 4, 5);
	printf("Rozvjazok metodom prostogo podilu.\n");
	for (int i = 0; i < 4; i++){
		for (int j = 0; j < 5; j++){
			printf("%f ", M1[i][j]);
		}
		printf("\n");
	}
	printf("\n\n");
	
	double* k1 = ZvorotnijHid(M1, 4, 5);
	//ObnulenniaNyzhniojiChastyny(M1, 4, 5);
	printf("\n\n");
	for (int i = 0; i < 4; i++){
		printf("X%d = %f\n", i + 1, k1[i]);
	}
	
	getchar();
	return 0;
}
#include "NormalnaSystema.h"

/*
Виконує створення і першопочаткове заповнення матриці, що відпловідає нормальній системі.
Розмірність системи залежить від необхідного ступеню поліному m.
*/
double** StvorenniaNormalnojiSystemy(int m, double a, double b, double eps, int r){
	double** ret = (double**)malloc((m + 1)*sizeof(double*));
	//FILE* f;
	//f = fopen("NormalnaSystema.txt", "wt");
	for(int i = 0; i <= m; i++){
		ret[i] = (double*)malloc((m + 2)*sizeof(double));
		for(int j = 0; j <= m; j++){
			ret[i][j] = PodvijnyjPererahunokVidDobutku1(ObchyslenniaMnogochlenaLezhandra, ObchyslenniaMnogochlenaLezhandra,
				ObchyslenniaIntegralaVidDobutku1, a, b, eps, r, i, j);
	//		fprintf(f, "%.8f\t", ret[i][j]);
		}
		ret[i][m + 1] = PodvijnyjPererahunokVidDobutku2(ObchyslenniaFunktsijiZaVariantom, ObchyslenniaMnogochlenaLezhandra,
				ObchyslenniaIntegralaVidDobutku2, a, b, eps, r, i);
	//	fprintf(f, "%.8f\n", ret[i][m + 1]);
	}
//fclose(f);
	return ret;
}
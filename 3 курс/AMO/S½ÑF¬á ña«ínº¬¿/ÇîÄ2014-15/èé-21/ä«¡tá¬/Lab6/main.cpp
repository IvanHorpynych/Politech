#include "Spline.h"

double F(double x){
	return 3*x*log10(x)*sin(x/1.7);
}

int main(){
	//double** TestSyst = PobudovaSystemyRivnian(NULL, NULL, 4);
	/*for(int i = 0; i <= 4; i++){
		for(int j = 0; j <= 5; j++){
			printf("%f\t",TestSyst[i][j]);
		}
		printf("\n");
		free(TestSyst[i]);
	}*/
	KubichnyjSplajn(F, 1.0, 10.0, 10, 10);
	getchar();
	return 0;
}
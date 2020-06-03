#include "MnogochlenLezhandra.h"
#include "Funktsija.h"
#include "FormulaTrapetsij.h"
#include "NormalnaSystema.h"
#include "Metod_jedynogo_podilu.h"
#include "SeredniokvadratychneVidhylennia.h"

int main(){
	
	int m = 5;
	double** TestMatr = StvorenniaNormalnojiSystemy(m, 1.0, 1.5, 0.01, 2);


	ObnulenniaNyzhniojiChastyny(TestMatr, m+1, m+2);
	double* Koreni = ZvorotnijHid(TestMatr, m+1, m+2);
	for(int i = 0; i <= m; i++)
		printf("%f\t", Koreni[i]);
	printf("\n");

	//перевірка правильності
	FILE* f = fopen("Test.txt", "wt");
	int testn = 50;
	double testh = (1.5-1.0)/testn;
	for(int i = 0; i < testn; i++){
		fprintf(f, "%.3f;", 2.0 + i*testh);
	}
	fprintf(f, "\n");
	for(int i = 0; i < testn; i++){
		double a = 0.0;
		for(int j = 0; j <= m; j++){
			a += Koreni[j]*ObchyslenniaMnogochlenaLezhandra(2.0+i*testh, j);
		}
		fprintf(f, "%.3f;", a);
	}
	fprintf(f, "\n");
	
	//********************************************************************
	int m1 = ObchyslenniaStepeniUzagalnenogoMnogochlena(ObchyslenniaFunktsijiZaVariantom, ObchyslenniaUzagalnenogoMnogochlenaLezhandra,
		ObchyslenniaIntegralaVidRiznytsi, 1.0, 1.5, 2, 0.09);
	printf("Stepin - %d\n", m1);
	fcloseall();
	getchar();
	return 0;
}
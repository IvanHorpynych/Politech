#include "lib.h"

double Remainder_Term(double eps, int &series_length, double &x, double &result){
	double Gen_Mem = 1, First_multiplier = 1, Second_multiplier = 1, RemTerm = 1, divx = 0, modx = 0;
	
		divx = modf(x, &modx);
		First_multiplier = exp(double (modx));
			
		while (abs(Gen_Mem) >= eps){								
			Gen_Mem = divx*Gen_Mem/series_length;			
			Second_multiplier = Second_multiplier + Gen_Mem;
			series_length++;
		}
		result = Second_multiplier*First_multiplier;
	
		for(int i = 0; i < series_length-1; i++){
			RemTerm = RemTerm*abs(divx)/(i + 1);
		}
		RemTerm = RemTerm*(series_length + 1)/series_length;
	
	return RemTerm;
}

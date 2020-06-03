#include "lnx.h"



void main() {

	double a = 0.98, b = 5.5, x = 0,a2 = 0, n1 =0,
		eps = 1e-002, h = 0, abserr = 0, Rn = 0;
	int ex,series_len = 0;
	x = (a + b) / 2;
	double a1 = (1 - frexp(x, &ex)) / (1 + frexp(x, &ex));
	cout << "TABLE_1"<<endl;
	cout << "eps	  n  abs		 rn	    		res"<<endl;

	for (int i = 0; i <= 4; i++){
		abserr = (double)abs(lnX(eps,&series_len, ex, a1,&Rn,&n1) - log(x));
	cout << setw(10) <<left<< eps <<
		setw(3)<<left<<series_len<<
		setw(20)<<left <<abserr <<
		setw(20)<<left<<Rn <<
		setw(20)<<left<< lnX(eps,&series_len, ex, a1,&Rn,&n1)<<endl;		
		eps = eps/1000;
		series_len = 0;
		Rn = 0;
	}
	
	cout <<endl<< "TABLE_2"<<endl;
	cout << "x	  abs  		      rn			 res"<<endl;	
	h = (b - a)/10;
	for (int i = 0; i <= 10; i++){
	x = a + h*i;
	a2 = (1 - frexp(x, &ex)) / (1 + frexp(x, &ex));
	abserr = abs(lnx2(n1, ex, a2,&Rn) - log(x));
	cout <<setw(10)<<left<< x <<
		setw(20)<<left <<abserr << 
		setw(20)<<left<<Rn <<  
		setw(20)<<left<< lnx2(n1,ex, a2,&Rn)<<endl;		
	}

	system("pause");
}
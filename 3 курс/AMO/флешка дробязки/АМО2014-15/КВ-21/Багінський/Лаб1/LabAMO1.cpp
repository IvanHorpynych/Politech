#include <math.h>
#include <stdio.h>
#include <conio.h>
#define a 0.98	
#define b 5.5
#define n 10

double MZA(double d){
	 int m= floor( log10(2*d)/log10(2.0));
	 double z;
	 if (m==0) z=d ;
	 else z=d/pow(2.0,m);
	 double Al=(1-z)/(1+z);
	 return Al;
}
void main()

{   double Rn1=0;
	int n1=0;
	double L=0;
	double L1=0;
	double Sum=0;
	double XI[n+1];
	double h=(b-a)/n;
	for (int i=0; i<11; i++)
	{	XI[i]=a+h*i;}
	if ((a<=0)||(b<=0)) printf("Invalid data");
	else {
	double Al=MZA((a+b)/2);
	int k=1;
	printf("eps\tn\tAbsolute error\t\tRemainder term\t\n");
	for (double eps = 1e-2; eps >= 1e-14; eps *= 1e-3) {
		do {
			L=pow(Al, 2*k-1)/(2*k-1);
			Sum+=L;
			k++;
			} while (L>4*eps);
		double Rn=pow(1.0/3.0,2.0*(k-1.0)-1)/(8.0*(k-1.0)+4);
		if (eps==1e-8){ 
			n1=k-1;
			Rn1=Rn;
			L1=L;}
		double delt=abs(2*log(2.0)-2*Sum-Rn-log((a+b)/2));
		printf("%g\t%i\t%1.15f\t%1.15f\t\n",eps,k-1,delt,L);
						
	   }
	printf("\n\nXi\tAbsolute error\t\tRemainder term\t\n");
	Sum=0;
	for (int i=0; i<n+1; i++) {
		int m= floor( log10(2*XI[i])/log10(2.0));
		for (int j=1; j<n1+1; j++) {
			Sum+=pow(MZA(XI[i]), 2*j-1)/(2*j-1);}
		double delta=abs(m*log(2.0)-2.0*Sum-Rn1-log(XI[i]));
		Sum=0;
		printf("%1.3f\t%1.15f\t%1.15f\t\n",XI[i],delta,pow(MZA(XI[i]), 2*n1-1)/(2*n1-1));
	}
	}
}

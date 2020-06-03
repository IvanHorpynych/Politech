#include "Algorithm.h"


int main()
{
	float a = 1.4;
	float b = 25.7;
	int Length;
	int k = 1;
	//float TrueRes = exp((a+b)/2);

	//Task 1
	float step = pow(10,-3);
	float epsilon = pow(10,-2);
	Task1 T1(a, b);

	std::cout << "eps" << "\t" << "n" << "\t" << "Abs" << "\t" << "Memb" << std::endl;
	while(epsilon >= pow(10, -14))
	{
		T1.dExp = T1.dPartFunc(epsilon);
		std::cout << epsilon << '\t';
		std::cout << T1.ReturnLength() << '\t';
		if(k == 3)
			Length = T1.ReturnLength();
		//std::cout << T1.AbsMistake() << '\t';
		printf("%.16f\t", T1.AbsMistake());
		//std::cout << T1.Rn() << '\n';
		//std::cout << T1.ReturnLastElem() << '\n';
		printf("%.20f\n", T1.ReturnLastElem());
		epsilon = epsilon*step;
		k++;
	}

	//Task2
 	epsilon = pow(10, -8);
	int i;
	double h = (b - a)/10;
	double xi;
	std::cout << '\n';
	std::cout << "Xi" << "\t" << "Abs" << "\t" << "Memb" << std::endl;

	for(i = 0; i <= 10; i++)
	{
		xi = a + h*i;
		Task1 T2(xi, Length);
		std::cout << xi << '\t';
		//std::cout << T2.AbsMistake(epsilon) << '\t';
		printf("%.20f\t", T2.AbsMistake(epsilon));
		printf("%.20f\n", T2.ReturnLastElem());
	}


	system("pause");
	return 0;
}
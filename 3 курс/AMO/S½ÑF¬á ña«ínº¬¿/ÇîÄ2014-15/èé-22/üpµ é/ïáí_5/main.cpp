#include <conio.h>
#include "functions.h"
#include <iostream>
using namespace std;


int main()
{
	//вектор с коеффициентами а0..аn полинома
	double* Result;
	//степень многочлена
	int n = 1;
	//количество точек приближения функции
	int N = 50;
	//пределы интегрирования
	double A = 0;
	double B = 6;
	//относительная точность вычисления интегралов
	double epsIntegral = 1e-13;
	//необходимая точность среднеквадратического отклонения
	double epsLSD = 0.01;
	//среднеквадратическое отклонение интеграла
	double LSD = 10;
	//вычисляе среднеквадратическое отклонение пока не достигнем нужной точности
	do
	{
		cout<<"Stepen mnogochlena = "<<n-1<<endl;
		Result = BuildMatrix(n, A, B, epsIntegral);
		LSD = LestSquaresApproximation(Result, n, N, A, B);
		cout<<endl<<"DELTA["<<n<<"]="<<LSD<<endl<<endl;
		n+=5;
		_getch(); system("cls");
	}
	while (LSD >= epsLSD);

	_getch();
}
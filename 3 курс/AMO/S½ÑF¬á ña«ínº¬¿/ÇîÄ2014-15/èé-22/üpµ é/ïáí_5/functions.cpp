#define _USE_MATH_DEFINES
#include <math.h>
#include "Functions.h"
#include <iostream>
#include <conio.h>
using namespace std;
//функция подсчёта коэфициентов в нормальной матрице
double CountNormMatrixElement(int step1, int step2, double _X)
{
	return CountTn(step1, _X) * CountTn(step2, _X);
}
//функция подсчёта n-го члена полинома
double CountTn(int step, double _X)
{
	if (step == 0) return 1;
	if (step == 1) return _X;
	double Prev = 1;
	double Next = _X;
	double Help = 0;
	double Result;
	for (int i = 2; i <= step; i++)
	{
		Help = 2 * _X * Next - Prev; 
		Result = Help;
		Prev = Next;
		Next = Help;
	}
	return Result;
}
//функция почсчёта свободных членов в нормальной матрице
double CountNormMatrixFreeTerm(int step, double _X)
{
	return CountTn(step, _X) * CountFx(_X);
}
//функция подсчёта значения функции 
double CountFx(double _X)
{
	/*1 */return (double)(0.1 * _X * _X * sin(3 * _X) * pow(M_E,pow(_X, 1/3)));
	
}


//функция построения нормальной матрицы и подсчёта коефициентов полинома, 
//возвращает указатель на вектор коеффициентов полинома
double* BuildMatrix(int n, double A, double B, double eps)
{
	double* NormMatrix = new double [n * (n + 1)];
	double* KoefOfPol = new double [n];

	cout.precision(5);
	cout<<fixed;

	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
		{
			*(NormMatrix + i * (n + 1) + j) = CountIntegral(A, B, eps, i, j, 0);
		}
	}

	for (int i = 0; i < n; i++)
	{
		*(NormMatrix + i * (n + 1) + n) = CountIntegral(A, B, eps, i, n, 1);
	}

	KoefOfPol = SolveMatr(NormMatrix,n);

	cout.precision(65);
	cout<<endl;

	return KoefOfPol;
}
//подсчёта значения полинома от X
double CountPolynomValue(double* Result, int n, double X)
{
	double result = 0;

	for (int i = 0; i < n; i++)
	{
		result += Result[i] * CountTn(i, X);
	}

	return result;
}
//функция подсчёта среднего квадратического отклонения
double LestSquaresApproximation(double* Result, int n, int N, double a, double b)
{
	double result = 0;
	double tmp = 0;
	double h = (b - a)/N;
	double X = a;

	cout<<fixed;
	cout.precision(3);
	cout<<endl;

	cout<<"Result	X	Polinom	Fx"<<endl;
	for (int i = 0; i <= N; i++)
	{
		tmp = (fabs(CountFx(X) - CountPolynomValue(Result, n, X)));
		cout<<X<<"	"<<CountPolynomValue(Result, n, X)<<"	"<<CountFx(X)<<endl;
		result += tmp * tmp;
		cout<<tmp<<"	";
		X += h;
	}

	cout<<endl;
	result /= (N + 1);
	return sqrt(result);
}
//решение матрицы 
double* SolveMatr ( double *mat, int razmer)
{
	int ii,jj ;
	int i,j,iter1 ;
	double ai, sum;
	double * ryad = new double [razmer+1];
	double* korni = new double [razmer];
	//double * korni = new double [razmer];
	for (i=0 ; i <razmer ; i ++)
	{
		ai = mat[i*(razmer+1) +i];
		//делим на аи всю строку текущую
		for (j =0 ;j<=razmer ; j++)
					mat[i*(razmer+1) +j]=mat[i*(razmer+1) +j] /ai ;
		
		for (j =0 ;j<=razmer ; j++)
					ryad[j]=mat[i*(razmer+1) +j]  ;
		
		for (iter1 = i ; iter1 < razmer-1 ; iter1++)
		{
			
			
		//вычитаем
				ai = mat[(iter1+1)*(razmer + 1)+i];
			for (j =i ;j<=razmer ; j++)
					mat[(iter1+1)*(razmer+1) +j]=mat[(iter1+1)*(razmer+1) +j] - ai*mat[(i)*(razmer+1) +j] ;
							
			}
	}

	j = razmer-2;sum = 0; korni[razmer-1] = mat[(razmer -1)*(razmer+1) + razmer]; 
	for (i = (razmer-2) ; i>=0; i--)
	{
		sum = 0;jj=razmer -1;
		for (ii = razmer-1 ; ii >= i+1; ii-- )
		{
			sum = sum + mat [i*(razmer+ 1)+ ii] *korni[jj];
			jj--;
		} 
		korni [j] = (mat [i*(razmer + 1) + razmer]- sum);
		j--;
		
	}

return korni;
}
//функция подсчёта интеграла
// 0 : считаем интеграл для коэфициентов нормальной матрицы
// 1 : считаем интеграл для свободных членов нормальной матрицы
double CountIntegral(double A, double B, double eps, int step1, int step2, int Flag)
{
	int n,m,i,j;
	double I1,I2, sum,sum1,sum2, x,h;
	n = ceil (1/(sqrt(sqrt(eps))));
	m = ceil((n/2.0));
	h = (B-A)/(2.0*m);
	I1 = 0; I2 = 0; j = 1;
	if (Flag == 0)
	{
		do 
		{
			h = (B-A)/(2.0*m);
			i=1; 
			sum1 = 0; 
			while ( i <= (2*m-1) )
			{
				x = A + (i)*h;
				sum1 = sum1 + CountNormMatrixElement(step1,step2,x);
		
				i=i+2;
			}

			i = 2 ; 
			sum2 = 0;
			
			while (i <= (2*m-2))
			{
				x = A + i*h;
				sum2 = sum2 + CountNormMatrixElement(step1,step2,x);	
				i = i+ 2;
			}

			I1 = I2;
			I2 =  (h/3 ) * (CountNormMatrixElement(step1,step2,A) + CountNormMatrixElement(step1,step2,B) + 4 * sum1 + 2 * sum2);
			j++;
			m = 2*m;
		}
		while ((abs ((I2 - I1 ) /I2 )) > eps );
	}
	else 
	{
		do 
		{
			h = (B-A)/(2.0*m);
			i=1; 
			sum1 = 0; 
			while ( i <= (2*m-1) )
			{
				x = A + (i)*h;
				sum1 = sum1 + CountNormMatrixFreeTerm(step1,x);
		
				i=i+2;
			}

			i = 2 ; 
			sum2 = 0;
			while (i <= (2*m-2))
			{
				x = A + i*h;
				sum2 = sum2 + CountNormMatrixFreeTerm(step1,x);
	
				i = i + 2;
			}
			I1 =I2;
			I2 =  (h/3 ) * (CountNormMatrixFreeTerm(step1,A) + CountNormMatrixFreeTerm(step1,B) + 4 * sum1 + 2 * sum2);
			j++;
			m = 2*m;
		} 
		while ((abs ((I2 - I1 ) / I2)) >eps) ;
	}

	return I2;
}
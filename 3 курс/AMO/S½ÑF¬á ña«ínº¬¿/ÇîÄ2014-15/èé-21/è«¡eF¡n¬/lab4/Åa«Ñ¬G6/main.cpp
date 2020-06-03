#include <iostream>
#include <cmath>

/*уранение по дефолту*/
double y1(double x)
{
	return (x*x*x/6-sin(x)+x);
}
/*Первісна*/
double Y(double x)
{
	return x*x*x*x/24+cos(x)+x*x/2;
}
/*Методом трапеції*/
double Trapezium_rule(double a, double b, double h)
{
	double i, tmp = 0;
	for(i = a; i < b - h ; i += h)
		tmp += (y1(i) + y1(i + h));
		tmp *= (h/2);
	if (i < b)
		tmp += (y1(i) + y1(b)) / 2 * (b - i);
	return tmp;
}
/*Обчислення інтегралу для методу перерахунку*/
double Refined_calc(double a, double b, double n)
{
	double h = (b - a) / n, tmp = 0;
	for (int i = 0; i < n; i++)
		tmp += (y1(a + i * h) + y1(a + i * h + h));
		tmp *= h/2;
	return tmp;
}

int main()
{
	int r = 2;
	int n;
	double eps = 0.01, h, deltas[4];
	const double a = -1, b = 16, IstIntegral = Y(b) - Y(a);
	                                            // Метод трап.

	printf("%8s%14s%25s%10s\n", "eps", "h", "The exact value", "delta");

	for (int i = 0; i < 4; i++)
	{
		h = sqrt(12 * eps / ((b - a) * (b + sin(b))))*0.999999;		// y''(E) = b + sin(b);
		deltas[i] = abs(IstIntegral - Trapezium_rule(a, b, h));
		printf("%12.11f\t%11.10f\t%11.10f\t%20.18f\n", eps, h, IstIntegral, deltas[i]);
		eps *= 0.1;
	}
	                                            //Метод Рунге

	printf("\n%12s%15s%25s\n", "delta", "h", "new_delta");

	for (int i = 0; i < 4; i++)
	{
		eps = deltas[i];
		n = int(1 / pow(eps, 1/r));
		while(eps < (abs(Refined_calc(a, b, n) - Refined_calc(a, b, 2 * n))) / (pow(2, r) - 1))
		{
			n *= 2;
		}
		printf("%15.14f\t%7.6f\t%15.14f\n", eps, (b - a) / (2 * n), abs(IstIntegral - Refined_calc(a, b, 2 * n)));
	}
	getchar();
	return 0;
}

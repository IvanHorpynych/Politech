#include <stdio.h>
#include <math.h>

double step(double chislo, int stepen)
{
	double res = 1.0;
	int i = 0;
	while (i < stepen)
	{
		res = res * chislo;
		i++;
	}
	return res;
}

int schet(double x, double eps, int dia)
{

	int m = 0, st2 = 1;
	while (st2 < x)
	{
		st2 = st2 * 2;
		m++;
	}

	
	double z = x / (double)st2;
	double a = (1.0 - z) / (1.0 + z);
	double chn = a, znn = 1.0;
	double Ln = chn / znn;
	int n = 1;
	double Rn = 1.0 / (4.0 * ((double)n * 2 + 1) * 3.0);
	double result = m * log(2.0) - 2.0 * Ln;
	printf("X=%.4f\n", x);
	printf("M=%d\n", m);
	printf("Z=%.4f\n", z);

	if (dia == -1)
	{
		while (Ln >(4.0 * eps))
		{
			n++;
			chn = chn * a * a;
			znn = znn + 2;
			Ln = chn / znn;
			result = result - 2.0 * Ln;
		}
	}
	else
	{
		int i;
		for (i = 1; i < dia; i++)
		{
			chn = chn * a * a;
			znn = znn + 2;
			Ln = chn / znn;
			result = result - 2.0 * Ln;
		}
		n = dia;
	}
	

	Rn = 1.0 / (4.0 * ((double)n * 2 + 1) * step(3.0, 2 * n - 1));
	result = result - Rn;
	//printf("%d\n", n);

	if (dia == -1)
	{
		printf("Eps=%.15f  ", eps);
		printf("N=%3.d  ", n);
		printf("Pohubka=%.15f  ", log(x) - result);
		printf("Rn=%.15f\n", Rn);
	}
	else
	{
		FILE *file;
		file = fopen("out.txt", "a");

		printf("Xi=%.4f  ", x);
		printf("Pohubka=%.15f  ", log(x) - result);
		printf("Rn=%.15f \n", Rn);
		//printf("Ln=%.15f\n", Ln);

		//fprintf(file, "%.4f  ", x);
		fprintf(file, "%.4f ", log(x));
		fprintf(file, "%.15f\n", Rn);
		fclose(file);

	}
	return n;
}

int main(void)
{
	FILE *file;
	file = fopen("out.txt", "w");
	fclose(file);

	double eps = 0.01;
	double nach = 50.1, kon = 95.0;
	//double x = (nach + kon) / 2.0;
	double x = nach;
	int n8;

	int i = 2;
	while (i < 15)
	{
		if (i == 8) n8 = schet(x, eps, -1);
		else schet(x, eps, -1);
		i = i + 3;
		eps = eps / 1000.0;
	}
	
	printf("\n");
	eps = 0.00000001;
	printf("Eps=%.15f\n", eps);
	double h = (kon - nach) / 10.0;
	for (i = 0; i < 11; i++)
	{
		schet(nach + h * (double)i, eps, n8);
	}

	return 0;

}
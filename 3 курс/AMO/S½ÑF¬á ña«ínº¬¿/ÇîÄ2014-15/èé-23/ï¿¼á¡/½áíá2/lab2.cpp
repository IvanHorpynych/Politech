#include <math.h>
#include <stdio.h>
#include <locale.h>
#include "inc/Solver.h"

void table1(double low_lim, double up_lim);
void table2(double low_lim, double up_lim);
void table3();

int main()
{
	setlocale(LC_ALL, "");
	table1(LOWER_LIMIT1, UPPER_LIMIT1);
//	table1(LOWER_LIMIT2, UPPER_LIMIT2);
	table2(LOWER_LIMIT1, UPPER_LIMIT1);
//	table2(LOWER_LIMIT2, UPPER_LIMIT2);
	table3();

	return 0;
}

void table1(double low_lim, double up_lim) {
	Solver solver;
	double eps = EPS_0;
	double res = 0;
	printf("Метод iтерацiй\n%-13s %-18s %-10s\n", "EPS", "Значення кореня", "Оцiнка точностi");
	for (int i = 0; i < 4; i++)
	{
		res = solver.IterationMethod(low_lim,up_lim, eps);
		printf("%10e %18.14f %10e\n", eps, res, solver.GetLastPrecision());
		eps *= EPS_STEP;
	}
	printf("\n");
}

void table2(double low_lim, double up_lim) {
	Solver solver;
	printf("Метод дотичних\n%-13s %-18s %-10s\n", "EPS", "Значення кореня", "Оцiнка точностi");
	double eps = EPS_0;
	double res = 0;
	for (int i = 0; i < 4; i++)
	{
		res = solver.NewtonMethod(low_lim, up_lim, eps);
		printf("%10e %18.14f %10e\n", eps, res, solver.GetLastPrecision());
		eps *= EPS_STEP;
	}
	printf("\n");
}

void table3() {
	Solver solver;
	printf("Порiвняння\n%-13s %-10s %-10s\n", "EPS", "Iтер.", "Дотичних");
	double eps = EPS_0;
	int i, iterationsIterationMethod, iterationsSolverMethod;
	for (i = 0; i < 4; i++)
	{
		solver.IterationMethod(LOWER_LIMIT1, UPPER_LIMIT1, eps);
		iterationsIterationMethod = solver.GetLastIterationsNum();
		solver.NewtonMethod(LOWER_LIMIT1, UPPER_LIMIT1, eps);
		iterationsSolverMethod = solver.GetLastIterationsNum();
		printf("%-10e %-10d %-10d\n", eps, iterationsIterationMethod, iterationsSolverMethod);
		eps *= EPS_STEP;
	}
	printf("\n");
} 
#include "header.h"

double a = 2, b = 17;

double MyFuction(double x){
	return 0.7*x*x*sin(x);
}

double* findAi(double N){
	double* A = new double[(int)N];
	double h = (b - a) / N;
	double x = a;
	for (int i = 0; i < N; ++i)	{
		A[i] = MyFuction(x);
		x += h;
	}
	return A;
}

double Fi(int i, double N){
	double h = (b - a) / N;
	double x = a;
	double f_i;
	x += h * i;
	f_i = MyFuction(x);
	return f_i;
}

double* findCi(double N){
	double* C = new double[(int)N];
	double* matrix = new double[(int)N * ((int)N + 1)];
	double h = (b - a) / N;

	for (int i = 0; i <= N; ++i) {
		matrix[i] = 0;
		matrix[((int)N - 1) * ((int)N + 1) + i] = 0;
	}
	matrix[0] = 1;
	matrix[((int)N - 1) * ((int)N + 1) + (int)N - 1] = 1;

	for (int i = 1; i < (N - 1); ++i)
		for (int j = 0; j <= N; ++j){
		matrix[i * ((int)N + 1) + j] = 0;
		if (j == (i - 1)) matrix[i * ((int)N + 1) + j] = h;
		if (j == i) matrix[i * ((int)N + 1) + j] = 4 * h;
		if (j == (i + 1)) matrix[i * ((int)N + 1) + j] = h;
		if (j == N) matrix[i * ((int)N + 1) + j] = 6 * (Fi(i - 1, N) - 2 * Fi(i, N) + Fi(i + 1, N)) / h;
		}

	//Виключення Гауса-Жордана
	double app, aip;

	for (int p = 0; p < (N - 1); ++p){
		app = matrix[p * ((int)N + 1) + p];
		for (int j = 0; j <= N; ++j)
			matrix[p * ((int)N + 1) + j] = matrix[p * ((int)N + 1) + j] / app;

		for (int i = (p + 1); i < N; ++i){
			aip = matrix[i * ((int)N + 1) + p];
			for (int j = 0; j <= N; ++j)
				matrix[i * ((int)N + 1) + j] = matrix[i * ((int)N + 1) + j] - matrix[p * ((int)N + 1) + j] * aip;
		}
	}

	for (int i = 0; i < N; ++i){
		C[(int)N - i - 1] = matrix[((int)N - i - 1) * ((int)N + 1) + (int)N];
		for (int j = 0; j < i; ++j)
			C[(int)N - i - 1] -= matrix[((int)N - i - 1) * ((int)N + 1) + (int)N - 1 - j] * C[(int)N - i];
	}
	return C;
}

double* findDi(double* C, double N){
	double* D = new double[(int)N];
	double h = (b - a) / N;

	D[0] = C[0];
	for (int i = 1; i < N; ++i)
		D[i] = (C[i] - C[i - 1]) / h;
	return D;
}

double* findBi(double* C, double* D, double N){
	double* B = new double[(int)N];
	double h = (b - a) / N;
	B[0] = 0;

	for (int i = 1; i < N; ++i)
		B[i] = (h * C[i] / 2) - (h * h * D[i] / 2) + (Fi(i, N) - Fi(i - 1, N)) / h;
	return B;
}

double Spline(double x, double* A, double* B, double* C, double* D, double N){
	double h = (b - a) / N;
	int i = 0;

	if ((x <= a) || (x > b))
		return 0;
	//Номер відрізку
	while (x > (a + i * h)) { ++i; }
	--i;

	double xi = a + i * h;

	//Значення сплайну в точці
	return A[i] + B[i] * (x - xi) + C[i] * (x - xi) * (x - xi) / 2 + D[i] * (x - xi) * (x - xi) * (x - xi) / 6;
}
void SplineInterpolation(double N)
{
	double* A;
	double* C;
	double* D;
	double* B;

	A = findAi(N);
	C = findCi(N);
	D = findDi(C, N);
	B = findBi(C, D, N);

	ofstream fout("results.txt");
	int k = (b - a) * 250;
	double step = (b - a) / (double)k;
	double x, y;

	x = a;
	for (int i = 0; i <= k; ++i){
		y = Spline(x, A, B, C, D, N);
		if (abs(x) < 10e-6) fout << 0;
		else fout << x;
		fout << " ; ";
		if (abs(y) < 10e-6) fout << 0;
		else fout << y;
		fout << endl;
		x += step;
	}
	fout.close();
	return;
}

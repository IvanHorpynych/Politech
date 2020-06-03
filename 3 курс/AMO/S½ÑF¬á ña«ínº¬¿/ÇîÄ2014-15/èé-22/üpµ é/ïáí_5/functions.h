#pragma once
//функция подсчёта коэфициентов в нормальной матрице
double CountNormMatrixElement(int step1, int step2, double _X);
//функция подсчёта n-го члена полинома
double CountTn(int step, double _X);
//функция почсчёта сводных членов в нормальной матрице
double CountNormMatrixFreeTerm(int step, double _X);
//функция подсчёта значения функции 
double CountFx(double _X);
//функция построения нормальной матрицы и подсчёта коефициентов полинома,
//возвращает указатель на вектор коеффициентов полинома
double* BuildMatrix(int n, double A, double B, double eps);
//подсчёта значения полинома от X
double CountPolynomValue(double* Result, int n, double X);
//функция подсчёта среднего квадратического отклонения
double LestSquaresApproximation(double* Result, int n, int N, double a, double b);
//функция подсчёта интеграла 
double CountIntegral(double A, double B, double eps, int step1, int step2, int Flag);
double*  SolveMatr ( double *mat, int razmer);
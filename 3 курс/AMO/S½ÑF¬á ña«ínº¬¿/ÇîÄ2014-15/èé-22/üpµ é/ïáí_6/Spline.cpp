#include "Spline.h"
//  Функция построения кубического сплайна для n заданных точек
void CubSpline::BuildSpline(double *XVect, double *FxVect, int n)
{
	N = n;
	double* ResultArr = new double [n];

    // Инициализация массива сплайнов
	Koefs = new SplineKoefs [n];

	for (int i = 0; i < n; i++)
	{
		Koefs[i].X = XVect[i];
		Koefs[i].A = FxVect[i];
	}
	Koefs[0].C = 0;

	//  Решение тридиагональной матрицы методом прогона
    double *ALPHA = new double[n - 1];
    double *BETA = new double[n - 1];
    double A, B, C, F, h_i, h_i1, z;

    ALPHA[0] = BETA[0] = 0;
    for (int i = 1; i < n - 1; i++)
    {
		h_i = XVect[i] - XVect[i - 1]; 
		h_i1 = XVect[i + 1] - XVect[i];

		A = h_i;
		C = 2 * (h_i + h_i1);
		B = h_i1;

		F = 6 * ((FxVect[i + 1] - FxVect[i]) / h_i1 - (FxVect[i] - FxVect[i - 1]) / h_i);
		z = (A * ALPHA[i - 1] + C);
		ALPHA[i] = -B / z;
		BETA[i] = (F - A * BETA[i - 1]) / z;
    }
 
    Koefs[n - 1].C = (F - A * BETA[n - 2]) / (A * ALPHA[n - 2] + C);
 
    // Нахождение решения - обратный ход метода прогонки
    for (int i = n - 2; i > 0; i--)
		Koefs[i].C = ALPHA[i] * Koefs[i + 1].C + BETA[i];
 
    // Освобождение памяти, занимаемой прогоночными коэффициентами
    delete[] BETA;
    delete[] ALPHA;
 
    // По известным коэффициентам c[i] находим значения b[i] и d[i]
    for (int i = n - 1; i > 0; i--)
    {
		h_i = XVect[i] - XVect[i - 1];
		Koefs[i].D = (Koefs[i].C - Koefs[i - 1].C) / h_i;
		Koefs[i].B = h_i * (2. * Koefs[i].C + Koefs[i - 1].C) / 6. + (FxVect[i] - FxVect[i - 1]) / h_i;
    }

}
//  Возвращает значение сплайна по переданному значению Х
double CubSpline::ReturnSplineValue(double X)
{
	if ( Koefs[0].X >= X ) return CountValue(X, &Koefs[0]);

	if ( Koefs[N-1].X <= X ) return CountValue(X, &Koefs[N-1]);

    SplineKoefs *tmp;

    int i = 0; 

	int j = N - 1;

    while (i + 1 < j)
    {
		int k = i + (j - i) / 2;
		if (X <= Koefs[k].X) j = k;
		else i = k;
    }

    tmp = &Koefs[j];

	return CountValue(X, tmp);
}
//  Считает значение сплайна
double CubSpline::CountValue(double X, SplineKoefs* Ptr)
{
	double Sub = X - Ptr->X;
	return Ptr->A + Ptr->B * Sub + Ptr->C/2 * Sub * Sub + Ptr->D * Sub * Sub * Sub;
}